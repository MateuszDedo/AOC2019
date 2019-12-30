using System.Collections.Generic;

namespace AOC2019
{
    public class StringUtils
    {
        private static List<string> numberPermutations = new List<string>();
        public static List<string> permute(string str, 
                                int l, int r) 
        { 
            if (l == r) 
                numberPermutations.Add(str); 
            else { 
                for (int i = l; i <= r; i++) { 
                    str = swap(str, l, i); 
                    permute(str, l + 1, r); 
                    str = swap(str, l, i); 
                } 
            } 
            return numberPermutations;
        } 
        private static string swap(string a, int i, int j) 
        { 
            char temp; 
            char[] charArray = a.ToCharArray(); 
            temp = charArray[i]; 
            charArray[i] = charArray[j]; 
            charArray[j] = temp; 
            string s = new string(charArray); 
            return s; 
        } 
    }
}