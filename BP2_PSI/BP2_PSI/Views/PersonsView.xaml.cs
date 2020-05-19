using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace BP2_PSI.Views
{
    /// <summary>
    /// Interaction logic for PersonsView.xaml
    /// </summary>
    public partial class PersonsView : Window
    {
        private readonly IUnitOfWork _uow;

        public PersonsView(IUnitOfWork uow)
        {
            DataContext = this;

            InitializeComponent();

            _uow = uow;
        }

        public ObservableCollection<Person> DataItemList { get; set; } = new ObservableCollection<Person>();

        public Person SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var view = new AddOrUpdatePersonView(onSubmit: person =>
            {
                Log("Adding item...");
                _uow.Persons.Add(person);
                _uow.SaveChanges();
                Log("Item added");
            });
            view.ShowDialog();

            await RefreshAsync();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            deleteBtn.IsEnabled = editBtn.IsEnabled = SelectedDataItem != null;
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Deleting selected item...");

            await Task.Run(() =>
            {
                var entity = _uow.Persons.Get(SelectedDataItem.Id);
                _uow.Persons.Remove(entity);
                _uow.SaveChanges();
            });

            Log("Item deleted");

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var view = new AddOrUpdatePersonView(SelectedDataItem, onSubmit: person =>
            {
                Log("Updating item...");

                var p = _uow.Persons.Get(person.Id);
                p.FirstName = person.FirstName;
                p.LastName = person.LastName;
                p.BirthDate = person.BirthDate;
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

            var dbPersons = await Task.Run(() => _uow.Persons.GetAll());
            DataItemList.Clear();
            foreach (var item in dbPersons)
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