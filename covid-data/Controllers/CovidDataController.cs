using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using covid_data.Entities;
using covid_data;
using covid_data.FileIO;
using Microsoft.AspNetCore.Http;
using System;
using assignment1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using covid_data.Data;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    public class CovidDataController : ControllerBase
    {
        /// <summary>
        /// Controller class for the Covid Data routes.
        /// </summary>
        /// <author>Karl Rezansoff</author>
        /// <created>Jan 20, 2021</created>

        /// <value>Gives us a session of the database</value>
        private readonly DataContext _context;
        /// <summary>
        /// Value for default pagination size
        /// </summary>
        public const int RESULTS_PER_PAGE = 20;
        /// <summary>
        /// Needed to access our hostname
        /// https://www.c-sharpcorner.com/blogs/getting-host-information-from-current-the-url-in-asp-net-core-31
        /// </summary>
        private readonly ILogger<CovidDataController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method to access our middleware
        /// </summary>
        /// <param name="context"></param>
        public CovidDataController(ILogger<CovidDataController> logger, IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        /// <summary>
        /// Returns all database records from the DailyCovidData table
        /// with specified orderby and filter
        /// 
        /// Example sortby and filter (note the escape character for ampersand):
        /// api/coviddata/?orderby=date DESC, prname&amp;provinceFilter=Alberta&amp;dateFilter=2021-01-09
        /// 
        /// </summary>
        /// <returns>Returns all records as a list</returns>
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ListResponse> GetData(string provinceFilter = null, string dateFilter = null, string orderBy = "id", int pageNum = 1)
        {
            List<CovidData> result;

            // Apply filters if given
            if (provinceFilter != null && dateFilter != null)
            {
                result = await _context.DailyCovidData.Where(obj => obj.prname == provinceFilter).Where(obj => obj.date == DateTime.Parse(dateFilter)).OrderBy(orderBy).ToListAsync();
            }
            else if (provinceFilter != null)
            {
                result = await _context.DailyCovidData.Where(obj => obj.prname == provinceFilter).OrderBy(orderBy).ToListAsync();
            }
            else if (dateFilter != null)
            {
                result = await _context.DailyCovidData.Where(obj => obj.date == DateTime.Parse(dateFilter)).OrderBy(orderBy).ToListAsync();
            }
            else
            {
                // Default orderby always applied
                result = await _context.DailyCovidData.OrderBy(orderBy).ToListAsync();
            }

            ListResponse listResponse = new ListResponse();
            listResponse.page = pageNum;
            listResponse.totalPages = (result.Count() + RESULTS_PER_PAGE - 1) / RESULTS_PER_PAGE;

            // Setting nextPage & prevPage or leaving as null
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            string apiUrl = "https://" + host + "/api/CovidData/?orderBy=" + orderBy;
            if (provinceFilter != null) apiUrl.Concat("&provinceFilter=" + provinceFilter);

            if ((pageNum * RESULTS_PER_PAGE) < result.Count())
            {
                string page = "&pageNum=" + (pageNum + 1).ToString();
                listResponse.nextPage = apiUrl + page;
            }

            if (pageNum > 1)
            {
                string page = "&pageNum=" + (pageNum - 1).ToString();
                listResponse.prevPage = apiUrl + page;
            }

            listResponse.totalResults = result.Count();

            // Add only RESULTS_PER_PAGE to result data
            listResponse.data = result.Skip((RESULTS_PER_PAGE * pageNum) - RESULTS_PER_PAGE).Take(RESULTS_PER_PAGE).ToList();
            listResponse.resultCount = listResponse.data.Count();

            return listResponse;
        }

        /// <summary>
        /// Query for record based on id
        /// </summary>
        /// <returns>Returns list with queried record and a dummy variable with my name</returns>
        [HttpGet("api/[controller]/{id}")] // api/CovidDataController/5
        public async Task<ActionResult<List<CovidData>>> GetSingleRecord([FromRoute] int id)
        {
            List<CovidData> resultList = new List<CovidData>();
            try
            {
                CovidData covidData = await _context.DailyCovidData.FindAsync(id);
                KarlObject karlObject = new KarlObject();

                if (covidData == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                resultList.Add(covidData);
                resultList.Add(new KarlObject());

                // Testing out overidden toString()
                Console.WriteLine("Base class toString() \n" + covidData.toString());
                Console.WriteLine("Child class toString() \n " + karlObject.toString());


                return resultList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Response.StatusCode = 500;
                return null;
            }

        }

        /// <summary>
        /// Add a record to the DailyCovidData database table
        /// </summary>
        /// <param name="covidData">JSON request with variables mapped to a CovidData object.
        /// Ignores id (id autoincrements) and any missing values default to 0.</param>
        /// <returns>Returns a string confirmation or error to user</returns>
        [Route("api/[controller]/add")]
        [HttpPost]
        public async Task<ActionResult<string>> Add([FromBody] JSONData covidData)
        {
            // Create new object and set values
            CovidData newRecord = new CovidData();
            newRecord.setJsonValues(covidData);

            try
            {
                await _context.DailyCovidData.AddAsync(newRecord); // Add to our data context
                await _context.SaveChangesAsync(); // Commit to the db

                Response.StatusCode = 201;
                return "Record with ID: " + newRecord.id + " added successfully. \n Karl Rezansoff";
            }
            catch (Exception ex) { return "Error adding record\n" + ex; }
        }

        /// <summary>
        /// Edit a record in the db
        /// </summary>
        /// <param name="data">JSON request with variables mapped to CovidData object.
        /// Cant edit id, missing values default to 0</param>
        /// <returns>String confirmation or error message</returns>
        [Route("api/[controller]/edit")]
        [HttpPatch]
        public async Task<ActionResult<string>> Edit([FromBody] JSONData data)
        {
            try
            {
                CovidData editItem = await _context.DailyCovidData.FindAsync(data.id);
                if (editItem == null)
                {
                    return "Record with id: " + data.id + " not found in database";
                }

                editItem.setJsonValues(data); // Making changes
                await _context.SaveChangesAsync();
                return "Record with id: " + data.id + " modified successfully. \n Karl Rezansoff";
            }
            catch (Exception ex)
            {
                return "Error adding record\n" + ex;
            }
        }

        /// <summary>
        /// Remove a record from the DailyCovidData database table
        /// </summary>
        /// <param name="data">JSON request with value for id</param>
        /// <returns>String confirmation or error message</returns>
        [Route("api/[controller]/delete")]
        [HttpDelete]
        public async Task<ActionResult<string>> Delete([FromBody] JSONData data)
        {
            try
            {
                CovidData removeItem = await _context.DailyCovidData.FindAsync(data.id); // get item to remove
                if (removeItem == null)
                {
                    return "Record with id: " + data.id + " not found in database";
                }

                _context.DailyCovidData.Remove(removeItem); // Remove from data context
                await _context.SaveChangesAsync(); // Commit changes

                return "Record with id: " + data.id + " deleted successfully. \n Karl Rezansoff";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Error adding record\n" + ex;
            }
        }

        /// <summary>
        /// Removes all records from the DailyCovidData table and then reloads the database again with the csv data
        /// </summary>
        /// <returns>String confirmation or error message</returns>
        [Route("api/[controller]/refresh")]
        [HttpGet]
        public async Task<ActionResult<string>> Refresh()
        {
            try
            {
                // Remove all records
                List<CovidData> deletedObjects = await _context.DailyCovidData.ToListAsync();
                _context.DailyCovidData.RemoveRange(deletedObjects);

                // Add records in again
                ReadCSV readCSV = new ReadCSV();
                _context.DailyCovidData.AddRange(readCSV.covidDataObjects);
                await _context.SaveChangesAsync();
                return "Successfully refreshed database";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Error refreshing database \n" + ex;
            }

        }
    }
}
