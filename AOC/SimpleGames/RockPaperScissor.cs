using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.SimpleGames
{
    internal static class RockPaperScissor
    {
        private static Dictionary<char, char> WinningChoices = new Dictionary<char, char>
        {
            {'R', 'S' },
            {'P', 'R' },
            {'S', 'P' }
        };

        private static Dictionary<char, char> LosingChoices => WinningChoices.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        internal static char Result(char self, char other)
        {
            if (self == other) { return 'T'; }

            if (WinningChoices[self] == other) { return 'W'; }

            return 'L';
        }

        internal static char ChoiceSelf(char other, char result)
        {
            switch (result)
            {
                case 'W':
                    return LosingChoices[other];
                case 'L':
                    return WinningChoices[other];
                case 'T':
                    return other;
            }

            throw new Exception("Invalid char");
        }
    }
}
