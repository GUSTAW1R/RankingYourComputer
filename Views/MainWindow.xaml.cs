using Microsoft.Win32;
using RankingYourComputer.Controllers;
using RankingYourComputer.Controllers.Resourses;
using RankingYourComputer.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RankingYourComputer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSingleton Singleton = DataSingleton.getInstance();
        ComputerController computer = new ComputerController(new ContextPhysicalPC());
        FormController form = new FormController(new ContextCurrentForm());
        DataController controller = new DataController(new ContextComputerXML());

        public MainWindow()
        {
            Downloader downloader = new Downloader("ComputerDB.db");
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen("\\Views\\Splash.png");
            splashScreen.Show(true);
            computer.SetComputer(null, null, null, null, null, null, null);
            physicalName.Text = form.SetDataToTextBox("Info", "Name", Singleton.ComputerName);
            cpuPhys.Text = form.SetDataToTextBox("Info", "CPU", Singleton.ComputerName);
            gpuPhys.Text = form.SetDataToTextBox("Info", "GPU", Singleton.ComputerName);
            ramPhys.Text = form.SetDataToTextBox("Info", "RAM", Singleton.ComputerName);
            hardPhys.Text = form.SetDataToTextBox("Info", "HARD", Singleton.ComputerName);
            osPhys.Text = form.SetDataToTextBox("Info", "OS", Singleton.ComputerName);
            splashScreen.Close(TimeSpan.FromSeconds(0));
        }

        private void addVirtualPC(object sender, RoutedEventArgs e)
        {
            AddVirtualComputerWindow window = new AddVirtualComputerWindow();
            window.ShowDialog();
            if (window.DialogResult == false)
            {
                computer = new ComputerController(new ContextVirtualPC());
                virtualName.ItemsSource = computer.GetListComputers();
                virtualName.SelectedItem = Singleton.ComputerName;
            }
        }

        private void importVirtualPC(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            computer = new ComputerController(new ContextVirtualPC());
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                try
                {
                    controller.SetData(path);
                    virtualName.ItemsSource = computer.GetListComputers();
                    virtualName.SelectedItem = Singleton.ComputerName;
                }
                catch
                {
                    MessageBox.Show("Не удалось импортировать конфигурацию" +
                        "\nПроверьте качество импортируемого файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void getResultRating(object sender, RoutedEventArgs e)
        {
            RankingWindow window = new RankingWindow();
            window.Show();
        }

        private void editConfigurationPC(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Функция временно недоступна", "Функция недоступна",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            //EditVirtualComputerWindow window = new EditVirtualComputerWindow();
            //window.Show();
        }

        private void estimatePC(object sender, RoutedEventArgs e)
        {
            ExplaneWindow window = new ExplaneWindow();
            window.Show();
            //MessageBoxResult result = MessageBox.Show("Функция будет доступна с глобальным патчем 1.0.0" +
            //    "\nДата выхода: 25 мая 2021 года", "Функция недоступна",
            //                              MessageBoxButton.OK,
            //                              MessageBoxImage.Information);
        }

        private void compareWithPC(object sender, RoutedEventArgs e)
        {
            CompareConfigurationsWindow window = new CompareConfigurationsWindow();
            window.Show();
            //MessageBoxResult result = MessageBox.Show("Функция будет доступна с глобальным патчем 1.0.0" +
            //    "\nДата выхода: 25 мая 2021 года", "Функция недоступна",
            //                              MessageBoxButton.OK,
            //                              MessageBoxImage.Information);
        }

        private void compareWithGame(object sender, RoutedEventArgs e)
        {
            CompareWithGameWindow window = new CompareWithGameWindow();
            window.ShowDialog();
            if (window.DialogResult == false)
            {
                computer = new ComputerController(new ContextVirtualPC());
                virtualName.ItemsSource = computer.GetListComputers();
                virtualName.SelectedItem = Singleton.ComputerName;
            }
        }

        private void exportPC(object sender, RoutedEventArgs e)
        {
            ExportWindow window = new ExportWindow();
            window.ShowDialog();
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            OptionsWindow window = new OptionsWindow();
            window.Show();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void questionCPU(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(cpuPhys.Text);
        }

        private void questionGPU(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(gpuPhys.Text);
        }

        private void questionRAM(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(ramPhys.Text);
        }

        private void questionHARD(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(hardPhys.Text);
        }

        private void questionOS(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(osPhys.Text);
        }

        private void questionCPU1(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(cpuVirt.Text);
        }

        private void questionGPU1(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(gpuVirt.Text);
        }

        private void questionRAM1(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(ramVirt.Text);
        }

        private void questionHARD1(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(hardVirt.Text);
        }

        private void questionOS1(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(osVirt.Text);
        }

        private void virtualName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            gpuVirt.Text = form.SetDataToTextBox("Info", "GPU", virtualName.SelectedItem.ToString());
            cpuVirt.Text = form.SetDataToTextBox("Info", "CPU", virtualName.SelectedItem.ToString());
            ramVirt.Text = form.SetDataToTextBox("Info", "RAM", virtualName.SelectedItem.ToString());
            hardVirt.Text = form.SetDataToTextBox("Info", "HARD", virtualName.SelectedItem.ToString());
            osVirt.Text = form.SetDataToTextBox("Info", "OS", virtualName.SelectedItem.ToString());
        }
    }
}

