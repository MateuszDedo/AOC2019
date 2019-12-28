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
            }
            return null;
        }
    }
}