using System;

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

            public Moon(){}
            public Moon(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public void calculateNewVelocity(Moon m1)
            {
                if (m1.x > x)
                {
                    vX++;
                }
                else if (m1.x < x)
                {
                    vX--;
                }

                if (m1.y > y)
                {
                    vY++;
                }
                else if (m1.y < y)
                {
                    vY--;
                }

                if (m1.z > z)
                {
                    vZ++;
                }
                else if (m1.z < z)
                {
                    vZ--;
                }
            }

            public void move()
            {
                x += vX;
                y += vY;
                z += vZ;
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

        override public void part1()
        {
            Moon m1 = new Moon(13, 9, 5);
            Moon m2 = new Moon(8, 14, -2);
            Moon m3 = new Moon(-5, 4, 11);
            Moon m4 = new Moon(2, -6, 1);

            int energy = 0;

            for (int i = 0 ; i < 1000; i++)
            {

                m1.calculateNewVelocity(m2);
                m1.calculateNewVelocity(m3);
                m1.calculateNewVelocity(m4);

                m2.calculateNewVelocity(m1);
                m2.calculateNewVelocity(m3);
                m2.calculateNewVelocity(m4);

                m3.calculateNewVelocity(m1);
                m3.calculateNewVelocity(m2);
                m3.calculateNewVelocity(m4);

                m4.calculateNewVelocity(m1);
                m4.calculateNewVelocity(m2);
                m4.calculateNewVelocity(m3);

                
                m1.move();
                m2.move();
                m3.move();
                m4.move();

                energy = (m1.getPotentialEnergy() * m1.getKineticEnergy()) +
                (m2.getPotentialEnergy() * m2.getKineticEnergy()) +
                (m3.getPotentialEnergy() * m3.getKineticEnergy()) +
                (m4.getPotentialEnergy() * m4.getKineticEnergy());

                
            }
            System.Console.WriteLine("Day 12. Solution1:" + energy.ToString());
        }

        override public void part2()
        {
            Moon m1 = new Moon(13, 9, 5);
            Moon m2 = new Moon(8, 14, -2);
            Moon m3 = new Moon(-5, 4, 11);
            Moon m4 = new Moon(2, -6, 1);

            int period = 0;
            int xAxisPeriod = 0;
            int yAxisPeriod = 0;
            int zAxisPeriod = 0;
            while (xAxisPeriod == 0 || yAxisPeriod == 0 || zAxisPeriod == 0)
            {
                m1.calculateNewVelocity(m2);
                m1.calculateNewVelocity(m3);
                m1.calculateNewVelocity(m4);

                m2.calculateNewVelocity(m1);
                m2.calculateNewVelocity(m3);
                m2.calculateNewVelocity(m4);

                m3.calculateNewVelocity(m1);
                m3.calculateNewVelocity(m2);
                m3.calculateNewVelocity(m4);

                m4.calculateNewVelocity(m1);
                m4.calculateNewVelocity(m2);
                m4.calculateNewVelocity(m3);

                
                m1.move();
                m2.move();
                m3.move();
                m4.move();

                
                if (xAxisPeriod == 0) period++;
                if (m1.vX == 0 && m2.vX == 0 && m3.vX == 0 && m4.vX == 0 && m1.x == 13 && m2.x == 8 && m3.x == -5 && m4.x == 2)
                    xAxisPeriod = period;

                if (m1.vY == 0 && m2.vY == 0 && m3.vY == 0 && m4.vY == 0 && m1.y == 9 && m2.y == 14 && m3.y == 4 && m4.y == -6)
                    yAxisPeriod = period;

                if (m1.vZ == 0 && m2.vZ == 0 && m3.vZ == 0 && m4.vZ == 0 && m1.z == 5 && m2.z == -2 && m3.z == 11 && m4.z == 1)
                    zAxisPeriod = period;
            }
            System.Console.WriteLine("Day 12. Solution2:" + getCommonResult(xAxisPeriod, yAxisPeriod, zAxisPeriod).ToString());
        }

        public long getCommonResult(long a, long b, long c)
        { 
            long pA = a;
            long pB = b;
            long pC = c;
            while (a != b || b != c || a != c) 
            {
            
                if (a > b) 
                {
                    b += pB;
                }
                else if (b > c)
                {
                    c += pC;
                }
                else if (c > a)
                {
                    a += pA;
                }
            }
            return a;
        }
    }
}