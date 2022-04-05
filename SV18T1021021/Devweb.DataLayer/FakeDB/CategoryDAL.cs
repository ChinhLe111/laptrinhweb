using Devweb.DataLayer;
using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SV18T1021021.DataLayer.FakeDB
{
    //
    public class CategoryDAL : ICategoryDAL
    {
        private string connectionString;
        public CategoryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Category data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int categoryID)
        {
            throw new NotImplementedException();
        }

        public Category Get(int categoryID)
        {
            throw new NotImplementedException();
        }

        public IList<Category> List()
        {
            List<Category> data = new List<Category>();
            using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Categories";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Category()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryID"]),
                        Description = Convert.ToString(dbReader["Description"]),
                    });
                    
                }
                connection.Close();
            }
            return data;

        }

        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
