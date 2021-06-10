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
    /// Логика взаимодействия для CompareWithGameWindow.xaml
    /// </summary>
    public partial class CompareWithGameWindow : Window
    {
        ComputerController computer = new ComputerController(new ContextPhysicalPC());
        FormController form = new FormController(new ContextDatabaseForm());
        DataController data = new DataController(new ContextDatаGame());
        DataSingleton Singleton = DataSingleton.getInstance();

        public CompareWithGameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pcName.ItemsSource = computer.GetListComputers();
            data.SetData(Environment.CurrentDirectory + "\\" + "GameData.csv");
            form.SetDataToDataGrid(gameData, "Start Info", null, Singleton.Games);
        }

        private void Compare(object sender, RoutedEventArgs e)
        {
            try
            {
                form.SetDataToDataGrid(gameData, "Compare Info", computer.GetComputer(pcName.SelectedItem.ToString()), Singleton.Games);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Ошибка при сравнении" +
                "\nУбедитесь, что выбрана конфигурация", "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
            }

        }

        private void gameData_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Singleton.GamePosition = gameData.Items.IndexOf(gameData.SelectedCells[0].Item) + 1;
            GameWindow window = new GameWindow();
            window.ShowDialog();
            if (window.DialogResult == false)
            {
                computer = new ComputerController(new ContextPhysicalPC());
                pcName.ItemsSource = computer.GetListComputers();
                pcName.SelectedItem = Singleton.ComputerName;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
