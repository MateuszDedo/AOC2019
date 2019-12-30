using System.Collections.Generic;

namespace AOC2019
{
    public class Day8 : Solution
    {
        override public void part1()
        {
            List<char[]> boards = createLayesrsFromInput();

            int zeroDigits = 999;
            int result = 0;
            foreach (char[] board in boards)
            {
                int currentLayerZeroDigits = 0;
                int oneDigits = 0;
                int twoDigits = 0;
                foreach (char c in board)
                {
                    if (c == '0') currentLayerZeroDigits++;
                    if (c == '1') oneDigits++;
                    if (c == '2') twoDigits++;
                }
                if (zeroDigits > currentLayerZeroDigits)
                {
                    zeroDigits = currentLayerZeroDigits;
                    result = oneDigits * twoDigits;
                }
            }
            System.Console.WriteLine("Day 8. Solution1:" + result.ToString());
        }

        override public void part2()
        {
            List<char[]> layers = createLayesrsFromInput();
            List<char> board = createEmptyBoard();
            foreach (char[] layer in layers)
            {
                int pos = 0;
                foreach (char c in layer)
                {
                    if (board[pos] == '.')
                    {
                        if (c == '0') board[pos] = ' ';
                        if (c == '1') board[pos] = 'X';
                    }
                    pos++;
                }
            }
            System.Console.WriteLine("Day 8. Solution2:");
            displayBoard(board);
        }
        private List<char[]> createLayesrsFromInput()
        {
            InputLoader il = new InputLoader(8);
            List<char[]> boards = new List<char[]>();
            for (int i = 0; i < il.inputString.Length; i += 150)
                boards.Add(il.inputString.Substring(i, 150).ToCharArray());
            return boards;
        }
        private List<char> createEmptyBoard()
        {
            List<char> tab = new List<char>();
            for (int i = 0; i < 150; i++) tab.Add('.');
            return tab;
        }

        private void displayBoard(List<char> board)
        {
            for (int y = 0 ; y < 6; y++)
            {
                for(int x = 0; x < 25; x++)
                    System.Console.Write(board[x + y * 25]);
                System.Console.WriteLine();
            }
        }
    }
}