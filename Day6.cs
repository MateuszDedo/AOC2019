using System.Collections.Generic;
using System.Linq;

namespace AOC2019
{
    public class Day6 : Solution
    {
        private Dictionary<string, string> planets;
        
        override public void part1()
        {
            createPlanetsDictionary();
            int parents = (from s in planets select getNumberOfParents(s.Key)).Sum();
            System.Console.WriteLine("Day 6. Solution1:" + parents.ToString());
        }
        override public void part2()
        {
            createPlanetsDictionary();
            List<string> parentsOfSan = new List<string>();
            List<string> parentsOfYou = new List<string>();
            getParents(parentsOfSan, "SAN");
            getParents(parentsOfYou, "YOU");        
            System.Console.WriteLine("Day 6. Solution2:" + (parentsOfSan.Except(parentsOfYou).ToList().Count + 
                                                            parentsOfYou.Except(parentsOfSan).ToList().Count).ToString());
        }

        private string getParentName(string planetName)
        {
            if (planets.ContainsKey(planetName)) return planets[planetName];
            return "";
        }

        private int getNumberOfParents(string planetName)
        {
            string parentName = getParentName(planetName);
            if (parentName == "") return 0;
            return 1 + getNumberOfParents(parentName);
        }

        private void getParents(List<string> l, string planetName)
        {
            string parentName = getParentName(planetName);
            if (parentName == "") return;
            l.Add(parentName);
            getParents(l, parentName);
        }

        private void createPlanetsDictionary()
        {
            InputLoader il = new InputLoader(6);
            planets = new Dictionary<string, string>();
            foreach (string s in il.inputStringList)
            {
                string[] p = s.Split(')');
                string parent = p[0];
                string planet = p[1];
                planets.Add(planet, parent);
            }   
        }
    }
}