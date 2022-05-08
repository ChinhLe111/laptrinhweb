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
    
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Shipper
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            PaginationSearchInput model = Session["SHIPPER_SEARCH"] as PaginationSearchInput;
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
        /// <summary>
        /// Bổ sung kahsch hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung người giao hàng mới";
            Shipper model = new Shipper()
            {
                ShipperID = 0

            };
            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{shipperID?}")]
        public ActionResult Edit(int shipperID)
        {
            ViewBag.Title = "Cập nhật thông tin người giao hàng";
            var model = CommonDataService.GetShipper(shipperID);
            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);

        }
        [HttpPost]
        public ActionResult Save(Shipper model)
        {
            //kiem tra du lieu dau vao
            if (string.IsNullOrWhiteSpace(model.ShipperName))
                ModelState.AddModelError("ShipperName", "Tên người giao hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");

            // nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.ShipperID > 0)
                    ViewBag.Title = "Cập nhật thông tin người giao hàng";
                else
                    ViewBag.Title = "Bổ sung người giao hàng mới";
                return View("Create", model);
            }
            if (model.ShipperID > 0)
            {
                CommonDataService.UpdateShipper(model);
            }
            else
            {
                CommonDataService.AddShipper(model);
            }
                return RedirectToAction("Index");        
        }
        /// <summary>
        /// Xóa thông tin khách hàng khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{shipperID}")]
        public ActionResult Delete(int shipperID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(shipperID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetShipper(shipperID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(input.Page, input.PageSize, input.SearchValue, out rowCount);


            Models.ShipperPaginationResultModel model = new Models.ShipperPaginationResultModel()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["SHIPPER_SEARCH"] = input;
            return View(model);
        }
    }
}