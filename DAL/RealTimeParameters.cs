using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class RealTimeParameters
    {
        public string UserId { get; set; }
        public int GreenHous_Number { get; set; }
        public double Real_Temperature { get; set; }
        public double Humidity { get; set; }
        public double Amount_Of_Light { get; set; }
        public double Amount_Of_Water { get; set; }
        public string Time_of_sampling { get; set; }

        public RealTimeParameters(string userId, int greenHous_Number, double real_Temperature, double humidity, double amount_Of_Light, double amount_Of_Water, string time_of_sampling)
        {
            UserId = userId;
            GreenHous_Number = greenHous_Number;
            Real_Temperature = real_Temperature;
            Humidity = humidity;
            Amount_Of_Light = amount_Of_Light;
            Amount_Of_Water = amount_Of_Water;
            Time_of_sampling = time_of_sampling;
        }

        public RealTimeParameters()
        {
        }

        public override string ToString()
        {
            return $"{UserId}, {GreenHous_Number}, {Real_Temperature}, {Humidity}, {Amount_Of_Light}, {Amount_Of_Water}, {Time_of_sampling}";
        }
    }
}