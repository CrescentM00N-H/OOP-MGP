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

            List<Card> dealtCards = deck.Deal(5);
            player.AddCards(dealtCards);



            Card.ShowPlayerHand(
               player.Hand,
               pnlPlayerCard1,
               pnlPlayerCard2,
               pnlPlayerCard3,
               pnlPlayerCard4,
               pnlPlayerCard5);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settingsForm = new frmSettings();
            settingsForm.Show();        // Show the new form
        }
    }
}

