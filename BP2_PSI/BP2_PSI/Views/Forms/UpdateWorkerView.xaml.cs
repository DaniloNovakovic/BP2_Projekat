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
    /// Interaction logic for UpdateWorkerView.xaml
    /// </summary>
    public partial class UpdateWorkerView : Window
    {
        private IEnumerable<Person> persons;
        private Manager selectedDataItem;
        private Action<Worker> onSubmit;

        public UpdateWorkerView()
        {
            InitializeComponent();
        }

        public UpdateWorkerView(IEnumerable<Person> persons, Manager selectedDataItem, Action<Worker> onSubmit)
        {
            this.persons = persons;
            this.selectedDataItem = selectedDataItem;
            this.onSubmit = onSubmit;
        }
    }
}