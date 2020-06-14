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
    /// Interaction logic for AddContractView.xaml
    /// </summary>
    public partial class AddContractView : Window
    {
        private Contract _data = new Contract();
        private readonly Action<Contract> _onSubmit;
        public FamilyMember SelectedFamilyMember { get; set; }
        public ObservableCollection<FamilyMember> FamilyMembers { get; set; }

        public Manager SelectedManager { get; set; }
        public ObservableCollection<Manager> Managers { get; set; }

        public Chapel SelectedChapel { get; set; }
        public ObservableCollection<Chapel> Chapels { get; set; }

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

        public AddContractView(
            IEnumerable<FamilyMember> familyMembers,
            IEnumerable<Manager> managers,
            IEnumerable<Chapel> chapels,
            Action<Contract> onSubmit = null)
        {
            DataContext = this;
            FamilyMembers = new ObservableCollection<FamilyMember>(familyMembers);
            Managers = new ObservableCollection<Manager>(managers);
            Chapels = new ObservableCollection<Chapel>(chapels);

            var types = Enum.GetValues(typeof(FuneralType)).Cast<FuneralType>().ToList();
            FuneralTypes = new ObservableCollection<FuneralType>(types);

            InitializeComponent();

            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            Contract.ChapelId = SelectedChapel.Id;
            Contract.ManagerId = SelectedManager.WorkerId;
            Contract.FamilyMemberId = SelectedFamilyMember.Id;
            Contract.FuneralType = SelectedFuneralType;
            Contract.FuneralStartTime = DateTime.Parse(FuneralStartTimeInput.Text);

            _onSubmit?.Invoke(Contract);

            Close();
        }
    }
}