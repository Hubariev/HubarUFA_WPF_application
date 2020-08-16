using MagisterkaApp.UI.ViewModel;
using System.Windows;

namespace MagisterkaApp.UI.Views
{
    /// <summary>
    /// Interaction logic for MeasureWindow.xaml
    /// </summary>
    public partial class MeasureWindow : Window
    {
        public MeasureWindow(ApplicationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
