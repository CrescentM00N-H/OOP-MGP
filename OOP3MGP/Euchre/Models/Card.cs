using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euchre.Shared;

namespace Euchre.Models
{
    public class Card
    {
        #region Class Scope Variables
        #endregion

        #region Properties
        public Constants.Suit Suit { get; set; }

        public Constants.Rank Rank { get; set; }

        public Constants.Suit  CurrentTrump { get; set; }

        public bool IsTrump { get; set; }

        #endregion


        #region Constructors
        public Card(Constants.Suit suit, Constants.Rank rank, Constants.Suit currentTump)
        {
            Suit = suit;
            Rank = rank;
            IsTrump = currentTump;
        }
        #endregion
        #region methods
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        

        #endregion

    }
}
