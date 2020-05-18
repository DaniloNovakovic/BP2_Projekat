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
        private readonly IUnitOfWork _uow;

        public MainWindow()
        {
            InitializeComponent();

            var nameOrConnectionString = ConfigurationManager.AppSettings.Get("SelectedConnectionStringName");
            var factory = new UoWFactory(nameOrConnectionString);
            _uow = factory.CreateNew();
        }

        protected override void OnClosed(EventArgs e)
        {
            _uow.Dispose();

            base.OnClosed(e);
        }
    }
}