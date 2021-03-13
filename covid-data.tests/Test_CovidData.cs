using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using covid_data.Entities;
using covid_data.Data;
using System;

namespace covid_data.tests
{
    /// <summary>
    /// Unit tests for testing the CovidData class
    /// </summary>
    /// <author>Karl Rezansoff</author>
    /// <created>March 13, 2021</created>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CovidData_DefaultConstructor()
        {
            // KarlObject extends CovidData object and has different default values
            CovidData covidData = new CovidData();
            CovidData karlObject = new KarlObject();

            Assert.AreNotEqual(covidData.prname, karlObject.prname);
            Assert.AreNotEqual(covidData.prnameFR, karlObject.prnameFR);
        }

        [TestMethod]
        public void CovidData_TestToString()
        {
            // KarlObject overrides the base class toString method
            CovidData covidData = new CovidData();
            CovidData karlObject = new KarlObject();

            Assert.AreNotEqual(covidData.toString(), karlObject.toString());
        }
    }
}
