using Microsoft.Win32;
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
    /// Логика взаимодействия для GameConfigurationWindow.xaml
    /// </summary>
    public partial class GameConfigurationWindow : Window
    {
        DataSingleton Singleton;
        FormController form = new FormController(new ContextDatabaseForm());
        public GameConfigurationWindow()
        {
            Singleton = DataSingleton.getInstance();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = form.SetDataToTextBox("Name", null, null);
            cpuName.Text = form.SetDataToTextBox("CPU", null, null);
            gpuName.Text = form.SetDataToTextBox("GPU", cpuName.Text, null);
            ramName.Text = form.SetDataToTextBox("RAM", cpuName.Text, null);
            hardName.Text = form.SetDataToTextBox("HARD", null, null);
            os.Text = form.SetDataToTextBox("OS", null, null);
            ComputerController computer = new ComputerController(new ContextVirtualPC());
            string[] str = os.Text.Split(',');
            if (Singleton.Computers.Where(n => n.Name.Contains(Name.Text)).FirstOrDefault() != null)
            {
                MessageBoxResult result = MessageBox.Show("Текущая конфигурация уже есть в списке конфигураций" +
            "\nПоэтому она не будет добавлена", "Внимание",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Warning);
            }
            else
            {
                computer.SetComputer(Name.Text, cpuName.Text, gpuName.Text, ramName.Text, hardName.Text, str[0], str[1]);
                MessageBox.Show("Конфигурация успешно сохранена в списке конфигураций", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveToFile(object sender, RoutedEventArgs e)
        {
            DataController data = new DataController(new ContextComputerXML());
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                try
                {
                    data.SaveConfiguration(path, Name.Text);
                    MessageBox.Show("Конфигурация успешно экспортирована", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось экспортировать конфигурацию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }
}
