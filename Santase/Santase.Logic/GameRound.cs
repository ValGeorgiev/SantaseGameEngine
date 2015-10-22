using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic
{
    public class GameRound: IGameRound
    {
        private int totalPointWonByFirstPlayer;
        private int totalPointWonBySecondPlayer;
        public int TotalPointWonByFirstPlayer
        {
            get { throw new NotImplementedException(); }
        }

        public int TotalPointWonBySecondPlayer
        {
            get { throw new NotImplementedException(); }
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
