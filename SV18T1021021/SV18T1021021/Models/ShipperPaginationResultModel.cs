using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021021.Models
{
    public class ShipperPaginationResultModel : PaginationResultModel
    {
        public List<Shipper> Data { get; set; }
    }
}