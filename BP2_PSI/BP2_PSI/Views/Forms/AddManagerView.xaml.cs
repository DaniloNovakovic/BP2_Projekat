using Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BP2_PSI.Views.Forms
{
    /// <summary>
    /// Interaction logic for AddManagerView.xaml
    /// </summary>
    public partial class AddManagerView : Window
    {
        private Manager _data = new Manager();
        private readonly Action<Manager> _onSubmit;
        public Person SelectedPerson { get; set; }
        public ObservableCollection<Person> Persons { get; set; }

        public Manager Manager
        {
            get => _data; set
            {
                _data = value;
                WorktimeInput.Text = _data.Worker?.WorkTime ?? "";
            }
        }

        public AddManagerView(IEnumerable<Person> persons, Action<Manager> onSubmit = null)
        {
            DataContext = this;
            Persons = new ObservableCollection<Person>(persons);

            InitializeComponent();

            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            var worker = new Worker { PersonId = SelectedPerson.Id, Role = nameof(Manager) };
            worker.WorkTime = WorktimeInput.Text;
            Manager.Worker = worker;

            _onSubmit?.Invoke(Manager);

            Close();
        }
    }
}