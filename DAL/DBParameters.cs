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
    public static class DBParameters
    {
        static string strCon = @"Data Source=sql5108.site4now.net;Initial Catalog=db_a79b5b_proj12 ;User ID = db_a79b5b_proj12_admin; Password=zAtYZu29YnbF5gJ6";
        static SqlConnection con;
        static SqlDataAdapter adpter;
        static DataSet ds;

        static DBParameters()
        {
            con = new SqlConnection(strCon);
        }

        public static Parameters GetParameters(string id, int houseId)
        {
            SqlDataReader reader = null;
            try
            {
                string comm = $"SELECT * FROM ParametersTable WHERE UserId = {id} AND GreenHous_Number like {houseId}";
                reader = ExcNQ(comm);
                if (reader.Read())
                {
                    return new Parameters()
                    {
                        UserId = (string)reader["UserId"],
                        GreenHous_Number = (int)reader["GreenHous_Number"],
                        Minimum_Temperature = (double)reader["Minimum_Temperature"],
                        Maximum_Temperature = (double)reader["Maximum_Temperature"],
                        Humidity = (double)reader["Humidity"],
                        Daily_Amount_Of_Light = (double)reader["Daily_Amount_Of_Light"],
                        Daily_Amount_Of_Water = (double)reader["Daily_Amount_Of_Water"]
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

        public static int CreateParameters(Parameters value)
        {
            try
            {
                string comm = $"INSERT INTO ParametersTable(UserId, GreenHous_Number, Minimum_Temperature, Maximum_Temperature, Humidity, Daily_Amount_Of_Light, Daily_Amount_Of_Water)" +
                    $"VALUES(@UserId, @GreenHous_Number, @Minimum_Temperature, @Maximum_Temperature, @Humidity, @Daily_Amount_Of_Light, @Daily_Amount_Of_Water)";

                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandText = comm;
                command.Parameters.Add("@UserId", SqlDbType.NVarChar, 25).Value = value.UserId;
                command.Parameters.Add("@GreenHous_Number", SqlDbType.Int, 25).Value = value.GreenHous_Number;
                command.Parameters.Add("@Minimum_Temperature", SqlDbType.Float, 40).Value = value.Minimum_Temperature;
                command.Parameters.Add("@Maximum_Temperature", SqlDbType.Float, 12).Value = value.Maximum_Temperature;
                command.Parameters.Add("@Humidity", SqlDbType.Float, 32).Value = value.Humidity;
                command.Parameters.Add("@Daily_Amount_Of_Light", SqlDbType.Float, 32).Value = value.Daily_Amount_Of_Light;
                command.Parameters.Add("@Daily_Amount_Of_Water", SqlDbType.Float, 32).Value = value.Daily_Amount_Of_Water;
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in Create DBParameters " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        [HttpPut]
        public static int UpdateParameters(Parameters value)
        {
            SqlDataReader read;
            try
            {
                string comm = $"UPDATE ParametersTable SET Minimum_Temperature=@Minimum_Temperature, Maximum_Temperature=@Maximum_Temperature," +
                    $"Humidity=@Humidity, Daily_Amount_Of_Light=@Daily_Amount_Of_Light, Daily_Amount_Of_Water=@Daily_Amount_Of_Water WHERE UserId=@UserId AND GreenHous_Number=@GreenHous_Number";
                con.Open();

                SqlCommand command = new SqlCommand(comm, con);

                command.Parameters.AddWithValue("@UserId", value.UserId);
                command.Parameters.AddWithValue("@GreenHous_Number", value.GreenHous_Number);
                command.Parameters.AddWithValue("@Minimum_Temperature", value.Minimum_Temperature);
                command.Parameters.AddWithValue("@Maximum_Temperature", value.Maximum_Temperature);
                command.Parameters.AddWithValue("@Humidity", value.Humidity);
                command.Parameters.AddWithValue("@Daily_Amount_Of_Light", value.Daily_Amount_Of_Light);
                command.Parameters.AddWithValue("@Daily_Amount_Of_Water", value.Daily_Amount_Of_Water);
                read = command.ExecuteReader();

                if (read.RecordsAffected > 0)
                {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in Update DBParameters " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        [HttpDelete]
        public static int DeleteParameter(string id, int houseId)
        {

            SqlDataReader read = null;
            try
            {
                string comm = $"DELETE FROM ParametersTable WHERE UserId = {id} AND GreenHous_Number like {houseId} ";
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
        [HttpDelete]
        public static int DeleteParameters(string id)
        {

            SqlDataReader read = null;
            try
            {
                string comm = $"DELETE FROM ParametersTable WHERE UserId = {id}";
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
