using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public static class DBFarmers
    {
        static string strCon = @"Data Source=sql5108.site4now.net;Initial Catalog=db_a79b5b_proj12 ;User ID = db_a79b5b_proj12_admin; Password=zAtYZu29YnbF5gJ6";
        static SqlConnection con;
        static SqlDataAdapter adpter;
        static DataSet ds;

        static DBFarmers()
        {
            con = new SqlConnection(strCon);
        }

        public static Farmers GetFarmer(string id)
        {
            SqlDataReader reader = null;
            try
            {
                string comm = $"SELECT * FROM [Users] WHERE UserId = {id}";
                reader = ExcNQ(comm);
                if (reader.Read())
                {
                    return new Farmers()
                    {
                        UserId = (string)reader["UserId"],
                        UserName = (string)reader["UserName"],
                        UserEmail = (string)reader["UserEmail"],
                        UserPassword = (string)reader["UserPassword"],
                        Phone_Number = (int)reader["Phone_Number"]
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            finally
            {
                reader.Close();
                con.Close();
            }
            return null;
        }
        public static List<Farmers> GetAllFarmer()
        {
            List<Farmers> allFarmers = new List<Farmers>();
            SqlDataReader reader = null;
            try
            {
                string comm = $"SELECT * FROM [Users]";
                reader = ExcNQ(comm);
                if (reader.Read())
                {
                    Farmers farmers = new Farmers();
                    {
                        farmers.UserId = (string)reader["UserId"];
                        farmers.UserName = (string)reader["UserName"];
                        farmers.UserEmail = (string)reader["UserEmail"];
                        farmers.UserPassword = (string)reader["UserPassword"];
                        farmers.Phone_Number = (int)reader["Phone_Number"];
                    }
                    allFarmers.Add(farmers);

                    while (reader.Read())
                    {
                        farmers = new Farmers();
                        farmers.UserId = (string)reader["UserId"];
                        farmers.UserName = (string)reader["UserName"];
                        farmers.UserEmail = (string)reader["UserEmail"];
                        farmers.UserPassword = (string)reader["UserPassword"];
                        farmers.Phone_Number = (int)reader["Phone_Number"];
                        allFarmers.Add(farmers);

                    }
                }
                    else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            finally
            {
                reader.Close();
                con.Close();
            }
            return allFarmers;
        }

        public static int CreateFarmers(Farmers value)
        {
            try
            {
                string comm = $"INSERT INTO Users(UserId, UserName, UserEmail, UserPassword, Phone_Number)" +
                    $"VALUES(@UserId, @UserName, @UserEmail, @UserPassword, @Phone_Number)";

                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandText = comm;
                command.Parameters.Add("@UserId", SqlDbType.NVarChar, 25).Value = value.UserId;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 25).Value = value.UserName;
                command.Parameters.Add("@UserEmail", SqlDbType.VarChar, 40).Value = value.UserEmail;
                command.Parameters.Add("@UserPassword", SqlDbType.VarChar, 12).Value = value.UserPassword;
                command.Parameters.Add("@Phone_Number", SqlDbType.Int, 32).Value = value.Phone_Number;
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                   return 1;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            finally
            {              
                con.Close();
            }
            return 0;
        }
        [HttpPut]
        public static int UpdateFarmers(Farmers value)
        {
            SqlDataReader read;
            try
            {
                string comm = $"UPDATE Users SET UserName=@UserName, UserEmail=@UserEmail, UserPassword=@UserPassword, Phone_Number=@Phone_Number WHERE UserId=@UserId";
                con.Open();

                SqlCommand command = new SqlCommand(comm, con);

                command.Parameters.AddWithValue("@UserId", value.UserId);
                command.Parameters.AddWithValue("@UserName", value.UserName);
                command.Parameters.AddWithValue("@UserEmail", value.UserEmail);
                command.Parameters.AddWithValue("@UserPassword", value.UserPassword);
                command.Parameters.AddWithValue("@Phone_Number", value.Phone_Number);
                read = command.ExecuteReader();

                if (read.RecordsAffected > 0)
                {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        
        [HttpDelete]
        public static int DeleteFarmer(string id)
        {

            SqlDataReader read = null;
            try
            {
                DBGreenHouses.DeleteGreenHouses(id);
                string comm = $"DELETE FROM Users WHERE UserId= {id}";
                read = ExcNQ(comm);
                if (read.RecordsAffected > 0)
                {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            finally
            {

                con.Close();
            }
            return 0;
        }

        public static SqlDataReader ExcNQ(string command)
        {
            SqlDataReader reader = null;
            try
            {
                SqlCommand comm = new SqlCommand(command, con);
                con.Open();
                reader = comm.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            return reader;
        }

    }
}
