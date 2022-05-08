using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021021.Models
{
    /// <summary>
    /// chứa dữ liệu đầu vào tìm kiếm phân trang
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }

    }
}