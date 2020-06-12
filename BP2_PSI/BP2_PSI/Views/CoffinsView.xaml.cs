using BP2_PSI.Views.Forms;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BP2_PSI.Views
{
    /// <summary>
    /// Interaction logic for CoffinsView.xaml
    /// </summary>
    public partial class CoffinsView : Window
    {
        private readonly IUoWFactory _uowFactory;
        private IEnumerable<DeathRecord> _deathRecords = new List<DeathRecord>();

        public CoffinsView(IUoWFactory uowFactory)
        {
            DataContext = this;

            InitializeComponent();

            _uowFactory = uowFactory;
        }

        public ObservableCollection<Coffin> DataItemList { get; set; } = new ObservableCollection<Coffin>();

        public Coffin SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Adding item...");

            var view = new AddCoffinView(_deathRecords, onSubmit: item =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    uow.Coffins.Add(item);
                    uow.SaveChanges();
                }
            });
            view.ShowDialog();

            await RefreshAsync();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteBtn.IsEnabled = editBtn.IsEnabled = SelectedDataItem != null;
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Deleting selected item...");

            await Task.Run(() =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var entity = uow.Coffins.Get(SelectedDataItem.GraveSiteId);
                    uow.Coffins.Remove(entity);
                    uow.SaveChanges();
                }
            });

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Updating item...");
            var view = new UpdateCoffinView(SelectedDataItem, onSubmit: item =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var dbCoffin = uow.Coffins.Get(item.GraveSiteId);
                    dbCoffin.Mark = item.Mark;

                    var location = item.GraveSite.Location;
                    var dbLocation = uow.Locations.Get(item.GraveSite.LocationId);
                    dbLocation.Latitude = location.Latitude;
                    dbLocation.Longitude = location.Longitude;

                    uow.SaveChanges();
                }
            });
            view.ShowDialog();

            await RefreshAsync();
        }

        private void Log(string text)
        {
            Debug.WriteLine(text);
            statusBar.Text = text;
        }

        private async Task RefreshAsync()
        {
            Log("Refreshing data...");

            using (var uow = _uowFactory.CreateNew())
            {
                var dbCoffins = await Task.Run(() => uow.Coffins.GetAll());
                DataItemList.Clear();
                foreach (var item in dbCoffins)
                {
                    DataItemList.Add(item);
                }

                _deathRecords = await Task.Run(() => uow.DeathRecords.GetAllUnburied());
                addBtn.IsEnabled = _deathRecords.Any();
            }
            Log("Loaded");
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            addBtn.IsEnabled = false;
            await RefreshAsync();
        }
    }
}