using AOC.IO;
using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace AOC._2023
{
    static class AOC_23_07
    {
        private class Hand
        {
            public List<char> cards;
            public int bid;
            public int rank;
            private bool sortByA;

            Dictionary<char, int> cardRanksA = new Dictionary<char, int>
            {
                { 'A', 13 },
                { 'K', 12 },
                { 'Q', 11 },
                { 'J', 10 },
                { 'T', 9 },
                { '9', 8 },
                { '8', 7 },
                { '7', 6 },
                { '6', 5 },
                { '5', 4 },
                { '4', 3 },
                { '3', 2 },
                { '2', 1 }
            };

            Dictionary<char, int> cardRanksB = new Dictionary<char, int>
            {
                { 'A', 13 },
                { 'K', 12 },
                { 'Q', 11 },
                { 'J', 0 },
                { 'T', 9 },
                { '9', 8 },
                { '8', 7 },
                { '7', 6 },
                { '6', 5 },
                { '5', 4 },
                { '4', 3 },
                { '3', 2 },
                { '2', 1 }
            };

            public Hand(string cards, int bid, bool sortByA)
            {
                this.cards = cards.ToList();
                this.bid = bid;
                this.sortByA = sortByA;
            }


            public int HigherThan(Hand other)
            {
                return sortByA ? A_HigherThan(other) : B_HigherThan(other);
            }

            public int A_HigherThan(Hand other)
            {
                var thisT = A_TypeRank();
                var otherT = other.A_TypeRank();
                if (thisT != otherT) return otherT - thisT;

                for(int i = 0; i < 5; i++)
                {
                    if (cardRanksA[cards[i]] != cardRanksA[other.cards[i]]) 
                    { 
                        return cardRanksA[other.cards[i]] - cardRanksA[cards[i]]; 
                    }
                }

                return 0;
            }

            public int B_HigherThan(Hand other)
            {
                var thisT = B_TypeRank();
                var otherT = other.B_TypeRank();
                if (thisT != otherT) return otherT - thisT;

                for (int i = 0; i < 5; i++)
                {
                    if (cardRanksB[cards[i]] != cardRanksB[other.cards[i]])
                    {
                        return cardRanksB[other.cards[i]] - cardRanksB[cards[i]]; 
                    }
                }

                return 0;
            }


            int A_TypeRank()
            {
                return TypeRankBasedOnJoker('J');
            }

            int B_TypeRank()
            {
                var points = new List<int>();
                foreach (var card in cards.Distinct())
                {
                    points.Add(TypeRankBasedOnJoker(card));
                }

                return points.Max();

                throw new Exception();
            }

            int TypeRankBasedOnJoker(char jIs)
            {
                var cardsJ = cards.Select(c => (c == 'J') ? jIs : c).ToList();
                var count = new List<int>();
                foreach (var card in cardsJ.Distinct())
                {
                    var same = 0;
                    foreach (var card2 in cardsJ)
                    {
                        if (card2 == card)
                        {
                            same += 1;
                        }
                    }
                    count.Add(same);
                }

                if (count.Contains(5)) { return 7; }

                if (count.Contains(4)) { return 6; }

                if ((count.Contains(3) && count.Contains(2)) ||
                    count.Where(c => c == 3).Count() > 1) {return 5;}

                if (count.Contains(3)) { return 4; }

                if (count.Where(c => c == 2).Count() > 1) {return 3;}
                
                if (count.Contains(2)) { return 2; }

                return 1;
            }
        }

        public static int WinningsBasedOnSort(bool sortByA)
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var hands = lines.Select(l => new Hand(l.Split(' ')[0], Converter.ToInt(l.Split(' ')[1]), sortByA)).ToList();
            hands.Sort((a, b) => b.HigherThan(a));

            var winnings = 0;
            for (int h = 0; h < hands.Count(); h++)
            {
                winnings += (h + 1) * hands[h].bid;
            }

            return winnings;
        }

        public static int Result_A() => WinningsBasedOnSort(true);

        public static int Result_B() => WinningsBasedOnSort(false);
    }
}
