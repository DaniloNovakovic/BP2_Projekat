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
    /// Interaction logic for AddContractView.xaml
    /// </summary>
    public partial class AddContractView : Window
    {
        private IEnumerable<FamilyMember> familyMembers;
        private IEnumerable<Manager> managers;
        private IEnumerable<Chapel> chapels;
        private Action<Contract> onSubmit;

        public AddContractView()
        {
            InitializeComponent();
        }

        public AddContractView(IEnumerable<FamilyMember> familyMembers,
            IEnumerable<Manager> managers,
            IEnumerable<Chapel> chapels,
            Action<Contract> onSubmit)
        {
            this.familyMembers = familyMembers;
            this.managers = managers;
            this.chapels = chapels;
            this.onSubmit = onSubmit;
        }
    }
}