using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021021.Models
{
    /// <summary>
/// Lớp cơ sở cho các model chứa dữ liệu dưới dạng phân trang
/// </summary>
    public abstract class PaginationResultModel
    {
        /// <summary>
        /// số trag hiện tại
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// số dòng trên 1 trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// gia tri tim kiem
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// Số dòng dữ liệu truy vấn được
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            }
        }
    }
}