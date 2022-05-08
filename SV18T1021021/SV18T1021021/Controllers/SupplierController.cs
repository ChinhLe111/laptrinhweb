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
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Supplier
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            PaginationSearchInput model = Session["SUPPLIER_SEARCH"] as PaginationSearchInput;
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
            ViewBag.Title = "Bổ sung nhà cung cấp mới";
            Supplier model = new Supplier()
            {
                SupplierID = 0

            };
            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{supplierID?}")]
        public ActionResult Edit(int supplierID)
        {
            ViewBag.Title = "Cập nhật thông tin nhà cung cấp";
            var model = CommonDataService.GetSupplier(supplierID);
            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);

        }
        [HttpPost]
        public ActionResult Save(Supplier model)
        {
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                ModelState.AddModelError("SupplierName", "Tên nhà cung cấp không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Tên địa chỉ không được để trống");
            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Phải chọn quốc gia");
            if (string.IsNullOrWhiteSpace(model.Phone))
                model.Phone = "";

            // nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.SupplierID > 0)
                    ViewBag.Title = "Cập nhật thông tin nhà cung cấp";
                else
                    ViewBag.Title = "Bổ sung nhà cung cấp mới";
                return View("Create", model);
            }


            if (model.SupplierID > 0)
            {
                CommonDataService.UpdateSupplier(model);
            }
            else
            {
                CommonDataService.AddSupplier(model);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa thông tin khách hàng khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(int supplierID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(supplierID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(supplierID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(input.Page, input.PageSize, input.SearchValue, out rowCount);


            Models.SupplierPaginationResultModel model = new Models.SupplierPaginationResultModel()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["SUPPLIER_SEARCH"] = input;
            return View(model);
        }
    }
}