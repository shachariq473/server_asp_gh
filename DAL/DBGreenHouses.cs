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
    public static class DBGreenHouses
    {
        static string strCon = @"Data Source=sql5108.site4now.net;Initial Catalog=db_a79b5b_proj12 ;User ID = db_a79b5b_proj12_admin; Password=zAtYZu29YnbF5gJ6";
        static SqlConnection con;
        static SqlDataAdapter adpter;
        static DataSet ds;

        static DBGreenHouses()
        {
            con = new SqlConnection(strCon);
        }

        public static List<GreenHouses> GetAllGreenHouses(string id)
        {
            List<GreenHouses> greenHouses = new List<GreenHouses>();
            SqlDataReader reader = null;
            try
            {
                string comm = $"SELECT * FROM [GreenHouses] WHERE UserId = {id}";
                reader = ExcNQ(comm);
                if (reader.Read())
                {
                    GreenHouses house = new GreenHouses();
                    {
                        house.GreenHouse_Id = (int)reader["GreenHouse_Id"];
                        house.UserId = (string)reader["UserId"];
                        house.GreenHouse_Name = (string)reader["GreenHouse_Name"];
                        house.GreenHouse_Location = (string)reader["GreenHouse_Location"];
                    }
                    greenHouses.Add(house);

                    while (reader.Read())
                    {
                        house = new GreenHouses();
                        house.GreenHouse_Id = (int)reader["GreenHouse_Id"];
                        house.UserId = (string)reader["UserId"];
                        house.GreenHouse_Name = (string)reader["GreenHouse_Name"];
                        house.GreenHouse_Location = (string)reader["GreenHouse_Location"];
                        greenHouses.Add(house);

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
            return greenHouses;
        }

        public static GreenHouses GetGreenHouses(string id, int houseId)
        {
            SqlDataReader reader = null;
            try
            {
                string comm = $"SELECT * FROM [GreenHouses] WHERE UserId = {id} AND GreenHouse_Id = {houseId} ";
                reader = ExcNQ(comm);
                if (reader.Read())
                {
                    return new GreenHouses()
                    {
                        GreenHouse_Id = (int)reader["GreenHouse_Id"],
                        UserId = (string)reader["UserId"],
                        GreenHouse_Name = (string)reader["GreenHouse_Name"],
                        GreenHouse_Location = (string)reader["GreenHouse_Location"]
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

        public static int CreateGreenHouses(GreenHouses value)
        {
            try
            {
                string comm = $"INSERT INTO [GreenHouses](GreenHouse_Id, UserId, GreenHouse_Name, GreenHouse_Location)" +
                    $"VALUES(@GreenHouse_Id, @UserId, @GreenHouse_Name, @GreenHouse_Location)";

                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandText = comm;
                command.Parameters.Add("@GreenHouse_Id", SqlDbType.Int, 25).Value = value.GreenHouse_Id;
                command.Parameters.Add("@UserId", SqlDbType.NVarChar, 25).Value = value.UserId;
                command.Parameters.Add("@GreenHouse_Name", SqlDbType.NVarChar, 25).Value = value.GreenHouse_Name;
                command.Parameters.Add("@GreenHouse_Location", SqlDbType.NVarChar, 40).Value = value.GreenHouse_Location;
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in Create DBGreenHouses " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        [HttpPut]
        public static int UpdateGreenHouses(GreenHouses value)
        {
            SqlDataReader read;
            try
            {
                string comm = $"UPDATE [GreenHouses] SET GreenHouse_Name=@GreenHouse_Name, " +
                    $"GreenHouses_Location=@GreenHouses_Location WHERE UserId=@UserId AND GreenHouse_Id=@GreenHouse_Id";
                con.Open();

                SqlCommand command = new SqlCommand(comm, con);

                command.Parameters.AddWithValue("@GreenHouse_Id", value.GreenHouse_Id);
                command.Parameters.AddWithValue("@UserId", value.UserId);
                command.Parameters.AddWithValue("@GreenHouse_Name", value.GreenHouse_Name);
                command.Parameters.AddWithValue("@GreenHouses_Location", value.GreenHouse_Location);
                read = command.ExecuteReader();

                if (read.RecordsAffected > 0)
                {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in Update DBGreenHouses " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        [HttpDelete]
        public static int DeleteGreenHouse(string id, int houseId)
        {
                SqlDataReader read = null;
                try
                {
               
                    DBRealTimeParameters.DeleteRealTimeParameter(id, houseId);
                
                    DBParameters.DeleteParameter(id, houseId);

                    string comm = $"DELETE FROM GreenHouses WHERE UserId = {id} AND GreenHouse_Id={houseId}";
                    read = ExcNQ(comm);
                    if (read.RecordsAffected > 0)
                    {
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    new InvalidOperationException("error in Delete DBGreenHouses " + ex.Message);
                }
                finally
                {

                    con.Close();
                }
                return 0;
        }
        
        public static int DeleteGreenHouses(string id)
        {
            
            SqlDataReader read = null;
            try
            {                            
                DBRealTimeParameters.DeleteRealTimeParameters(id);              
                DBParameters.DeleteParameters(id);
                string comm = $"DELETE FROM GreenHouses WHERE UserId = {id}";
                read = ExcNQ(comm);
                if (read.Read())
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in Delete DBGreenHouses " + ex.Message);
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
