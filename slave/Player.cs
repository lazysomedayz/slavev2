using System;
using System.Collections;
using System.Collections.Generic;

namespace slave
{
    class Player
    {
        private List<Card> Cardlist { get; set; }
        private bool Pass { get; set; }
        private string Name { get; set; }
        private int TotalCard => Cardlist.Count;
        private bool have3Club { get; set; }
        private Card DroppedCard { get; set; }

        public string GetName()
        {
            return Name;
        }

        public bool GetPass()
        {
            return Pass;
        }
        public void SetPass(bool b)
        {
            Pass = b;
        }
        public int GetTotalCard()
        {
            return TotalCard;
        }

        public List<Card> GetCardlist()
        {
            return Cardlist;
        }

        public Card GetDroppedCard()
        {
            return DroppedCard;
        }

        public Player(string name = "Unknown")
        {
            this.Name = name;
            Cardlist = new List<Card>();

        }
        public bool Have3Club
        {
            get
            {
                var CardListStr = new ArrayList();
                foreach (var item in Cardlist)
                {
                    CardListStr.Add(item.ToString());
                }
                if (CardListStr.Contains("3♣"))
                {
                    return have3Club = true;
                }
                return have3Club;
            }
            set
            {

            }
        }

        public bool WantToPass()
        {
            Pass = true;
            return Pass;
        }

        public void AddCard(Card card)
        {
            Cardlist.Add(card);
        }

        public void DropCard()
        {
            Console.WriteLine("\nWhich card do you want to drop in field?");
            Console.Write("Select the position of card in your hand. : ");
        input:
            int PositionOfCard = Int32.Parse(Console.ReadLine()) - 1;
            if (PositionOfCard >= 0 && PositionOfCard <= 12)
            {
                this.DroppedCard = Cardlist[PositionOfCard];
                Cardlist.RemoveAt(PositionOfCard);
            }
            else
            {
                Console.WriteLine("Try Again!! : ");
                goto input;
            }
        }

        public override string ToString()
        {
            string returntext = string.Format("==============================================================\n\nName : {0}\n", Name);
            returntext += string.Format("Cards :");
            Cardlist.ForEach(card => returntext += string.Format(" {0},", card.ToString()));
            returntext = returntext.TrimEnd(',');
            //returntext += string.Format("\nHave 3♣? : {0}", Have3Club);
            //returntext += string.Format("\nDropped Card : {0}", DroppedCard);
            returntext += string.Format("\n\n==============================================================");
            return returntext;
        }

        public void ShowTotalCard()
        {
            int TotalCard = Cardlist.Count;
            Console.WriteLine("{0}'s Card remaining: {1}", Name, TotalCard);
        }

        public void PlayerOption()
        {
        input:
            Console.Write("Enter 'P' for Pass this round.\nEnter 'D' for Drop your card.\n\nSelect : ");
            string input = Console.ReadLine().ToLower();
            if (input == "p")
            {
                WantToPass();

            }
            else if (input == "d")
            {
                DropCard();
            }
            else
            {
                goto input;
            }
        }
    }
}
