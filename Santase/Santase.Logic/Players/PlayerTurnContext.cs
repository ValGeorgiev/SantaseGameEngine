using Santase.Logic.Cards;
using Santase.Logic.RoundStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic.Players
{
    public class PlayerTurnContext
    {
        #region Properties
        public Card TrumpCard { get; internal set; }

        public BaseRoundState State { get; internal set; }
        public int CardsLeftInDeck { get; private set; }

        public Card FirstPlayedCard { get; internal set; }
        public Card SecondPlayedCard { get; internal set; }

        public bool AmITheFirstPlayer
        {
            get
            {
                return this.FirstPlayedCard == null;
            }
        }
        #endregion
        public PlayerTurnContext(Card trumpCard, BaseRoundState state, int cardsLeftInDeck)
        {
            this.State = state;
            this.TrumpCard = trumpCard;
            this.CardsLeftInDeck = cardsLeftInDeck;

        }
        
    }
}
