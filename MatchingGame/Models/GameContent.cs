using System;
using System.Collections.Generic;
using System.Text;

namespace MatchingGame.Models
{
    public class GameContent
    {
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public string AssetsPath { get; set; }
        public int AmountOfPictures { get; set; }

        public List<FieldItem> FieldItems = new List<FieldItem>();

        public GameContent()
        {
        }

        public override string ToString()
        {
            return $"{FolderName} [{AmountOfPictures / 2} Pairs]";
        }
    }
}
