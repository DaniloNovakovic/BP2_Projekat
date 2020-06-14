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
    /// Interaction logic for UpdateContractView.xaml
    /// </summary>
    public partial class UpdateContractView : Window
    {
        private Contract _data = new Contract();
        private readonly Action<Contract> _onSubmit;

        public FuneralType SelectedFuneralType { get; set; }
        public ObservableCollection<FuneralType> FuneralTypes { get; set; }

        public Contract Contract
        {
            get => _data; set
            {
                _data = value;
                FuneralStartTimeInput.Text = _data.FuneralStartTime.Value.ToString(FuneralStartTimeInput.FormatString);
            }
        }

        public UpdateContractView(Contract contractToEdit, Action<Contract> onSubmit = null)
        {
            DataContext = this;

            var types = Enum.GetValues(typeof(FuneralType)).Cast<FuneralType>().ToList();
            FuneralTypes = new ObservableCollection<FuneralType>(types);

            InitializeComponent();

            Contract = contractToEdit;
            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            Contract.FuneralType = SelectedFuneralType;
            Contract.FuneralStartTime = DateTime.Parse(FuneralStartTimeInput.Text);

            _onSubmit?.Invoke(Contract);

            Close();
        }
    }
}