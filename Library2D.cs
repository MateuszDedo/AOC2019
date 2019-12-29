using System;
using System.Collections.Generic;

namespace AOC2019
{
    public class Point
    {
        public int x;
        public int y;
    }
    public class Line
    {
        public int x1, y1;
        public int x2, y2;
    }
    public class Library2D
    {
        public static int getDistanceToPoint(Point point, List<Line> lines)
        {
            int distToPoint = 0;

            foreach (Line l1 in lines)
            {
                int minX1 = System.Math.Min(l1.x1, l1.x2);
                int maxX1 = System.Math.Max(l1.x1, l1.x2);
                int minY1 = System.Math.Min(l1.y1, l1.y2);
                int maxY1 = System.Math.Max(l1.y1, l1.y2);

                if (point.x >= minX1 && point.x <= maxX1 &&
                    point.y >= minY1 && point.y <= maxY1)
                {
                    distToPoint += System.Math.Abs(l1.x1 - point.x);
                    distToPoint += System.Math.Abs(l1.y1 - point.y);
                    break; 
                }
                distToPoint += maxX1 - minX1;
                distToPoint += maxY1 - minY1;
            }
            return distToPoint;
        }
        public static List<Point> getIntersections(List<Line> lines1, List<Line> lines2)
        {
            List<Point> points = new List<Point>();

            foreach (Line l1 in lines1)
            {
                foreach (Line l2 in lines2)
                {
                    Point p = getCrossCoordsForTwoLines(l1, l2);
                    if (p != null) points.Add(p);
                }
            }
            return points;
        }
        public static Point getCrossCoordsForTwoLines(Line l1, Line l2)
        {
            bool xBetween = false;
            bool yBetween = false;
            

            int minX1 = Math.Min(l1.x1, l1.x2);
            int maxX1 = Math.Max(l1.x1, l1.x2);
            int minY1 = Math.Min(l1.y1, l1.y2);
            int maxY1 = Math.Max(l1.y1, l1.y2);

            int minX2 = Math.Min(l2.x1, l2.x2);
            int maxX2 = Math.Max(l2.x1, l2.x2);
            int minY2 = Math.Min(l2.y1, l2.y2);
            int maxY2 = Math.Max(l2.y1, l2.y2);


            if (l1.x1 == l1.x2)
            {
                if (l1.x1 >= minX2 && l1.x2 <= maxX2)
                {
                    xBetween = true;
                }

                if (l2.y1 >= minY1 && l2.y1 <= maxY1)
                {
                    yBetween = true;
                }
            }

            if (l1.y1 == l1.y2)
            {
                if (l1.y1 >= minY2 && l1.y2 <= maxY2)
                {
                    yBetween = true;
                }

                if (l2.x1 >= minX1 && l2.x1 <= maxX1)
                {
                    xBetween = true;
                }
            }

            if (xBetween && yBetween)
            {
                int crossX = 0;
                int crossY = 0;
                if (l1.x1 == l1.x2)
                {
                    crossX = l1.x1;
                    crossY = l2.y1;
                }
                else
                {
                    crossX = l2.x1;
                    crossY = l1.y1;
                }
                Point p = new Point();
                p.x = crossX;
                p.y = crossY;
                return p;
            }
            return null;
        }
    }
}