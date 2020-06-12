using Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddCoffinView.xaml
    /// </summary>
    public partial class AddCoffinView : Window
    {
        private Action<Coffin> _onSubmit;
        public DeathRecord SelectedDeathRecord { get; set; }
        public ObservableCollection<DeathRecord> DeathRecords { get; set; }

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

        public AddCoffinView(IEnumerable<DeathRecord> deathRecords, Action<Coffin> onSubmit)
        {
            DataContext = this;
            DeathRecords = new ObservableCollection<DeathRecord>(deathRecords);

            InitializeComponent();

            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            var graveSite = new GraveSite { DeathRecordId = SelectedDeathRecord.PersonId, Type = nameof(Coffin) };
            graveSite.Location = new Location
            {
                Latitude = LatitudeInput.Value ?? 0,
                Longitude = LongitudeInput.Value ?? 0
            };

            Coffin.GraveSite = graveSite;
            Coffin.Mark = MarkInput.Text;

            _onSubmit?.Invoke(Coffin);

            Close();
        }
    }
}