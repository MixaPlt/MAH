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
    public partial class MainWindow : Window
    {
        public int language = 0;
        public MainWindow()
        {
            InitializeComponent();
            Load();
            mainCanvas.VerticalAlignment = VerticalAlignment.Center;
            mainCanvas.HorizontalAlignment = HorizontalAlignment.Center;
            mainCanvas.Height = mainWindow.Height - 30;
            mainCanvas.Width = mainWindow.Width - 4;
            MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
        private void Load()
        {
            try
            {
                string s = System.IO.File.ReadAllText("Settings/ScreenSolution.cfg");
                string[] r = s.Split(' ');
                mainWindow.Height = Int32.Parse(r[0]);
                mainWindow.Width = Int32.Parse(r[1]);
                Languages.language = Int32.Parse(r[2]);
            }
            catch { }
        }
    }
}
