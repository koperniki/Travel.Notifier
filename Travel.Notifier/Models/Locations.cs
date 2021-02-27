using System.Collections.Generic;
using System.Diagnostics;

namespace Travel.Notifier.Models
{
    public static class Locations
    {
        private static Dictionary<int, string> _locations = new Dictionary<int, string>()
        {
            {1, "Бухгалтерия"},
            {2, "112"},
            {3, "113"},
            {4, "114-тестеры"},
            {5, "103-продажники"},
            {6, "103-гробик"},
        };

        public static string GetLocation(int number)
        {
            if (_locations.ContainsKey(number))
            {
                return _locations[number];
            }

            return "unknown location";
        }
    }
}