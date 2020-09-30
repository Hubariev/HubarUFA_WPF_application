using MagisterkaApp.UI.ViewModel;
using System.Windows;
using System.Windows.Controls;

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

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString());
            var dataContext = DataContext as ApplicationViewModel;
            
        }
    }
}
