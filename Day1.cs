namespace AOC2019
{
    public class Day1 : Solution
    {
        private int requiredFuel(int fuel)
        {
            return fuel / 3 - 2;
        }
        
        override public void part1()
        {
            int sum = 0;
            new InputLoader(1, InputType.ALL).inputIntList.ForEach(s => sum += requiredFuel(s));
            System.Console.WriteLine("Day 1. Solution1:" + sum.ToString());
        }

        private int calculate(int fuel)
        {
            if (requiredFuel(fuel) <= 0) return 0;
            return requiredFuel(fuel) + (calculate(requiredFuel(fuel)));
        }

        override public void part2()
        {
            int sum = 0;
            new InputLoader(1, InputType.ALL).inputIntList.ForEach(s => sum += calculate(s));
            System.Console.WriteLine("Day 1. Solution2:" + sum.ToString());
        }
    }
}