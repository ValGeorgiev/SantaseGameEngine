using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic.Cards
{
    public interface IDeck
    {
        Card GetTrumpCard { get; }

        int CardsLeft { get; }

        Card GetNextCard();
        void ChangeTrumpCard(Card newCard);


    }
}
