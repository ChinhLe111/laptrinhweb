using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021021.DomainModel
{
    public class ProductAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public int AttributeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AttributeValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}