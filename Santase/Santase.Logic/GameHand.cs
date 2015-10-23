﻿using Santase.Logic.Cards;
using Santase.Logic.Players;
using Santase.Logic.RoundStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic
{
    public class GameHand : IGameHand
    {
        #region Fields
        private PlayerPosition whoWillPlayFirst;
        private IPlayer firstPlayer;
        private IList<Card> firstPlayerCards;
        private IPlayer secondPlayer;
        private IList<Card> secondPlayerCards;
        private BaseRoundState state;
        private IDeck deck;
        private IPlayerActionValidater actionValidater;
        private PlayerPosition whoClosedTheGame;
        private Card firstPlayerCard;
        private Card secondPlayerCard;
        private PlayerPosition winner;

        private Announce firstPlayerAnnounce;
        private Announce secondPlayerAnnounce;
        #endregion

        #region Properties
         public PlayerPosition Winner
        {
            get { return this.winner; }
        }

        public Card FirstPlayerCard
        {
            get { return this.firstPlayerCard; }
        }

        public Card SecondPlayerCard
        {
            get { return this.secondPlayerCard; }
        }

        public Announce FirstPlayerAnnounce
        {
            get { return this.firstPlayerAnnounce; }
        }

        public Announce SecondPlayerAnnounce
        {
            get { return this.secondPlayerAnnounce; }
        }

        public PlayerPosition GameClosedBy
        {
            get { return this.whoClosedTheGame; }
        }
        #endregion

        #region Constructor
        public GameHand(PlayerPosition whoWillPlayFirst,
           IPlayer firstPlayer,
           IList<Card> firstPlayerCards,
           IPlayer secondPlayer,
           IList<Card> secondPlayerCards,
           BaseRoundState state,
           IDeck deck)
        {
            this.whoWillPlayFirst = whoWillPlayFirst;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
            this.state = state;
            this.deck = deck;
            this.actionValidater = new PlayerActionValidater();
            this.firstPlayerCards = firstPlayerCards;
            this.secondPlayerCards = secondPlayerCards;
            this.whoClosedTheGame = PlayerPosition.NoOne;
        }
        #endregion

        #region Methods
        public void Start()
        {
            IPlayer firstToPlay;
            IPlayer secondToPlay;
            IList<Card> firstToPlayCards;
            IList<Card> secondToPlayCards;
            if (this.whoWillPlayFirst == PlayerPosition.FirstPlayer)
            {
                firstToPlay = this.firstPlayer;
                firstToPlayCards = this.firstPlayerCards;
                secondToPlay = this.secondPlayer;
                secondToPlayCards = this.secondPlayerCards;
            }
            else
            {
                firstToPlay = this.secondPlayer;
                firstToPlayCards = this.secondPlayerCards;
                secondToPlay = this.firstPlayer;
                secondToPlayCards = this.firstPlayerCards;
            }

            var context = new PlayerTurnContext(this.deck.GetTrumpCard, this.state, deck.CardsLeft);

            PlayerAction firstPlayerAction = null;
            do
            {
                firstPlayerAction = this.FirstPlayerTurn(firstToPlay, context);

                if (!this.actionValidater.isValid(firstPlayerAction, context, firstToPlayCards))
                {
                    //TODO: something more graceful?
                    throw new InternalGameExceptions("Invalid turn");
                }

            } while (firstPlayerAction.Type == PlayerActionType.PlayCard);

            context.FirstPlayedCard = firstPlayerAction.Card;

            PlayerAction secondPlayerAction = secondToPlay.GetTurn(
                context, 
                this.actionValidater);
            if (!this.actionValidater.isValid(secondPlayerAction, context, secondToPlayCards))
            {
                //TODO: something more graceful?
                throw new InternalGameExceptions("Invalid turn");
            }

            context.SecondPlayedCard = secondPlayerAction.Card;


            if (firstToPlay == this.firstPlayer)
            {
                this.firstPlayerCard = secondPlayerAction.Card;
                this.firstPlayerAnnounce = secondPlayerAction.Announce;
                this.secondPlayerCard = firstPlayerAction.Card;
                this.secondPlayerAnnounce = firstPlayerAction.Announce;
            }
            else
            {

            }
             
            firstToPlay.EndTurn(context);
            secondToPlay.EndTurn(context);


            ICardWinner cardWinner = new CardWinner();
            if (firstToPlay == this.firstPlayer)
            {
                this.winner = cardWinner.Winner(firstPlayerAction.Card, secondPlayerAction.Card, this.deck.GetTrumpCard.Suit);
            }
            else
            {
                this.winner = cardWinner.Winner(secondPlayerAction.Card, firstPlayerAction.Card, this.deck.GetTrumpCard.Suit);
            }

        }

        private PlayerAction FirstPlayerTurn(IPlayer firstToPlay, PlayerTurnContext context )
        {
            var firstToPlayTurn = firstToPlay.GetTurn(
                context,
                this.actionValidater);

            if (firstToPlayTurn.Type == PlayerActionType.CloseGame)
            {
                this.state.Close();
                context.State = new FinalRoundState();
                this.state = new FinalRoundState();
                if (firstToPlay == this.firstPlayer)
                {
                    this.whoClosedTheGame = PlayerPosition.FirstPlayer;
                }
                else
                {
                    this.whoClosedTheGame = PlayerPosition.SecondPlayer;
                }
               

            }
            if (firstToPlayTurn.Type == PlayerActionType.ChangeTrump)
            {
                var changeTrump = new Card(this.deck.GetTrumpCard.Suit, CardType.Nine);
                var oldTrump = this.deck.GetTrumpCard;
                context.TrumpCard = oldTrump;
                this.deck.ChangeTrumpCard(changeTrump);

                if (firstToPlay == this.firstPlayer)
                {
                    this.firstPlayerCards.Remove(changeTrump);
                    this.firstPlayerCards.Add(oldTrump);
                    this.firstPlayer.AddCard(oldTrump);
                }
                else
                {
                    this.secondPlayerCards.Remove(changeTrump);
                    this.secondPlayerCards.Add(oldTrump);
                    this.secondPlayer.AddCard(oldTrump);
                }

                
            }
            return firstToPlayTurn;
        }

        #endregion
    }
}
