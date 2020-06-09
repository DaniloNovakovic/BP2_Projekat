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
    /// Interaction logic for AddManagerView.xaml
    /// </summary>
    public partial class AddManagerView : Window
    {
        private IEnumerable<Person> persons;
        private Action<Manager> onSubmit;

        public AddManagerView()
        {
            InitializeComponent();
        }

        public AddManagerView(IEnumerable<Person> persons, Action<Manager> onSubmit)
        {
            this.persons = persons;
            this.onSubmit = onSubmit;
        }
    }
}