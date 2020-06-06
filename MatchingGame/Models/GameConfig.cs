using System;
using System.Collections.Generic;
using System.Text;
using static MatchingGame.Enums.MatchingGameEnums;

namespace MatchingGame.Models
{
    public class GameConfig
    {
        public bool WithBonusGame { get; set; }
        public bool WithAdminPanel { get; set; }
        public FieldSize FieldSize { get; set; }
    }
}
