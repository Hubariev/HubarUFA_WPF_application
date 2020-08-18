using MagisterkaApp.Domain;
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
        public FrequenceStepsWindow(Measure measure)
        {
            InitializeComponent();
            DataContext = new FrequenceStepsViewModel(measure);
        }
    }
}
