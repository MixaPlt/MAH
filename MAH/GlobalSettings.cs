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
    class GlobalSettings
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        private Button SaveButton = new Button() { Content = Languages.Save() };
        private Button ExitButton = new Button() { Content = Languages.Exit_Without_Saving() };
        private Button ApplyButton = new Button() { Content = Languages.Apply() };
        private ComboBox LanguageList = new ComboBox() { HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center };
        private ComboBox ScreenSolutions = new ComboBox() { HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center };
        private ComboBox Colors = new ComboBox() { HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center };
        private Label LanguageListInfo = new Label() { Content = Languages.Choose_Language(), HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center };
        private Label ScreenLabel = new Label() { Content = Languages.Screen_Size(), HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center };
        private Label InterfaceColor = new Label() { Content = Languages.Interface_Color(), HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center };
        public void Build()
        {
            LanguageList.ItemsSource = new string[] { "Русский", "English" };
            LanguageList.SelectedIndex = Languages.language;
            ScreenSolutions.ItemsSource = new string[] {((int)mainWindow.Width).ToString() + "x" + ((int)mainWindow.Height).ToString(), "640x480", "800x600", "1024x768", "1366x768"};
            ScreenSolutions.SelectedIndex = 0;
            var values = typeof(Brushes).GetProperties().
            Select(p => new
            { p.Name}).
            ToArray();
            Colors.ItemsSource = values;
            ApplyButton.Click += Apply;
            ExitButton.Click += Cancel;
            SaveButton.Click += Save;
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(LanguageList);
            mainCanvas.Children.Add(ScreenSolutions);
            mainCanvas.Children.Add(SaveButton);
            mainCanvas.Children.Add(ExitButton);
            mainCanvas.Children.Add(ApplyButton);
            mainCanvas.Children.Add(LanguageListInfo);
            mainCanvas.Children.Add(ScreenLabel);
            mainCanvas.Children.Add(InterfaceColor);
            mainCanvas.Children.Add(Colors);
            WindowSizeChanged(null, null);
            mainWindow.SizeChanged += WindowSizeChanged;
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = mainWindow.ActualHeight * 9 / 10;
            mainCanvas.Width = mainWindow.ActualWidth * 9 / 10;
            mainCanvas.Height = Math.Min(mainCanvas.Height, mainCanvas.Width);
            mainCanvas.Width = mainCanvas.Height;
            Thickness margin = new Thickness() { Top = 0, Left = 0 };
            int fontSize = (int)(mainCanvas.Width / 16);
            LanguageListInfo.Margin = margin;
            LanguageListInfo.Height = mainCanvas.Height / 9;
            LanguageListInfo.Width = mainCanvas.Width * 3 / 5;
            LanguageListInfo.FontSize = fontSize;
            margin.Left = mainCanvas.Width * 3 / 5;
            margin.Top += mainCanvas.Height / 50;
            LanguageList.Margin = margin;
            LanguageList.Height = mainCanvas.Height / 12;
            LanguageList.Width = mainCanvas.Width * 2 / 5;
            LanguageList.FontSize = mainCanvas.Width / 20;
            margin.Left = 0;
            margin.Top += mainCanvas.Height / 9 - mainCanvas.Height / 50;
            ScreenLabel.Height = mainCanvas.Height / 9;
            ScreenLabel.Width = mainCanvas.Width * 3 / 5;
            ScreenLabel.Margin = margin;
            ScreenLabel.FontSize = fontSize;
            margin.Left = mainCanvas.Width * 3 / 5;
            margin.Top += mainCanvas.Height / 50;
            ScreenSolutions.Margin = margin;
            ScreenSolutions.Height = mainCanvas.Height / 12;
            ScreenSolutions.Width = mainCanvas.Width * 2 / 5;
            ScreenSolutions.FontSize = mainCanvas.Width / 20;
            margin.Left = 0;
            margin.Top += mainCanvas.Height / 9 - mainCanvas.Height / 50;
            InterfaceColor.Height = mainCanvas.Height / 9;
            InterfaceColor.Width = mainCanvas.Width * 3 / 5;
            InterfaceColor.Margin = margin;
            InterfaceColor.FontSize = fontSize;
            margin.Left = mainCanvas.Width * 3 / 5;
            margin.Top += mainCanvas.Height / 50;
            Colors.Margin = margin;
            Colors.Height = mainCanvas.Height / 12;
            Colors.Width = mainCanvas.Width * 2 / 5;
            Colors.FontSize = mainCanvas.Width / 20;
            margin.Top = mainCanvas.Height * 8 / 9;
            margin.Left = 0;
            ApplyButton.Margin = margin;
            ApplyButton.Height = mainCanvas.Height / 9;
            ApplyButton.Width = mainCanvas.Width / 3;
            ApplyButton.FontSize = fontSize;
            margin.Left += mainCanvas.Width / 3;
            SaveButton.Margin = margin;
            SaveButton.Height = mainCanvas.Height / 9;
            SaveButton.Width = mainCanvas.Width / 3;
            SaveButton.FontSize = fontSize;
            margin.Left += mainCanvas.Width / 3;
            ExitButton.Margin = margin;
            ExitButton.Height = mainCanvas.Height / 9;
            ExitButton.Width = mainCanvas.Width / 3;
            ExitButton.FontSize = fontSize;
        }
        private void Cancel(object sender, RoutedEventArgs e)
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
            mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
        private void Apply(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] t = (ScreenSolutions.SelectedItem.ToString()).Split('x');
                mainWindow.Width = Int32.Parse(t[0]);
                mainWindow.Height = Int32.Parse(t[1]);
                Languages.language = LanguageList.SelectedIndex;
                LanguageListInfo.Content = Languages.Choose_Language();
                ScreenLabel.Content = Languages.Screen_Size();
                InterfaceColor.Content = Languages.Interface_Color();
                ApplyButton.Content = Languages.Apply();
                SaveButton.Content = Languages.Save();
                ExitButton.Content = Languages.Exit_Without_Saving();
            }
            catch { }
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            Apply(null, null);
            try
            {
                string w = ((int)mainWindow.Height).ToString() + " " + ((int)mainWindow.Width).ToString() + " " + Languages.language.ToString();
                System.IO.Directory.CreateDirectory("Settings");
                System.IO.File.WriteAllText("Settings/ScreenSolution.cfg", w);
            }
            catch { }
            mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
    }
}
