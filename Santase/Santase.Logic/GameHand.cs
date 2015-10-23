using Santase.Logic.Cards;
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
        private PlayerPosition whoWillPlayFirst;
        private IPlayer firstPlayer;
        private IPlayer secondPlayer;
        private BaseRoundState state;
        private IDeck deck;
        private IPlayerActionValidater actionValidater;

        public GameHand(PlayerPosition whoWillPlayFirst,
            IPlayer firstPlayer,
            IPlayer secondPlayer,
            BaseRoundState state,
            IDeck deck)
        {
            this.whoWillPlayFirst = whoWillPlayFirst;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
            this.state = state;
            this.deck = deck;
            this.actionValidater = new PlayerActionValidater();
        }
        public void Start()
        {
            IPlayer firstToPlay;
            IPlayer secondToPlay;
            if (this.whoWillPlayFirst == PlayerPosition.FirstPlayer)
            {
                firstToPlay = this.firstPlayer;
                secondToPlay = this.secondPlayer;
            }
            else
            {
                firstToPlay = this.secondPlayer;
                secondToPlay = this.firstPlayer;
            }

            var context = new PlayerTurnContext(this.deck.GetTrumpCard, this.state, deck.CardsLeft);

            PlayerAction firstPlayerAction = null;
            do
            {
                firstPlayerAction = this.FirstPlayerTurn(firstToPlay, context);

                if (!this.actionValidater.isValid(firstPlayerAction, context))
                {
                    //TODO: something more graceful?
                    throw new InternalGameExceptions("Invalid turn");
                }

            } while (firstPlayerAction.Type == PlayerActionType.PlayCard);

            context.FirstPlayedCard = firstPlayerAction.Card;

            PlayerAction secondPlayerAction = secondToPlay.GetTurn(
                new PlayerTurnContext(this.deck.GetTrumpCard, this.state, deck.CardsLeft), 
                this.actionValidater);

            context.SecondPlayedCard = secondPlayerAction.Card;

             
            firstToPlay.EndTurn(context);
            secondToPlay.EndTurn(context);

        }

        private PlayerAction FirstPlayerTurn(IPlayer firstToPlay, PlayerTurnContext context )
        {
            var firstToPlayTurn = firstToPlay.GetTurn(
                context,
                this.actionValidater);

            if (firstToPlayTurn.Type == PlayerActionType.CloseGame)
            {
                this.state.Close();
               

                return firstToPlayTurn;
            }
            if (firstToPlayTurn.Type == PlayerActionType.ChangeTrump)
            {
              
                return firstToPlayTurn;   
            }
            if (firstToPlayTurn.Type == PlayerActionType.PlayCard)
            {
                
                return firstToPlayTurn;
            }
            return null; //should
        }

        public PlayerPosition Winner
        {
            get { throw new NotImplementedException(); }
        }


        public Card FirstPlayerCard
        {
            get { throw new NotImplementedException(); }
        }

        public Card SecondPlayerCard
        {
            get { throw new NotImplementedException(); }
        }

        public Announce FirstPlayerAnnounce
        {
            get { throw new NotImplementedException(); }
        }

        public Announce SecondPlayerAnnounce
        {
            get { throw new NotImplementedException(); }
        }


        public PlayerPosition GameClosedBy
        {
            get { throw new NotImplementedException(); }
        }
    }
}
