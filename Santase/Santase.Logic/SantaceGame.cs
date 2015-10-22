﻿using System;
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

            UpdatePoints(round);
        }
        private void UpdatePoints(IGameRound round)
        {
            if (round.ClosedByPlayer == PlayerPosition.FirstPlayer)
            {
                if (round.FirstPlayerPoints < 66)
                {
                    this.secondPlayerTotalPoints += 3;
                    return;
                }
            }
            if (round.ClosedByPlayer == PlayerPosition.SecondPlayer)
            {
                if (round.SecondPlayerPoints < 66)
                {
                    this.firstPlayerTotalPoints += 3;
                    return;
                }
            }

            if (round.FirstPlayerPoints > round.SecondPlayerPoints)
            {
                if (round.SecondPlayerPoints >= 33)
                {
                    this.firstPlayerTotalPoints += 1;
                }
                else if (round.SecondPlayerHasHand)
                {
                    this.firstPlayerTotalPoints += 2;
                }
                else
                {
                    this.firstPlayerTotalPoints += 3;
                }
            }
            else if (round.SecondPlayerPoints > round.FirstPlayerPoints)
            {
                if (round.FirstPlayerPoints >= 33)
                {
                    this.secondPlayerTotalPoints += 1;
                }
                else if (round.FirstPlayerHasHand)
                {
                    this.secondPlayerTotalPoints += 2;
                }
                else
                {
                    this.secondPlayerTotalPoints += 3;
                }
            }
            else
            {
                //Equal  points 0 points to each
            }
        }

        
    }
}
