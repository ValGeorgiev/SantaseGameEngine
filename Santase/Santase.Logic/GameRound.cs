﻿using Santase.Logic.Cards;
using Santase.Logic.Players;
using Santase.Logic.RoundStates;
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

        private BaseRoundState state;

        private PlayerPosition gameClosedBy;

        public GameRound(IPlayer firstPlayer, IPlayer secondPlayer, PlayerPosition firstToPlay)
        {
            this.deck = new Deck();

            this.firstPlayer = firstPlayer;
            this.firstPlayerPoints = 0;
            this.firstPlayerCards = new List<Card>();
            this.firstPlayerCollectedHands = new List<Card>();

            this.secondPlayer = secondPlayer;
            this.secondPlayerPoints = 0;
            this.secondPlayerCards = new List<Card>();
            this.secondPlayerCollectedHands = new List<Card>();

            this.firstToPlay = firstToPlay;

            this.SetState(new StartRoundState(this));

            this.gameClosedBy = PlayerPosition.NoOne;
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
            IGameHand hand = new GameHand(
                this.firstToPlay,
                this.firstPlayer, 
                this.secondPlayer, 
                this.state,
                this.deck);
            hand.Start();

            this.UpdatePoint(hand);
            
            if (hand.Winner == PlayerPosition.FirstPlayer)
            {
                firstPlayerCollectedHands.Add(hand.FirstPlayerCard);
                firstPlayerCollectedHands.Add(hand.SecondPlayerCard);
            }
            else
            {

                secondPlayerCollectedHands.Add(hand.FirstPlayerCard);
                secondPlayerCollectedHands.Add(hand.SecondPlayerCard);
            }

            this.firstToPlay = hand.Winner;

            this.firstPlayerCards.Remove(hand.FirstPlayerCard);
            this.secondPlayerCards.Remove(hand.SecondPlayerCard);
            
            this.DrawNewCards();

            this.state.PlayHand(this.deck.CardsLeft);

            if (hand.GameClosedBy == PlayerPosition.FirstPlayer 
                || hand.GameClosedBy == PlayerPosition.SecondPlayer)
            {
                this.state.Close();
                this.gameClosedBy = hand.GameClosedBy;
            }
            
        }

        private void DrawNewCards()
        {

            if (this.state.ShouldDrawCard)
            {
                if (this.firstToPlay == PlayerPosition.FirstPlayer)
                {
                    GiveCardToFirstPlayer();
                    GiveCardToSecondPlayer();
                }
                else
                {
                    GiveCardToSecondPlayer();
                    GiveCardToFirstPlayer();
                }
            }
        }

        private void UpdatePoint(IGameHand hand)
        {
            if (hand.Winner == PlayerPosition.FirstPlayer)
            {
                this.firstPlayerPoints += hand.FirstPlayerCard.GetValue();
                this.firstPlayerPoints += hand.SecondPlayerCard.GetValue();
            }
            else
            {
                this.secondPlayerPoints += hand.FirstPlayerCard.GetValue();
                this.secondPlayerPoints += hand.SecondPlayerCard.GetValue();
            }
            this.firstPlayerPoints += (int)hand.FirstPlayerAnnounce;
            this.secondPlayerPoints += (int)hand.SecondPlayerAnnounce;
        
        }


        private void GiveCardToSecondPlayer()
        {
            var card = this.deck.GetNextCard();
            this.secondPlayer.AddCard(card);
            this.secondPlayerCards.Add(card);
        }
        private void GiveCardToFirstPlayer()
        {
            var card = this.deck.GetNextCard();
            this.firstPlayer.AddCard(card);
            this.firstPlayerCards.Add(card);
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
                this.GiveCardToFirstPlayer();
            }

            for (int i = 0; i < 3; i++)
            {
                this.GiveCardToSecondPlayer();
            }

            for (int i = 0; i < 3; i++)
            {
                this.GiveCardToFirstPlayer();
            }

            for (int i = 0; i < 3; i++)
            {
                this.GiveCardToSecondPlayer();
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
            get { return this.secondPlayerCollectedHands.Count > 0; }
        }


        public PlayerPosition ClosedByPlayer
        {
            get { return this.gameClosedBy; }
        }


        public void SetState(BaseRoundState newState)
        {
            this.state = newState;
        }


        public PlayerPosition LastHandInPlayer
        {
            get { return this.firstToPlay; }
        }
    }
}
