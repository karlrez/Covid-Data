using System.IO;
using System.Collections.Generic;
using covid_data.Entities;
using covid_data.Data;
using System;

namespace covid_data.FileIO
{
    public class ReadCSV
    {
        /// <value>String path to the dataset file.</value>
        string filePath = Environment.CurrentDirectory + "/FileIO/covid19-download.csv";
        /// <value> String array to represent headers in csv dataset.</value>
        private string[] headers = { "id", "pruid", "prname", "prnameFR", "date", "numconf", "numprob", "numdeaths", "numtotal", "numtoday", "ratetotal" };
        /// <value>List to store CovidDataObjects</value>
        public List<CovidData> covidDataObjects { get; set; }
        /// <value>Meant to be a dummy variable so I can show my name in the covidDataObjects list</value>

        public ReadCSV()
        {
            /// <summary>
            /// Class contructor that initializes the class fields
            /// </summary>
            /// <typeparam name="CovidData">Contains fields corresponding to the dataset.</typeparam>
            /// <returns></returns>
            covidDataObjects = new List<CovidData>();
            // Let user know if dataset exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist at: " + filePath);
                return;
            }
            CreateDataObjects();
        }

        /// <summary>
        /// Method to parse dataset and create CovidData objects for each record.
        /// </summary>
        /// <param name="numOfLines">Int value for how many CovidData Objects you want to create.</param>
        public void CreateDataObjects()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                int index = 1;
                //Parsing in a lines from dataset
                while (!reader.EndOfStream)
                {
                    covidDataObjects.Add(new CovidData(headers, reader.ReadLine().Split(','), index));
                    index++;
                }
            }
            Console.WriteLine("Finished parsing csv\n Karl Rezansoff");
        }

    }
}