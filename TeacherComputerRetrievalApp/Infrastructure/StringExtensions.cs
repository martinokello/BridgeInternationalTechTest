using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeacherComputerRetrievalApp.Infrastructure
{
    public static class StringExtensions
    {
        public static int UniqueCharacters(this string word)
        {
            var charList = new List<char>();
            foreach (var ch in word)
            {
                charList.Add(ch);
            }

            return charList.Union(new List<char>()).Count();
        }

        public static bool AlreadyContainsOccurencesOf(this string word, string str, int repetition = 1)
        {
            var pattern = GetPattern(repetition,str);
            return Regex.IsMatch(word, pattern);
        }

        public static string GetPattern(int repetition, string str)
        {
            var repeatString = $"(({str})+.*)" +"{"+$"{repetition}"+"}";
            return repeatString;
        }
        public static Dictionary<string, int> GetAllOtherRoutes(this string routeKey, Dictionary<string, int> route)
        {
            var dictRoutes = new Dictionary<string, int>();
            var distance = 0;
            if(routeKey.Length >= 6)
            {
                for (int n = 0; n < routeKey.Length; n += 2)
                {
                    if (n > 2)
                    {
                        for (int j = routeKey.Length - 2; j >= 0; j-=2)
                        {
                            var fromBeg = routeKey.Substring(0, n - 2);
                            var fromEnd = routeKey.Substring(j, 2);
                            if (fromEnd.StartsWith(fromBeg.Substring(1, 1)))
                            {
                                var key = fromBeg + routeKey.Substring(j);
                                distance = GetDistance(route, key);
                                if (!dictRoutes.ContainsKey(key))
                                {
                                    dictRoutes.Add(key, distance);
                                }
                            }
                        }
                    }
                }

            }
            return dictRoutes;
        }

        public static int GetDistance(Dictionary<string, int> route, string key)
        {
            var distance = 0;

            for(int n=0; n<key.Length - 1; n+=2)
            {
                var rKey = key.Substring(n, 2);
                distance += route[rKey];
            }
            return distance;
        }
    }
}
