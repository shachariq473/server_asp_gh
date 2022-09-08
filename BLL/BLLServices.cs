using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
    public static class BLLServices
    {
        public static Farmers GetFarmerId(Farmers value)
        {
            return DBFarmers.GetFarmer(value.UserId);
        }
        public static int CreateNewFarmers(Farmers value)
        {
            return DBFarmers.CreateFarmers(value);
        }
        public static int UpdateFarmersData(Farmers value)
        {
            return DBFarmers.UpdateFarmers(value);
        }
        public static int DeleteFarmersData(Farmers value)
        {
            return DBFarmers.DeleteFarmer(value.UserId);
        }
    }

}
