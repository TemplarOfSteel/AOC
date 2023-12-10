using AOC.IO;
using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Numerics;
using AOC.MathE;

namespace AOC._2023
{
    class AOC_23_08
    {
        public static int Result_A()
        {
            var directions = InputHelper.ReadAllLinesUntilEmpty();
            var nodesString = InputHelper.ReadAllLinesUntilEmpty();
            var lefts = new Dictionary<string, string>();
            var rights = new Dictionary<string, string>();
            foreach (var n in nodesString)
            {
                var data = n.Replace("(", "").Replace(")", "").Replace(",", "").Replace("=", "").Replace("  ", " ").Split(' ');
                lefts.Add(data[0], data[1]);
                rights.Add(data[0], data[2]);
            }

            int instructions = 0;
            string node = "AAA";
            while (true)
            {
                foreach (var d in directions[0].ToCharArray())
                {
                    if (d == 'L')
                    {
                        node = lefts[node];
                    }
                    else
                    {
                        node = rights[node];
                    }
                    instructions++;
                    if (node == "ZZZ")
                    {
                        return instructions;
                    }
                }
            }
        }

        public static long Result_B()
        {
            var directions = InputHelper.ReadAllLinesUntilEmpty();
            var instructionsPerInputLoop = directions[0].Length;
            var nodesString = InputHelper.ReadAllLinesUntilEmpty();
            var lefts = new Dictionary<string, string>();
            var rights = new Dictionary<string, string>();
            foreach (var n in nodesString)
            {
                var data = n.Replace("(", "").Replace(")", "").Replace(",", "").Replace("=", "").Replace("  ", " ").Split(' ');
                lefts.Add(data[0], data[1]);
                rights.Add(data[0], data[2]);
            }

            var loops = new List<int>();
            var nodes = nodesString.Select(s => s.Split(' ')[0]).Where(s => s.Last() == 'A').ToList();
            var loopsPerCycle = new List<long>();

            for (int n = 0; n < nodes.Count(); n++)
            {
                OutputHelper.Print("*" + nodes[n] + ": ");
                int instructions = 0;
                bool found = false; //loop self
                var loopNodes = new List<string>();
                var loopNodesStart = new List<int>();
                while (!found)
                {
                    if (loopNodes.Contains(nodes[n]))
                    {
                        var instructionsPerNodeCycle = instructions - loopNodesStart[loopNodes.FindIndex(s => s == nodes[n])];
                        var loopsForCycle = instructionsPerNodeCycle / instructionsPerInputLoop;
                        loopsPerCycle.Add(loopsForCycle);
                        loops.Add(instructionsPerNodeCycle);

                        found = true;
                        break;
                    }

                    loopNodes.Add(nodes[n]);
                    OutputHelper.Print("   *" + nodes[n] + ": " + instructions);
                    loopNodesStart.Add(instructions);
                    foreach (var d in directions[0].ToCharArray())
                    {
                        if (d == 'L')
                        {
                            nodes[n] = lefts[nodes[n]];
                        }
                        else
                        {
                            nodes[n] = rights[nodes[n]];
                        }
                        instructions++;
                    }
                }
            }

            long divisor = MathLong.LeastCommonMultiple(loopsPerCycle.ToArray());

            return divisor * instructionsPerInputLoop;
        }
    }

    class AOC_23_08_Old
    {
        public static int Result_A()
        {
            var directions = InputHelper.ReadAllLinesUntilEmpty();
            var nodesString = InputHelper.ReadAllLinesUntilEmpty();
            var lefts = new Dictionary<string, string>();
            var rights = new Dictionary<string, string>();
            foreach(var n in nodesString)
            {
                var data = n.Replace("(", "").Replace(")", "").Replace(",", "").Replace("=", "").Replace("  ", " ").Split(' ');
                lefts.Add(data[0], data[1]);
                rights.Add(data[0], data[2]);
            }

            int instructions = 0;
            string node = "AAA";
            while (true)
            {
                foreach(var d in directions[0].ToCharArray())
                {
                    if(d=='L')
                    {
                        node = lefts[node];
                    }
                    else
                    {
                        node = rights[node];
                    }
                    instructions++;
                    if(node=="ZZZ")
                    {
                        return instructions;
                    }
                }
            }
        }

        static string Node(List<string> directions, string inputNode, Dictionary<string, string> lefts, Dictionary<string, string> rights, long insts)
        {
            var i = 0;
            while (i < insts)
            {
                foreach (var d in directions[0].ToCharArray())
                {
                    if (d == 'L')
                    {
                        inputNode = lefts[inputNode];
                    }
                    else
                    {
                        inputNode = rights[inputNode];
                    }
                    i++;
                    if(i>=insts)
                    { break; }
                }
            }
            return inputNode;
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            if(a == b) { return a; }
            long num1;
            long num2;
            if(a>b)
            {
                num1 = a;
                num2 = b;
            }
            else
            {
                num1 = b;
                num2 = a;
            }

            for(long i = 1;  i<num2;i++)
            {
                if((i*num1)%num2==0)
                {
                    return i * num1;
                }
            }
            return num2 * num1;
        }

        public static long Result_B()
        {
            var directions = InputHelper.ReadAllLinesUntilEmpty();
            var instructionsPerInputLoop = directions[0].Length;
            var nodesString = InputHelper.ReadAllLinesUntilEmpty();
            var lefts = new Dictionary<string, string>();
            var rights = new Dictionary<string, string>();
            foreach (var n in nodesString)
            {
                var data = n.Replace("(", "").Replace(")", "").Replace(",", "").Replace("=", "").Replace("  ", " ").Split(' ');
                lefts.Add(data[0], data[1]);
                rights.Add(data[0], data[2]);
            }

            List<int> loops = new List<int>();
            List<string> nodes = nodesString.Select(s => s.Split(' ')[0]).Where(s => s.Last() == 'A').ToList();
            List<List<int>> validInstructions = new List<List<int>>();
            var loopsPerCycle = new List<int>();
            for (int n = 0; n < nodes.Count(); n++)
            {
                validInstructions.Add(new List<int>());
            }

            for (int n = 0; n < nodes.Count(); n++)
            {
                OutputHelper.Print("*" + nodes[n] + ": ");
                int instructions = 0;
                bool found = false;//loop self
                var loopNodes = new List<string>();
                var loopNodesStart = new List<int>();
                while (!found)
                {
                    if (loopNodes.Contains(nodes[n]))
                    {
                        var instructionsPerNodeCycle = instructions - loopNodesStart[loopNodes.FindIndex(s => s == nodes[n])];
                        var loopsForCycle = instructionsPerNodeCycle / instructionsPerInputLoop;
                        loopsPerCycle.Add(loopsForCycle);
                        loops.Add(instructionsPerNodeCycle);

                        OutputHelper.Print("===");
                        OutputHelper.Print(nameof(instructionsPerNodeCycle) + ": " + instructionsPerNodeCycle);
                        OutputHelper.Print(nameof(loopsForCycle) + ": " + loopsForCycle);
                        OutputHelper.Print(nameof(instructions) + ": " + instructions);
                        OutputHelper.Print("Start at " + loopNodesStart[loopNodes.FindIndex(s => s == nodes[n])]);
                        OutputHelper.Print(nodes[n] + " => " + Node(directions, nodes[n], lefts, rights, instructionsPerNodeCycle));
                        OutputHelper.Print("===");

                        found = true;
                        break;
                    }

                    loopNodes.Add(nodes[n]);
                    OutputHelper.Print("   *" + nodes[n] + ": " + instructions);
                    loopNodesStart.Add(instructions);
                    foreach (var d in directions[0].ToCharArray())
                    {
                        if (d == 'L')
                        {
                            nodes[n] = lefts[nodes[n]];
                        }
                        else
                        {
                            nodes[n] = rights[nodes[n]];
                        }
                        instructions++;
                        if (nodes[n].Last()=='Z')
                        {
                            if(instructions% instructionsPerInputLoop != 0)
                            {
                                var breakHere = "Appearently none of these cases with valid node which is not at cycle start";
                            }
                            validInstructions[n].Add(instructions);
                            OutputHelper.Print(validInstructions[n].Count() + ": " + nodes[n] + " - " + instructions);
                        }
                    }
                }
            }

            long divisor = 1;
            foreach(var l in loopsPerCycle)
            {
                divisor = LeastCommonMultiple(divisor, l);
            }

            return divisor * instructionsPerInputLoop; //When reading the output it was found that both the start and end nodes (last char a/z) alll are on position 0 of a input loop
        }
    }
}
