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

namespace MatchingGame.UserControlls
{
    /// <summary>
    /// Interaktionslogik für StartScreen.xaml
    /// </summary>
    public partial class StartScreen : UserControl
    {
        public StartScreenViewModel ViewModel { get; set; }
        public StartScreen()
        {
            InitializeComponent();
            this.ViewModel = new StartScreenViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}
