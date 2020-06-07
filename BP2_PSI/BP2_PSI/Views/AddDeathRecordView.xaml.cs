﻿using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace BP2_PSI.Views
{
    /// <summary>
    /// Interaction logic for AddDeathRecordView.xaml
    /// </summary>
    public partial class AddDeathRecordView : Window
    {
        private DeathRecord _data = new DeathRecord();
        private readonly Action<DeathRecord> _onSubmit;
        public Person SelectedPerson { get; set; }
        public ObservableCollection<Person> Persons { get; set; }

        public DeathRecord DeathRecord
        {
            get => _data; set
            {
                _data = value;
                DeathDateInput.Text = _data.DeathDate.Value.ToString(DeathDateInput.FormatString);
            }
        }

        public AddDeathRecordView(IEnumerable<Person> Persons, Action<DeathRecord> onSubmit = null)
        {
            InitializeComponent();

            _onSubmit = onSubmit;
            this.Persons = new ObservableCollection<Person>(Persons);
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            DeathRecord.PersonId = SelectedPerson.Id;
            DeathRecord.DeathDate = DateTime.Parse(DeathDateInput.Text);

            _onSubmit?.Invoke(DeathRecord);

            Close();
        }
    }
}