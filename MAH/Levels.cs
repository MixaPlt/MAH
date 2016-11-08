using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAH
{
    struct pair
    {
        public int i;
        public int j;
    }
    static class Levels
    {
        public static int[] LevelsNumber = new int[3] { 1, 0, 0};
        public delegate pair LevelHandler(int i, int j);  
        public static Level Step(int i, int j, int book, int lvl)
        {
            if(book == 0)
                switch(lvl)
                {
                    case 1:
                        return new Level() { Step = b1l1, Height = 10, Width = 10 };
                }
            return new Level() { Step = b1l1, Height = 10, Width = 10};
        }      
        private static pair b1l1(int i, int j)
        {
            pair ans = new pair() { i = 0, j = 0 };
            return ans;
        }
    }
}
