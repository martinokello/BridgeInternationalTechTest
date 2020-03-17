using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherComputerRetrievalApp.Interfaces
{
    public interface IRouteTable
    {
        Dictionary<string, int> Routes { get; set; }
        int DistanceBetweenKnownRoutes(string routePath);
        void FindRoutes(string startAcademy, string endAcademy, string currentHop, KeyValuePair<string, int> route, StringBuilder keyBuilder,ref int distance, ref bool routeFound, ref int numberOfLoopsAllowed);
        Dictionary<string, int> GetAllRoutes(string startAcademy, string endAcademy, int numberOfLoopsAllowed = 1);
        int GetShortestRouteDistance(string startAcademy, string endAcadamey);     
        int NumberOfRoutesBetweenWithMaxStops(string startAcademy, string endAcademy, int maxStops);
        int NumberOfRoutesBetweenWithExactStops(string startAcademy, string endAcademy, int exacStops);
        int NumberOfRoutesBetweenWithMaxDistance(string startAcademy, string endAcademy, int maxDistance);
    }
}
