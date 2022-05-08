using SV18T1021021.BusinessLayer;
using SV18T1021021.DomainModel;
using SV18T1021021.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021021.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            PaginationSearchInput model = Session["EMPLOYEE_SEARCH"] as PaginationSearchInput;
            if (model == null)
            {
                model = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên mới";
            Employee model = new Employee()
            {
                EmployeeID = 0

            };
            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{employeeID?}")]
        public ActionResult Edit(string employeeID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(employeeID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật thông tin nhân viên";
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);

        }
        [HttpPost]
        public ActionResult Save(Employee model, string birthDateString, HttpPostedFileBase uploadPhoto)
        {
            //if (model.EmployeeID > 0)
            //{
            //    CommonDataService.UpdateEmployee(model);
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    CommonDataService.AddEmployee(model);
            //    return RedirectToAction("Index");
            //}

            try
            {
                DateTime d = DateTime.ParseExact(birthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                model.BirthDate = d;
            }
            catch
            {
                ModelState.AddModelError("BirthDate", "Ngày sinh không hợp lệ");

            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Employees");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                model.Photo = $"/Images/Employees/{fileName}";
            }
            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName))
                ModelState.AddModelError("FullName", "Họ tên không được để trống");
            if (string.IsNullOrEmpty(model.Notes))
                model.Notes = "";
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật thông tin nhân viên";
                return View("Create", model);

            };
            if (model.EmployeeID == 0)
                CommonDataService.AddEmployee(model);
            else
                CommonDataService.UpdateEmployee(model);
            return RedirectToAction("Index");

            //return Json(new
            //{
            //    Model = model
            //});

        }
        /// <summary>
        /// Xóa thông tin khách hàng khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{employeeID}")]
        public ActionResult Delete(int employeeID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(employeeID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(employeeID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(input.Page, input.PageSize, input.SearchValue, out rowCount);


            Models.EmployeePaginationResultModel model = new Models.EmployeePaginationResultModel()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["EMPLOYEE_SEARCH"] = input;
            return View(model);
        }
    }
}
