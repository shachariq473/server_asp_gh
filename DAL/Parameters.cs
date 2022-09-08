using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Parameters
    {
        public string UserId { get; set; }
        public int GreenHous_Number { get; set; }
        public double Minimum_Temperature { get; set; }
        public double Maximum_Temperature { get; set; }
        public double Humidity { get; set; }
        public double Daily_Amount_Of_Light { get; set; }
        public double Daily_Amount_Of_Water { get; set; }
        public Parameters(string userId, int greenHous_Number, double minimum_Temperature, double maximum_Temperature, double humidity, double daily_Amount_Of_Light, double daily_Amount_Of_Water)
        {
            UserId = userId;
            GreenHous_Number = greenHous_Number;
            Minimum_Temperature = minimum_Temperature;
            Maximum_Temperature = maximum_Temperature;
            Humidity = humidity;
            Daily_Amount_Of_Light = daily_Amount_Of_Light;
            Daily_Amount_Of_Water = daily_Amount_Of_Water;
        }

        public Parameters()
        {
        }

        public override string ToString()
        {
            return $"{UserId}, {GreenHous_Number}, {Minimum_Temperature}, {Maximum_Temperature}, {Humidity}, {Daily_Amount_Of_Light}, {Daily_Amount_Of_Water}";
        }
    }
}