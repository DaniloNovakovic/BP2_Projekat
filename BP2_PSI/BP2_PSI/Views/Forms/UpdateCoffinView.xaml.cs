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
    /// Interaction logic for UpdateCoffinView.xaml
    /// </summary>
    public partial class UpdateCoffinView : Window
    {
        private Action<Coffin> _onSubmit;

        private Coffin _data = new Coffin();

        public Coffin Coffin
        {
            get => _data;
            set
            {
                _data = value;
                MarkInput.Text = _data.Mark;
                LongitudeInput.Value = _data.GraveSite?.Location?.Longitude ?? 0;
                LatitudeInput.Value = _data.GraveSite?.Location?.Latitude ?? 0;
            }
        }

        public UpdateCoffinView(Coffin coffinToEdit, Action<Coffin> onSubmit)
        {
            DataContext = this;

            InitializeComponent();

            Coffin = coffinToEdit;
            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            Coffin.GraveSite.Location = new Location
            {
                Latitude = LatitudeInput.Value ?? 0,
                Longitude = LongitudeInput.Value ?? 0
            };
            Coffin.Mark = MarkInput.Text;

            _onSubmit?.Invoke(Coffin);

            Close();
        }
    }
}