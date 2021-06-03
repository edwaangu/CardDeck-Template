using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace CardDeck
{
    public partial class Form1 : Form
    {
        //standard deck of cards
        List<string> deck = new List<string>();
        List<string> dealerCards = new List<string>();
        List<string> playerCards = new List<string>();

        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();

            //fill deck with standard 52 cards
            //H - Hearts, D - Diamonds, C - Clubs, S - Spades
            
            deck.AddRange(new string[] { "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "10H", "JH", "QH", "KH", "AH" });
            deck.AddRange(new string[] { "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "10D", "JD", "QD", "KD", "AD" });
            deck.AddRange(new string[] { "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "JC", "QC", "KC", "AC" });
            deck.AddRange(new string[] { "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "10S", "JS", "QS", "KS", "AS" });

            shuffleButton.Enabled = true;
            dealButton.Enabled = false;
            collectButton.Enabled = false;
        }

        private void shuffleButton_Click(object sender, EventArgs e)
        {
            //re-arrange the order of the cards in the deck list
            List<string> tempDeck = new List<string>();
            while(deck.Count > 0)
            {
                int nextCard = randGen.Next(0, deck.Count);
                tempDeck.Add(deck[nextCard]);
                deck.RemoveAt(nextCard);
            }
            deck = tempDeck;
            shuffleButton.Enabled = false;
            dealButton.Enabled = true;
        }

        private void dealButton_Click(object sender, EventArgs e)
        {
            //deal 5 cards each to dealer and player and display them
            for(int i = 0;i < 5;i++)
            {
                playerCards.Add(deck[0]);
                deck.RemoveAt(0);
                dealerCards.Add(deck[0]);
                deck.RemoveAt(0);
            }

            playerCardsLabel.Text = "";
            dealerCardsLabel.Text = "";
            for (int i = 0;i < playerCards.Count;i++)
            {
                playerCardsLabel.Text += $"{playerCards[i]} ";
                this.Refresh();
                Thread.Sleep(250);
                dealerCardsLabel.Text += $"{dealerCards[i]} ";
                this.Refresh();
                Thread.Sleep(250);
            }

            dealButton.Enabled = false;
            collectButton.Enabled = true;
        }

        private void collectButton_Click(object sender, EventArgs e)
        {
            //put player and dealer cards back into the deck
            for (int i = 0; i < playerCards.Count; i++)
            {
                deck.Add(playerCards[i]);
            }
            for (int i = 0; i < dealerCards.Count; i++)
            {
                deck.Add(dealerCards[i]);
            }

            playerCards.Clear();
            dealerCards.Clear();

            playerCardsLabel.Text = "--";
            dealerCardsLabel.Text = "--";

            collectButton.Enabled = false;
            shuffleButton.Enabled = true;
        }
    }
}
