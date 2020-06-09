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
    /// Interaction logic for TechnicalStaffView.xaml
    /// </summary>
    public partial class TechnicalStaffView : Window
    {
        private readonly IUoWFactory _uowFactory;
        private IEnumerable<Manager> _managers = new List<Manager>();
        private IEnumerable<Person> _persons = new List<Person>();

        public TechnicalStaffView(IUoWFactory uowFactory)
        {
            DataContext = this;

            InitializeComponent();

            _uowFactory = uowFactory;
        }

        public ObservableCollection<TechnicalStaff> DataItemList { get; set; } = new ObservableCollection<TechnicalStaff>();

        public TechnicalStaff SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Adding item...");

            var view = new AddTechnicalStaffView(_persons, _managers, onSubmit: staff =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    uow.TechnicalStaff.Add(staff);
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
                    var entity = uow.TechnicalStaff.Get(SelectedDataItem.WorkerId);
                    uow.TechnicalStaff.Remove(entity);
                    uow.SaveChanges();
                }
            });

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Updating item...");
            var view = new UpdateTechnicalStaffView(_managers, SelectedDataItem, onSubmit: staff =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var dbWorker = uow.Workers.Get(staff.WorkerId);
                    dbWorker.WorkTime = staff.Worker.WorkTime;

                    var dbTechStaff = uow.TechnicalStaff.Get(staff.WorkerId);
                    dbTechStaff.ManagerId = staff.ManagerId;

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
                var dbTechnicalStaff = await Task.Run(() => uow.TechnicalStaff.GetAll());
                DataItemList.Clear();
                foreach (var item in dbTechnicalStaff)
                {
                    DataItemList.Add(item);
                }

                _managers = await Task.Run(() => uow.Managers.GetAll());

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