using BP2_PSI.Views;
using Core.Interfaces;
using Persistance;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace BP2_PSI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUnitOfWork _uow;
        private UoWFactory _uowFactory;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            _uow.Dispose();
            base.OnClosed(e);
        }

        private void Log(string text)
        {
            Debug.WriteLine(text);
            statusBar.Text = text;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string nameOrConnectionString = ConfigurationManager.AppSettings.Get("SelectedConnectionStringName");
            _uowFactory = new UoWFactory(nameOrConnectionString);

            Log("Starting up database...");

            SetBtnsAvailability(false);

            await Task.Run(() => _uow = _uowFactory.CreateNew());

            SetBtnsAvailability(true);

            Log("Loaded");
        }

        private void SetBtnsAvailability(bool isEnabled)
        {
            btnChapels.IsEnabled = btnWorkers.IsEnabled = btnContracts.IsEnabled = btnDeathRecords.IsEnabled
                = btnFamilyMembers.IsEnabled = btnGraveSites.IsEnabled = btnPersons.IsEnabled = isEnabled;
        }

        #region OpenWindowMethods

        private void OpenPersonsWindow(object sender, RoutedEventArgs e)
        {
            var view = new PersonsView(_uow);
            view.ShowDialog();
        }

        private void OpenChapelsWindow(object sender, RoutedEventArgs e)
        {
            var view = new ChapelsView(_uow);
            view.ShowDialog();
        }

        private void OpenDeathRecordsWindow(object sender, RoutedEventArgs e)
        {
            var view = new DeathRecordsView(_uowFactory);
            view.ShowDialog();
        }

        private void OpenFamilyMembersWindow(object sender, RoutedEventArgs e)
        {
            var view = new FamilyMembersView(_uow);
            view.ShowDialog();
        }

        private void OpenContractsWindow(object sender, RoutedEventArgs e)
        {
            var view = new ContractsView(_uow);
            view.ShowDialog();
        }

        private void OpenGraveSitesWindow(object sender, RoutedEventArgs e)
        {
            var view = new GraveSitesView(_uow);
            view.ShowDialog();
        }

        private void OpenWorkersWindow(object sender, RoutedEventArgs e)
        {
            var view = new WorkersView(_uow);
            view.ShowDialog();
        }

        #endregion OpenWindowMethods
    }
}