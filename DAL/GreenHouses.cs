using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class GreenHouses
    {
        public int GreenHouse_Id { get; set; }
        public string UserId { get; set; }
        public string GreenHouse_Name { get; set; }
        public string GreenHouse_Location { get; set; }
        public GreenHouses(int greenHouse_Id, string userId, string greenHouse_Name, string greenHouse_Location)
        {
            GreenHouse_Id = greenHouse_Id;
            UserId = userId;
            GreenHouse_Name = greenHouse_Name;
            GreenHouse_Location = greenHouse_Location;
        }

        public GreenHouses()
        {
        }

        public override string ToString()
        {
            return $"{GreenHouse_Id}, {UserId}, {GreenHouse_Name}, {GreenHouse_Location}";
        }
    }
}