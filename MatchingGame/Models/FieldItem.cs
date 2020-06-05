using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MatchingGame.Models
{
    public class FieldItem : INotifyPropertyChanged
    {
        private string currentDisplayPath;
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }

        public string CurrentDisplayPath {
            get
            {
                return currentDisplayPath;
            }
            set
            {
                currentDisplayPath = value;
                OnPropertyChanged("CurrentDisplayPath");
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
