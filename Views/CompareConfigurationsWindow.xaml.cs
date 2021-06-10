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
    /// Логика взаимодействия для CompareConfigurationsWindow.xaml
    /// </summary>
    public partial class CompareConfigurationsWindow : Window
    {
        ComputerController computer = new ComputerController(new ContextPhysicalPC());
        FormController form = new FormController(new ContextCurrentForm());

        public CompareConfigurationsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            configName1.ItemsSource = computer.GetListComputers();
            configName2.ItemsSource = computer.GetListComputers();
        }

        private void configName1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> vs = computer.GetListComputers();
            if (configName1.SelectedItem != null)
            {
                vs.Remove(configName1.SelectedItem.ToString());
                configName2.ItemsSource = vs;
                cpu1.Text = form.SetDataToTextBox("Info", "CPU", configName1.SelectedItem.ToString());
                gpu1.Text = form.SetDataToTextBox("Info", "GPU", configName1.SelectedItem.ToString());
                ram1.Text = form.SetDataToTextBox("Info", "RAM", configName1.SelectedItem.ToString());
                hard1.Text = form.SetDataToTextBox("Info", "HARD", configName1.SelectedItem.ToString());
                os1.Text = form.SetDataToTextBox("Info", "OS", configName1.SelectedItem.ToString());
            }
            configName2.ItemsSource = vs;
        }

        private void configName2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> vs = computer.GetListComputers();
            if (configName2.SelectedItem != null)
            {
                vs.Remove(configName2.SelectedItem.ToString());
                configName1.ItemsSource = vs;
                cpu2.Text = form.SetDataToTextBox("Info", "CPU", configName2.SelectedItem.ToString());
                gpu2.Text = form.SetDataToTextBox("Info", "GPU", configName2.SelectedItem.ToString());
                ram2.Text = form.SetDataToTextBox("Info", "RAM", configName2.SelectedItem.ToString());
                hard2.Text = form.SetDataToTextBox("Info", "HARD", configName2.SelectedItem.ToString());
                os2.Text = form.SetDataToTextBox("Info", "OS", configName2.SelectedItem.ToString());
            }
            configName1.ItemsSource = vs;
        }

        private void Compare(object sender, RoutedEventArgs e)
        {
            if (configName1.SelectedItem != null && configName2.SelectedItem != null)
            {
                //CPU//
                cpu1rank.Text = form.SetDataToTextBox("Rank", "CPU", configName1.SelectedItem.ToString());
                cpu2rank.Text = form.SetDataToTextBox("Rank", "CPU", configName2.SelectedItem.ToString());
                cpu1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(cpu1rank.Text, cpu2rank.Text, null, null).ElementAt(0)));
                cpu2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(cpu1rank.Text, cpu2rank.Text, null, null).ElementAt(1)));
                cpu1rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(cpu1rank.Text, cpu2rank.Text, null, null).ElementAt(0)));
                cpu2rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(cpu1rank.Text, cpu2rank.Text, null, null).ElementAt(1)));
                ////GPU//
                gpu1rank.Text = form.SetDataToTextBox("Rank", "GPU", configName1.SelectedItem.ToString());
                gpu2rank.Text = form.SetDataToTextBox("Rank", "GPU", configName2.SelectedItem.ToString());
                gpu1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(gpu1rank.Text, gpu2rank.Text, null, null).ElementAt(0)));
                gpu2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(gpu1rank.Text, gpu2rank.Text, null, null).ElementAt(1)));
                gpu1rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(gpu1rank.Text, gpu2rank.Text, null, null).ElementAt(0)));
                gpu2rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(gpu1rank.Text, gpu2rank.Text, null, null).ElementAt(1)));
                ////RAM//
                ram1rank.Text = form.SetDataToTextBox("Rank", "RAM", configName1.SelectedItem.ToString());
                ram2rank.Text = form.SetDataToTextBox("Rank", "RAM", configName2.SelectedItem.ToString());
                ram1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(ram1rank.Text, ram2rank.Text, null, null).ElementAt(0)));
                ram2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(ram1rank.Text, ram2rank.Text, null, null).ElementAt(1)));
                ram1rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(ram1rank.Text, ram2rank.Text, null, null).ElementAt(0)));
                ram2rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(ram1rank.Text, ram2rank.Text, null, null).ElementAt(1)));
                ////HARD//
                hard1rank.Text = form.SetDataToTextBox("Rank", "HARD", configName1.SelectedItem.ToString());
                hard2rank.Text = form.SetDataToTextBox("Rank", "HARD", configName2.SelectedItem.ToString());
                hard1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(hard1rank.Text, hard2rank.Text, null, null).ElementAt(0)));
                hard2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(hard1rank.Text, hard2rank.Text, null, null).ElementAt(1)));
                hard1rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(hard1rank.Text, hard2rank.Text, null, null).ElementAt(0)));
                hard2rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(hard1rank.Text, hard2rank.Text, null, null).ElementAt(1)));
                ////RANK//
                pc1rank.Text = form.SetDataToTextBox("Rank", "RANK", configName1.SelectedItem.ToString());
                pc2rank.Text = form.SetDataToTextBox("Rank", "RANK", configName2.SelectedItem.ToString());
                pc1rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(pc1rank.Text, pc2rank.Text, null, null).ElementAt(0)));
                pc2rank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(form.SetDataToForm(pc1rank.Text, pc2rank.Text, null, null).ElementAt(1)));
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Ошибка при сравнении конфигураций" +
                "\nПроверьте наличие двух конфигураций", "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
            }

        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            configName1.SelectedItem = null; configName2.SelectedItem = null;
            cpu1.Text = null; cpu1.Background = new SolidColorBrush(Colors.White);
            cpu2.Text = null; cpu2.Background = new SolidColorBrush(Colors.White);
            gpu1.Text = null; gpu1.Background = new SolidColorBrush(Colors.White);
            gpu2.Text = null; gpu2.Background = new SolidColorBrush(Colors.White);
            ram1.Text = null; ram1.Background = new SolidColorBrush(Colors.White);
            ram2.Text = null; ram2.Background = new SolidColorBrush(Colors.White);
            hard1.Text = null; hard1.Background = new SolidColorBrush(Colors.White);
            hard2.Text = null; hard2.Background = new SolidColorBrush(Colors.White);
            os1.Text = null; os2.Text = null;
            cpu1rank.Text = null; cpu1rank.Background = new SolidColorBrush(Colors.White);
            cpu2rank.Text = null; cpu2rank.Background = new SolidColorBrush(Colors.White);
            gpu1rank.Text = null; gpu1rank.Background = new SolidColorBrush(Colors.White);
            gpu1.Text = null; gpu2rank.Background = new SolidColorBrush(Colors.White);
            ram1rank.Text = null; ram1rank.Background = new SolidColorBrush(Colors.White);
            ram2rank.Text = null; ram2rank.Background = new SolidColorBrush(Colors.White);
            hard1rank.Text = null; hard1rank.Background = new SolidColorBrush(Colors.White);
            hard2rank.Text = null; hard2rank.Background = new SolidColorBrush(Colors.White);
            pc1rank.Text = null; pc1rank.Background = new SolidColorBrush(Colors.White);
            pc2rank.Text = null; pc2rank.Background = new SolidColorBrush(Colors.White);
        }
    }
}
