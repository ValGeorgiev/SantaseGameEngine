using Santase.Logic.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic
{
    public class SantaceGame: ISantaceGame
    {
        #region Fields
        private int firstPlayerTotalPoints;
        private int secondPlayerTotalPoints;
        private int roundsCount;

        private IPlayer firstPlayer;
        private IPlayer secondPlayer;

        private PlayerPosition firstToPlay;
        #endregion

        #region Methods
        public int FirstPlayerTotalPoints
        {
            get { return this.firstPlayerTotalPoints; }
        }

        public int SecondPlayerTotalPoints
        {
            get { return this.secondPlayerTotalPoints; }
        }
        public int RoundsPlayed
        {
            get { return this.roundsCount; }
        }
        #endregion

        #region Constructor
        public SantaceGame(IPlayer firstPlayer, IPlayer secondPlayer, PlayerPosition firstToPlay)
        {
            this.firstPlayerTotalPoints = 0;
            this.secondPlayerTotalPoints = 0;
            this.roundsCount = 0;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
            this.firstToPlay = firstToPlay;
        }
        #endregion

        #region Methods
        private bool isGameFinished()
        {
            return (FirstPlayerTotalPoints >= 11 || SecondPlayerTotalPoints >= 11);
        }
        public void Start()
        {
            while (!this.isGameFinished())
            {
                this.PlayRound();
                this.roundsCount++;
            }
        }

        private void PlayRound()
        {
            IGameRound round = new GameRound(this.firstPlayer, this.secondPlayer, this.firstToPlay);
            round.Start();

            UpdatePoints(round);

        }
        private void UpdatePoints(IGameRound round)
        {
            if (round.ClosedByPlayer == PlayerPosition.FirstPlayer)
            {
                if (round.FirstPlayerPoints < 66)
                {
                    this.secondPlayerTotalPoints += 3;
                    this.firstToPlay = PlayerPosition.FirstPlayer;
                    return;
                }
            }
            if (round.ClosedByPlayer == PlayerPosition.SecondPlayer)
            {
                if (round.SecondPlayerPoints < 66)
                {
                    this.firstPlayerTotalPoints += 3;
                    this.firstToPlay = PlayerPosition.SecondPlayer;
                    return;
                }
            }
            if (round.FirstPlayerPoints < 66 &&  round.SecondPlayerPoints < 66)
            {
                var winner = round.LastHandInPlayer;
                if (winner == PlayerPosition.FirstPlayer)
                {
                    this.firstPlayerTotalPoints += 1;
                    this.firstToPlay = PlayerPosition.FirstPlayer;
                    return;
                }
                else
                {
                    this.secondPlayerTotalPoints += 1;
                    this.firstToPlay = PlayerPosition.SecondPlayer;
                    return;
                }
            }

            if (round.FirstPlayerPoints > round.SecondPlayerPoints)
            {
                if (round.SecondPlayerPoints >= 33)
                {
                    this.firstPlayerTotalPoints += 1;
                    this.firstToPlay = PlayerPosition.SecondPlayer;
                }
                else if (round.SecondPlayerHasHand)
                {
                    this.firstPlayerTotalPoints += 2;
                    this.firstToPlay = PlayerPosition.SecondPlayer;
                }
                else
                {
                    this.firstPlayerTotalPoints += 3;
                    this.firstToPlay = PlayerPosition.SecondPlayer;
                }
            }
            else if (round.SecondPlayerPoints > round.FirstPlayerPoints)
            {
                if (round.FirstPlayerPoints >= 33)
                {
                    this.secondPlayerTotalPoints += 1;
                    this.firstToPlay = PlayerPosition.FirstPlayer;
                }
                else if (round.FirstPlayerHasHand)
                {
                    this.secondPlayerTotalPoints += 2;
                    this.firstToPlay = PlayerPosition.FirstPlayer;
                }
                else
                {
                    this.secondPlayerTotalPoints += 3;
                    this.firstToPlay = PlayerPosition.FirstPlayer;
                }
            }
            else
            {
                //Equal  points 0 points to each
            }
        }
        #endregion
    }
}
