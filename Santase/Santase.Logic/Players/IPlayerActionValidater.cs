using Santase.Logic.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic
{
    public interface IPlayerActionValidater
    {
        bool isValid(PlayerAction action, PlayerTurnContext context);
    }
}
