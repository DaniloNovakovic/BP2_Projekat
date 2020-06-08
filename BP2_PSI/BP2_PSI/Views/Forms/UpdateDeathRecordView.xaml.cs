using Core.Entities;
using System;
using System.Windows;

namespace BP2_PSI.Views.Forms
{
    /// <summary>
    /// Interaction logic for UpdateDeathRecordView.xaml
    /// </summary>
    public partial class UpdateDeathRecordView : Window
    {
        private DeathRecord _data = new DeathRecord();
        private readonly Action<DeathRecord> _onSubmit;

        public DeathRecord DeathRecord
        {
            get => _data; set
            {
                _data = value;
                DeathDateInput.Text = _data.DeathDate.Value.ToString(DeathDateInput.FormatString);
            }
        }

        public UpdateDeathRecordView(DeathRecord deathRecordToEdit, Action<DeathRecord> onSubmit = null)
        {
            InitializeComponent();

            DeathRecord = deathRecordToEdit;
            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            DeathRecord.DeathDate = DateTime.Parse(DeathDateInput.Text);

            _onSubmit?.Invoke(DeathRecord);

            Close();
        }
    }
}