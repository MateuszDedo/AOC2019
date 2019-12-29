using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2019
{
    public class Day2 : Solution
    {
       IntcodeComputer computer = new IntcodeComputer();
        override public void part1()
        {
            computer.loadProgramInput(2);
            computer.createProgramFromInput();
            computer.executeProgram(12,2);
            System.Console.WriteLine("Day 2. Solution1 expected: 3716293");
            System.Console.WriteLine("Day 2. Solution1:" + computer.getComputerOutput(0).ToString());
        }

        override public void part2()
        {
            computer.loadProgramInput(2);
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    computer.createProgramFromInput();
                    computer.executeProgram(noun, verb);
                    if (computer.getComputerOutput(0) == 19690720)
                    {
                        System.Console.WriteLine("Day 2. Solution2 expected: 6429");
                        System.Console.WriteLine("Day 2. Solution2:" + (100 * noun + verb).ToString());
                        break;
                    }
                }
            }
            
        }
    }
}