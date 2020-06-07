using MatchingGame.Interface;
using MatchingGame.Models;
using MatchingGame.ViewModels;
using MatchingGame.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MatchingGame.Enums.MatchingGameEnums;

namespace MatchingGame.UserControlls
{
    /// <summary>
    /// Interaktionslogik für GameField28.xaml
    /// </summary>
    public partial class GameField28 : UserControl, IGameField
    {
        private GameViewModel gameViewModel;
        public GameField28(GameContent content, GameConfig config)
        {
            InitializeComponent();
            gameViewModel = new GameViewModel(content, config);
            this.DataContext = gameViewModel;

            if(config.WithAdminPanel)
            {
                AdminPanel adminPanel = new AdminPanel(gameViewModel);
                adminPanel.Show();
            }

        }

        void Click_Field(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image)
            {
                var img = sender as Image;
                FieldItem item = (FieldItem)img.DataContext;
                gameViewModel.ClickField(item.Id);
            }
        }
    }


}
