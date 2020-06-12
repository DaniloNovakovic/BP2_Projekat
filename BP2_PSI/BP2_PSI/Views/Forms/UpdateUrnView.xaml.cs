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

namespace BP2_PSI.Views.Forms
{
    /// <summary>
    /// Interaction logic for UpdateUrnView.xaml
    /// </summary>
    public partial class UpdateUrnView : Window
    {
        private Action<Urn> _onSubmit;

        private Urn _data = new Urn();

        public Urn Urn
        {
            get => _data;
            set
            {
                _data = value;
                CapacityInput.Value = _data.Capacity;
                LongitudeInput.Value = _data.GraveSite?.Location?.Longitude ?? 0;
                LatitudeInput.Value = _data.GraveSite?.Location?.Latitude ?? 0;
            }
        }

        public UpdateUrnView(Urn coffinToEdit, Action<Urn> onSubmit)
        {
            DataContext = this;

            InitializeComponent();

            Urn = coffinToEdit;
            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            Urn.GraveSite.Location = new Location
            {
                Latitude = LatitudeInput.Value ?? 0,
                Longitude = LongitudeInput.Value ?? 0
            };
            Urn.Capacity = CapacityInput.Value ?? 0;

            _onSubmit?.Invoke(Urn);

            Close();
        }
    }
}