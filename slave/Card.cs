using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace slave
{
    public enum CardSuit
    {
        Club = 1,
        Diamond = 2,
        Heart = 3,
        Spade = 4
    }

    public class Card : IEquatable<Card>, IComparable<Card>
    {
        private readonly int value;
        private CardSuit Suit { get; }

        public int Value
        {
            get
            {
                if (value > 13)
                {
                    return 13;
                }
                return value;
            }
            set
            {

            }
        }

        public Card(int number)
        {
            if (number >= 52 || number < 0)
            {
                number = 0;
            }
            switch (number / 13)
            {
                case 0:
                    Suit = CardSuit.Club;
                    break;

                case 1:
                    Suit = CardSuit.Diamond;
                    break;

                case 2:
                    Suit = CardSuit.Heart;
                    break;

                default:
                    Suit = CardSuit.Spade;
                    break;
            }
            value = number % 13 + 1;
        }

        public override string ToString()
        {
            string text = "\u2460";
            switch (Suit)
            {
                case CardSuit.Club:
                    text = "\u2663";
                    break;

                case CardSuit.Diamond:
                    text = "\u2666";
                    break;

                case CardSuit.Heart:
                    text = "\u2665";
                    break;

                case CardSuit.Spade:
                    text = "\u2660";
                    break;

                default:
                    break;
            }

            if (value == 9)
            {
                text = "J" + text;
            }
            else if (value == 10)
            {
                text = "Q" + text;
            }
            else if (value == 11)
            {
                text = "K" + text;
            }
            else if (value == 12)
            {
                text = "A" + text;
            }
            else if (value == 13)
            {
                text = "2" + text;
            }
            else
            {
                int valueforslave = value + 2;
                text = valueforslave.ToString() + text;
            }
            return text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Card objAsCard = obj as Card;
            if (objAsCard == null) return false;
            else return Equals(objAsCard);
        }
        
        public int SortByValue(string card1, string card2)
        {
            return card1.CompareTo(card2);
        }

        public int CompareTo(Card compareCard)
        {
            if (compareCard == null)
                return 1;
            else
                return this.Value.CompareTo(compareCard.Value);
        }

        public bool Equals(Card card)
        {
            if (card == null) return false;
            return (this.Value.Equals(card.Value));
        }
    }
}