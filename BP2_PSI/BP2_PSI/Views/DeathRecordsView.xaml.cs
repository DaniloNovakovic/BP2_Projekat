﻿using Core.Interfaces;
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

namespace BP2_PSI.Views
{
    /// <summary>
    /// Interaction logic for DeathRecordsView.xaml
    /// </summary>
    public partial class DeathRecordsView : Window
    {
        private IUnitOfWork uow;

        public DeathRecordsView()
        {
            InitializeComponent();
        }

        public DeathRecordsView(IUnitOfWork uow)
        {
            this.uow = uow;
        }
    }
}