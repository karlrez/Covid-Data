using System;

namespace covid_data.Entities
{
    public class JSONData
    {
        public int pruid { get; set; } // Province ID
        public string prname { get; set; } // Province name
        public string prnameFR { get; set; } // Province name French
        public DateTime date { get; set; }
        public int numconf { get; set; } // Number confirmed
        public int numprob { get; set; }
        public int numdeaths { get; set; }
        public int numtotal { get; set; }
        public int numtoday { get; set; }
        public double ratetotal { get; set; }
        public int id { get; set; }

        // Returns class fields as string array
        public string[] fieldsToArray()
        {
            string[] fieldsArray = { pruid.ToString(), prname, prnameFR, date.ToString(), numconf.ToString(), numprob.ToString(), numdeaths.ToString(), numtotal.ToString(), numtoday.ToString(), ratetotal.ToString() };
            return fieldsArray;
        }
    }
}