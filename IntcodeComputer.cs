using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2019
{
    enum ParameterMode
    {
        POSITION_MODE,
        IMMEDIATE_MODE
    }
    public class IntcodeComputer
    {
        private List<int> programInt;
        private List<string> program;
        private ParameterMode param1Mode;
        private ParameterMode param2Mode;
        private int position = 0;
        private int systemId = 0;
        private int diagnosticCode = 0;
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

        public int getDiagnosticCode()
        {
            return diagnosticCode;
        }
        private int getOpcodeAndSaveParametersMode(int position)
        {
            string tmp = programInt[position].ToString();
            while (tmp.Length < 5)
            {
                tmp = '0' + tmp;
            }
            if (tmp[2] == '0') param1Mode = ParameterMode.POSITION_MODE;
            else param1Mode = ParameterMode.IMMEDIATE_MODE;
        
            if (tmp[1] == '0') param2Mode = ParameterMode.POSITION_MODE;
            else param2Mode = ParameterMode.IMMEDIATE_MODE;

            return Convert.ToInt32(tmp[4].ToString()) + Convert.ToInt32(tmp[3].ToString()) * 10;
        }
        private void saveInputAtPosition(int position, int value)
        {
            programInt[position] = value;
        }
        private int getParamIndex(int position, ParameterMode pm = ParameterMode.POSITION_MODE)
        {
            int idx = programInt[position];
            if (pm == ParameterMode.IMMEDIATE_MODE) idx = position;
            return idx;
        }
        List<Delegate> opcodeHandlers = new List<Delegate>();
        delegate void OpcodeDelegate();
        public IntcodeComputer()
        {
            opcodeHandlers.Add(new OpcodeDelegate(handleUnknownOpcode));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode1));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode2));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode3));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode4));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode5));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode6));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode7));
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode8));
        }
        private void handleUnknownOpcode()
        {
        }
        private void handleOpcode1()
        {
            int outputPosition = programInt[position + 3];
            programInt[outputPosition] = programInt[getParamIndex(position + 1, param1Mode)] + 
                                            programInt[getParamIndex(position + 2, param2Mode)];
            position += 4;
        }

        private void handleOpcode2()
        {
            int outputPosition = programInt[position + 3];
            programInt[outputPosition] = programInt[getParamIndex(position + 1, param1Mode)] * 
                                            programInt[getParamIndex(position + 2, param2Mode)];
            position += 4;
        }

        private void handleOpcode3()
        {
            saveInputAtPosition(getParamIndex(position + 1), systemId);
            position += 2;
        }

        private void handleOpcode4()
        {
            diagnosticCode = programInt[getParamIndex(position + 1)];
            position += 2;
        }

        private void handleOpcode5()
        {
            if (programInt[getParamIndex(position + 1, param1Mode)] != 0) position = programInt[getParamIndex(position + 2, param2Mode)];
            else position += 3;
        }

        private void handleOpcode6()
        {
            if (programInt[getParamIndex(position + 1, param1Mode)] == 0) position = programInt[getParamIndex(position + 2, param2Mode)];
            else position += 3;
        }

        private void handleOpcode7()
        {
            int outputPosition = programInt[position + 3];
            if (programInt[getParamIndex(position + 1, param1Mode)] < programInt[getParamIndex(position + 2, param2Mode)]) 
                programInt[outputPosition] = 1;
            else 
                programInt[outputPosition] = 0;
            position += 4;
        }

        private void handleOpcode8()
        {
            int outputPosition = programInt[position + 3];
            if (programInt[getParamIndex(position + 1, param1Mode)] == programInt[getParamIndex(position + 2, param2Mode)]) 
                programInt[outputPosition] = 1;
            else 
                programInt[outputPosition] = 0;
            position += 4;
        }

        public void executeProgram(int input)
        {
            position = 0;
            systemId = input;
            while (programInt[position] != 99)
                opcodeHandlers[getOpcodeAndSaveParametersMode(position)].DynamicInvoke();
        }
        public void executeProgram(int param1, int param2)
        {
            programInt[1] = param1;
            programInt[2] = param2;
            executeProgram(0);
        }
    }
}