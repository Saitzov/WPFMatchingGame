using MatchingGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MatchingGame.Extension;
using static MatchingGame.Enums.MatchingGameEnums;

namespace MatchingGame.ViewModels
{
    public class GameViewModel
    {
        private const string fieldItemFile = "FieldItems.csv";
        private const string fieldPairFile = "FieldPairs.csv";
        private GameContent Content { get; set; }
        private List<FieldItem> FieldItems { get; set; }
        private List<FieldPairs> FieldPairs { get; set; }
        private FieldSize FieldSize { get; set; }

        public GameViewModel(GameContent content, FieldSize fieldSize)
        {
            this.Content = content;
            this.FieldSize = fieldSize;
            InitGame();
        }

        private void InitGame()
        {
            try
            {
                this.FieldItems = this.LoadFieldItems();
                this.FieldPairs = this.LoadFieldPairs();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private List<FieldPairs> LoadFieldPairs()
        {
            List<FieldPairs> res = new List<FieldPairs>();

            var path = this.Content.FolderPath + "\\" + fieldPairFile;
            using (var reader = new StreamReader(path))
            {
                var headLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    FieldPairs pair = new FieldPairs();

                    pair.FieldA = this.FieldItems.First(i => i.Id == Convert.ToInt32(values[0]));
                    pair.FieldB = this.FieldItems.First(i => i.Id == Convert.ToInt32(values[1]));
                    pair.Description = values[2];

                    res.Add(pair);
                }

                res.Shuffle();

                int takeAmount = (int)this.FieldSize/ 2;
                res = res.Take(takeAmount).ToList();

                return res;
            }
        }

        private List<FieldItem> LoadFieldItems()
        {            
            List<FieldItem> res = new List<FieldItem>();

            var path = this.Content.FolderPath + "\\" + fieldItemFile;

            using (var reader = new StreamReader(path))
            {
                var headLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {                    
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    FieldItem item = new FieldItem();
                    item.Id = Convert.ToInt32(values[0]);
                    item.FileName = values[1];
                    item.Description = values[2];
                    item.PicturePath = this.Content.AssetsPath + "\\" + item.FileName+ ".png";

                    res.Add(item);
                }
            }

            return res;
        }

        
    }
}
