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
    /// Interaction logic for UpdateTechnicalStaffView.xaml
    /// </summary>
    public partial class UpdateTechnicalStaffView : Window
    {
        private TechnicalStaff _data = new TechnicalStaff();
        private readonly Action<TechnicalStaff> _onSubmit;
        public Manager SelectedManager { get; set; }
        public ObservableCollection<Manager> Managers { get; set; }

        public TechnicalStaff TechnicalStaff
        {
            get => _data; set
            {
                _data = value;
                WorktimeInput.Text = _data.Worker?.WorkTime ?? "";
            }
        }

        public UpdateTechnicalStaffView(IEnumerable<Manager> managers, TechnicalStaff technicalStaff, Action<TechnicalStaff> onSubmit = null)
        {
            DataContext = this;
            Managers = new ObservableCollection<Manager>(managers);
            SelectedManager = Managers.FirstOrDefault(m => m.WorkerId == technicalStaff.ManagerId);

            InitializeComponent();

            TechnicalStaff = technicalStaff;
            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            TechnicalStaff.Worker.WorkTime = WorktimeInput.Text;
            TechnicalStaff.ManagerId = SelectedManager.WorkerId;

            _onSubmit?.Invoke(TechnicalStaff);

            Close();
        }
    }
}