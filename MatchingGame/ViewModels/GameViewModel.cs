using MatchingGame.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchingGame.ViewModels
{
    public class GameViewModel
    {
        private string Folder { get; set; }

        public GameViewModel(string folder)
        {
            this.Folder = folder;
        }
    }
}
