using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021021.DomainModel
{
    //Loại hàng
    public class Category
    {
        //Mã loại hàng
        public int CategoryID { get; set; }
        //Tên loại hàng
        public string CategoryName { get; set; }
        //Mô tả
        public string Description { get; set; }
        

    }
}
