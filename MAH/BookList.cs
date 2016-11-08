using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MAH
{
    class BookList
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        public int BookNumber = 0, LevelsNumber = 0;
        private ScrollViewer LoadList = new ScrollViewer() { };
        private LevelinList[] lvls;
        private BitmapImage[] Stars = new BitmapImage[3];
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(LoadList);
            LevelsNumber = Levels.LevelsNumber[BookNumber];
            lvls = new LevelinList[LevelsNumber];
            for(int i = 0; i < 3; i++)
            {

            }
            for(int i = 0; i < LevelsNumber; i++)
            {

            }
            WindowSizeChanged(null, null);
            mainWindow.SizeChanged += WindowSizeChanged;
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
