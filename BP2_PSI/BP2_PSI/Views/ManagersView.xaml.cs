using BP2_PSI.Views.Forms;
using Core.Entities;
using Core.Interfaces;
using Persistance;
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
    /// Interaction logic for ManagersView.xaml
    /// </summary>
    public partial class ManagersView : Window
    {
        private readonly IUoWFactory _uowFactory;
        private IEnumerable<Person> _persons = new List<Person>();

        public ManagersView(IUoWFactory uowFactory)
        {
            DataContext = this;

            InitializeComponent();

            _uowFactory = uowFactory;
        }

        public ObservableCollection<Manager> DataItemList { get; set; } = new ObservableCollection<Manager>();

        public Manager SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Adding item...");

            var view = new AddManagerView(_persons, onSubmit: manager =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    uow.Managers.Add(manager);
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
                    var entity = uow.Managers.Get(SelectedDataItem.WorkerId);
                    uow.Managers.Remove(entity);
                    uow.SaveChanges();
                }
            });

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Updating item...");
            var view = new UpdateWorkerView(SelectedDataItem.Worker, onSubmit: worker =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var dbWorker = uow.Workers.Get(worker.PersonId);
                    dbWorker.WorkTime = worker.WorkTime;
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
                var dbManagers = await Task.Run(() => uow.Managers.GetAll());
                DataItemList.Clear();
                foreach (var item in dbManagers)
                {
                    DataItemList.Add(item);
                }
                _persons = await Task.Run(() => uow.Persons.GetUnemployeedPersons());
                addBtn.IsEnabled = _persons.Any();
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