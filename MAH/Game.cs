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
using System.Windows.Threading;

namespace MAH
{
    class Game
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        public int book, lvlnum;
        private int Height, Width, LogNum = 0;
        private Rectangle[,] field;
        private Level lvl;
        private double h;
        private ScrollViewer LogScroll = new ScrollViewer() { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Center };
        private Canvas fieldCanvas = new Canvas() { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left}, LogList = new Canvas() { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Right };
        private Label CordLabel = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = Languages.MouseOver(), Background = Brushes.Aqua};
        private DispatcherTimer timer = new DispatcherTimer();
        private Label[] Log = new Label[4000];
        private Button AcceptButton = new Button() { Content = Languages.Ready(), Background = Brushes.Aquamarine }, BackButton = new Button() { Content = Languages.Back(), Background = Brushes.Aquamarine };
        public void Build()
        {
            mainCanvas.Children.Clear();
            lvl = Levels.Init(book, lvlnum);
            field = new Rectangle[lvl.Height, lvl.Width];
            Height = lvl.Height;
            Width = lvl.Width;
            BitmapImage bcsrc = new BitmapImage();
            mainCanvas.Children.Add(BackButton);
            mainCanvas.Children.Add(fieldCanvas);
            mainCanvas.Children.Add(CordLabel);
            BackButton.Click += Back;
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    field[i, j] = new Rectangle() { Fill = Brushes.Aqua, StrokeThickness = 1, Stroke = Brushes.AliceBlue };
                    fieldCanvas.Children.Add(field[i, j]);
                    field[i, j].MouseEnter += ImageMouseEnter;
                    field[i, j].MouseLeave += ImageMouseLeave;
                    field[i, j].MouseUp += RectClick;
                }
            }
            mainCanvas.Children.Add(LogScroll);
            LogScroll.Content = LogList;
            LogScroll.Background = Brushes.Blue;
            fieldCanvas.Background = Brushes.Black;
            mainCanvas.Children.Add(AcceptButton);
    //-------------------------------------------------------------------------------------------------
            WindowSizeChanged(null, null);
            mainWindow.SizeChanged += WindowSizeChanged;
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Width = mainWindow.ActualWidth * 29 / 30;
            mainCanvas.Height = mainWindow.ActualHeight - 45;
            fieldCanvas.Height = mainCanvas.Height * 9 / 10;
            fieldCanvas.Width = mainCanvas.Width * 2 / 3;
            fieldCanvas.Height = Math.Min(fieldCanvas.Height, fieldCanvas.Width * Height / Width);
            fieldCanvas.Width = fieldCanvas.Height / Height * Width;
            Thickness margin = new Thickness() { Left = 0, Top = mainCanvas.Height * 9 / 10 };
            BackButton.Margin = margin;
            BackButton.Height = mainCanvas.Height / 10;
            BackButton.Width = mainCanvas.Height * 3 / 10;
            BackButton.FontSize = Math.Min(mainCanvas.Height / 20, mainCanvas.Width / 24);
            h = fieldCanvas.Height / Height;
            margin.Top = 0;
            margin.Left = 0;
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    field[i, j].Margin = margin;
                    field[i, j].Height = h * 29 / 30;
                    field[i, j].Width = h * 29 / 30;
                    margin.Left += h;
                }
                margin.Left = 0;
                margin.Top += h;
            }
            margin.Top = mainCanvas.Height / 10;
            margin.Left = fieldCanvas.Width;
            LogScroll.Margin = margin;
            LogScroll.Height = mainCanvas.Height * 4 / 5;
            LogScroll.Width = mainCanvas.Width - fieldCanvas.Width;
            margin.Top = 0;
            margin.Left = fieldCanvas.Width;
            CordLabel.Margin = margin;
            CordLabel.Height = mainCanvas.Height * 29 / 300;
            CordLabel.Width = mainCanvas.Width  - fieldCanvas.Width;
            CordLabel.FontSize = Math.Min(mainCanvas.Height / 20, mainCanvas.Width / 24);
            margin.Top = mainCanvas.Height  * 9 / 10;
            margin.Left = fieldCanvas.Width;
            AcceptButton.Margin = margin;
            AcceptButton.Height = mainCanvas.Height / 10;
            AcceptButton.Width = mainCanvas.Width - fieldCanvas.Width;
            AcceptButton.FontSize = Math.Min(mainCanvas.Height / 20, mainCanvas.Width / 24);
            LogList.Width = LogScroll.Width * 39 / 40;
            margin.Left = 0;
            margin.Top = 0;
            LogList.Height = mainCanvas.Height * LogNum / 10;
            for(int i = LogNum - 1; i >= 0; i--)
            {
                Log[i].Margin = margin;
                Log[i].Height = mainCanvas.Height * 29 / 300;
                Log[i].Width = LogList.Width;
                Log[i].FontSize = Math.Min(mainCanvas.Height / 20, LogList.Width / 11);
                margin.Top += mainCanvas.Height / 10;
            }
        }
        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement o = (FrameworkElement)sender;
            Thickness margin = o.Margin;
            margin.Top = o.Margin.Top - o.Height / 20;
            margin.Left = o.Margin.Left - o.Width / 20;
            o.Height = o.Height / 10 * 11;
            o.Width = o.Width * 11 / 10;
            o.Margin = margin;
            try
            {
                Rectangle p = (Rectangle)sender;
                int i = (int)(p.Margin.Top / h + 0.5), j = (int)(p.Margin.Left / h + 0.5);
                CordLabel.Content = "X:" + j.ToString() + "; Y:" + i.ToString();
            }
            catch { }
        }
        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            FrameworkElement o = (FrameworkElement)sender;
            Thickness margin = o.Margin;
            o.Height = o.Height / 11 * 10;
            o.Width = o.Width * 10 / 11;
            margin.Top = o.Margin.Top + o.Height / 20;
            margin.Left = o.Margin.Left + o.Width / 20;
            o.Margin = margin;
            try
            {
                Rectangle p = (Rectangle)sender;
                CordLabel.Content = Languages.MouseOver();
            }
            catch { }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            BookList bookList = new BookList() { mainCanvas = mainCanvas, mainWindow = mainWindow, BookNumber = book };
            bookList.Build();
        }
        private void RectClick(object sender, MouseEventArgs e)
        { 
            Rectangle o = (Rectangle)sender;
            if (o.Fill == Brushes.Blue)
                return;
            int i = (int)(o.Margin.Top / h + 0.5), j = (int)(o.Margin.Left / h + 0.5);
            pair s = lvl.Step(i, j);
            field[i, j].Fill = Brushes.Blue;
            field[s.i, s.j].Fill = Brushes.Red;
            Thickness margin = new Thickness() { Top = 0, Left = 0 };
            Log[LogNum] = new Label() { Content = "(X:" + j.ToString() + "; Y:" + i.ToString() + ")-->(X:" + s.j.ToString() + "; Y:" + s.i.ToString() + ");", Height = mainCanvas.Height * 29 / 300, Width = LogList.Width, Background = Brushes.Aqua, FontSize = Math.Min(mainCanvas.Height / 20, LogList.Width / 11), HorizontalContentAlignment = HorizontalAlignment.Center, FontWeight = FontWeights.Medium };
            LogList.Children.Add(Log[LogNum]);
            LogNum++;
            LogList.Height += mainCanvas.Height / 10;
            for (int k = LogNum - 1; k >= 0; k--)
            {
                Log[k].Margin = margin;
                margin.Top += mainCanvas.Height / 10;
            }
        }
    }
}
