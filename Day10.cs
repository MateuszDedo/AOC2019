using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2019
{
    public class Day10 : Solution
    {
        private List<Point> getAsteroidsCoordinatesFromInput()
        {
            InputLoader il = new InputLoader(10);
            List<Point> asteroids = new List<Point>();
            int x = 0;
            int y = 0;
            foreach (string s in il.inputStringList)
            {
                foreach (char c in s.ToCharArray())
                {
                    if (c == '#') asteroids.Add(new Point(x,y));
                    x++;
                }
                y++;
                x = 0;
            }
            return asteroids;
        }
        private PolarPoint cartesianToPolar(Point cartesianPoint, double offsetX, double offsetY)
        {
            cartesianPoint.x -= offsetX;
            cartesianPoint.y -= offsetY;
            double radius = Math.Sqrt((cartesianPoint.x*cartesianPoint.x) + (cartesianPoint.y*cartesianPoint.y));
            double angle = Math.Atan2(cartesianPoint.y, cartesianPoint.x);
            return new PolarPoint(radius, angle, cartesianPoint.x, cartesianPoint.y);
        }
        private List<PolarPoint> getPolarAsteroidCoords(List<Point> asteroids, double beginX, double beginY)
        {
                List<PolarPoint> asteroidsPolar = new List<PolarPoint>();
                foreach (Point asteroid in asteroids)
                {
                    if (asteroid == new Point(beginX, beginY)) continue;
                    asteroidsPolar.Add(cartesianToPolar(new Point(asteroid.x, asteroid.y), beginX, beginY));
                }
                return asteroidsPolar;
        }
        private int getMaxReachableAsteroids(List<Point> asteroids)
        {
            int max = 0;
            Point bestCoords = new Point();
            foreach (Point asteroid in asteroids)
            {
                List<PolarPoint> asteroidsPolar = getPolarAsteroidCoords(asteroids, asteroid.x, asteroid.y);
                int reachable = getReachableAsteroids(asteroidsPolar);
                if (max < reachable) 
                {
                    bestCoords = new Point(asteroid.x, asteroid.y);
                    max = reachable;
                }
            }
            Console.WriteLine("Best x:" + bestCoords.x + " Best y:" + bestCoords.y);
            return max;
        }
        override public void part1()
        {
            System.Console.WriteLine("Day 10. Solution1:" + getMaxReachableAsteroids(getAsteroidsCoordinatesFromInput()).ToString());
        }
        private int getReachableAsteroids(List<PolarPoint> asteroidsPolar)
        {
            List<double> items = new List<double>();
            foreach (PolarPoint asteroid in asteroidsPolar)
            {
                if (!items.Contains(asteroid.angle)) items.Add(asteroid.angle);
            }
            return items.Count;
        }
        private int findIndexForAngleHigherThan(List<PolarPoint> pps, double angle)
        {
            for (int idx = 0; idx < pps.Count; idx++)
                if (pps[idx].angle > angle) return idx;
            return 0;
        }
        override public void part2()
        {
            const int bestX = 17;
            const int bestY = 23;
            double startAngle = -1.5707963267948967;
            List<PolarPoint> polarAsteroids = getPolarAsteroidCoords(getAsteroidsCoordinatesFromInput(), bestX, bestY);
            List<PolarPoint> polarAsteroidsOrdered = polarAsteroids.OrderBy(s => s.radius).OrderBy(s => s.angle).ToList();
            Point p = new Point();
            for (int i = 0; i < 200; i++)
            {
                int index = findIndexForAngleHigherThan(polarAsteroidsOrdered, startAngle);
                startAngle = polarAsteroidsOrdered[index].angle;
                p = new Point(polarAsteroidsOrdered[index].x + bestX, polarAsteroidsOrdered[index].y + bestY);
                polarAsteroidsOrdered.RemoveAt(index);   
            }
            System.Console.WriteLine("Day 10. Solution2:" + (p.x * 100 + p.y).ToString());
        }
    }
}