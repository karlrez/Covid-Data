using System;
using System.Globalization;
using System.Reflection;

namespace assignment1.Entities
{
    public class CovidData
    {
        /// <summary>
        /// This class serves as the data transfer object for the Covid
        /// Data dataset to represent a single record.
        /// </summary>
        /// <param name="headers">String array of the dataset headers.</param>
        /// <param name="dataArray">String array of the values for one data record.</param>
        /// <author>Karl Rezansoff</author>
        /// <created>Jan 20, 2021</created>
        public CovidData(string[] headers, string[] dataArray)
        {
            name = "Karl Rezansoff";

            try
            {
                this.pruid = Convert.ToInt32(dataArray[0].ToString().Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[0], ex);
            }

            try
            {
                this.prname = dataArray[1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[1], ex);
            }

            try
            {
                this.prnameFR = dataArray[3];
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[2], ex);
            }

            try
            {
                string dateInput = dataArray[3];
                this.date = DateTime.Parse(dateInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[3], ex);
            }

            try
            {
                this.numconf = Int32.Parse(dataArray[5]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[5], ex);
            }
            try
            {
                this.numprob = Int32.Parse(dataArray[6]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[6], ex);
            }
            try
            {
                this.numdeaths = Int32.Parse(dataArray[7]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[7], ex);
            }
            try
            {
                this.numtotal = Int32.Parse(dataArray[8]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[8], ex);
            }
            try
            {
                this.numtoday = Int32.Parse(dataArray[13]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[13], ex);
            }
            try
            {
                this.ratetotal = Double.Parse(dataArray[15].ToString().Trim(), NumberStyles.Number);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception at: " + headers[15], ex);
            }
        }

        // Declaring class fields with getter/setters
        public string name { get; set; } // Just to satisfy the name requirement of the assignment 
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
    }
}