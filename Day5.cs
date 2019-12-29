namespace AOC2019
{
    public class Day5 : Solution
    {
        IntcodeComputer computer = new IntcodeComputer();
        override public void part1()
        {
            computer.loadProgramInput(5);
            computer.createProgramFromInput();
            computer.executeProgram(1);
            System.Console.WriteLine("Day 5. Solution1 expected: 12234644");
            System.Console.WriteLine("Day 5. Solution1:" + computer.getDiagnosticCode().ToString());
        }

        override public void part2()
        {
            computer.loadProgramInput(5);
            computer.createProgramFromInput();
            computer.executeProgram(5);
            System.Console.WriteLine("Day 5. Solution2 expected: 3508186");
            System.Console.WriteLine("Day 5. Solution2:" + computer.getDiagnosticCode().ToString());
        }
    }
}