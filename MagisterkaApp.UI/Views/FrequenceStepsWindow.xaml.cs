using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.UI.ViewModel;
using System;
using System.Windows;

namespace MagisterkaApp.UI.Views
{
    /// <summary>
    /// Interaction logic for FrequenceStepsWindow.xaml
    /// </summary>
    public partial class FrequenceStepsWindow : Window
    {
        public FrequenceStepsWindow(Measure measure, IFrequenceStepsRepository frequenctStepsRepository)
        {
            InitializeComponent();
            DataContext = new FrequenceStepsViewModel(measure, frequenctStepsRepository);
        }
    }
}
