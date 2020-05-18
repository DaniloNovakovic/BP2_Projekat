using Core.Entities;
using Core.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace BP2_PSI.Views
{
    /// <summary>
    /// Interaction logic for PersonsView.xaml
    /// </summary>
    public partial class PersonsView : Window
    {
        private readonly IUnitOfWork _uow;

        public PersonsView(IUnitOfWork uow)
        {
            InitializeComponent();

            _uow = uow;

            var dbPersons = _uow.Persons.GetAll();
            Persons = new ObservableCollection<Person>(dbPersons);
        }

        public ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();
        public Person SelectedPerson { get; set; }
    }
}