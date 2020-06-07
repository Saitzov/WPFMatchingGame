using MatchingGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MatchingGame.Views
{
    /// <summary>
    /// Interaktionslogik für AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private GameViewModel gameViewModel;


        public AdminPanel(GameViewModel gameViewModel)
        {
            InitializeComponent();
            this.gameViewModel = gameViewModel;
        }

        private void ShowAllClick(object sender, RoutedEventArgs e)
        {
            gameViewModel.TurnOverAllFields(true);
        }

        private void ShowNothing(object sender, RoutedEventArgs e)
        {
            gameViewModel.TurnOverAllFields(false);
        }

        private void ShowBonusResults(object sender, RoutedEventArgs e)
        {
            gameViewModel.BonusGame.ShowResult();
        }

        private void CloseBonus(object sender, RoutedEventArgs e)
        {
            gameViewModel.BonusGame.CloseBonusGame();
        }
    }
}
