using Euchre.Models;
using Euchre.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euchre.Views
{
    public partial class frmEuchre : Form
    {
        // Declare instance fields for the deck and player.
        private Deck deck;
        private Player player;

        public frmEuchre()
        {
            InitializeComponent();

            deck = new Deck();
            deck.Shuffle();

            
            player = new Player("Player 1", new List<Card>(), 0);
        }

        private void frmEuchre_Load(object sender, EventArgs e)
        {

            List<Card> playerCards = new List<Card>
            {
                new Card(Constants.Suit.Spades, Constants.Rank.Ace)
                // ... add additional test cards if needed.
            };

           
            Card.ShowPlayerHand(playerCards, pnlPlayerCard1, pnlPlayerCard2, pnlPlayerCard3, pnlPlayerCard4, pnlPlayerCard5);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settingsForm = new frmSettings();
            settingsForm.Show();        // Show the new form
        }
    }
}

