using Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BP2_PSI.Views.Forms
{
    /// <summary>
    /// Interaction logic for AddFamilyMemberView.xaml
    /// </summary>
    public partial class AddFamilyMemberView : Window
    {
        private FamilyMember _data = new FamilyMember();
        private readonly Action<FamilyMember> _onSubmit;
        public Person SelectedPerson { get; set; }
        public ObservableCollection<Person> Persons { get; set; }

        public DeathRecord SelectedDeathRecord { get; set; }
        public ObservableCollection<DeathRecord> DeathRecords { get; set; }

        public FamilyMember FamilyMember
        {
            get => _data; set
            {
                _data = value;
                RelationshipTypeInput.Text = _data.RelationType;
            }
        }

        public AddFamilyMemberView(IEnumerable<Person> persons, IEnumerable<DeathRecord> deathRecords, Action<FamilyMember> onSubmit = null)
        {
            DataContext = this;
            Persons = new ObservableCollection<Person>(persons);
            DeathRecords = new ObservableCollection<DeathRecord>(deathRecords);

            InitializeComponent();

            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            FamilyMember.MemberId = SelectedPerson.Id;
            FamilyMember.RelatedToId = SelectedDeathRecord.PersonId;
            FamilyMember.RelationType = RelationshipTypeInput.Text;

            _onSubmit?.Invoke(FamilyMember);

            Close();
        }
    }
}