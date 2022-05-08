using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021021.DataLayer.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Product data)
        {
            int result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products(ProductName, Unit, Price, Photo, CategoryID, SupplierID)
                                    VALUES(@productName, @unit, @price, @photo, @categoryID, @supplierID)
                                    SELECT SCOPE_IDENTITY(); ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productName", data.ProductName);
                cmd.Parameters.AddWithValue("@unit", data.Unit);
                cmd.Parameters.AddWithValue("@price", data.Price);
                cmd.Parameters.AddWithValue("@photo", data.Photo);
                cmd.Parameters.AddWithValue("categoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("supplierID", data.SupplierID);


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public int Count(string searchValue)
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @" select count(*)
                                     from    Products
                                     where    (@searchValue = N'')
                                          or (
                                                  ProductName like @searchValue
                                              )";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();

            }

            return count;
        }

        public bool Delete(int productID)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Products WHERE ProductID = @productID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        public Product Get(int productID)
        {
            Product result = null;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @productID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                    };
                }

                cn.Close();
            }

            return result;

        }

        public bool InUsed(int productID)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE 
                                    WHEN EXISTS(
                                                 SELECT * FROM ProductAttributes WHERE ProductID = @productID
                                                ) THEN 1 ELSE 0 END";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }
        /*
         * 
         * @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ProductName) as RowNumber
                                            from    Products as p
                                            where   
                                            (
                                                    ((@categoryID = 0) or (p.CategoryID = @categoryID)) and
                                                    ((@supplierID = 0) or (p.SupplierID = @supplierID)) and
                                                    ((@searchValue = '') or (p.ProductName like @searchValue))
                                            ) as t
                                    where   (@pageSize = 0) or (t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                                    order by t.RowNumber";
         * 
         * 
         * 
         * 
         * 
         * @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ShipperName) as RowNumber
                                            from    Shippers
                                            where    (@searchValue = N'')
                                                or (
                                                        (ShipperName like @searchValue)
                                                        or
                                                        (Phone like @searchValue)
                                                    )
                                        ) as t
                                    where    t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize
                                    order by t.RowNumber";
         * 
         * 
         * 
         * declare @page int = 1;
         *          @pageSize int = 5;
         *          @categoryID int = 0;
         *          @supplierID int = 0;
         *          @searchvalue nvarchar(255) = N'';
          select *, 
               row_number() over(order by ProductName) as RowNumber
          from Products as p
          where ((@categoryID = 0) or (p.CategoryID = @categoryID)) and
               ((@supplierID = 0) or (p.SupplierID = @supplierID)) and
               ((@searchValue = '') or (p.ProductName like @searchValue))
         */
        public IList<Product> List(int page, int pageSize, string searchValue, int categoryID, int supplierID)
        {
            List<Product> data = new List<Product>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";


            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                Console.Write(categoryID + "id;" + supplierID);

                cmd.CommandText = @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ProductName) as RowNumber
                                            from    Products as p
                                            where   
                                            (
                                                    ((@categoryID = 0) or (p.CategoryID = @categoryID)) and
                                                    ((@supplierID = 0) or (p.SupplierID = @supplierID)) and
                                                    ((@searchValue = '') or (p.ProductName like @searchValue))
                                            )
                                        )
                                            as t
                                    where   (@pageSize = 0) or (t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                                    order by t.RowNumber";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"])
                    });
                }

                dbReader.Close();

                cn.Close();
            }

            return data;
        }

        public bool Update(Product data)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Products 
                                        SET ProductName = @productName,
                                        Unit = @unit,
                                        Price = @price,
                                        Photo = @photo
                                    WHERE ProductID = @productID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productName", data.ProductName);
                cmd.Parameters.AddWithValue("@unit", data.Unit);
                cmd.Parameters.AddWithValue("@price", data.Price);
                cmd.Parameters.AddWithValue("@photo", data.Photo);
                cmd.Parameters.AddWithValue("@productID", data.ProductID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
    }
}