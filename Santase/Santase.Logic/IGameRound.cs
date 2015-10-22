using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic
{
    public interface IGameRound
    {
        int TotalPointWonByFirstPlayer { get; }

        int TotalPointWonBySecondPlayer { get; }

        void Start();
        
    }
}
