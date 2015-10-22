using Santase.Logic.Cards;
using Santase.Logic.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic
{
    public class GameRound: IGameRound
    {
        private IDeck deck;
     
        private IPlayer firstPlayer;
        private int firstPlayerPoints;
        private IList<Card> firstPlayerCards;
        private IList<Card> firstPlayerCollectedHands;
        
        private IPlayer secondPlayer;
        private int secondPlayerPoints;
        private IList<Card> secondPlayerCards;
        private IList<Card> secondPlayerCollectedHands;

        private PlayerPosition firstToPlay;

        public GameRound(IPlayer firstPlayer, IPlayer secondPlayer, PlayerPosition firstToPlay)
        {
            this.deck = new Deck();

            this.firstPlayer = firstPlayer;
            this.firstPlayerPoints = 0;
            IList<Card> firstPlayerCards = new List<Card>();
            IList<Card> firstPlayerCollectedHands = new List<Card>();

            this.secondPlayer = secondPlayer;
            this.secondPlayerPoints = 0;
            IList<Card> secondPlayerCards = new List<Card>();
            IList<Card> secondPlayerCollectedHands = new List<Card>();

            this.firstToPlay = firstToPlay;
        }
        public void Start()
        {
            this.DealFirstCards();
            while (!this.isFinished())
            {
                this.PlayHand();
            }
        
        }

        private void PlayHand()
        {
            IGameHand hand = new GameHand();
            hand.Start();
            //TODO  update points
            //TODO add new card to the players
            this.firstToPlay = hand.Winner;
            
        }

        private bool isFinished()
        {
            if (this.FirstPlayerPoints >= 66)
            {
                return true;
            }
            if (this.SecondPlayerPoints >= 66)
            {
                return true;
            }
            if (this.firstPlayerCards.Count == 0 || this.secondPlayerCards.Count == 0)
            {
                return true;
            }
            return false;
        }

        private void DealFirstCards()
        {
            for (int i = 0; i < 3; i++)
            {
                var card = this.deck.GetNextCard();
                this.firstPlayer.AddCard(card);
            }

            for (int i = 0; i < 3; i++)
            {
                var card = this.deck.GetNextCard();
                this.secondPlayer.AddCard(card);
            }

            for (int i = 0; i < 3; i++)
            {
                var card = this.deck.GetNextCard();
                this.firstPlayer.AddCard(card);
            }

            for (int i = 0; i < 3; i++)
            {
                var card = this.deck.GetNextCard();
                this.secondPlayer.AddCard(card);
            }
        }

        public int FirstPlayerPoints
        {
            get { return firstPlayerPoints; }
        }

        public int SecondPlayerPoints
        {
            get { return secondPlayerPoints; }
        }

        public bool FirstPlayerHasHand
        {
            get { return this.firstPlayerCollectedHands.Count > 0; }
        }

        public bool SecondPlayerHasHand
        {
            get { return this.secondPlayerCollectedHands.Count > 0s; }
        }


        public PlayerPosition ClosedByPlayer
        {
            get { throw new NotImplementedException(); }
        }
    }
}
