using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2019
{
    public enum InputType
    {
        BASIC,
        ALL
    }
    public class InputLoader
    {
        public List<string> inputStringList = new List<string>();
        public List<int> inputIntList = new List<int>();
        public string inputString = "";

        public InputLoader(UInt32 day, InputType it = InputType.BASIC)
        {
            try
            {
                StreamReader sr = new StreamReader("input\\Day" + day.ToString());
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    inputString += line;
                    inputStringList.Add(line);                    
                }
                sr.Close();
                if (it == InputType.ALL)
                {
                    getIntList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void getIntList()
        {
            inputStringList.ForEach(s => inputIntList.Add(Convert.ToInt32(s)));
        }
    }
}