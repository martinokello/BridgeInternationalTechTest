using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeacherComputerRetrievalApp.Concretes;
using TeacherComputerRetrievalApp.Infrastructure;
using TeacherComputerRetrievalApp.Interfaces;

namespace TeacherComputerRetrieval.Tests
{
    [TestClass]
    public class TeacherComputerRetrievalTests
    {
        private IRouteTable _routeTable;
        private JourneyCalculator _journeyCalculator;

        [TestInitialize]
        public void Setup()
        {
            var routeDictionary = new Dictionary<string, int>();

            routeDictionary.Add("AB", 5);
            routeDictionary.Add("BC", 4);
            routeDictionary.Add("CD", 8);
            routeDictionary.Add("DC", 8);
            routeDictionary.Add("DE", 6);
            routeDictionary.Add("AD", 5);
            routeDictionary.Add("CE", 2);
            routeDictionary.Add("EB", 3);
            routeDictionary.Add("AE", 7);

            _routeTable = new RouteTable(routeDictionary);
        }
        [TestMethod]
        public void UniqueCharacters_ABBCD_Returns_ExpectedResults()
        {
            var input = "ABBCD";
            var result =  StringExtensions.UniqueCharacters(input);
            Assert.AreEqual(result, 4);

            input = "ABBCDEC";
            result = StringExtensions.UniqueCharacters(input);
            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void GetPattern_Returns_RightString()
        {
            var input = "CD";
            var result = StringExtensions.GetPattern(2, input);
            Assert.AreEqual(result, "((CD)+.*){2}");

            input = "CDRT";
            result = StringExtensions.GetPattern(3, input);
            Assert.AreEqual(result, "((CDRT)+.*){3}");
        }
        [TestMethod]
        public void AlreadyContainsOccurencesOf_Returns_ExpectedResults()
        {
            var input = "CDXCCDDE";
            var result = StringExtensions.AlreadyContainsOccurencesOf(input,"CD",2);
            Assert.IsTrue(result);

            input = "CDXC";
            result = StringExtensions.AlreadyContainsOccurencesOf(input, "CD", 2);
            Assert.IsFalse(result);

            input = "ABCEFABEFECDEFCD";
            result = StringExtensions.AlreadyContainsOccurencesOf(input, "EF", 3);
            Assert.IsTrue(result);

            input = "ABCEFABEFECDEFCD";
            result = StringExtensions.AlreadyContainsOccurencesOf(input, "EF", 4);
            Assert.IsFalse(result);

            input = "ABDCDCDEFEFEF";
            result = StringExtensions.AlreadyContainsOccurencesOf(input, "CD", 2);
            Assert.IsTrue(result);

            input = "ABCEFABEFECDFACD";
            result = StringExtensions.AlreadyContainsOccurencesOf(input, "EF", 3);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Calculate_GetDistanceBetweenRoutesA_B_C()
        {
            var dis = _routeTable.DistanceBetweenKnownRoutes("A-B-C");

            Assert.AreEqual(dis, 9);
        }
        [TestMethod]
        public void Calculate_GetDistanceBetweenRoutesA_E_B_C_D()
        {
            var dis = _routeTable.DistanceBetweenKnownRoutes("A-E-B-C-D");

            Assert.AreEqual(dis, 22);
        }
        [TestMethod]
        public void Calculate_GetDistanceBetweenRoutesA_E_D()
        {
            var dis = _routeTable.DistanceBetweenKnownRoutes("A-E-D");
            Assert.AreEqual(dis, 0);
        }

        [TestMethod]
        public void NumberOfTripsFromCtoC_Max3Stops()
        {
            var trips = _routeTable.NumberOfRoutesBetweenWithMaxStops("C", "C", 3);

            Assert.AreEqual(trips, 2);
        }
        [TestMethod]
        public void NumberOfTripsFromAtoC_Exactly4Stops()
        {
            var trips = _routeTable.NumberOfRoutesBetweenWithExactStops("A", "C", 4);

            Assert.AreEqual(trips, 3);
        }
        [TestMethod]
        public void LengthOfShortestRoute_GetShortestRouteDistanceAtoC()
        {
            var dist = _routeTable.GetShortestRouteDistance("A", "C");

            Assert.AreEqual(dist, 9);
        }
        [TestMethod]
        public void LengthOfShortestRoute_GetShortestRouteDistanceBtoB()
        {
            var dist = _routeTable.GetShortestRouteDistance("B", "B");

            Assert.AreEqual(dist, 9);
        }
        [TestMethod]
        public void NumberOfTripsFromCtoC_DistanceLessThat30()
        {
            var trips = _routeTable.NumberOfRoutesBetweenWithMaxDistance("C", "C", 30);

            Assert.AreEqual(trips, 7);
        }
    }
}
