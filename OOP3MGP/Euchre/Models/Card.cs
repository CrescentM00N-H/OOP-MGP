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

        public string Rank { get; set; }

        public bool IsTrump { get; set; }

        #endregion
    }
}
