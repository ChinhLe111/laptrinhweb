using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021021.DomainModel;

namespace SV18T1021021.Models
{
    /// <summary>
    /// Lưu kết quả tìm kiếm loại hàng dưới dạng phân trang
    /// </summary>
    public class CategoryPaginationResultModel : PaginationResultModel
    {
        /// <summary>
        /// Danh sách loại hàng
        /// </summary>
        public List<Category> Data { get; set; }
    }
}