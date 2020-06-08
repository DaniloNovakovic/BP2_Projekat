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
    /// Interaction logic for AddOrUpdatePersonView.xaml
    /// </summary>
    public partial class AddOrUpdatePersonView : Window
    {
        private Person _data = new Person();
        private readonly Action<Person> _onSubmit;

        public Person Person
        {
            get => _data; set
            {
                _data = value;
                FirstNameInput.Text = _data.FirstName;
                LastNameInput.Text = _data.LastName;
                BirthDateInput.Text = _data.BirthDate.Value.ToString(BirthDateInput.FormatString);
            }
        }

        public AddOrUpdatePersonView(Action<Person> onSubmit = null)
        {
            InitializeComponent();

            _onSubmit = onSubmit;
        }

        public AddOrUpdatePersonView(Person personToEdit, Action<Person> onSubmit = null) : this(onSubmit)
        {
            Person = personToEdit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            Person.FirstName = FirstNameInput.Text;
            Person.LastName = LastNameInput.Text;
            Person.BirthDate = DateTime.Parse(BirthDateInput.Text);

            _onSubmit?.Invoke(Person);

            Close();
        }
    }
}