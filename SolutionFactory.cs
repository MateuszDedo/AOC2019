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
            }
            return null;
        }
    }
}