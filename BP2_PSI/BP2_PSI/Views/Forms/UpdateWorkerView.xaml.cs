using Core.Entities;
using System;
using System.Windows;

namespace BP2_PSI.Views.Forms
{
    /// <summary>
    /// Interaction logic for UpdateWorkerView.xaml
    /// </summary>
    public partial class UpdateWorkerView : Window
    {
        private readonly Action<Worker> _onSubmit;
        private Worker _data = new Worker();

        public Worker Worker
        {
            get => _data; set
            {
                _data = value;
                WorktimeInput.Text = _data.WorkTime ?? "";
            }
        }

        public UpdateWorkerView(Worker worker, Action<Worker> onSubmit = null)
        {
            DataContext = this;

            InitializeComponent();

            Worker = worker;
            _onSubmit = onSubmit;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            Worker.WorkTime = WorktimeInput.Text;

            _onSubmit?.Invoke(Worker);

            Close();
        }
    }
}