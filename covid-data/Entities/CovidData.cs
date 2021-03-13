using System;
using System.Globalization;
using System.Reflection;

namespace covid_data.Entities
{
    public class CovidData
    {
        /// <summary>
        /// This class serves as the data transfer object for a record from the covid dataset.
        /// Each attribute maps to a column in the database table.
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

        public CovidData()
        {

        }

        public CovidData(string[] headers, string[] dataArray, int index)
        {
            try
            {
                this.pruid = Convert.ToInt32(dataArray[1].ToString().Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[1] + " line: " + index + "\n" + ex);
            }

            try
            {
                this.prname = dataArray[2];
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[2] + " line: " + index + "\n" + ex);
            }

            try
            {
                this.prnameFR = dataArray[3];
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[3] + " line: " + index + "\n" + ex);
            }

            try
            {
                string dateInput = dataArray[4];
                this.date = DateTime.Parse(dateInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[4] + " line: " + index + "\n" + ex);
            }

            try
            {
                this.numconf = Int32.Parse(dataArray[5]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[5] + " line: " + index + "\n" + ex);
            }
            try
            {
                this.numprob = Int32.Parse(dataArray[6]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[6] + " line: " + index + "\n" + ex);
            }
            try
            {
                if (dataArray[7] == "")
                {
                    this.numdeaths = 0;
                }
                else
                {
                    this.numdeaths = Int32.Parse(dataArray[7]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[7] + " line: " + index + "\n" + ex);
            }
            try
            {
                this.numtotal = Int32.Parse(dataArray[8]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[8] + " line: " + index + "\n" + ex);
            }
            try
            {
                this.numtoday = Int32.Parse(dataArray[9]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[9] + " line: " + index + "\n" + ex);
            }
            try
            {
                if (dataArray[10] == "")
                {
                    this.ratetotal = 0;
                }
                else
                {
                    this.ratetotal = Double.Parse(dataArray[10].ToString().Trim(), NumberStyles.Number);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[10] + " line: " + index + "\n" + ex);
            }
        }

        // Returns class fields as string array
        public string[] fieldsToArray()
        {
            string[] fieldsArray = { pruid.ToString(), prname, prnameFR, date.ToString(), numconf.ToString(), numprob.ToString(), numdeaths.ToString(), numtotal.ToString(), numtoday.ToString(), ratetotal.ToString() };
            return fieldsArray;
        }

        // Set values fields based on JSON data
        public bool setJsonValues(JSONData jsonData)
        {
            try
            {
                pruid = jsonData.pruid;
                prname = jsonData.prname;
                prnameFR = jsonData.prnameFR;
                date = jsonData.date;
                numconf = jsonData.numconf;
                numprob = jsonData.numprob;
                numdeaths = jsonData.numdeaths;
                numtotal = jsonData.numtotal;
                numtoday = jsonData.numtoday;
                ratetotal = jsonData.ratetotal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error setting JSON values:\n" + e);
                return false;
            }
            return true;
        }

        // To edit the object
        public CovidData Edit(JSONData data)
        {
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

        public virtual string toString()
        {
            return pruid + "\n" +
                prname + "\n" +
                prnameFR + "\n" +
                date + "\n" +
                numconf + "\n" +
                numprob + "\n" +
                numdeaths + "\n" +
                numtotal + "\n" +
                numtoday + "\n" +
                ratetotal + "\n";
        }
    }
}
