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
    /// Interaction logic for DeathRecordsView.xaml
    /// </summary>
    public partial class DeathRecordsView : Window
    {
        private readonly IUoWFactory _uowFactory;
        private IEnumerable<Person> _alivePersons = new List<Person>();

        public DeathRecordsView(IUoWFactory uowFactory)
        {
            DataContext = this;

            InitializeComponent();

            _uowFactory = uowFactory;
        }

        public ObservableCollection<DeathRecord> DataItemList { get; set; } = new ObservableCollection<DeathRecord>();

        public DeathRecord SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Adding item...");

            var view = new AddDeathRecordView(_alivePersons, onSubmit: deathRecord =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    uow.DeathRecords.Add(deathRecord);
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
                    var entity = uow.DeathRecords.Get(SelectedDataItem.PersonId);
                    uow.DeathRecords.Remove(entity);
                    uow.SaveChanges();
                }
            });

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Updating item...");
            var view = new UpdateDeathRecordView(SelectedDataItem, onSubmit: deathRecord =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var dr = uow.DeathRecords.Get(deathRecord.PersonId);
                    dr.DeathDate = deathRecord.DeathDate;
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
                var dbDeathRecords = await Task.Run(() => uow.DeathRecords.GetAll());
                DataItemList.Clear();
                foreach (var item in dbDeathRecords)
                {
                    DataItemList.Add(item);
                }
                _alivePersons = await Task.Run(() => uow.Persons.GetAlivePersons());
                addBtn.IsEnabled = _alivePersons.Any();
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