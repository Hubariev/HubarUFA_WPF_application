using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TEMdominantWindow.xaml
    /// </summary>
    public partial class TEMdominantWindow : Window
    {
        public TEMdominantWindow(ObservableCollection<FrequencyStep> frequencySteps)
        {
            InitializeComponent();
            DataContext = new TEMdominantViewModel(frequencySteps);
        }
    }
}
