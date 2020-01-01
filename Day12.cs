using System;
using System.Collections.Generic;
using System.Linq;
namespace AOC2019
{
    public class Day12 : Solution
    {
        internal class Moon
        {
            public int x;
            public int y;
            public int z;
            public int vX;
            public int vY;
            public int vZ;
            public Moon(int x, int y, int z)
            {
                this.x = x; this.y = y; this.z = z;
            }
            public void calculateNewVelocity(Moon m1)
            {
                if (m1.x > x) vX++; else if (m1.x < x) vX--;
                if (m1.y > y) vY++; else if (m1.y < y) vY--;
                if (m1.z > z) vZ++; else if (m1.z < z) vZ--;
            }
            public void move()
            {
                x += vX; y += vY; z += vZ;
            }
            public int getPotentialEnergy()
            {
                return Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
            }
            public int getKineticEnergy()
            {
                return Math.Abs(vX) + Math.Abs(vY) + Math.Abs(vZ);
            }
        }
        private void calculateVelocity(List<Moon> moons)
        {
            foreach (Moon m in moons)
                foreach (Moon m2 in moons)
                    if (m != m2) m.calculateNewVelocity(m2);
        }
        private void moveMoons(List<Moon> moons)
        {
            moons.ForEach(m => m.move());
        }
        private int calculateEnergy(List<Moon> moons)
        {
            return (from s in moons select s.getPotentialEnergy() * s.getKineticEnergy()).Sum();
        }
        private List<Moon> createMoons()
        {
            return new List<Moon>{new Moon(13, 9, 5), new Moon(8, 14, -2), new Moon(-5, 4, 11), new Moon(2, -6, 1)};
        }
        override public void part1()
        {
            List<Moon> moons = createMoons();
            int energy = 0;
            for (int i = 0 ; i < 1000; i++)
            {
                calculateVelocity(moons);
                moveMoons(moons);
                energy = calculateEnergy(moons);
            }
            System.Console.WriteLine("Day 12. Solution1:" + energy.ToString());
        }
        override public void part2()
        {
           List<Moon> moons = createMoons();
            int period = 0;
            int xAxisPeriod = 0;
            int yAxisPeriod = 0;
            int zAxisPeriod = 0;
            while (xAxisPeriod == 0 || yAxisPeriod == 0 || zAxisPeriod == 0)
            {
                calculateVelocity(moons);
                moveMoons(moons);
                period++;
                if (moons[0].vX == 0 && moons[1].vX == 0 && moons[2].vX == 0 && moons[3].vX == 0 && 
                    moons[0].x == 13 && moons[1].x == 8 && moons[2].x == -5 && moons[3].x == 2)
                    xAxisPeriod = period;

                if (moons[0].vY == 0 && moons[1].vY == 0 && moons[2].vY == 0 && moons[3].vY == 0 && 
                    moons[0].y == 9 && moons[1].y == 14 && moons[2].y == 4 && moons[3].y == -6)
                    yAxisPeriod = period;

                if (moons[0].vZ == 0 && moons[1].vZ == 0 && moons[2].vZ == 0 && moons[3].vZ == 0 &&
                    moons[0].z == 5 && moons[1].z == -2 && moons[2].z == 11 && moons[3].z == 1)
                    zAxisPeriod = period;
            }
            System.Console.WriteLine("Day 12. Solution2:" + getCommonResult(xAxisPeriod, yAxisPeriod, zAxisPeriod).ToString());
        }
        public long getCommonResult(long a, long b, long c)
        { 
            long pA = a;
            long pB = b;
            long pC = c;
            while (a != b || b != c || a != c) if (a > b) b += pB; else if (b > c) c += pC; else if (c > a) a += pA;
            return a;
        }
    }
}