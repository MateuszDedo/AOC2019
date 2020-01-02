using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2019
{
    enum ParameterMode
    {
        POSITION_MODE,
        IMMEDIATE_MODE,
        RELATIVE_MODE
    }

    public enum RunMode
    {
        NORMAL,
        BREAK_ON_OUTPUT
    }

    public class IntcodeComputer
    {
        private List<long> programLong;
        private List<string> program;
        private ParameterMode param1Mode;
        private ParameterMode param2Mode;
        private ParameterMode param3Mode;
        private int position = 0;
        private List<int> systemId = new List<int>();
        private long diagnosticCode = 0;
        private int systemIdNumber = 0;
        private bool isRunning = false;
        private RunMode runMode = RunMode.NORMAL;
        private long relativeBase = 0;
        public void loadProgramInput(UInt32 day)
        {
            InputLoader il = new InputLoader(day);
            program = il.inputString.Split(',').ToList();
        }
        public void createProgramFromInput()
        {
            position = 0;
            programLong = new List<long>();
            program.ForEach(s => programLong.Add(Convert.ToInt64(s)));
            for (int i = 0 ; i < 1000; i++)
            {
                programLong.Add(0);
            }
        }

        public long getComputerOutput(int idx)
        {
            return programLong[idx];
        }

        public long getDiagnosticCode()
        {
            return diagnosticCode;
        }
        private int getOpcodeAndSaveParametersMode(int position)
        {
            string tmp = programLong[position].ToString();
            while (tmp.Length < 5)
            {
                tmp = '0' + tmp;
            }
            if (tmp[2] == '0') param1Mode = ParameterMode.POSITION_MODE;
            else if (tmp[2] == '1') param1Mode = ParameterMode.IMMEDIATE_MODE;
            else param1Mode = ParameterMode.RELATIVE_MODE;
        
            if (tmp[1] == '0') param2Mode = ParameterMode.POSITION_MODE;
            else if (tmp[1] == '1') param2Mode = ParameterMode.IMMEDIATE_MODE;
            else param2Mode = ParameterMode.RELATIVE_MODE;

            if (tmp[0] == '0') param3Mode = ParameterMode.POSITION_MODE;
            else if (tmp[0] == '1') param3Mode = ParameterMode.IMMEDIATE_MODE;
            else param3Mode = ParameterMode.RELATIVE_MODE;

            return Convert.ToInt32(tmp[4].ToString()) + Convert.ToInt32(tmp[3].ToString()) * 10;
        }
        public void saveInputAtPosition(int position, int value)
        {
            programLong[position] = value;
        }
        private long getParamIndex(int position, ParameterMode pm = ParameterMode.POSITION_MODE)
        {
            long idx = programLong[position];
            if (pm == ParameterMode.IMMEDIATE_MODE) idx = position;
            if (pm == ParameterMode.RELATIVE_MODE) idx = relativeBase + programLong[position];
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
            opcodeHandlers.Add(new OpcodeDelegate(handleOpcode9));
        }
        private bool handleUnknownOpcode()
        {
            return true;
        }
        private bool handleOpcode1()
        {
            long outputPosition = getParamIndex(position + 3, param3Mode);
            programLong[(int)outputPosition] = programLong[(int)getParamIndex(position + 1, param1Mode)] + 
                                            programLong[(int)getParamIndex(position + 2, param2Mode)];
            position += 4;
            return true;
        }

        private bool handleOpcode2()
        {
            long outputPosition = getParamIndex(position + 3, param3Mode);
            programLong[(int)outputPosition] = programLong[(int)getParamIndex(position + 1, param1Mode)] * 
                                            programLong[(int)getParamIndex(position + 2, param2Mode)];
            position += 4;
            return true;
        }

        private bool handleOpcode3()
        {
            saveInputAtPosition((int)getParamIndex(position + 1, param1Mode), systemId[systemIdNumber]);
            position += 2;
            systemIdNumber++;
            return true;
        }

        private bool handleOpcode4()
        {
            diagnosticCode = programLong[(int)getParamIndex(position + 1, param1Mode)];
            position += 2;
            if (runMode == RunMode.NORMAL) return true;
            return false;
        }

        private bool handleOpcode5()
        {
            if (programLong[(int)getParamIndex(position + 1, param1Mode)] != 0) 
                position = (int)programLong[(int)getParamIndex(position + 2, param2Mode)];
            else 
                position += 3;
            return true;
        }

        private bool handleOpcode6()
        {
            if (programLong[(int)getParamIndex(position + 1, param1Mode)] == 0) 
                position = (int)programLong[(int)getParamIndex(position + 2, param2Mode)];
            else 
                position += 3;
            return true;
        }

        private bool handleOpcode7()
        {
            long outputPosition = getParamIndex(position + 3, param3Mode);
            if (programLong[(int)getParamIndex(position + 1, param1Mode)] < programLong[(int)getParamIndex(position + 2, param2Mode)]) 
                programLong[(int)outputPosition] = 1;
            else 
                programLong[(int)outputPosition] = 0;
            position += 4;
            return true;
         
        }

        private bool handleOpcode8()
        {
            long outputPosition = getParamIndex(position + 3, param3Mode);
            if (programLong[(int)getParamIndex(position + 1, param1Mode)] == programLong[(int)getParamIndex(position + 2, param2Mode)]) 
                programLong[(int)outputPosition] = 1;
            else 
                programLong[(int)outputPosition] = 0;
            position += 4;
            return true;
        }

        private bool handleOpcode9()
        {
            relativeBase += programLong[(int)getParamIndex(position + 1, param1Mode)];
            position += 2;
            return true;
        }

        public long executeProgram(List<int> input, RunMode runMode = RunMode.NORMAL)
        {
            this.runMode = runMode;
            systemId = input;

            if (isRunning)
                systemIdNumber = 1;
            else
                systemIdNumber = 0;
            
            isRunning = true;

            while (programLong[position] != 99)
            {
                int opcode = getOpcodeAndSaveParametersMode(position);
                if ((bool)(opcodeHandlers[opcode].DynamicInvoke()) == false) return getDiagnosticCode();
            }
            hasStopped = true;
            return getDiagnosticCode();
        }
        public void executeProgram(int param1, int param2)
        {
            programLong[1] = param1;
            programLong[2] = param2;
            List<int> l = new List<int>();
            executeProgram(l);
        }
    }
}