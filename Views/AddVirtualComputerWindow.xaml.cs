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
    /// Логика взаимодействия для AddVirtualComputerWindow.xaml
    /// </summary>
    public partial class AddVirtualComputerWindow : Window
    {
        FormController form = new FormController(new ContextDatabaseForm());
        public AddVirtualComputerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cpuVender.ItemsSource = form.SetDataToForm(null, null, "InfoCPU", "Vender");
            gpuVender.ItemsSource = form.SetDataToForm(null, null, "InfoGPU", "Vender");
            hardTypeConn.ItemsSource = form.SetDataToForm(null, null, "InfoHARD", "Type");
            osName.ItemsSource = form.SetDataToForm(null, null, "OS", "Name");
        }

        private void cpuVender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cpuGeneration.ItemsSource = null;
            cpuGeneration.ItemsSource = form.SetDataToForm(cpuVender.SelectedItem.ToString(), null, "InfoCPU", "Generation");
        }

        private void cpuGeneration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cpuName.ItemsSource = null;
            gpuSeries.ItemsSource = null;
            gpuName.ItemsSource = null;
            if (!cpuGeneration.Items.IsEmpty)
            {
                cpuName.ItemsSource = form.SetDataToForm(cpuGeneration.SelectedItem.ToString(), null, "InfoCPU", "Name");
            }
        }

        private void cpuName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gpuSeries.ItemsSource = null;
            gpuName.ItemsSource = null;
            if (!cpuName.Items.IsEmpty)
            {
                gpuVender.ItemsSource = form.SetDataToForm(cpuName.SelectedItem.ToString(), null, "InfoGPU", "Vender");
                ramType.ItemsSource = form.SetDataToForm(cpuName.SelectedItem.ToString(), null, "RAM", "Type");
            }
        }

        private void gpuVender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gpuSeries.ItemsSource = null;
            if (gpuVender.SelectedItem != null)
            {
                gpuSeries.ItemsSource = form.SetDataToForm(gpuVender.SelectedItem.ToString(), cpuName.SelectedItem.ToString(), "InfoGPU", "Generation");
            }
        }

        private void gpuSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gpuName.ItemsSource = null;
            if (gpuSeries.SelectedItem != null)
            {
                gpuName.ItemsSource = form.SetDataToForm(gpuSeries.SelectedItem.ToString(), cpuName.SelectedItem.ToString(), "InfoGPU", "Name");
            }
        }

        private void ramType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ramCapacity.ItemsSource = form.SetDataToForm(ramType.SelectedItem.ToString(), null, "RAM", "Value");
        }

        private void ramCapacity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ramFrequency.ItemsSource = null;
            ramFrequency.ItemsSource = form.SetDataToForm(ramCapacity.SelectedItem.ToString(), ramType.SelectedItem.ToString(), "RAM", "Frequency");
        }

        private void hardTypeConn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hardVender.ItemsSource = null;
            hardVender.ItemsSource = form.SetDataToForm(hardTypeConn.SelectedItem.ToString(), null, "InfoHARD", "Company");
        }

        private void hardVender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hardCapacity.ItemsSource = null;
            if (hardVender.SelectedItem != null)
            {
                hardCapacity.ItemsSource = form.SetDataToForm(hardVender.SelectedItem.ToString(), hardTypeConn.SelectedItem.ToString(), "InfoHARD", "Value");
            }
        }

        private void hardCapacity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hardCode.ItemsSource = null;
            if (hardCapacity.SelectedItem != null)
            {
                hardCode.ItemsSource = form.SetDataToForm(hardCapacity.SelectedItem.ToString(), hardVender.SelectedItem.ToString() + "&" + hardTypeConn.SelectedItem.ToString(), "InfoHARD", "Name");
            }
        }

        private void osName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            osVersion.ItemsSource = null;
            osVersion.ItemsSource = form.SetDataToForm(osName.SelectedItem.ToString(), null, "OS", "Version");
        }

        private void addVirtualPC(object sender, RoutedEventArgs e)
        {
            ComputerController computer = new ComputerController(new ContextVirtualPC());
            if (namePC.Text != null && cpuName.SelectedItem != null && gpuName.SelectedItem != null && ramFrequency.SelectedItem != null && hardCode.SelectedItem != null && osVersion.SelectedItem != null)
            {
                computer.SetComputer(namePC.Text, cpuName.SelectedItem.ToString(), gpuName.SelectedItem.ToString(),
                ramType.SelectedItem.ToString() + ", " + ramCapacity.SelectedItem.ToString() + " GB, " + ramFrequency.SelectedItem.ToString() + " MHz",
                hardCode.SelectedItem.ToString(), osName.SelectedItem.ToString(), osVersion.SelectedItem.ToString());
                MessageBox.Show("Компьютер успешно добавлен", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();
            }

            else
            {
                MessageBoxResult result = MessageBox.Show("Ошибка при добавлении компьютера" +
                "\nПроверьте заполненность полей", "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
