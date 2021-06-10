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
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        DataSingleton Singleton = DataSingleton.getInstance();
        FormController form = new FormController(new ContextCurrentForm());
        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gameName.Text = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.Name).FirstOrDefault();
            gameGenre.Text = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.Genre).FirstOrDefault();
            form.SetDataToDataGrid(minRequier, "Min", null, null);
            form.SetDataToDataGrid(recRequier, "Rec", null, null);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Searcher searcher = new Searcher(gameName.Text);
        }

        private void CreateConfiguration(object sender, RoutedEventArgs e)
        {
            GameConfigurationWindow window = new GameConfigurationWindow();
            window.ShowDialog();
        }
    }
}
