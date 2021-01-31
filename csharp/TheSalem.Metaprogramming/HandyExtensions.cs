using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TheSalem.Metaprogramming
{
    public static class HandyExtensions
    {
        public static Dictionary<string, string> MatchNamedCaptures(this Regex regex, string input)
        {
            var namedCaptureDictionary = new Dictionary<string, string>();
            var groups = regex.Match(input).Groups;

            foreach (string groupName in regex.GetGroupNames())
                if (groups[groupName].Captures.Count > 0)
                    namedCaptureDictionary.Add(groupName, groups[groupName].Value);

            return namedCaptureDictionary;
        }
    }
}
