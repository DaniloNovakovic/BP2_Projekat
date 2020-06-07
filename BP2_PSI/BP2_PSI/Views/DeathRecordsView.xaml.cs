using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private readonly IUnitOfWork _uow;

        public DeathRecordsView(IUnitOfWork uow)
        {
            DataContext = this;

            InitializeComponent();

            _uow = uow;
        }

        public ObservableCollection<DeathRecord> DataItemList { get; set; } = new ObservableCollection<DeathRecord>();

        public DeathRecord SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var persons = await Task.Run(() => _uow.Persons.GetAlivePersons());

            var view = new AddDeathRecordView(persons, onSubmit: deathRecord =>
            {
                Log("Adding item...");
                _uow.DeathRecords.Add(deathRecord);
                _uow.SaveChanges();
                Log("Item added");
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
                var entity = _uow.DeathRecords.Get(SelectedDataItem.PersonId);
                _uow.DeathRecords.Remove(entity);
                _uow.SaveChanges();
            });

            Log("Item deleted");

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var view = new UpdateDeathRecordView(SelectedDataItem, onSubmit: deathRecord =>
            {
                Log("Updating item...");

                var dr = _uow.DeathRecords.Get(deathRecord.PersonId);
                dr.DeathDate = deathRecord.DeathDate;
                _uow.SaveChanges();

                Log("Item updated");
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

            var dbDeathRecords = await Task.Run(() => _uow.DeathRecords.GetAll());
            DataItemList.Clear();
            foreach (var item in dbDeathRecords)
            {
                DataItemList.Add(item);
            }

            Log("Loaded");
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
    }
}