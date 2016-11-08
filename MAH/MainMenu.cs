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
    class MainMenu
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        // 0 - русский, 1 - английский
        private Button StartButton, ExitButton, SettingsButton;
        static private bool wsc = false;
        public void Build()
        {
            mainCanvas.Children.Clear();
            StartButton = new Button() { Content = Languages.StartGame(), FontWeight = FontWeights.Medium };
            SettingsButton = new Button() { Content = Languages.Settings(), FontWeight = FontWeights.Medium };
            ExitButton = new Button() { Content = Languages.Exit(), FontWeight = FontWeights.Medium };
            StartButton.Click += Start;
            SettingsButton.Click += Settings;
            ExitButton.Click += Exit;
            mainCanvas.Children.Add(StartButton);
            mainCanvas.Children.Add(SettingsButton);
            mainCanvas.Children.Add(ExitButton);
            mainWindow.SizeChanged += WindowSizeChanged;
            if(wsc)
                WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            wsc = true;
            mainCanvas.Height = mainWindow.ActualHeight * 7 / 10;
            mainCanvas.Width = mainWindow.ActualWidth * 9 / 10;
            mainCanvas.Height = Math.Min(mainCanvas.Height, mainCanvas.Width);
            mainCanvas.Width = Math.Min(mainCanvas.Height, mainCanvas.Width);
            int h = (int)(mainCanvas.Height / 3);
            Thickness margin = new Thickness() { Top = 0, Left = 0 };
            SettingsButton.Margin = margin;
            StartButton.Height = h;
            StartButton.Width = h * 3;
            StartButton.FontSize = h / 3;
            margin.Top += h;
            SettingsButton.Margin = margin;
            SettingsButton.Height = h;
            SettingsButton.Width = h * 3;
            SettingsButton.FontSize = h / 3;
            margin.Top += h;
            ExitButton.Margin = margin;
            ExitButton.Height = h;
            ExitButton.Width = h * 3;
            ExitButton.FontSize = h / 3;
        }
        public void Start(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            StartGame startGame = new StartGame() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            startGame.Build();
        }
        public void Settings(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            GlobalSettings globalSettings = new GlobalSettings() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            globalSettings.Build();
        }
        public void Exit(object sender, RoutedEventArgs e)
        {
            try
            {
                string w = ((int)mainWindow.Height).ToString() + " " + ((int)mainWindow.Width).ToString() + " " + Languages.language.ToString();
                System.IO.Directory.CreateDirectory("Settings");
                System.IO.File.WriteAllText("Settings/ScreenSolution.cfg", w);
            }
            catch { }
            Environment.Exit(0);
        }
    }
}
