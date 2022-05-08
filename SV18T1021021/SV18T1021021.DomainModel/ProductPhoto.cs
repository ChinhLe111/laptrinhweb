using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021021.DomainModel
{
    public class ProductPhoto
    {
        /// <summary>
        /// 
        /// </summary>
        public int PhotoID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsHidden { get; set; }
    }
}