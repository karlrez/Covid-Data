using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using covid_data.Entities;
using covid_data;
using Microsoft.AspNetCore.Http;
using System;

namespace API.Controllers
{
    // Attributes for the controller
    [ApiController]

    public class CovidDataController : ControllerBase
    {
        /// <summary>
        /// Controller class for the Covid Data routes.
        /// </summary>
        /// <author>Karl Rezansoff</author>
        /// <created>Jan 20, 2021</created>

        // Return all objects or specified object from the covid data list.
        // route is api/CovidData
        // Add id? query param to get single object
        [Route("api/[controller]")]
        [HttpGet]
        public List<CovidData> GetData()
        {
            return ReadCSV.GetAll();
        }

        [Route("api/[controller]/get")]
        [HttpGet]
        public List<CovidData> GetSingleRecord()
        {
            string id = HttpContext.Request.Query["id"].ToString();
            if (id != null)
            {
                CovidData record = ReadCSV.get(Int32.Parse(id));

                if (record == null)
                {
                    this.HttpContext.Response.StatusCode = 404;
                    return null;
                }
                return new List<CovidData>() { ReadCSV.karlObject, record };
            }
            return null;
        }

        // Refresh the covid data list
        [Route("api/[controller]/refresh")]
        [HttpGet]
        public ActionResult<string> Refresh()
        {
            ReadCSV.Refresh();
            return "Karl Rezansoff";
        }

        // Add to covid data list
        [Route("api/[controller]/add")]
        [HttpPost]
        public ActionResult<string> Add([FromBody] JSONData covidData)
        {
            ReadCSV.Add(covidData.fieldsToArray());
            this.HttpContext.Response.StatusCode = 201;
            return "Karl Rezansoff";
        }

        // Edit a record in the covid data list
        [Route("api/[controller]/edit")]
        [HttpPatch]
        public ActionResult<string> Edit([FromBody] JSONData data)
        {
            ReadCSV.Edit(data);
            this.HttpContext.Response.StatusCode = 200;
            return "Karl Rezansoff";
        }

        // Delete a record in the covid data list
        [Route("api/[controller]/delete")]
        [HttpDelete]
        public ActionResult<string> Delete([FromBody] JSONData data)
        {
            if (!ReadCSV.Delete(data.id))
            {
                this.HttpContext.Response.StatusCode = 404;
            }
            // Should be status 204(No Content) but then i wont be able to send back my name
            return "Karl Rezansoff";
        }

        // Write the current covid data list to a csv file
        [Route("api/[controller]/save")]
        [HttpGet]
        public ActionResult<string> Save()
        {
            ReadCSV.writeToFile();
            return "Karl Rezansoff";
        }
    }
}
