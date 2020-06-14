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
    /// Interaction logic for UpdateContractView.xaml
    /// </summary>
    public partial class UpdateContractView : Window
    {
        private Contract selectedDataItem;
        private Action<Contract> onSubmit;

        public UpdateContractView()
        {
            InitializeComponent();
        }

        public UpdateContractView(Contract selectedDataItem, Action<Contract> onSubmit)
        {
            this.selectedDataItem = selectedDataItem;
            this.onSubmit = onSubmit;
        }
    }
}