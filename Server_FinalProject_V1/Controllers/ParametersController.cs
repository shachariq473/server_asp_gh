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
    public class ParametersController : ApiController
    {
        [Route("Farmers/{id}GreenHouses/{houseId}/Parameters")]
        public IHttpActionResult Get(int id, int houseId)
        {
            try
            {
                Parameters Parameters2Return = DBParameters.GetParameters(id.ToString(), houseId);
                if (Parameters2Return == null)
                {
                    return Content(HttpStatusCode.NotFound, $"Parameters not found in SQL! ");
                }

                return Ok(Parameters2Return);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Parameters value)
        {
            try
            {
                int num = DBParameters.CreateParameters(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Parameters {value.GreenHous_Number} not be added to database! ");
                }

                return Ok($"Parameters {value.GreenHous_Number} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody] Parameters value)
        {
            try
            {
                int num = DBParameters.UpdateParameters(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Parameters {value.GreenHous_Number} not be added to database! ");
                }

                return Ok($"Parameters {value.GreenHous_Number} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("Farmers/{id}GreenHouses/{houseId}/Parameters")]
        public IHttpActionResult DeleteOne(int id, int houseId)
        {
            try
            {
                int checkDelete = DBParameters.DeleteParameter(id.ToString(), houseId);
                if (checkDelete == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Parameters not found in SQL! ");
                }

                return Content(HttpStatusCode.Found, $"Parameters with Green Houses {houseId} deleted! "); ;
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
                int checkDelete = DBParameters.DeleteParameters(id.ToString());
                if (checkDelete == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Parameters not found in SQL! ");
                }

                return Content(HttpStatusCode.Found, $"farmer id numbert {id} with all green hose parameters deleted! "); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}