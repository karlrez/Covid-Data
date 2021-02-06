using System;
using System.Globalization;
using System.Reflection;

namespace covid_data.Entities
{
    public class CovidData
    {
        /// <summary>
        /// This class serves as the data transfer object for the Covid
        /// Data dataset to represent a single record.
        /// </summary>
        /// <param name="headers">String array of the dataset headers.</param>
        /// <param name="dataArray">String array of the values for one data record.</param>
        /// <param name="id">Int value that corressponds to records line number in the csv
        /// <author>Karl Rezansoff</author>
        /// <created>Jan 20, 2021</created>
        public int id { get; set; }
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

        public CovidData(string[] headers, string[] dataArray, int id)
        {
            if (dataArray[0] == "karl object") // dont need to error check when making Karl Objects
            {
                return;
            }

            this.id = id; // unique identifier for record

            try
            {
                this.pruid = Convert.ToInt32(dataArray[0].ToString().Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[0] + " line: " + id, ex);
            }

            try
            {
                this.prname = dataArray[1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[1] + " line: " + id, ex);
            }

            try
            {
                this.prnameFR = dataArray[2];
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[2] + " line: " + id, ex);
            }

            try
            {
                string dateInput = dataArray[3];
                this.date = DateTime.Parse(dateInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[3] + " line: " + id, ex);
            }

            try
            {
                this.numconf = Int32.Parse(dataArray[5]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[5] + " line: " + id, ex);
            }
            try
            {
                this.numprob = Int32.Parse(dataArray[6]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[6] + " line: " + id, ex);
            }
            try
            {
                this.numdeaths = Int32.Parse(dataArray[7]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[7] + " line: " + id, ex);
            }
            try
            {
                this.numtotal = Int32.Parse(dataArray[8]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[8] + " line: " + id, ex);
            }
            try
            {
                this.numtoday = Int32.Parse(dataArray[13]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[13] + " line: " + id, ex);
            }
            try
            {
                this.ratetotal = Double.Parse(dataArray[15].ToString().Trim(), NumberStyles.Number);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[15] + " line: " + id, ex);
            }
        }

        // Creates an easy to read string of the class fields. 
        public override string ToString()
        {
            return $@"CovidData object fields:
            pruid: {this.pruid}
            prname: {this.prname}
            prnameFR: {this.prnameFR}
            date: {this.date}
            numconf: {this.numconf}
            numprob: {this.numprob}
            numdeaths: {this.numdeaths}
            numtotal: {this.numtotal}
            numtoday: {this.numtoday}
            ratetotal: {this.ratetotal}";
        }

        // Returns class fields as string array
        public string[] fieldsToArray()
        {
            string[] fieldsArray = { pruid.ToString(), prname, prnameFR, date.ToString(), numconf.ToString(), numprob.ToString(), numdeaths.ToString(), numtotal.ToString(), numtoday.ToString(), ratetotal.ToString() };
            return fieldsArray;
        }

        // To edit the object
        public CovidData Edit(JSONData data)
        {
            id = data.id; // unique identifier for record
            pruid = data.pruid;
            prname = data.prname;
            prnameFR = data.prnameFR;
            date = data.date;
            numconf = data.numconf;
            numprob = data.numprob;
            numdeaths = data.numdeaths;
            numtotal = data.numtotal;
            numtoday = data.numtoday;
            ratetotal = data.ratetotal;

            return this;
        }
    }
}
