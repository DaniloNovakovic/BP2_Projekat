using Core.Interfaces;
using Persistance;
using System;
using System.Configuration;
using System.Windows;

namespace BP2_PSI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UoWFactory _uowFactory;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            string nameOrConnectionString = ConfigurationManager.AppSettings.Get("SelectedConnectionStringName");
            _uowFactory = new UoWFactory(nameOrConnectionString);
        }
    }
}