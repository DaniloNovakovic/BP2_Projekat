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
    /// Interaction logic for UpdateCoffinView.xaml
    /// </summary>
    public partial class UpdateCoffinView : Window
    {
        private Coffin selectedDataItem;
        private Action<Coffin> onSubmit;

        public UpdateCoffinView()
        {
            InitializeComponent();
        }

        public UpdateCoffinView(Coffin selectedDataItem, Action<Coffin> onSubmit)
        {
            this.selectedDataItem = selectedDataItem;
            this.onSubmit = onSubmit;
        }
    }
}