using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAH
{
    static class Progress
    {
        public static int[] LoadStats(int book)
        {
            int[] ans = new int[Levels.LevelsNumber[book]];
            for (int i = 0; i < Levels.LevelsNumber[book]; i++)
                ans[i] = 0;
            try
            {
                string[] s = (System.IO.File.ReadAllLines("Saves/Stats.save")[book]).Split(' ');
                for(int i = 0; i < Levels.LevelsNumber[book]; i++)
                {
                    ans[i] = Int32.Parse(s[i]);
                }
            }
            catch { }
            return ans;
        }
        public static void Save(int[] ch, int bk)
        {
            string[] w = new string[3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < Levels.LevelsNumber[i]; j++)
                    w[i] += "0 ";
            try
            {
                w = (System.IO.File.ReadAllLines("Saves/Stats.save"));
            }
            catch { }
            w[bk] = "";
            for (int i = 0; i < Levels.LevelsNumber[bk]; i++)
                w[bk] += ch[i].ToString() + " ";
            try
            {
                System.IO.Directory.CreateDirectory("Saves");
                System.IO.File.WriteAllLines("Saves/Stats.save", w);
            }
            catch { }
        }
    }
}
