using SV18T1021021.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021021
{
    /// <summary>
    /// Cung cấp các tiện lợi liên quan đến SelectListItem
    /// </summary>
    public class SelectListHelper
    {
        /// <summary>
        ///Danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem(){
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list;
        }
    }
}