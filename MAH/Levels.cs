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
        //5 0 0
        public static int[] LevelsNumber = new int[3] { 100, 1, 0};
        public delegate pair LevelHandler(int i, int j);  
        public static Level Init(int book, int lvl)
        {
            if(book == 0)
                switch(lvl)
                {
                    case 0:
                        return new Level() { Step = b1l1, Height = 10, Width = 10 };
                    case 1:
                        return new Level() { Step = b1l2, Height = 10, Width = 10 };
                    case 2:
                        return new Level() { Step = b1l3, Height = 10, Width = 10 };
                    case 3:
                        return new Level() { Step = b1l4, Height = 10, Width = 10 };
                    case 4:
                        return new Level() { Step = b1l5, Height = 10, Width = 10 };
                }
            if(book == 1)
                switch(lvl)
                {
                    case 0:
                        return new Level() { Step = b2l1, Height = 10, Width = 15 };
                }
            return new Level() { Step = b1l1, Height = 10, Width = 10};
        }  
        //10x10    
        private static pair b1l1(int i, int j)
        {
            pair ans = new pair() { i = 0, j = 0 };
            return ans;
        }
        //10x10
        private static pair b1l2(int i, int j)
        {
            pair ans = new pair() { i = (i + 1) % 10, j = (j + 1) % 10};
            return ans;
        }
        //10x10
        private static pair b1l3(int i, int j)
        {
            pair ans = new pair() { i = (i + 1) % 10, j = (j + 1) % 10 };
            return ans;
        }
        //10x10
        private static pair b1l4(int i, int j)
        {
            pair ans = new pair() { i = (i + 3) % 10, j = (j + 1) % 10 };
            return ans;
        }
        //10x10
        private static pair b1l5(int i, int j)
        {
            pair ans = new pair() { i = (i + 3) % 10, j = (j + 9) % 10 };
            return ans;
        }
        //10x15
        private static pair b2l1(int i, int j)
        {
            pair ans = new pair() { i = (i * 2) % 10, j = (j + 1) % 10 };
            return ans;
        }
    }
}
