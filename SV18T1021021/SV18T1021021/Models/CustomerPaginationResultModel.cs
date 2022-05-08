using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021021.Models
{
    /// <summary>
    /// Lưu kết quả tìm kiếm khách hàng dưới dạng phân trang
    /// </summary>
    public class CustomerPaginationResultModel : PaginationResultModel 
    {
        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}