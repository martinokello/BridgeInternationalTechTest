using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherComputerRetrievalApp.Interfaces;

namespace TeacherComputerRetrievalApp.Concretes
{
    public class JourneyCalculator
    {
        IRouteTable _routeTable;

        public JourneyCalculator(IRouteTable routeTable)
        {
            _routeTable = routeTable;
        }
        
        
        public int NumberOfRoutesBetweenWithMaxDistance(string startAcademy, string endAcademy, int maxDistance)
        {
            return _routeTable.NumberOfRoutesBetweenWithMaxDistance(startAcademy, endAcademy, maxDistance);
        }
    }
}
