using SV18T1021021.BusinessLayer;
using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021021.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            int pageSize = 10;
            int rowCount = 0;
            int pageMax = 1000;
            int rowNull = -1;
            var data = ProductDataService.ListOfProducts(page, pageSize, searchValue, out rowCount, categoryID, supplierID);
            //var data2 = CommonDataService.ListOfProducts(page, pageMax, searchValue, out rowCount, categoryID, supplierID);
            var catID = CommonDataService.ListOfCategories(1,
                                                         pageMax,
                                                         "",
                                                         out rowNull);
            var supID = CommonDataService.ListOfSuppliers(1,
                                                         pageMax,
                                                         "",
                                                         out rowNull);
            Models.ProductPaginationResultModel model = new Models.ProductPaginationResultModel()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data,
                catID = catID,
                supID = supID
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            int rowNull = -1;
            int pageMax = 1000;
            ViewBag.Title = "Bổ sung mặt hàng mới";
            var catID = CommonDataService.ListOfCategories(1,
                                                      pageMax,
                                                      "",
                                                      out rowNull);
            var supID = CommonDataService.ListOfSuppliers(1,
                                                        pageMax,
                                                        "",
                                                        out rowNull);
            Product model = new Product()
            {
                ProductID = 0,
                categories = catID,
                suppliers = supID
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto)
        {

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                model.Photo = $"/Images/Products/{fileName}";
            }

            if (string.IsNullOrEmpty(model.ProductName))
                ModelState.AddModelError("ProductName", "Họ tên không được để trống");
            if (string.IsNullOrEmpty(model.Unit))
                ModelState.AddModelError("Unit", "Đơn vị không được để trống");
            if (model.Price == 0)
                ModelState.AddModelError("Price", "Giá không được để mặc định");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.ProductID == 0 ? "Bổ sung mặt hàng" : "Cập nhật thông tin mặt hàng";
                return View("Create", model);
            }
            if (model.ProductID == 0)
                ProductDataService.AddProduct(model);
            else
                ProductDataService.UpdateProduct(model);

            return RedirectToAction("Index");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("edit/{productID}")]
        public ActionResult Edit(string productID)
        {
            int rowNull = -1;
            int pageMax = 1000;
            var catID = CommonDataService.ListOfCategories(1,
                                                      pageMax,
                                                      "",
                                                      out rowNull);
            var supID = CommonDataService.ListOfSuppliers(1,
                                                        pageMax,
                                                        "",
                                                        out rowNull);
            int id = 0;
            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            var listProID = ProductDataService.GetProduct(id);
            Product model = new Product()
            {
                CategoryID = listProID.CategoryID,
                ProductName = listProID.ProductName,
                Photo = listProID.Photo,
                Price = listProID.Price,
                ProductID = listProID.ProductID,
                SupplierID = listProID.SupplierID,
                Unit = listProID.Unit,
                categories = catID,
                suppliers = supID
            };
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật thông tin nhân viên";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {
            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(productID);
                return RedirectToAction("Index");
            }
            var model = ProductDataService.GetProduct(productID);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method}/{productID}/{photoID?}")]
        public ActionResult Photo(string method, int productID, int? photoID)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    break;
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    break;
                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, int productID, int? attributeID)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    break;
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    break;
                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}
