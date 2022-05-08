using SV18T1021021.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021021.DataLayer.SQLServer
{
    public class ShipperDAL : _BaseDAL, ICommonDAL<Shipper>
    {
        public ShipperDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Shipper data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Shippers(ShipperName,Phone)" +
                    " values(@shipperName,@phone)" +
                    " select scope_identity()";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@phone", data.Phone);


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
                cmd.CommandText = @"select count(*)                                  
                                    from Shippers
                                    where (@searchValue = N'')
                                    or (
                                        (ShipperName like @searchValue)
                                        or
                                        (Phone like @searchValue)
                                    )";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

            }
            return count;
        }

        public bool Delete(int shipperID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"delete from Shippers where ShipperID = @shipperID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();

            }

            return result;
        }

        public Shipper Get(int shipperID)
        {
            Shipper result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Shippers where ShipperID = @shipperID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Shipper()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
                        Phone = Convert.ToString(dbReader["Phone"]),
                    };
                }
                cn.Close();
            }
            return result;
        }
        public bool InUsed(int shipperID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select case when exists(select * from Orders where ShipperID = @shipperID) then 1 else 0 end";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        public IList<Shipper> List(int page, int pageSize, string searchValue)
        {
            {
                List<Shipper> data = new List<Shipper>();

                if (searchValue != "")
                    searchValue = "%" + searchValue + "%";

                using (SqlConnection cn = OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = @"select *
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
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = cn;

                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (dbReader.Read())
                    {
                        data.Add(new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                        });
                    }
                    dbReader.Close();
                    cn.Close();
                }

                return data;
            }
        }

        public bool Update(Shipper data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"update Shippers
                                    set ShipperName = @shipperName,
                                        Phone = @phone
                                    where ShipperID=@shipperID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@shipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@phone", data.Phone);
                cmd.Parameters.AddWithValue("@shipperID", data.ShipperID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();

            }
            return result;
        }
    }
}