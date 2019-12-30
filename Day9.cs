using System.Collections.Generic;

namespace AOC2019
{
    public class Day9 : Solution
    {
         override public void part1()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.loadProgramInput(9);
            computer.createProgramFromInput();
            computer.executeProgram(new List<int>{1});
            long local = computer.getDiagnosticCode();
            System.Console.WriteLine("Day 9. Solution1:" + local.ToString());
        }


        override public void part2()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.loadProgramInput(9);
            computer.createProgramFromInput();
            computer.executeProgram(new List<int>{2});
            long local = computer.getDiagnosticCode();
            System.Console.WriteLine("Day 9. Solution2:" + local.ToString());
        }
    }
}