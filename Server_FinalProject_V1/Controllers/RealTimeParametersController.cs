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
    public class RealTimeParametersController : ApiController
    {
        [Route("GetRealParameters")]
        public IHttpActionResult Get(int id, int houseId)
        {
            try
            {
                List<RealTimeParameters> RealTimeParameters2Return = new List<RealTimeParameters>();
                RealTimeParameters2Return = DBRealTimeParameters.GetRealTimeParameters(id.ToString(), houseId);
                if (RealTimeParameters2Return == null)
                {
                    return Content(HttpStatusCode.NotFound, $"Parameters not found in SQL! ");
                }

                return Ok(RealTimeParameters2Return);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("GetLastRealTimeParameter")]
        public IHttpActionResult GetLast(int id, int houseId)
        {
            try
            {
                List<RealTimeParameters> RealTimeParameters2Return = new List<RealTimeParameters>();
                RealTimeParameters2Return = DBRealTimeParameters.GetLastRealTimeParameter(id.ToString(), houseId);
                if (RealTimeParameters2Return == null)
                {
                    return Content(HttpStatusCode.NotFound, $"Parameters not found in SQL! ");
                }

                return Ok(RealTimeParameters2Return);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] RealTimeParameters value)
        {
            try
            {
                int num = DBRealTimeParameters.CreateRealTimeParameters(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Real Time Parameters in Green Houses {value.GreenHous_Number} not be added to database! ");
                }

                return Ok($"Real Time Parameters in Green Houses {value.GreenHous_Number} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody] RealTimeParameters value)
        {
            try
            {
                int num = DBRealTimeParameters.UpdateRealTimeParameters(value);
                if (num == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Real Time Parameters in Green Houses {value.GreenHous_Number} not be added to database! ");
                }

                return Ok($"Real Time Parameters in Green Houses {value.GreenHous_Number} added to database successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
        [Route("Farmers/{id}/RealTimeParameters")]
        public IHttpActionResult DeleteAll(int id)
        {
            try
            { 
                int checkDelete = DBRealTimeParameters.DeleteRealTimeParameters(id.ToString());
                if (checkDelete == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Real Time Parameters not found in SQL! ");
                }

                return Content(HttpStatusCode.Found, $"Real Time Parameters with id: {id} deleted! "); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}