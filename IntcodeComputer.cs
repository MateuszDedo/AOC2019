using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2019
{
    public class IntcodeComputer
    {
        private List<int> programInt;
        private List<string> program;
        public void loadProgramInput(UInt32 day)
        {
            InputLoader il = new InputLoader(day);
            program = il.inputString.Split(',').ToList();
        }
        public void createProgramFromInput()
        {
            programInt = new List<int>();
            program.ForEach(s => programInt.Add(Convert.ToInt32(s)));
        }

        public int getComputerOutput(int idx)
        {
            return programInt[idx];
        }

        public void executeProgram(int param1, int param2)
        {
            programInt[1] = param1;
            programInt[2] = param2;

            int position = 0;
            while (programInt[position] != 99)
            {
                int opcode = programInt[position];
                int idx1 = programInt[position + 1];
                int idx2 = programInt[position + 2];
                int outputPosition = programInt[position + 3];

                if (opcode == 1)
                {
                    programInt[outputPosition] = programInt[idx1] + programInt[idx2];
                }
                if (opcode == 2)
                {
                    programInt[outputPosition] = programInt[idx1] * programInt[idx2];
                }
                position += 4;
            }
        }
    }
}