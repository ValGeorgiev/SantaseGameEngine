using Santase.Logic.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            var card = new Card(CardSuit.Heart, CardType.Ace);
            var card2 = new Card(CardSuit.Heart, CardType.Ace);
            Console.WriteLine(card.ToString());
            Console.WriteLine(card.Equals(card2));
        }
    }
}
