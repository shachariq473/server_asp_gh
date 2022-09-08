using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Bogus.DataSets;
using DAL;
using DocumentFormat.OpenXml.Wordprocessing;
using Nest;

namespace Server_FinalProject_V1.Controllers
{
    public class GreenHousesController : ApiController
    {
        [Route("GetAll")] //, Name = "GetGreenHousesById"
        public IHttpActionResult GetAll(int id)
        {
           
            try
            {
                List<GreenHouses> houses2Return = new List<GreenHouses>();
                houses2Return = DBGreenHouses.GetAllGreenHouses(id.ToString());
                if (houses2Return == null)
                {
                    return Ok("אין חממות פעילות");
                }

                return Ok(houses2Return);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("GetOne")] //, Name = "GetGreenHousesById"
        public IHttpActionResult GetOne(int id, int houseId)
        {
            try
            {
                GreenHouses house2Return = DBGreenHouses.GetGreenHouses(id.ToString(),houseId);
                if (house2Return == null)
                {
                    return Content(HttpStatusCode.NotFound, $"farmer not found in SQL! ");
                }

                return Ok(house2Return);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] GreenHouses value)
        {
            try
            {
                int num = DBGreenHouses.CreateGreenHouses(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Green House {value.GreenHouse_Name} not be added to database! Apparently the number of the greenhouse exists in the system");
                }

                return Ok($"Green House {value.GreenHouse_Name} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody] GreenHouses value)
        {
            try
            {
                int num = DBGreenHouses.UpdateGreenHouses(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Green Houses {value.GreenHouse_Name} not be added to database! ");
                }

                return Ok($"Green Houses {value.GreenHouse_Name} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("DeleteOne")] //, Name = "GetFarmerById"
        public IHttpActionResult DeleteOne(int id, int houseId)
        {
            try
            {
                int checkDelete = DBGreenHouses.DeleteGreenHouse(id.ToString(),houseId);
                if (checkDelete == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"farmer not found in SQL! ");
                }

                return Content(HttpStatusCode.Found, $"farmer id = {id} with green house number = {houseId} deleted! "); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("id:int")]
        public IHttpActionResult DeleteAll(int id)
        {
            try
            {
                int checkDelete = DBGreenHouses.DeleteGreenHouses(id.ToString());
                if (checkDelete == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"farmer not found in SQL! ");
                }

                return Content(HttpStatusCode.Found, $"farmer with id {id} deleted! "); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}