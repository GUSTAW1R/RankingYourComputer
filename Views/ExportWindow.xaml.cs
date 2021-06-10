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
    /// Логика взаимодействия для ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        ComputerController computer = new ComputerController(new ContextVirtualPC());
        public ExportWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            namePC.ItemsSource = computer.GetListComputers();
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            DataController controller = new DataController(new ContextComputerXML());
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                try
                {
                    controller.SaveConfiguration(path, namePC.SelectedItem.ToString());
                    MessageBox.Show("Конфигурация успешно экспортирована", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось экспортировать конфигурацию" +
                        "\nНеобходимо выбрать конфигурацию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }
}
