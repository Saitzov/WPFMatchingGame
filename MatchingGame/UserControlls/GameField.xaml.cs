﻿using MatchingGame.Models;
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

        public GameField(GameContent content, FieldSize size)
        {
            InitializeComponent();
            gameViewModel = new GameViewModel(content, size);
            this.DataContext = gameViewModel;
        }
    }
}
