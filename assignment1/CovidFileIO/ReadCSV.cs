using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using assignment1.Entities;

namespace assignment1
{
    public class ReadCSV
    {
        /// <summary>
        /// This class will perform file io on our csv dataset and has a method
        /// to create CovidData objects to represent each record in the dataset.
        /// </summary>
        /// <param name="filePath">String path to the dataset.</param>
        /// <author>Karl Rezansoff</author>
        /// <created>Jan 20, 2021</created>
        public ReadCSV(string filePath)
        {
            /// <summary>
            /// Class contructor that initializes the class fields
            /// </summary>
            /// <typeparam name="CovidData">Contains fields corresponding to the dataset.</typeparam>
            /// <returns></returns>
            this.covidDataObjects = new List<CovidData>();
            this.filePath = filePath;
        }
        /// <summary>
        /// String path to the dataset file.
        /// </summary>
        /// <value>String path to the dataset file.</value>
        private string filePath { get; set; }
        /// <summary>
        /// List to store data transfer objects of type CovidData.
        /// </summary>
        /// <value>Each CovidData object represents a record in the dataset.</value>
        public List<CovidData> covidDataObjects { get; set; }

        /// <summary>
        /// Method to parse dataset and create CovidData objects for each record.
        /// </summary>
        /// <param name="numOfLines">Int value for how many CovidData Objects you want to create.</param>
        public void CreateDataObjects(int numOfLines)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string[] headers = reader.ReadLine().Split(','); // first line is headers

                //Parsing in a lines from dataset
                for (int i = 0; i < numOfLines; i++)
                {
                    covidDataObjects.Add(new CovidData(headers, reader.ReadLine().Split(',')));
                }
            }
        }
    }
}