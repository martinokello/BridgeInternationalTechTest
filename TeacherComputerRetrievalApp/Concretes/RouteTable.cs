using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherComputerRetrievalApp.Infrastructure;
using TeacherComputerRetrievalApp.Interfaces;

namespace TeacherComputerRetrievalApp.Concretes
{
    public class RouteTable : IRouteTable
    {
        private Dictionary<string, int> _routes;
        private char NO_SUCH_ROUTE = ':';
        public RouteTable(Dictionary<string, int> routes)
        {
            _routes = routes;
        }
        public Dictionary<string, int> Routes {
            get { return _routes; }
            set { _routes = value; }
        }

        public int DistanceBetweenKnownRoutes(string routePath)
        {
            var successiveHops = routePath.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            int distance = 0;

            for (int n = 0, i = 1; n < successiveHops.Length - 1; n++, i++)
            {
                var hopKey = $"{successiveHops[n]}{successiveHops[i]}";
                int d = 0;
                _routes.TryGetValue(hopKey, out d);
                if (d == 0) return 0;
                distance += d;
            }
            return distance;
        }

        public Dictionary<string, int> GetAllRoutes(string startAcademy, string endAcademy, int numberOfLoopsAllowed = 1)
        {
            var results = new Dictionary<string, int>();
            var hopeRouteValuePair = _routes.Where(k => k.Key.StartsWith(startAcademy)).ToList();
            foreach (var availableHopRoute in hopeRouteValuePair)
            {
                var keyBuilder = new StringBuilder();
                var distance = 0;
                var routeFound = false;
                var availableHop = availableHopRoute.Key;
                if (keyBuilder.ToString().AlreadyContainsOccurencesOf(availableHop, numberOfLoopsAllowed)) continue;
                FindRoutes(startAcademy, endAcademy, availableHop, availableHopRoute, keyBuilder, ref distance, ref routeFound, ref numberOfLoopsAllowed);
                if (routeFound)
                {
                    var key = keyBuilder.ToString();
                    routeFound = false;

                    var otherQuickRoutes = key.GetAllOtherRoutes(Routes);
                    foreach(var oKey in otherQuickRoutes.Keys)
                    {
                        if (!results.ContainsKey(oKey))
                            results.Add(oKey, otherQuickRoutes[oKey]);
                    }
                    if (!results.ContainsKey(key))
                    {
                        results.Add(key, distance);
                        continue;
                    }
                }
            }
            return results;
        }

        public void FindRoutes(string startAcademy, string endAcademy, string currentHop, KeyValuePair<string, int> route, StringBuilder keyBuilder, ref int distance, ref bool routeFound,ref int numberOfLoopsAllowed)
        {
            if (route.Key.StartsWith(currentHop)) distance += _routes[currentHop];
            keyBuilder.Append(currentHop);
            if (currentHop.EndsWith(endAcademy))
            {
                routeFound = true;
            }
            if (routeFound && keyBuilder.ToString().AlreadyContainsOccurencesOf(currentHop, numberOfLoopsAllowed))
            {
                return;
            }

            var nextHops = _routes.Where(k => k.Key.StartsWith(currentHop.Substring(1, 1))).ToList();
            foreach (var kv in nextHops)
            {
                if (keyBuilder.ToString().AlreadyContainsOccurencesOf(kv.Key, numberOfLoopsAllowed)) continue;
                FindRoutes(startAcademy, endAcademy, kv.Key, kv, keyBuilder, ref distance, ref routeFound, ref numberOfLoopsAllowed);
                if (routeFound) break;
            }
        }
        public int NumberOfRoutesBetweenWithMaxStops(string startAcademy, string endAcademy, int maxStops)
        {
            var finalResults = GetAllRoutes(startAcademy, endAcademy);
            return finalResults.Keys.Where(p => p.UniqueCharacters() <= maxStops).Count();
        }
        public int NumberOfRoutesBetweenWithExactStops(string startAcademy, string endAcademy, int exacStops)
        {
            var numberOfLoopsAllowed = 5;
            var results = GetAllRoutes(startAcademy, endAcademy, numberOfLoopsAllowed);
            return results.Keys.Where(p => p.UniqueCharacters() == exacStops).Count();
        }
        public int NumberOfRoutesBetweenWithMaxDistance(string startAcademy, string endAcademy, int maxDistance)
        {
            var numberOfLoopsAllowed = 5;
            var results = GetAllRoutes(startAcademy, endAcademy, numberOfLoopsAllowed);
            return results.Where(p => p.Value < maxDistance).Count();
        }

        public string GetShortestRoute(string startAcademy, string endAcademy)
        {
            return GetAllRoutes(startAcademy, endAcademy).Where(v => v.Value > 0).OrderBy(p => p.Value).First().Key;
        }

        public int GetShortestRouteDistance(string startAcademy, string endAcademy)
        {
            return GetAllRoutes(startAcademy, endAcademy).Where(v => v.Value > 0).OrderBy(p => p.Value).First().Value;
        }
    }
}
