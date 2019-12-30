using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AOC2019
{
    public class Day3 : Solution
    {
        override public void part1()
        {
            InputLoader il = new InputLoader(3);
            List<Line> lines1 = getLinesFromInput(il.inputStringList[0]);
            List<Line> lines2 = getLinesFromInput(il.inputStringList[1]);

            int lowestDist = (from res in (from p in Library2D.getIntersections(lines1, lines2) 
                                           select (System.Math.Abs((int)p.x) + System.Math.Abs((int)p.y)))
                              where (res != 0)
                              select res).Min();

            System.Console.WriteLine("Day3: Solution1:" + lowestDist);
        }
        override public void part2()
        {
            InputLoader il = new InputLoader(3);
            List<Line> lines1 = getLinesFromInput(il.inputStringList[0]);
            List<Line> lines2 = getLinesFromInput(il.inputStringList[1]);
            List<Point> points = Library2D.getIntersections(lines1, lines2);
     
            int low = (from res in (from s in points
                                    select (Library2D.getDistanceToPoint(s, lines1) +
                                            Library2D.getDistanceToPoint(s, lines2)))
                       where res != 0
                       select res).Min();
           
            System.Console.WriteLine("Day3: Solution2:" + low);
        }

        private List<Line> getLinesFromInput(string input)
        {
            List<Line> lines = new List<Line>();
            string[] moves = input.Split(',');
            int actualX = 0;
            int actualY = 0;
            foreach(string s in moves)
            {
                Line l = new Line();
                l.x1 = actualX;
                l.y1 = actualY;

                char op = s[0];
                int val = System.Convert.ToInt32(s.Substring(1));
                if (op == 'U') actualY -= val;
                if (op == 'D') actualY += val;
                if (op == 'L') actualX -= val;
                if (op == 'R') actualX += val;
                l.x2 = actualX;
                l.y2 = actualY;
                lines.Add(l);
            }
            return lines;
        }
    }
}