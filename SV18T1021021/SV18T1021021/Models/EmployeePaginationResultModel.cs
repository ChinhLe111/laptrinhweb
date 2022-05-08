using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021021.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeePaginationResultModel : PaginationResultModel
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}