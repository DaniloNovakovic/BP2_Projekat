using Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BP2_PSI.Views.Forms
{
    /// <summary>
    /// Interaction logic for AddTechnicalStaffView.xaml
    /// </summary>
    public partial class AddTechnicalStaffView : Window
    {
        private TechnicalStaff _data = new TechnicalStaff();
        private readonly Action<TechnicalStaff> _onSubmit;
        public Person SelectedPerson { get; set; }
        public ObservableCollection<Person> Persons { get; set; }
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

        public AddTechnicalStaffView(IEnumerable<Person> persons, IEnumerable<Manager> managers, Action<TechnicalStaff> onSubmit = null)
        {
            DataContext = this;
            Persons = new ObservableCollection<Person>(persons);
            Managers = new ObservableCollection<Manager>(managers);

            InitializeComponent();

            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            var worker = new Worker { PersonId = SelectedPerson.Id, Role = nameof(TechnicalStaff) };
            worker.WorkTime = WorktimeInput.Text;
            TechnicalStaff.Worker = worker;
            TechnicalStaff.ManagerId = SelectedManager.WorkerId;

            _onSubmit?.Invoke(TechnicalStaff);

            Close();
        }
    }
}