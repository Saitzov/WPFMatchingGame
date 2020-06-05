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
        GameEditor gameEditor;

        public MainWindowViewModel()
        {            
            InitStartScreen();
            ChangeMainUserControl(startScreen);
        }        

        private void InitStartScreen()
        {
            startScreen = new StartScreen();
            this.startScreen.btn_GameStart.Click += Click_StartNewGame;
            this.startScreen.btn_CreateOwnGame.Click += Btn_CreateOwnGame_Click;
        }        

        private void ChangeMainUserControl(UserControl userControl)
        {
            this.CurrentView = userControl;
        }

        private void StartNewGame()
        {
            var gameContent = startScreen.ViewModel.SelectedFolder;

            if(gameContent != null)
            {
                gameField = new GameField(gameContent, Enums.MatchingGameEnums.FieldSize.Sixteen);
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

        private void Btn_CreateOwnGame_Click(object sender, RoutedEventArgs e)
        {
            gameEditor = new GameEditor();
            ChangeMainUserControl(gameEditor);
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
