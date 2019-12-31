using System.Collections.Generic;
using System.IO;
namespace AOC2019
{
    public class Day11 : Solution
    {
        private int currentPositionX = 0;
        private int currentPositionY = 0;
        private Direction direction;
        private Dictionary<string, char> visitedPanels;
        private IntcodeComputer computer;
        private List<int> input;
        override public void part1()
        {
            initialize();
            doPainting();
            System.Console.WriteLine("Day 11. Solution1:" + visitedPanels.Count.ToString());
        }
        override public void part2()
        {
            initialize();
            visitedPanels.Add(currentPositionX.ToString() + "," + currentPositionY.ToString(), '#');
            doPainting();
            displayPaintedPanels();
        }
        private void doPainting()
        {
            while (!computer.hasStopped)
            {
                input.Add(getInput());
                computer.executeProgram(input, RunMode.BREAK_ON_OUTPUT);
                paintVisitedPanel(computer.getDiagnosticCode());
                computer.executeProgram(input, RunMode.BREAK_ON_OUTPUT);
                changeDirection(computer.getDiagnosticCode());
                changePosition();
                input = new List<int>{0};
            }
        }
        private void initialize()
        {
            currentPositionX = 0;
            currentPositionY = 0;
            direction = Direction.N;
            visitedPanels = new Dictionary<string, char>();
            computer = new IntcodeComputer();
            computer.loadProgramInput(11);
            computer.createProgramFromInput();
            input = new List<int>();
        }
        private void changePosition()
        {
            if (direction == Direction.N) currentPositionY++;
            if (direction == Direction.S) currentPositionY--;
            if (direction == Direction.W) currentPositionX++;
            if (direction == Direction.E) currentPositionX--;
        }
        private int getInput()
        {
            if (!visitedPanels.ContainsKey(currentPositionX.ToString() + "," + currentPositionY.ToString())) return 0;
            else if (visitedPanels.GetValueOrDefault(currentPositionX.ToString() + "," + currentPositionY.ToString()) == '.') return 0; else return 1;
        }
        private void changeDirection(long turn)
        {
            if (turn == 0) 
            {
                if (--direction < Direction.N) direction = Direction.E;
            }
            else 
            {
                if (++direction > Direction.E) direction = Direction.N;
            }
        }
        private void paintVisitedPanel(long color)
        {
            char s = ' ';
            if (color == 0) s = '.'; else s = '#';
            if (!visitedPanels.ContainsKey(currentPositionX.ToString() + "," + currentPositionY.ToString())) visitedPanels.Add(currentPositionX.ToString() + "," + currentPositionY.ToString(), s);
            else visitedPanels[currentPositionX.ToString() + "," + currentPositionY.ToString()] = s;
        }
        private void displayPaintedPanels()
        {
            for (int b = 0; b > -8; b--)
            {
                for (int a = 0; a < 45; a++)
                {
                    if (visitedPanels.ContainsKey(a.ToString() + "," + b.ToString()))
                    {
                        if (visitedPanels.GetValueOrDefault(a.ToString() + "," + b.ToString()) == '#')  System.Console.Write("X");
                        else System.Console.Write(" ");
                    }
                    else System.Console.Write(" ");
                }
                System.Console.WriteLine();
            }
        }
    }
}