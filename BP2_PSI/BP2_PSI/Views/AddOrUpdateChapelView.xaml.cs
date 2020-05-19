using Core.Entities;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddOrUpdateChapelView.xaml
    /// </summary>
    public partial class AddOrUpdateChapelView : Window
    {
        private Chapel _data = new Chapel();
        private readonly Action<Chapel> _onSubmit;

        public Chapel Chapel
        {
            get => _data; set
            {
                _data = value;

                NameInput.Text = _data.Name;
                LongitudeInput.Value = _data.Location.Longitude;
                LatitudeInput.Value = _data.Location.Latitude;
            }
        }

        public AddOrUpdateChapelView(Action<Chapel> onSubmit = null)
        {
            InitializeComponent();

            _onSubmit = onSubmit;
        }

        public AddOrUpdateChapelView(Chapel personToEdit, Action<Chapel> onSubmit = null) : this(onSubmit)
        {
            Chapel = personToEdit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            Chapel.Name = NameInput.Text;
            Chapel.Location = new Location
            {
                Latitude = LatitudeInput.Value ?? 0,
                Longitude = LongitudeInput.Value ?? 0
            };

            _onSubmit?.Invoke(Chapel);

            Close();
        }
    }
}