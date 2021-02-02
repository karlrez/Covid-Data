using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using assignment1.Data;
using assignment1.Entities;
using assignment1;


namespace API.Controllers
{
    // Attributes for the controller
    [ApiController]
    [Route("api/[controller]")] // route is api/CovidData

    public class CovidDataController : ControllerBase
    {
        /// <summary>
        /// File io on the covid dataset is performed once when the class is initialized.
        /// One api endpoint provided to view the data.
        /// </summary>
        /// <author>Karl Rezansoff</author>
        /// <created>Jan 20, 2021</created>
        private ReadCSV readCSV;

        public CovidDataController()
        {
            readCSV = new ReadCSV("CovidFileIO/covid19-download.csv");
            readCSV.CreateDataObjects(15); // Reading in 15 records.
        }

        // This endpoint will return records from the covid dataset.
        [HttpGet]
        public List<CovidData> GetData()
        {
            return readCSV.covidDataObjects;
        }
    }
}
