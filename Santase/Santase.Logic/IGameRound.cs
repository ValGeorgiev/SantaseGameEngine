﻿using Santase.Logic.RoundStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic
{
    public interface IGameRound
    {
        int FirstPlayerPoints { get; }

        int SecondPlayerPoints{ get; }

        bool FirstPlayerHasHand { get; }
        bool SecondPlayerHasHand { get; }

        PlayerPosition ClosedByPlayer { get; }
        void Start();
        void SetState(BaseRoundState newState);

        PlayerPosition LastHandInPlayer { get; }
    }
}
