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

namespace MagisterkaApp.UI.Views
{
    /// <summary>
    /// Interaction logic for FilesColumnNotificationWindow.xaml
    /// </summary>
    public partial class FilesColumnNotificationWindow : Window
    {
        public FilesColumnNotificationWindow()
        {
            InitializeComponent();
        }

        private void FilesColumnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
