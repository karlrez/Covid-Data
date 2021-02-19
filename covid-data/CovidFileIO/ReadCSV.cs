using System.IO;
using System.Collections.Generic;
using covid_data.Entities;
using covid_data.Data;
using System;

namespace covid_data
{
    public static class ReadCSV
    {
        /// <summary>
        /// This class will perform file io on our csv dataset and includes methods for CRUD operations.
        /// Class and method is declared static so we only perform file io once on startup.
        /// </summary>
        /// <param name="filePath">String path to the dataset.</param>
        /// <author>Karl Rezansoff</author>
        /// <created>Jan 20, 2021</created>


        /// <value>String path to the dataset file.</value>
        private static string filePath { get; set; }
        /// <value>Int number of how many lines to read in from the csv dataset.</value>
        private static int numOfLines { get; set; }
        /// <value> String array to represent headers in csv dataset.</value>
        private static string[] headers { get; set; }
        /// <value>List to store CovidDataObjects</value>
        private static List<CovidData> covidDataObjects { get; set; }
        /// <value>Int to keep track of index of covidDataObjects list, so we can add a unique id to each record.</value>
        private static int index = 1;
        /// <value>Meant to be a dummy variable so I can show my name in the covidDataObjects list</value>
        public static KarlObject karlObject;

        static ReadCSV()
        {
            /// <summary>
            /// Class contructor that initializes the class fields
            /// </summary>
            /// <typeparam name="CovidData">Contains fields corresponding to the dataset.</typeparam>
            /// <returns></returns>
            numOfLines = 100;
            covidDataObjects = new List<CovidData>();

            filePath = "/home/karl/vscode/dotnet/covid-data/covid-data/CovidFileIO/covid19-download.csv";
            // Let user know if dataset exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return;
            }


            string[] karlData = new string[20];
            karlData[0] = "karl object";
            karlObject = new KarlObject(headers, karlData, 0);

            CreateDataObjects(numOfLines);
        }

        /// <summary>
        /// Method to parse dataset and create CovidData objects for each record.
        /// </summary>
        /// <param name="numOfLines">Int value for how many CovidData Objects you want to create.</param>
        public static void CreateDataObjects(int numOfLines)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                headers = reader.ReadLine().Split(','); // first line is headers

                //Parsing in a lines from dataset
                for (int i = 0; i < numOfLines; i++)
                {
                    covidDataObjects.Add(new CovidData(headers, reader.ReadLine().Split(','), index));
                    index++;
                }
            }
            Console.WriteLine("Finished parsing csv\n Karl Rezansoff");
            covidDataObjects.Insert(0, karlObject); // Insert an object with my name just to fulfill name requirement
        }

        /// <summary>
        /// Returns the covidDataObjects list
        /// </summary>
        /// <returns>Returns the entire covidDataObjects list</returns>
        public static List<CovidData> GetAll()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return null;
            }

            return covidDataObjects;
        }

        /// <summary>
        /// Returns a single record from the covidDataObjects list
        /// </summary>
        /// <param name="id">int id of record</param>
        /// <returns>Returns single covidDataObject</returns>
        public static CovidData get(int id)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return null;
            }

            for (int i = 0; i < covidDataObjects.Count; i++)
            {
                if (covidDataObjects[i].id == id)
                {
                    return covidDataObjects[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Clears the covidDataObjects list and resets the index back to 1.
        /// </summary>
        public static void Refresh()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return;
            }

            covidDataObjects.Clear();
            index = 1;
            CreateDataObjects(numOfLines);
        }

        /// <summary>
        /// Method to add a single object to the covidDataObject list
        /// </summary>
        /// <param name="dataArray">string array to represent headers of the csv dataset.</param>
        /// <returns>Returns the created CovidData object</returns>
        public static CovidData Add(string[] dataArray)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return null;
            }

            CovidData newObj = new CovidData(headers, dataArray, index);
            covidDataObjects.Add(newObj);
            index++;
            return newObj;
        }

        /// <summary>
        /// Method to edit an object in the covidDataObject list.
        /// </summary>
        /// <param name="data">Data from api request converted to type JSONData</param>
        /// <returns>Edited object of type CovidData</returns>
        public static CovidData Edit(JSONData data)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return null;
            }

            // Find item to edit
            for (int i = 0; i < covidDataObjects.Count; i++)
            {
                if (covidDataObjects[i].id == data.id)
                {
                    return covidDataObjects[i].Edit(data);
                }
            }
            return null;
        }

        /// <summary>
        /// Remove a record from the covidDataObject dataset.
        /// </summary>
        /// <param name="id">Int id of the object</param>
        /// <returns>bool value to indicate if delete was success</returns>
        public static bool Delete(int id)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return false;
            }

            for (int i = 0; i < covidDataObjects.Count; i++)
            {
                if (covidDataObjects[i].id == id)
                {
                    covidDataObjects.Remove(covidDataObjects[i]);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to write the current covidDataObject list to the filesystem as a new file name newFile.csv
        /// </summary>
        /// <returns>bool value to indicate success or fail</returns>
        public static bool writeToFile()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Covid dataset does not exist");
                return false;
            }

            using (var file = File.CreateText("CovidFileIO/newFile.csv"))
            {
                // Adding headers
                string[] rowLabels = { "pruid", "prname", "prnameFR", "date", "numconf", "numprob", "numdeaths", "numtotal", "numtoday", "ratetotal" };
                file.WriteLine(string.Join(",", rowLabels));
                for (int i = 0; i < covidDataObjects.Count; i++)
                {
                    string[] fieldsArray = covidDataObjects[i].fieldsToArray();
                    file.WriteLine(string.Join(",", fieldsArray));
                }
            }
            return true;
        }
    }
}