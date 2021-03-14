using System.Collections.Generic;

namespace covid_data.Entities
{
    /// <summary>
    /// Class is to be used with the GetData() api controller to provide the
    /// list of data and pagination info
    /// </summary>
    public class ListResponse
    {
        public string myName = "Karl Rezansoff";
        public int page { get; set; }
        public int totalPages { get; set; }
        public string nextPage { get; set; }
        public string prevPage { get; set; }
        public int resultCount { get; set; }
        public int totalResults { get; set; }
        public List<CovidData> data { get; set; }

        public ListResponse()
        {
            page = 1;
            totalPages = 1;
            nextPage = null;
            prevPage = null;
            resultCount = 0;
            totalResults = 0;
            data = new List<CovidData>();
        }

        public ListResponse(int page, int totalPages, string nextPage, string prevPage, int resultCount, int totalResults, List<CovidData> data)
        {
            this.page = page;
            this.totalPages = totalPages;
            this.nextPage = nextPage;
            this.prevPage = prevPage;
            this.resultCount = resultCount;
            this.totalResults = totalResults;
            this.data = data;
        }
    }
}