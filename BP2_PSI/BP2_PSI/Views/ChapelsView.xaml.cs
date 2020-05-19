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
    /// Interaction logic for ChapelsView.xaml
    /// </summary>
    public partial class ChapelsView : Window
    {
        private readonly IUnitOfWork _uow;

        public ChapelsView(IUnitOfWork uow)
        {
            DataContext = this;

            InitializeComponent();

            _uow = uow;
        }

        public ObservableCollection<Chapel> DataItemList { get; set; } = new ObservableCollection<Chapel>();

        public Chapel SelectedDataItem { get; set; }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var view = new AddOrUpdateChapelView(onSubmit: item =>
            {
                Log("Adding item...");
                _uow.Chapels.Add(item);
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
                var entity = _uow.Chapels.Get(SelectedDataItem.Id);
                _uow.Chapels.Remove(entity);
                _uow.SaveChanges();
            });

            Log("Item deleted");

            await RefreshAsync();
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var view = new AddOrUpdateChapelView(SelectedDataItem, onSubmit: item =>
            {
                Log("Updating item...");

                var entity = _uow.Chapels.Get(item.Id);
                entity.Name = item.Name;
                entity.Location.Latitude = item.Location.Latitude;
                entity.Location.Longitude = item.Location.Longitude;
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

            var items = await Task.Run(() => _uow.Chapels.GetAll());
            DataItemList.Clear();
            foreach (var item in items)
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