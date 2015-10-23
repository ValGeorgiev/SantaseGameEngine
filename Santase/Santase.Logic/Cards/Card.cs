using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic.Cards
{
    public class Card
    {
        #region Properties
        public CardType Type { get; private set; }
        public CardSuit Suit { get; private set; }
        #endregion

        #region Constructor
        public Card(CardSuit suit, CardType type)
        {
            this.Type = type;
            this.Suit = suit;
        }
        #endregion

        #region Methods
        public int GetValue()
        {
            switch (this.Type)
            {
                case CardType.Nine:
                    return 0;
                case CardType.Ten:
                    return 10;
                case CardType.Jack:
                    return 2;
                case CardType.Queen:
                    return 3;
                case CardType.King:
                    return 4;
                case CardType.Ace:
                    return 11;
                default:
                    throw new InternalGameExceptions("Invalid card type" );
            }
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
        #endregion
    }
}
