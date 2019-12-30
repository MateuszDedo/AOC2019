using System.Collections.Generic;

namespace AOC2019
{
    public class Day5 : Solution
    {
        override public void part1()
        {
            IntcodeComputer computer = new IntcodeComputer();
            List<int> l = new List<int>();
            l.Add(1);
            computer.loadProgramInput(5);
            computer.createProgramFromInput();
            computer.executeProgram(l);
            System.Console.WriteLine("Day 5. Solution1 expected: 12234644");
            System.Console.WriteLine("Day 5. Solution1:" + computer.getDiagnosticCode().ToString());
        }

        override public void part2()
        {
            IntcodeComputer computer = new IntcodeComputer();
            List<int> l = new List<int>();
            l.Add(5);
            computer.loadProgramInput(5);
            computer.createProgramFromInput();
            computer.executeProgram(l);
            System.Console.WriteLine("Day 5. Solution2 expected: 3508186");
            System.Console.WriteLine("Day 5. Solution2:" + computer.getDiagnosticCode().ToString());
        }
    }
}