using BP2_PSI.Views.Forms;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace BP2_PSI.Views
{
    /// <summary>
    /// Interaction logic for UrnsView.xaml
    /// </summary>
    public partial class UrnsView : Window
    {
        private readonly IUoWFactory _uowFactory;
        private IEnumerable<DeathRecord> _deathRecords = new List<DeathRecord>();

        public UrnsView(IUoWFactory uowFactory)
        {
            DataContext = this;

            InitializeComponent();

            _uowFactory = uowFactory;
        }

        public ObservableCollection<Urn> DataItemList { get; set; } = new ObservableCollection<Urn>();

        public Urn SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Adding item...");

            var view = new AddUrnView(_deathRecords, onSubmit: item =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    uow.Urns.Add(item);
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
                    var entity = uow.Urns.Get(SelectedDataItem.GraveSiteId);
                    uow.Urns.Remove(entity);
                    uow.SaveChanges();
                }
            });

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Updating item...");
            var view = new UpdateUrnView(SelectedDataItem, onSubmit: item =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var dbUrn = uow.Urns.Get(item.GraveSiteId);
                    dbUrn.Capacity = item.Capacity;

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
                var dbUrns = await Task.Run(() => uow.Urns.GetAll());
                DataItemList.Clear();
                foreach (var item in dbUrns)
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