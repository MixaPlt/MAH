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
        public int BookNumber = 0;
        private Image BackButton;
        private int LevelsNumber = 0;
        private ScrollViewer LoadList = new ScrollViewer() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top};
        private Canvas ScrollList = new Canvas() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
        private LevelinList[] lvls;
        private BitmapImage[] Stars = new BitmapImage[4];
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(LoadList);
            LevelsNumber = Levels.LevelsNumber[BookNumber];
            lvls = new LevelinList[LevelsNumber];
            LoadList.Content = ScrollList;
            for(int i = 0; i < 4; i++)
            {
                Stars[i] = new BitmapImage();
                Stars[i].BeginInit();
                Stars[i].UriSource = new Uri("Images/Stars" + i.ToString() + ".png", UriKind.Relative);
                Stars[i].EndInit();
            }
            int[] s = Progress.LoadStats(BookNumber);
            for(int i = 0; i < LevelsNumber; i++)
            {
                lvls[i] = new LevelinList() { Stars = s[i], Source = Stars[s[i]], Height = 200, Width = 200};
                ScrollList.Children.Add(lvls[i]);
                lvls[i].MouseEnter += ImageMouseEnter;
                lvls[i].MouseLeave += ImageMouseLeave;
                lvls[i].MouseUp += Start;
                lvls[i].Lvl = i;
            }
            BitmapImage src3 = new BitmapImage();
            src3.BeginInit();
            src3.UriSource = new Uri("Images/Back_" + Languages.language.ToString() + ".png", UriKind.Relative);
            src3.EndInit();
            BackButton = new Image { Source = src3 };
            mainCanvas.Children.Add(BackButton);
            BackButton.MouseEnter += ImageMouseEnter;
            BackButton.MouseLeave += ImageMouseLeave;
            BackButton.MouseUp += Back;
            WindowSizeChanged(null, null);
            mainWindow.SizeChanged += WindowSizeChanged;
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = mainWindow.ActualHeight * 9 / 10;
            mainCanvas.Width = mainWindow.ActualWidth * 9 / 10;
            LoadList.Width = mainCanvas.Width;
            LoadList.Height = mainCanvas.Height * 9 / 10;
            ScrollList.Width = LoadList.Width * 19 / 20;
            ScrollList.Height = (ScrollList.Width / 4) * (LevelsNumber / 4 + 1);
            double h = ScrollList.Width / 4;
            Thickness margin = new Thickness() { Top = h / 20, Left = h / 20};
            for(int i = 0; i < LevelsNumber / 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    lvls[i * 4 + j].Margin = margin;
                    lvls[i * 4 + j].Height = h * 24 / 25;
                    lvls[i * 4 + j].Width = h * 24 / 25;
                    margin.Left += h;
                }
                margin.Top += h;
                margin.Left = h / 20;
            }
            for (int j = 0; j < LevelsNumber % 4; j++)
            {
                lvls[LevelsNumber - LevelsNumber % 4 + j].Margin = margin;
                lvls[LevelsNumber - LevelsNumber % 4 + j].Height = h * 24 / 25;
                lvls[LevelsNumber - LevelsNumber % 4 + j].Width = h * 24 / 25;
                margin.Left += h;
            }
            margin.Top = mainCanvas.Height * 9 / 10;
            margin.Left = 0;
            BackButton.Margin = margin;
            BackButton.Height = mainCanvas.Height / 12;
            BackButton.Width = mainCanvas.Height / 4;

        }
        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            Image o = (Image)sender;
            Thickness margin = o.Margin;
            margin.Top = o.Margin.Top - o.Height / 20;
            margin.Left = o.Margin.Left - o.Width / 20;
            o.Height = o.Height / 10 * 11;
            o.Width = o.Width * 11 / 10;
            o.Margin = margin;
        }
        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            Image o = (Image)sender;
            Thickness margin = o.Margin;
            o.Height = o.Height / 11 * 10;
            o.Width = o.Width * 10 / 11;
            margin.Top = o.Margin.Top + o.Height / 20;
            margin.Left = o.Margin.Left + o.Width / 20;
            o.Margin = margin;
        }
        private void Back(object sender, MouseEventArgs e)
        {
            int[] w = new int[LevelsNumber];
            for (int i = 0; i < LevelsNumber; i++)
                w[i] = lvls[i].Stars;
            Progress.Save(w, BookNumber);
            mainWindow.SizeChanged -= WindowSizeChanged;
            StartGame startGame = new StartGame() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            startGame.Build();
        }
        private void Start(object sender, MouseEventArgs e)
        {
            LevelinList o = (LevelinList)sender;
            if (o.Stars == 0)
            {
                mainWindow.SizeChanged -= WindowSizeChanged;
                Game game = new Game() { mainCanvas = mainCanvas, mainWindow = mainWindow, book = BookNumber, lvlnum = o.Lvl };
                game.Build();
            }
        }
    }
}
