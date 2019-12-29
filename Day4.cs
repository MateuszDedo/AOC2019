using System;
using System.Linq;

namespace AOC2019
{
    public class Day4 : Solution
    {
        private const int PasswordStart = 108457;
        private const int PasswordEnd = 562041;

        override public void part1()
        {
            int a = 0;
            for (int i = PasswordStart; i < PasswordEnd; i++)
                if (doDigitsOnlyIncrease(i) && doDigitsContainsAtLeastOneSameAdjacentNumbers(i)) a++;
            System.Console.WriteLine("Day 4. Solution1:" + a.ToString());
        }

        override public void part2()
        {
             int a = 0;
            for (int i = PasswordStart; i < PasswordEnd; i++)
                if (doDigitsOnlyIncrease(i) && doDigitsContainsAtOnlyOneSameAdjacentNumbers(i)) a++;

            System.Console.WriteLine("Day 4. Solution2:" + a.ToString());
        }

        private bool doDigitsOnlyIncrease(int number)
        {
           return number == Convert.ToInt32(String.Concat(number.ToString().ToCharArray().OrderBy(c => c)));
        }

        private bool doDigitsContainsAtLeastOneSameAdjacentNumbers(int num)
        {
            string s = num.ToString();
            int number = System.Convert.ToInt16(s[0]);
     
            for (int x = 1 ; x < 6; x++)
            {
                if (number == System.Convert.ToInt16(s[x])) return true;
                else number = System.Convert.ToInt16(s[x]);
            }
            return false;
        }

        private bool doDigitsContainsAtOnlyOneSameAdjacentNumbers(int num)
        {
            string s = num.ToString();
            int number = System.Convert.ToInt16(s[0]);
            int counter = 0;
            for (int x = 1 ; x < 6; x++)
                if (number == System.Convert.ToInt16(s[x])) counter++;
                else
                {
                    if (counter == 1) return true;
                    number = System.Convert.ToInt16(s[x]);
                    counter = 0;
                }
            return counter == 1;
        }
        
    }
}