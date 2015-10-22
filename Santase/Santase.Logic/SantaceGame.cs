using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic
{
    public class SantaceGame: ISantaceGame
    {
        private int firstPlayerTotalPoints;
        private int secondPlayerTotalPoints;

        public int FirstPlayerTotalPoints
        {
            get { return this.firstPlayerTotalPoints; }
        }

        public int SecondPlayerTotalPoints
        {
            get { return this.secondPlayerTotalPoints; }
        }

        public SantaceGame()
        {
            this.firstPlayerTotalPoints = 0;
            this.secondPlayerTotalPoints = 0;
        }

        private bool isGameFinished()
        {
            return (FirstPlayerTotalPoints >= 11 || SecondPlayerTotalPoints >= 11);
        }
        public void Start()
        {
            while (!this.isGameFinished())
            {
                this.PlayRound();
            }
        }

        private void PlayRound()
        {
            IGameRound round = new GameRound();
            round.Start();
            this.firstPlayerTotalPoints += round.TotalPointWonByFirstPlayer;
            this.secondPlayerTotalPoints += round.TotalPointWonBySecondPlayer;
        }

        
    }
}
