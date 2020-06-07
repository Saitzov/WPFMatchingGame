using MatchingGame.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MatchingGame.Enums.MatchingGameEnums;

namespace MatchingGame.UserControlls
{
    /// <summary>
    /// Interaktionslogik für GameField.xaml
    /// </summary>
    public partial class GameField : UserControl
    {
        private GameViewModel gameViewModel;

        public GameField(GameContent content, GameConfig size)
        {
            InitializeComponent();
            gameViewModel = new GameViewModel(content, size);
            this.DataContext = gameViewModel;

        }

        private void Click_Field(object sender, MouseButtonEventArgs e)
        {
            if(sender is Image)
            {
                FieldItem item = (FieldItem)(sender as System.Windows.Controls.Image).DataContext;
                gameViewModel.ClickField(item.Id);
            }
        }
    }
}
