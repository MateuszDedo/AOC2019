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

    public enum RunMode
    {
        NORMAL,
        BREAK_ON_OUTPUT
    }

    public class IntcodeComputer
    {
        private List<int> programInt;
        private List<string> program;
        private ParameterMode param1Mode;
        private ParameterMode param2Mode;
        private int position = 0;
        private List<int> systemId = new List<int>();
        private int diagnosticCode = 0;
        private int systemIdNumber = 0;
        private bool isRunning = false;
        private RunMode runMode = RunMode.NORMAL;
        public void loadProgramInput(UInt32 day)
        {
            InputLoader il = new InputLoader(day);
            program = il.inputString.Split(',').ToList();
        }
        public void createProgramFromInput()
        {
            position = 0;
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
        public bool hasStopped = false;

        delegate bool OpcodeDelegate();
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
        private bool handleUnknownOpcode()
        {
            return true;
        }
        private bool handleOpcode1()
        {
            int outputPosition = programInt[position + 3];
            programInt[outputPosition] = programInt[getParamIndex(position + 1, param1Mode)] + 
                                            programInt[getParamIndex(position + 2, param2Mode)];
            position += 4;
            return true;
        }

        private bool handleOpcode2()
        {
            int outputPosition = programInt[position + 3];
            programInt[outputPosition] = programInt[getParamIndex(position + 1, param1Mode)] * 
                                            programInt[getParamIndex(position + 2, param2Mode)];
            position += 4;
            return true;
        }

        private bool handleOpcode3()
        {
            saveInputAtPosition(getParamIndex(position + 1), systemId[systemIdNumber]);
            position += 2;
            systemIdNumber++;
            return true;
        }

        private bool handleOpcode4()
        {
            diagnosticCode = programInt[getParamIndex(position + 1)];
            position += 2;
            if (runMode == RunMode.NORMAL) return true;
            return false;
        }

        private bool handleOpcode5()
        {
            if (programInt[getParamIndex(position + 1, param1Mode)] != 0) position = programInt[getParamIndex(position + 2, param2Mode)];
            else position += 3;
            return true;
        }

        private bool handleOpcode6()
        {
            if (programInt[getParamIndex(position + 1, param1Mode)] == 0) position = programInt[getParamIndex(position + 2, param2Mode)];
            else position += 3;
            return true;
        }

        private bool handleOpcode7()
        {
            int outputPosition = programInt[position + 3];
            if (programInt[getParamIndex(position + 1, param1Mode)] < programInt[getParamIndex(position + 2, param2Mode)]) 
                programInt[outputPosition] = 1;
            else 
                programInt[outputPosition] = 0;
            position += 4;
            return true;
        }

        private bool handleOpcode8()
        {
            int outputPosition = programInt[position + 3];
            if (programInt[getParamIndex(position + 1, param1Mode)] == programInt[getParamIndex(position + 2, param2Mode)]) 
                programInt[outputPosition] = 1;
            else 
                programInt[outputPosition] = 0;
            position += 4;
            return true;
        }

        public void executeProgram(List<int> input, RunMode runMode = RunMode.NORMAL)
        {
            this.runMode = runMode;
            systemId = input;

            if (isRunning)
                systemIdNumber = 1;
            else
                systemIdNumber = 0;
            
            isRunning = true;
            
            while (programInt[position] != 99)
                if ((bool)(opcodeHandlers[getOpcodeAndSaveParametersMode(position)].DynamicInvoke()) == false) return;
            hasStopped = true;
        }
        public void executeProgram(int param1, int param2)
        {
            programInt[1] = param1;
            programInt[2] = param2;
            List<int> l = new List<int>();
            executeProgram(l);
        }
    }
}