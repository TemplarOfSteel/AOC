using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Convertion
{
    internal static class StringOperations
    {
        internal static List<string> MatchesInOrder(string source, IEnumerable<string> matches)
        {
            var ret = new List<string>();

            for(int i = 0;i<source.Length;i++)
            {
                var match = MatchAtStart(string.Concat( source.Skip(i)), matches);
                if(match != null) { ret.Add(match); }
            }

            return ret;
        }

        internal static string MatchAtStart(string source, IEnumerable<string> matches)
        {
            foreach(var match in matches)
            {
                if(MatchesAtStart(source, match)) { return match; }
            }

            return null;
        }

        internal static bool MatchesAtStart(string source, string match)
        {
            if (source.Count() < match.Count()) { return false; }

            string sourceTryMatch = string.Concat( source.Take(match.Length));
            var doesMatch = sourceTryMatch ==  match;

            return doesMatch;
        }

        internal static string Combine(IEnumerable<string> strings, string seperator)
        {
            var ret = "";
            bool first = true;
            foreach(var s in strings)
            {
                if (first) { first = false; }
                else { ret += seperator; }
                ret += s;
            }

            return ret;
        }

        internal static string Replace(string source, Dictionary<string, string> mappings)
        {
            foreach(var mapping in mappings)
            {
                source = source.Replace(mapping.Key, mapping.Value);
            }
            return source;
        }

        internal static string Replace(string source, Dictionary<char, char> mappings)
        {
            foreach (var mapping in mappings)
            {
                source = source.Replace(mapping.Key, mapping.Value);
            }
            return source;
        }
    }
}
