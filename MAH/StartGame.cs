﻿using System;
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
    class StartGame
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        private Image BackButton;
        private Canvas lvCanvas = new Canvas() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
        private Image[] Books = new Image[3];
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(lvCanvas);
            BitmapImage src0 = new BitmapImage();
            src0.BeginInit();
            src0.UriSource = new Uri("Images/Book1.png", UriKind.Relative);
            src0.EndInit();
            Books[0] = new Image { Source = src0 };
            lvCanvas.Children.Add(Books[0]);
            BitmapImage src1 = new BitmapImage();
            src1.BeginInit();
            src1.UriSource = new Uri("Images/Book2.png", UriKind.Relative);
            src1.EndInit();
            Books[1] = new Image { Source = src1 };
            lvCanvas.Children.Add(Books[1]);
            BitmapImage src2 = new BitmapImage();
            src2.BeginInit();
            src2.UriSource = new Uri("Images/Book3.png", UriKind.Relative);
            src2.EndInit();
            Books[2] = new Image { Source = src2 };
            lvCanvas.Children.Add(Books[2]);
            BitmapImage src3 = new BitmapImage();
            src3.BeginInit();
            src3.UriSource = new Uri("Images/Back_" + Languages.language.ToString() + ".png", UriKind.Relative);
            src3.EndInit();
            BackButton = new Image { Source = src3 };
            mainCanvas.Children.Add(BackButton);
            Books[0].MouseEnter += ImageMouseEnter;
            Books[0].MouseLeave += ImageMouseLeave;
            Books[1].MouseEnter += ImageMouseEnter;
            Books[1].MouseLeave += ImageMouseLeave;
            Books[2].MouseEnter += ImageMouseEnter;
            Books[2].MouseLeave += ImageMouseLeave;
            BackButton.MouseEnter += ImageMouseEnter;
            BackButton.MouseLeave += ImageMouseLeave;
            BackButton.MouseUp += Back;
            Books[0].MouseUp += B1;
            Books[1].MouseUp += B2;
            Books[2].MouseUp += B3;
            mainWindow.SizeChanged += WindowSizeChanged;
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = mainWindow.ActualHeight * 9 / 10;
            mainCanvas.Width = mainWindow.ActualWidth * 9 / 10;
            lvCanvas.Width = Math.Min(mainCanvas.Width * 8 / 10, mainCanvas.Height * 2.7 * 8 / 10);
            lvCanvas.Height = lvCanvas.Width / 2.7;
            Thickness margin = new Thickness() { Top = (mainCanvas.Height - lvCanvas.Height) / 2, Left = (mainCanvas.Width - lvCanvas.Width) / 2 };
            lvCanvas.Margin = margin;
            margin.Top = lvCanvas.Height / 20;
            margin.Left = lvCanvas.Width / 60;
            Books[0].Margin = margin;
            Books[0].Height = lvCanvas.Height * 9 / 10;
            Books[0].Width = lvCanvas.Width * 9 / 30;
            margin.Left += lvCanvas.Width / 3;
            Books[1].Margin = margin;
            Books[1].Height = lvCanvas.Height * 9 / 10;
            Books[1].Width = lvCanvas.Width * 9 / 30;
            margin.Left += lvCanvas.Width / 3;
            Books[2].Margin = margin;
            Books[2].Height = lvCanvas.Height * 9 / 10;
            Books[2].Width = lvCanvas.Width * 9 / 30;
            margin.Top = mainCanvas.Height - lvCanvas.Height / 5;
            margin.Left = 0;
            BackButton.Margin = margin;
            BackButton.Height = lvCanvas.Height / 6;
            BackButton.Width = lvCanvas.Height / 2;
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
            mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
        private void B1(object sender, MouseEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            BookList bookList = new BookList() { mainCanvas = mainCanvas, mainWindow = mainWindow, BookNumber = 0 };
            bookList.Build();
        }
        private void B2(object sender, MouseEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            BookList bookList = new BookList() { mainCanvas = mainCanvas, mainWindow = mainWindow, BookNumber = 1 };
            bookList.Build();
        }
        private void B3(object sender, MouseEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            BookList bookList = new BookList() { mainCanvas = mainCanvas, mainWindow = mainWindow, BookNumber = 2 };
            bookList.Build();
        }
    }
}
