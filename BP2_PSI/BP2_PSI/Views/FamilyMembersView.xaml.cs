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
    /// Interaction logic for FamilyMembersView.xaml
    /// </summary>
    public partial class FamilyMembersView : Window
    {
        private readonly IUoWFactory _uowFactory;
        private IEnumerable<Person> _alivePersons = new List<Person>();
        private IEnumerable<DeathRecord> _deathRecords = new List<DeathRecord>();

        public FamilyMembersView(IUoWFactory uowFactory)
        {
            DataContext = this;

            InitializeComponent();

            _uowFactory = uowFactory;
        }

        public ObservableCollection<FamilyMember> DataItemList { get; set; } = new ObservableCollection<FamilyMember>();

        public FamilyMember SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Adding item...");

            var view = new AddFamilyMemberView(_alivePersons, _deathRecords, onSubmit: familyMember =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    uow.FamilyMembers.Add(familyMember);
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
                    var entity = uow.FamilyMembers.Get(SelectedDataItem.Id);
                    uow.FamilyMembers.Remove(entity);
                    uow.SaveChanges();
                }
            });

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Log("Updating item...");
            var view = new UpdateFamilyMemberView(SelectedDataItem, onSubmit: familyMember =>
            {
                using (var uow = _uowFactory.CreateNew())
                {
                    var dr = uow.FamilyMembers.Get(familyMember.Id);
                    dr.RelationType = familyMember.RelationType;
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
                var familyMembers = await Task.Run(() => uow.FamilyMembers.GetAll());
                DataItemList.Clear();
                foreach (var item in familyMembers)
                {
                    DataItemList.Add(item);
                }

                _alivePersons = await Task.Run(() => uow.Persons.GetAlivePersons());
                _deathRecords = await Task.Run(() => uow.DeathRecords.GetAll());
                addBtn.IsEnabled = _alivePersons.Any() && _deathRecords.Any();
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