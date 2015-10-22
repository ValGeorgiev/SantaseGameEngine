﻿using Santase.Logic.Cards;
using Santase.Logic.Players;
using Santase.Logic.RoundStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic
{
    public class GameHand:IGameHand
    {
        private PlayerPosition whoWillPlayFirst;
        private IPlayer firstPlayer;
        private IPlayer secondPlayer;
        private BaseRoundState state;
        public GameHand(PlayerPosition whoWillPlayFirst,
            IPlayer firstPlayer,
            IPlayer secondPlayer,
            BaseRoundState state )
        {
            this.whoWillPlayFirst = whoWillPlayFirst;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
            this.state = state;
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
            //TODO prepare Player turn context  

            PlayerAction firstPlayerAction = null;
            do
            {
                firstPlayerAction = this.FirstPlayerTurn(firstToPlay);
            } while (firstPlayerAction.Type == PlayerActionType.PlayCard);

            PlayerAction secondPlayerAction = firstToPlay.GetTurn(new PlayerTurnContext());


        }

        private PlayerAction FirstPlayerTurn(IPlayer firstToPlay)
        {
            var firstToPlayTurn = firstToPlay.GetTurn(new PlayerTurnContext());

            if (firstToPlayTurn.Type == PlayerActionType.CloseGame)
            {
                this.state.Close();
                //todo

                return firstToPlayTurn;
            }
            if (firstToPlayTurn.Type == PlayerActionType.ChangTrump)
            {
                //todo
                return firstToPlayTurn;   
            }
            if (firstToPlayTurn.Type == PlayerActionType.PlayCard)
            {
                //todo
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
