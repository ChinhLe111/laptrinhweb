using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devweb.BusinessLayer;
using Devweb.DataLayer;
using SV18T1021021.DomainModel;
using System.Configuration;
using SV18T1021021.DataLayer;
using System.Data.SqlClient;
using System.Data;


namespace Devweb.BusinessLayer
{
    public static class CommonDataService
    {
        private static ICategoryDAL categoryDB;

        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SqlServer.CategoryDAL(connectionString);
                    break;
                default:
                    categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }

        public static List<Category> ListOfCategories()
        {
            return categoryDB.List().ToList();
        }
    }
}
