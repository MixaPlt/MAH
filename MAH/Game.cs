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
        private int Height, Width;
        private Rectangle[,] field;
        private Level lvl;
        private double h;
        private Canvas fieldCanvas = new Canvas() { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Center};
        private Image BackButton;
        private DispatcherTimer timer = new DispatcherTimer();
        public void Build()
        {
            mainCanvas.Children.Clear();
            lvl = Levels.Init(book, lvlnum);
            field = new Rectangle[lvl.Height, lvl.Width];
            Height = lvl.Height;
            Width = lvl.Width;
            BitmapImage bcsrc = new BitmapImage();
            bcsrc.BeginInit();
            bcsrc.UriSource = new Uri("Images/Back_" + Languages.language.ToString() + ".png", UriKind.Relative);
            bcsrc.EndInit();
            BackButton = new Image() { Source = bcsrc };
            mainCanvas.Children.Add(BackButton);
            mainCanvas.Children.Add(fieldCanvas);
            BackButton.MouseUp += Back;
            BackButton.MouseEnter += ImageMouseEnter;
            BackButton.MouseLeave += ImageMouseLeave;
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
            WindowSizeChanged(null, null);
            mainWindow.SizeChanged += WindowSizeChanged;
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Width = mainWindow.ActualWidth * 9 / 10;
            mainCanvas.Height = mainWindow.ActualHeight * 9 / 10;
            fieldCanvas.Height = mainCanvas.Height * 9 / 10;
            fieldCanvas.Width = mainCanvas.Width;
            fieldCanvas.Height = Math.Min(fieldCanvas.Height, fieldCanvas.Width * Height / Width);
            fieldCanvas.Width = mainCanvas.Height / Height * Width;
            BackButton.Height = mainCanvas.Height / 10;
            Thickness margin = new Thickness() { Left = 0, Top = mainCanvas.Height * 9 / 10 };
            BackButton.Margin = margin;
            BackButton.Height = mainCanvas.Height / 12;
            BackButton.Width = mainCanvas.Height / 4;
            h = fieldCanvas.Height / Height;
            margin.Top = 0;
            margin.Left = 0;
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    field[i, j].Margin = margin;
                    field[i, j].Height = h * 57 / 60;
                    field[i, j].Width = h * 29 / 30;
                    margin.Left += h;
                }
                margin.Left = 0;
                margin.Top += h;
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
        }
        private void Back(object sender, MouseEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            BookList bookList = new BookList() { mainCanvas = mainCanvas, mainWindow = mainWindow, BookNumber = book };
            bookList.Build();
        }
        private void RectClick(object sender, MouseEventArgs e)
        {
            Rectangle o = (Rectangle)sender;
            int i = (int)(o.Margin.Top / h + 0.5), j = (int)(o.Margin.Left / h + 0.5);
            pair s = lvl.Step(i, j);
            field[i, j].Fill = Brushes.Blue;
            field[s.i, s.j].Fill = Brushes.Red;
        }
    }
}
