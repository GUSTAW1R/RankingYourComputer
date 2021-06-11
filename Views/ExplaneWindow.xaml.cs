using RankingYourComputer.Controllers;
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

namespace RankingYourComputer.Views
{
    /// <summary>
    /// Логика взаимодействия для ExplaneWindow.xaml
    /// </summary>
    public partial class ExplaneWindow : Window
    {
        ComputerController computer = new ComputerController(new ContextPhysicalPC());

        public ExplaneWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            namePC.ItemsSource = computer.GetListComputers();
        }

        private void Explane(object sender, RoutedEventArgs e)
        {
            ExplaneController explane = new ExplaneController(computer.GetComputer(namePC.SelectedItem.ToString()));
            explaneCPU.ItemsSource = explane.Result();
        }
    }
}
