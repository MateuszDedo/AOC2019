using System;
using System.Collections.Generic;

namespace AOC2019
{
    public class Day7 : Solution
    {
        override public void part1()
        {
            List<string> numberPermutations = StringUtils.permute("01234",0, 4);
            int result = 0;
            foreach (string perm in numberPermutations)
            {
                int local = 0;
                List<int> phases = createPhases(perm);
                for (int i = 0; i < 5; i++)
                {
                    IntcodeComputer computer = new IntcodeComputer();
                    computer.loadProgramInput(7);
                    computer.createProgramFromInput();
                    computer.executeProgram(new List<int>{phases[i], local});
                    local = (int)computer.getDiagnosticCode();
                }
                if (local > result) result = local;
            }
            System.Console.WriteLine("Day 7. Solution1 Expected:277328");
            System.Console.WriteLine("Day 7. Solution1:" + result.ToString());
        }
        override public void part2()
        {      
            List<string> numberPermutations = StringUtils.permute("56789",0, 4);
            int result = 0;
            foreach (string perm in numberPermutations)
            {
                List<IntcodeComputer> computers = createComputers();
                int local = 0;
                List<int> phases = createPhases(perm);
                while (!computers[4].hasStopped)
                    for (int i = 0; i < 5; i++)
                    {
                        computers[i].executeProgram(new List<int>{phases[i], local}, RunMode.BREAK_ON_OUTPUT);
                        local = (int)computers[i].getDiagnosticCode();
                    }
                if (local > result) result = local;
            }
            System.Console.WriteLine("Day 7. Solution2 Expected: 11304734");
            System.Console.WriteLine("Day 7. Solution2:" + result.ToString());
        }
        private List<IntcodeComputer> createComputers()
        {
            List<IntcodeComputer> computers = new List<IntcodeComputer>();
            for (int i = 0; i < 5; i++) computers.Add(new IntcodeComputer());
            computers.ForEach(c => {c.loadProgramInput(7); c.createProgramFromInput();});
            return computers;
        }
        private List<int> createPhases(string perm)
        {
            List<int> phases = new List<int>();
            for (int i = 0; i < 5; i++) phases.Add(Convert.ToInt32(perm[i].ToString()));
            return phases;
        }
    }
}