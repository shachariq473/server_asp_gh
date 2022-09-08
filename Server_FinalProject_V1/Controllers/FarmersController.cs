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

    public class FarmersController : ApiController
    {
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Farmers farmer2Return = DBFarmers.GetFarmer(id.ToString());
                if (farmer2Return == null)
                {
                    return Content(HttpStatusCode.NotFound, $"farmer not found in SQL! ");
                }

                return Ok(farmer2Return);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("GetAllFarmers")]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<Farmers> farmers2Return = new List<Farmers>();
                farmers2Return = DBFarmers.GetAllFarmer();
                if (farmers2Return == null)
                {
                    return Content(HttpStatusCode.NotFound, $"farmer not found in SQL! ");
                }

                return Ok(farmers2Return);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Farmers value)
        {
            try
            {
                int num = DBFarmers.CreateFarmers(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Farmer {value.UserName} not be added to database!");
                }

                return Ok($"Farmer {value.UserName} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody] Farmers value)
        {
            try
            {
                int num = DBFarmers.UpdateFarmers(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Farmer {value.UserName} not be added to database! ");
                }

                return Ok($"Farmer {value.UserName} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("{id:int}")] //, Name = "GetFarmerById"
        public IHttpActionResult Delete(int id)
        {
            try
            {
                int checkDelete = DBFarmers.DeleteFarmer(id.ToString());
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
