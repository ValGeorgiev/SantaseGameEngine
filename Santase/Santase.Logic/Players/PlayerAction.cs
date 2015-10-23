using Santase.Logic.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic.Players
{
    public class PlayerAction
    {
        public Card Card { get; private set; }
        public PlayerActionType Type { get; private set; }
        public Announce Announce { get; private set; }

        public PlayerAction(PlayerActionType type, Card card, Announce announce)
        {
            this.Announce = announce;
            this.Type = type;
            this.Card = card;
        }
    }
}
