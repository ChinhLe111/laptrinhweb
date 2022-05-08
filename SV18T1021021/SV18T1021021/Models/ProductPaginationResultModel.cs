using SV18T1021021.DomainModel;
using SV18T1021021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021021.Web.Models
{
    public class ProductPaginationResultModel : PaginationResultModel
    {
        public List<Product> Data { get; set; }
        public List<Category> catID { get; set; }
        public List<Supplier> supID { get; set; }

    }
}