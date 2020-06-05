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
    /// Interaktionslogik für GameEditor.xaml
    /// </summary>
    public partial class GameEditor : UserControl
    {
        private GameEditorViewModel viewModel;
        public GameEditor()
        {
            InitializeComponent();
            viewModel = new GameEditorViewModel();
            this.DataContext = viewModel;
        }
    }
}
