﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic.Cards
{
    public class Card
    {
        public CardType Type { get; private set; }
        public CardSuit Suit { get; private set; }


        public Card(CardSuit suit, CardType type)
        {
            this.Type = type;
            this.Suit = suit;
        }

        public override bool Equals(object obj)
        {
            var anotherCard = obj as Card;
            if (anotherCard == null)
            {
                return false;
            }

            return this.Suit == anotherCard.Suit && this.Type == anotherCard.Type;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}",
                this.Type.ToFriendlyString(),
                this.Suit.ToFriendlyString()); 
        }
    }
}
