using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using assignment1.Entities;

namespace assignment1
{
    /**
    This class will perform file io on our csv dataset and has a method
    to create CovidData objects to represent each record in the dataset.

    @author Karl Rezansoff
    */
    public class ReadCSV
    {
        public ReadCSV(string filePath)
        {
            this.covidDataObjects = new List<CovidData>();
            this.filePath = filePath;
        }
        private string filePath { get; set; }
        public List<CovidData> covidDataObjects { get; set; }
        public void ReadSomeLines(int numOfLines)
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