using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic.Players
{
    public class PlayerActionValidater: IPlayerActionValidater
    {

        public bool isValid(PlayerAction action, PlayerTurnContext context)
        {
            return false;
        }
    }
}
