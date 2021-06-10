using RankingYourComputer.Controllers;
using RankingYourComputer.Controllers.Resourses;
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
    /// Логика взаимодействия для RankingWindow.xaml
    /// </summary>
    public partial class RankingWindow : Window
    {
        FormController form = new FormController(new ContextCurrentForm());
        ComputerController computer = new ComputerController(new ContextPhysicalPC());
        public RankingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.ItemsSource = computer.GetListComputers();
        }

        private void Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculatorRank calculator = new CalculatorRank();
            gpuName.Text = form.SetDataToTextBox("Info", "GPU", Name.SelectedItem.ToString());
            cpuName.Text = form.SetDataToTextBox("Info", "CPU", Name.SelectedItem.ToString());
            ramName.Text = form.SetDataToTextBox("Info", "RAM", Name.SelectedItem.ToString());
            hardName.Text = form.SetDataToTextBox("Info", "HARD", Name.SelectedItem.ToString());
            os.Text = form.SetDataToTextBox("Info", "OS", Name.SelectedItem.ToString());
            cpuValue.Text = form.SetDataToTextBox("Rank", "CPU", Name.SelectedItem.ToString());
            gpuValue.Text = form.SetDataToTextBox("Rank", "GPU", Name.SelectedItem.ToString());
            ramValue.Text = form.SetDataToTextBox("Rank", "RAM", Name.SelectedItem.ToString());
            hardValue.Text = form.SetDataToTextBox("Rank", "HARD", Name.SelectedItem.ToString());
            ranking.Text = form.SetDataToTextBox("Rank", "RANK", Name.SelectedItem.ToString()) + " / " + calculator.GetMaxRank();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
