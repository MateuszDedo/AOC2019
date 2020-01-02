using System;
using System.Collections.Generic;
using System.Threading;
namespace AOC2019
{
    public class Day13 : Solution
    {
        override public void part1()
        {
            Dictionary<string, long> board = new Dictionary<string, long>();
            IntcodeComputer computer = new IntcodeComputer();
            computer.loadProgramInput(13);
            computer.createProgramFromInput();
            while (!computer.hasStopped)
            {
                long x = computer.executeProgram(null, RunMode.BREAK_ON_OUTPUT);
                long y = computer.executeProgram(null, RunMode.BREAK_ON_OUTPUT);
                long tileId = computer.executeProgram(null, RunMode.BREAK_ON_OUTPUT);
                tileId = computer.getDiagnosticCode();
                if (!board.ContainsKey(x.ToString() + ',' + y.ToString())) board.Add(x.ToString() + ',' + y.ToString(), tileId);
                else board[x.ToString() + ',' + y.ToString()] = tileId;
            } 
            int result = 0;  
            foreach (int i in board.Values) if (i == 2) result++;
            System.Console.WriteLine("Day 13. Solution1:" + result.ToString());
        }

        override public void part2()
        {
            Dictionary<string, long> board = new Dictionary<string, long>();
            IntcodeComputer computer = new IntcodeComputer();
            computer.loadProgramInput(13);
            computer.createProgramFromInput();
            computer.saveInputAtPosition(0, 2);
            List <int> input = new List<int>();
            long paddleX = -1;
            long ballX = -1;
            long score = 0;
            while (!computer.hasStopped)
            {
                int joystick = 0;
                if (ballX > paddleX && ballX >= 0 && paddleX >= 0) joystick = 1;
                if (ballX < paddleX && ballX >= 0 && paddleX >= 0) joystick = -1;
                input.Add(joystick);
                long x = computer.executeProgram(input, RunMode.BREAK_ON_OUTPUT);
                long y = computer.executeProgram(null, RunMode.BREAK_ON_OUTPUT);
                long tileId = computer.executeProgram(null, RunMode.BREAK_ON_OUTPUT);
                if (!board.ContainsKey(x.ToString() + ',' + y.ToString())) board.Add(x.ToString() + ',' + y.ToString(), tileId);
                else board[x.ToString() + ',' + y.ToString()] = tileId;
                if (x == -1 && y == 0) score = tileId;
                if (tileId == 3)  paddleX = x;
                if (tileId == 4)  ballX = x;
                input = new List<int>{0};
            } 
            System.Console.WriteLine("Day 13. Solution2:" + score.ToString());
        }
    }
}