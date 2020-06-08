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
    /// Interaction logic for UpdateFamilyMemberView.xaml
    /// </summary>
    public partial class UpdateFamilyMemberView : Window
    {
        private FamilyMember _data = new FamilyMember();
        private readonly Action<FamilyMember> _onSubmit;

        public FamilyMember FamilyMember
        {
            get => _data; set
            {
                _data = value;
                RelationshipTypeInput.Text = _data.RelationType;
            }
        }

        public UpdateFamilyMemberView(FamilyMember deathRecordToEdit, Action<FamilyMember> onSubmit = null)
        {
            InitializeComponent();

            FamilyMember = deathRecordToEdit;
            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            FamilyMember.RelationType = RelationshipTypeInput.Text;

            _onSubmit?.Invoke(FamilyMember);

            Close();
        }
    }
}