using System;

namespace AOC2019
{
    internal class SolutionFactory
    {
        internal static Solution getSolution(UInt32 day)
        {
            switch(day)
            {
                case 1:
                    return new Day1();
                case 2:
                    return new Day2();
                case 3:
                    return new Day3();
                case 4:
                    return new Day4();
                case 5:
                    return new Day5();
                case 6:
                    return new Day6();
                case 7:
                    return new Day7();
                case 8:
                    return new Day8();
                case 9:
                    return new Day9();
                case 10:
                    return new Day10();
            }
            return null;
        }
    }
}