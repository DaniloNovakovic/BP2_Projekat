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
    /// Interaction logic for AddCoffinView.xaml
    /// </summary>
    public partial class AddCoffinView : Window
    {
        private IEnumerable<DeathRecord> deathRecords;
        private Action<Coffin> onSubmit;

        public AddCoffinView()
        {
            InitializeComponent();
        }

        public AddCoffinView(IEnumerable<DeathRecord> deathRecords, Action<Coffin> onSubmit)
        {
            this.deathRecords = deathRecords;
            this.onSubmit = onSubmit;
        }
    }
}