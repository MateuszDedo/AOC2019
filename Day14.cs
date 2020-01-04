using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2019
{
    internal class Requirement
    {
        public int count;
        public string name;
        
        public Requirement(){}
        public Requirement(Requirement r)
        {
            this.name = r.name;
            this.count = r.count;
        }

    }

    internal class Product
    {
        public string name;
        public int inMagazine;
        public int amount;
        public List<Requirement> requirements;

        public Product()
        {

        }
        public Product(Product p)
        {
            this.name = p.name;
            this.amount = p.amount;
            this.requirements = new List<Requirement>(p.requirements);

        }
    }
    public class Day14 : Solution
    {
        List<Product> products;
        InputLoader il;
        int idxFuel = 0;
        private void prepareData()
        {
            products = new List<Product>();
            il = new InputLoader(14);
            foreach (string line in il.inputStringList)
            {
                string[] s = line.Split("=>");
                string[] p = s[0].Split(',');
                Product product = new Product();
                product.amount = Convert.ToInt32(s[1].Trim().Split(' ')[0].Trim());
                product.name = s[1].Trim().Split(' ')[1].Trim();
                product.inMagazine = 0;
                product.requirements = new List<Requirement>();
                foreach (string item in p)
                {
                    Requirement req = new Requirement();
                    req.count = Convert.ToInt32(item.Trim().Split(' ')[0].Trim());
                    req.name = item.Trim().Split(' ')[1].Trim();
                    product.requirements.Add(req);
                }
                products.Add(product);   
            }
            idxFuel = 0;
            for (int a = 0; a < products.Count;a++)
            {
                if (products[a].name == "FUEL")
                {
                    idxFuel = a;
                    break;
                }
            }
        }

        private void calculate()
        {
            bool hasOnlyOre = false;
            while(!hasOnlyOre)
            {
                hasOnlyOre = true;
                int idx = 0;
                foreach (Requirement r in products[idxFuel].requirements)
                {
                    if (r.name != "ORE")
                    {
                        Product p = (from p1 in products where p1.name == r.name select p1).ToList()[0];
                        int producted = 0;
                        while (r.count > p.inMagazine)
                        {
                            producted++;
                            p.inMagazine += p.amount; 
                        }
                        foreach (Requirement item in p.requirements)
                        {
                            Requirement newReq = new Requirement();
                            newReq.count = producted * item.count;
                            newReq.name = item.name;
                            products[idxFuel].requirements.Add(newReq);
                        }
                        p.inMagazine -= (r.count);
                        hasOnlyOre = false;
                        products[idxFuel].requirements.RemoveAt(idx);
                        List<Requirement> newFuelRequirements = new List<Requirement>();
                        foreach (Requirement item in products[idxFuel].requirements)
                        {
                            int fidx = -1;
                            for (int c = 0; c < newFuelRequirements.Count; c++)
                            {
                                if (newFuelRequirements[c].name == item.name)
                                {
                                    fidx = c;
                                    break;
                                }
                            }
                            if (fidx == -1)
                            {
                                newFuelRequirements.Add(item);
                            }
                            else
                            {
                                newFuelRequirements[fidx].count += item.count;
                            }
                        }
                        products[idxFuel].requirements = newFuelRequirements;
                        break;
                    }
                    idx++;
                }
            }
        }
        override public void part1()
        {
            prepareData();
            calculate();
            System.Console.WriteLine("Day 14. Solution1:" + products[idxFuel].requirements[0].count.ToString());
        }

        override public void part2()
        {
            
            prepareData();
            Product p = new Product(products[idxFuel]);
            long ores = 1000000000000;
            int fuel = 0;
            long onePercent = 10000000000;
            int done = 0;
            while (ores > 0)
            {
                fuel++;
                calculate();
                ores -= products[idxFuel].requirements[0].count;
                onePercent -= products[idxFuel].requirements[0].count;
                if (onePercent < 0)
                {
                    onePercent = 10000000000;
                    done++;
                    System.Console.WriteLine(DateTime.Now.ToString() + ": Solution2 done:" + done.ToString() + "%");
                }
                products[idxFuel] = new Product(p);
                
            }
            System.Console.WriteLine("Day 14. Solution2:" + (fuel - 1).ToString());
        }
    }
}