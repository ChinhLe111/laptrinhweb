using SV18T1021021.BusinessLayer;
using SV18T1021021.DomainModel;
using SV18T1021021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021021.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("customer")]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Giao diện để tìm kiếm
        /// </summary>
        /// <returns></returns>
        // GET: Customer
        public ActionResult Index()
        {
            PaginationSearchInput model = Session["CUSTOMER_SEARCH"] as PaginationSearchInput;
            if(model == null)
            {
                model = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }
            return View(model);
            //var model = CommonDataService.ListOfCustomer(page, pageSize, searchValue, out rowCount);
            //int pageCount = rowCount / pageSize;
            //if (rowCount % pageSize > 0)
            //    pageCount += 1;


            //ViewBag.RowCount = rowCount;
            //ViewBag.PageCount = pageCount;
            //ViewBag.Page = page;
            //ViewBag.SearchValue = searchValue;
            // return View(model);

        }
        /// <summary>
        /// Bổ sung kahsch hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng mới";
            Customer model = new Customer()
            {
                CustomerID = 0

            };
            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{customerID?}")]
        public ActionResult Edit(int customerID)
        {
            ViewBag.Title = "Cập nhật thông tin khách hàng";
            var model = CommonDataService.GetCustomer(customerID);
            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);

        }
        [HttpPost]
        public ActionResult Save(Customer model)
        {
            //kiem tra du lieu dau vao
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                ModelState.AddModelError("CustomerName", "Tên khách hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Phải chọn quốc gia");
            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";

            // nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.CustomerID > 0)
                    ViewBag.Title = "Cập nhật thông tin khách hàng";
                else
                    ViewBag.Title = "Bổ sung khách hàng mới";
                return View("Create", model);
            }

            //Xử lí dữ liệu đầu vào
            if (model.CustomerID > 0)
            {
                CommonDataService.UpdateCustomer(model);
            }
            else
            {
                CommonDataService.AddCustomer(model);
                Session["CUSTOMER_SEARCH"] = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = model.CustomerName
                };
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa thông tin khách hàng khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{customerID}")]
        public ActionResult Delete(int customerID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(customerID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(customerID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(input.Page, input.PageSize, input.SearchValue, out rowCount);


            Models.CustomerPaginationResultModel model = new Models.CustomerPaginationResultModel()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["CUSTOMER_SEARCH"]= input;
            return View(model);
        }
    }
}