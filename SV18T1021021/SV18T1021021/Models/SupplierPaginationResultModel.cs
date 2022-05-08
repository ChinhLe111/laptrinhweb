using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021021.DomainModel;

namespace SV18T1021021.Models
{
     /// <summary>
     /// Lưu kết quả tìm kiếm nhà cung cấp dưới dạng phân trang
     /// </summary>
    public class SupplierPaginationResultModel : PaginationResultModel
    {     
            /// <summary>
            /// Danh sách nhà cung cấp
            /// </summary>
            public List<Supplier> Data { get; set; }
    }
}
