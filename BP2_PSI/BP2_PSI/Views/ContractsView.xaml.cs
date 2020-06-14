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
    /// Interaction logic for ContractsView.xaml
    /// </summary>
    public partial class ContractsView : Window
    {
        private readonly IUoWFactory _uowFactory;
        private IEnumerable<Manager> _managers = new List<Manager>();
        private IEnumerable<FamilyMember> _familyMembers = new List<FamilyMember>();
        private IEnumerable<Chapel> _chapels = new List<Chapel>();

        public ContractsView(IUoWFactory uowFactory)
        {
            DataContext = this;

            InitializeComponent();

            _uowFactory = uowFactory;
        }

        public ObservableCollection<Contract> DataItemList { get; set; } = new ObservableCollection<Contract>();

        public Contract SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Adding item...");

            var view = new AddContractView(_familyMembers, _managers, _chapels, onSubmit: contract =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    uow.Contracts.Add(contract);
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
                    var entity = uow.Contracts.Get(SelectedDataItem.Id);
                    uow.Contracts.Remove(entity);
                    uow.SaveChanges();
                }
            });

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Updating item...");
            var view = new UpdateContractView(SelectedDataItem, onSubmit: contract =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var dbContract = uow.Contracts.Get(contract.Id);
                    dbContract.FuneralStartTime = contract.FuneralStartTime;
                    dbContract.FuneralType = contract.FuneralType;

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
                var contracts = await Task.Run(() => uow.Contracts.GetAll());
                DataItemList.Clear();
                foreach (var item in contracts)
                {
                    DataItemList.Add(item);
                }

                _managers = await Task.Run(() => uow.Managers.GetAll());
                _familyMembers = await Task.Run(() => uow.FamilyMembers.GetAll());
                _chapels = await Task.Run(() => uow.Chapels.GetAll());

                addBtn.IsEnabled = _familyMembers.Any() && _managers.Any() && _chapels.Any();
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