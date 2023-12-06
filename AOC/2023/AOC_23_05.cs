using AOC.IO;
using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using AOC.Generics;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;
using AOC.Comparison.LookUpTable;

namespace AOC._2023
{
    static class AOC_23_05
    {
        //Cleaned up

        public static LongLookUpTableMultiple GetTable(List<List<string>> categories)
        {
            var tables = new List<LongLookUpTable>();
            for (int i = 1; i < categories.Count(); i++)
            {
                var ranges = new List<MappedPairsRange>();
                for (int j = 1; j < categories[i].Count; j++)
                {
                    var numbers = Converter.ToLong(categories[i][j].Split(' '));
                    ranges.Add(MappedPairsRange.GetRange(numbers[1], numbers[0], numbers[2]));
                }
                tables.Add(new LongLookUpTable(ranges));
            }

            return new LongLookUpTableMultiple(tables);
        }

        public static long Result_A()
        {
            List<List<string>> categories = InputHelper.ReadAllLinesSeperatedByEmptyLine();
            var lookUpHandler = GetTable(categories);

            var seeds = categories[0][0].Split(' ').Skip(1).Select(s => long.Parse(s));

            var locations = seeds.Select(S => lookUpHandler.GetTarget(S));

            return locations.Min();
        }

        public static long Result_B()
        {
            List<List<string>> categories = InputHelper.ReadAllLinesSeperatedByEmptyLine();
            var lookUpHandler = GetTable(categories);

            var seedCouples = categories[0][0].Split(' ').Skip(1).Select(s => long.Parse(s)).ToList();

            long location = 0;
            while (true)
            {
                if (location % 1000000 == 0)
                {
                    Console.WriteLine("*" + location);
                }

                var seed = lookUpHandler.GetSource(location);

                for (int i = 0; i < seedCouples.Count(); i = i + 2)
                {
                    if (seed >= seedCouples[i] && seed <= seedCouples[i] + seedCouples[i + 1])
                    {
                        return location;
                    }
                }

                location++;
            }

            return -1;
        }
    }

    static class AOC_23_05_Original
    {
        private class CoupleRange
        {
            internal long sourceMin;
            internal long targetMin;

            internal long sourceMax;
            internal long targetMax;

            internal CoupleRange (long sourceMin, long targetMin, long sourceMax, long targetMax)
            {
                this.sourceMin = sourceMin;
                this.targetMin = targetMin;
                this.sourceMax = sourceMax;
                this.targetMax = targetMax;
            }

            internal bool TryGetTarget(long source, out long target)
            {
                target = source + targetMin - sourceMin;

                return (source >= sourceMin && source <= sourceMax);
            }

            internal bool TryGetSource(long target, out long source)
            {
                source = target + sourceMin - targetMin;

                return source >= sourceMin && source <= sourceMax;
                    //&& target >= targetMin && target <= targetMax;
            }
        }

        private static List<CoupleRange> SeedToSoil;
        private static List<CoupleRange> SoilToFertilizer;
        private static List<CoupleRange> FertilizerToWater;
        private static List<CoupleRange> WaterToLight;
        private static List<CoupleRange> LightToTemperature;
        private static List<CoupleRange> TemperatureToHumidity;
        private static List<CoupleRange> HumiodityToLocation;

        private static long Target(List<CoupleRange> couples, long sourceStart)
        {
            foreach(var c in couples)
            {
                if(c.TryGetTarget(sourceStart, out long target))
                {
                    //Console.WriteLine(sourceStart + "=>"+target);
                    return target;
                }
            }

           // Console.WriteLine(sourceStart + "=>"+sourceStart);
            return sourceStart;
        }

        private static long Source(List<CoupleRange> couples, long targetStart)
        {
            foreach (var c in couples)
            {
                if (c.TryGetSource(targetStart, out long source))
                {
                    //Console.WriteLine(targetStart +"=>"+source);
                    return source;
                }
            }

            //Console.WriteLine(targetStart + "=>"+targetStart);
            return targetStart;
        }

        private static CoupleRange Couples(long sourceStart, long targetStart, long count)
        {
            return new CoupleRange(sourceStart, targetStart, sourceStart + count-1, targetStart + count-1);
        }
            
        private static void CreateList(ref List<CoupleRange> list , List<string> sourceData)
        {
            list= new List<CoupleRange>();
            for (int i = 1; i< sourceData.Count;i++)
            {
                var numbers = Converter.ToLong(sourceData[i].Split(' '));
                list.Add(Couples(numbers[1], numbers[0], numbers[2]));
            }
        }
        
        private static long SeedToLocation(long seed)
        {
            return Target(HumiodityToLocation,
                Target(TemperatureToHumidity,
                Target(LightToTemperature,
                Target(WaterToLight,
                Target(FertilizerToWater,
                Target(SoilToFertilizer,
                Target(SeedToSoil, seed)))))));
        }

        private static long LocationToSeed(long location)
        {
           // Console.WriteLine("loc " + location);
            return Source(SeedToSoil, 
                    Source(SoilToFertilizer,
                    Source(FertilizerToWater,
                    Source(WaterToLight,
                    Source(LightToTemperature,
                    Source(TemperatureToHumidity,
                    Source(HumiodityToLocation, location)))))));
        }

        public static long Result_A()
        {
            var categories = InputHelper.ReadAllLinesSeperatedByEmptyLine();
            CreateList(ref SeedToSoil, categories[1]);
            CreateList(ref SoilToFertilizer, categories[2]);
            CreateList(ref FertilizerToWater, categories[3]);
            CreateList(ref WaterToLight, categories[4]);
            CreateList(ref LightToTemperature, categories[5]);
            CreateList(ref TemperatureToHumidity, categories[6]);
            CreateList(ref HumiodityToLocation, categories[7]);

            var seeds = categories[0][0].Split(' ').Skip(1).Select(s => long.Parse(s));
            var locations = seeds.Select(S => SeedToLocation(S));

            return locations.Min();
        }

        public static long Result_B()
        {
            var categories = InputHelper.ReadAllLinesSeperatedByEmptyLine();
            CreateList(ref SeedToSoil, categories[1]);
            CreateList(ref SoilToFertilizer, categories[2]);
            CreateList(ref FertilizerToWater, categories[3]);
            CreateList(ref WaterToLight, categories[4]);
            CreateList(ref LightToTemperature, categories[5]);
            CreateList(ref TemperatureToHumidity, categories[6]);
            CreateList(ref HumiodityToLocation, categories[7]);



            var seedCouples = categories[0][0].Split(' ').Skip(1).Select(s => long.Parse(s)).ToList();

            long location = 0;
            while(true)
            {
                if(location % 1000000 ==0)
                {
                    Console.WriteLine("*"+location);
                }

                var seed = LocationToSeed(location);

                for (int i = 0; i < seedCouples.Count(); i = i + 2)
                {
                    if (seed >= seedCouples[i] && seed <= seedCouples[i] + seedCouples[i + 1])
                    {
                        return location;
                    }
                }
                
                location++;
            }


            return -1;
        }
    }
}
