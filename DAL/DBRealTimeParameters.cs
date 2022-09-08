using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL;

namespace DAL
{
    public static class DBRealTimeParameters
    {
        static string strCon = @"Data Source=sql5108.site4now.net;Initial Catalog=db_a79b5b_proj12 ;User ID = db_a79b5b_proj12_admin; Password=zAtYZu29YnbF5gJ6";
        static SqlConnection con;
        static SqlDataAdapter adpter;
        static DataSet ds;

        static DBRealTimeParameters()
        {
            con = new SqlConnection(strCon);
        }

        public static List<RealTimeParameters> GetRealTimeParameters(string id, int houseId)
        {
            List<RealTimeParameters> realTimeParameters = new List<RealTimeParameters>();
            SqlDataReader reader = null;
            try
            {
                string comm = $"SELECT * FROM [Real_time_parameters] WHERE UserId = {id} AND GreenHous_Number like {houseId} ";
                reader = ExcNQ(comm);
   
                if (reader.Read())
                {
                    RealTimeParameters realTime = new RealTimeParameters();
                    {
                        realTime.UserId = (string)reader["UserId"];
                        realTime.GreenHous_Number = (int)reader["GreenHous_Number"];
                        realTime.Real_Temperature = (double)reader["Real_Temperature"];
                        realTime.Humidity = (double)reader["Humidity"];
                        realTime.Amount_Of_Light = (double)reader["Amount_Of_Light"];
                        realTime.Amount_Of_Water = (double)reader["Amount_Of_Water"];
                        realTime.Time_of_sampling = (string)reader["Time_of_sampling"];
                    }
                    realTimeParameters.Add(realTime);
                    while (reader.Read())
                    {
                        realTime = new RealTimeParameters();
                        realTime.UserId = (string)reader["UserId"];
                        realTime.GreenHous_Number = (int)reader["GreenHous_Number"];
                        realTime.Real_Temperature = (double)reader["Real_Temperature"];
                        realTime.Humidity = (double)reader["Humidity"];
                        realTime.Amount_Of_Light = (double)reader["Amount_Of_Light"];
                        realTime.Amount_Of_Water = (double)reader["Amount_Of_Water"];
                        realTime.Time_of_sampling = (string)reader["Time_of_sampling"];
                        realTimeParameters.Add(realTime);
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
            return realTimeParameters;
        }

        public static List<RealTimeParameters> GetLastRealTimeParameter(string id, int houseId)
        {
            List<RealTimeParameters> realTimeParameters = new List<RealTimeParameters>();
            SqlDataReader reader = null;
            RealTimeParameters LastToAdd = new RealTimeParameters();
            int time = 0;
            try
            {
                string comm = $"SELECT * FROM [Real_time_parameters] WHERE UserId = {id} AND GreenHous_Number like {houseId} ";
                reader = ExcNQ(comm);

                if (reader.Read())
                {
                    RealTimeParameters realTime = new RealTimeParameters();
                    {
                        realTime.UserId = (string)reader["UserId"];
                        realTime.GreenHous_Number = (int)reader["GreenHous_Number"];
                        realTime.Real_Temperature = (double)reader["Real_Temperature"];
                        realTime.Humidity = (double)reader["Humidity"];
                        realTime.Amount_Of_Light = (double)reader["Amount_Of_Light"];
                        realTime.Amount_Of_Water = (double)reader["Amount_Of_Water"];
                        realTime.Time_of_sampling = (string)reader["Time_of_sampling"];
                    }

                    //realTimeParameters.Add(realTime);
                    while (reader.Read())
                    {
                        realTime = new RealTimeParameters();
                        realTime.UserId = (string)reader["UserId"];
                        realTime.GreenHous_Number = (int)reader["GreenHous_Number"];
                        realTime.Real_Temperature = (double)reader["Real_Temperature"];
                        realTime.Humidity = (double)reader["Humidity"];
                        realTime.Amount_Of_Light = (double)reader["Amount_Of_Light"];
                        realTime.Amount_Of_Water = (double)reader["Amount_Of_Water"];
                        realTime.Time_of_sampling = (string)reader["Time_of_sampling"];
                        string str = realTime.Time_of_sampling;
                        int timeCheck = 0;
                        for (int i = 0; i < str.Length; i++)
                        {
                            timeCheck += str[i];
                        }
                        if (time < timeCheck)
                            LastToAdd = realTime;
                        
                    }
                    realTimeParameters.Add(LastToAdd);
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
            return realTimeParameters;
        }

        public static int CreateRealTimeParameters(RealTimeParameters value)
        {
            try
            {
                string comm = $"INSERT INTO Real_time_parameters(UserId, GreenHous_Number, Real_Temperature, Humidity, Amount_Of_Light, Amount_Of_Water, Time_of_sampling)" +
                    $"VALUES(@UserId, @GreenHous_Number, @Real_Temperature, @Humidity, @Amount_Of_Light, @Amount_Of_Water, @Time_of_sampling)";

                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandText = comm;
                command.Parameters.Add("@UserId", SqlDbType.NVarChar, 25).Value = value.UserId;
                command.Parameters.Add("@GreenHous_Number", SqlDbType.Int, 25).Value = value.GreenHous_Number;
                command.Parameters.Add("@Real_Temperature", SqlDbType.Float, 40).Value = value.Real_Temperature;
                command.Parameters.Add("@Humidity", SqlDbType.Float, 12).Value = value.Humidity;
                command.Parameters.Add("@Amount_Of_Light", SqlDbType.Float, 32).Value = value.Amount_Of_Light;
                command.Parameters.Add("@Amount_Of_Water", SqlDbType.Float, 32).Value = value.Amount_Of_Water;
                command.Parameters.Add("@Time_of_sampling", SqlDbType.NVarChar, 32).Value = value.Time_of_sampling;
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in Create DBRealTimeParameters " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        [HttpPut]
        public static int UpdateRealTimeParameters(RealTimeParameters value)
        {
            SqlDataReader read;
            try
            {
                string comm = $"UPDATE Real_time_parameters SET Real_Temperature=@Real_Temperature, Humidity=@Humidity," +
                    $"Amount_Of_Light=@Amount_Of_Light, Amount_Of_Water=@Amount_Of_Water, Time_of_sampling=@Time_of_sampling WHERE UserId=@UserId AND GreenHous_Number=@GreenHous_Number";
                con.Open();

                SqlCommand command = new SqlCommand(comm, con);
                command.Parameters.AddWithValue("@UserId", value.UserId);
                command.Parameters.AddWithValue("@GreenHous_Number", value.GreenHous_Number);
                command.Parameters.AddWithValue("@Real_Temperature", value.Real_Temperature);
                command.Parameters.AddWithValue("@Humidity", value.Humidity);
                command.Parameters.AddWithValue("@Amount_Of_Light", value.Amount_Of_Light);
                command.Parameters.AddWithValue("@Amount_Of_Water", value.Amount_Of_Water);
                command.Parameters.AddWithValue("@Time_of_sampling", value.Time_of_sampling);
                read = command.ExecuteReader();

                if (read.RecordsAffected > 0)
                {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in Update DBRealTimeParameters " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        [HttpDelete]
        public static int DeleteRealTimeParameter(string id, int houseId)
        {

            SqlDataReader read = null;
            try
            {
                string comm = $"DELETE FROM Real_time_parameters WHERE UserId = {id} AND GreenHous_Number like {houseId}";
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
        public static int DeleteRealTimeParameters(string id)
        {

            SqlDataReader read = null;
            try
            {
                string comm = $"DELETE FROM Real_time_parameters WHERE UserId LIKE {id}";
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
