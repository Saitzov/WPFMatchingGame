using MatchingGame.Models;
using MatchingGame.UserControlls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MatchingGame.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        UserControl currentView;
        public UserControl CurrentView { get { return currentView; } set {currentView = value;NotifyPropertyChanged(); } }


        ViewConfig ViewConfig = new ViewConfig();

        StartScreen startScreen;
        GameField gameField;

        public MainWindowViewModel()
        {            
            InitStartScreen();
            ChangeMainUserControl(startScreen);
        }        

        private void InitStartScreen()
        {
            startScreen = new StartScreen();
            this.startScreen.btn_GameStart.Click += Click_StartNewGame;
        }

        private void ChangeMainUserControl(UserControl userControl)
        {
            this.CurrentView = userControl;
        }

        private void StartNewGame()
        {
            var gameFolder = startScreen.ViewModel.SelectedFolder;

            if(!string.IsNullOrWhiteSpace(gameFolder))
            {
                gameField = new GameField(gameFolder);
                gameField.btn_back.Click += Btn_back_Click;
                ChangeMainUserControl(gameField);
            }
            else
            {
                MessageBox.Show("Please Select a Folder");
            }
            
        }


        #region Event Handling
        private void Click_StartNewGame(object sender, System.Windows.RoutedEventArgs e)
        {
            StartNewGame();
        }


        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            ChangeMainUserControl(startScreen);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
