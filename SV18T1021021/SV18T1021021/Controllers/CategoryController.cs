using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021021.BusinessLayer;
using SV18T1021021.DomainModel;
using SV18T1021021.Models;

namespace SV18T1021021.Controllers
{
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            PaginationSearchInput model = Session["CATEGORY_SEARCH"] as PaginationSearchInput;
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
        /// Bổ sung nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            Category model = new Category()
            {
                CategoryID = 0

            };
            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin nhà cung cấp
        /// </summary>
        /// <returns></returns>
        [Route("edit/{categoryID?}")]
        public ActionResult Edit(int categoryID)
        {
            ViewBag.Title = "Cập nhật thong tin loai hang";
            var model = CommonDataService.GetCategory(categoryID);
            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);

        }
        /// <summary>
        /// Xóa thông tin nhà cung cấp
        /// </summary>
        /// <returns></returns>
        [Route("delete/{categoryID}")]
        public ActionResult Delete(int categoryID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCategory(categoryID);

            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(Category model)
        {
            //kiem tra du lieu dau vao
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName", "Tên loại hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.Description))
                model.Description = "";

            // nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.CategoryID > 0)
                    ViewBag.Title = "Cập nhật thông tin loại hàng";
                else
                    ViewBag.Title = "Bổ sung loại hàng mới";
                return View("Create", model);
            }
            if (model.CategoryID > 0)
            {
                CommonDataService.UpdateCategory(model);
            }
            else
            {
                CommonDataService.AddCategory(model);
            }
                return RedirectToAction("Index");            
        }
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(input.Page, input.PageSize, input.SearchValue, out rowCount);


            Models.CategoryPaginationResultModel model = new Models.CategoryPaginationResultModel()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["CATEGORY_SEARCH"] = input;
            return View(model);
        }
    }
}