using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace slave
{
    class Field
    {
        public List<Player> players;
        private readonly Deck deck;
        private bool HaveWinner;
        private int queue;
        private int CntPassed;
        private Card lastcard;
        public int nextPlayer;
        public int round;

        public Deck Deck
        {
            get
            {
                return deck;
            }
            set
            {

            }
        }
        public int Queue
        {
            get
            {
                if (queue == 4)
                {
                    return queue = 0;
                }
                if (queue == -1)
                {
                    return queue = 3;
                }
                return queue;
            }
            set
            {
                queue = value;
            }
        }

        public Card LastCard
        {
            get { return lastcard; }
            set { lastcard = value; }
        }

        public Field(Deck deck)
        {
            players = new List<Player>();
            this.deck = deck;
        }
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void Play()
        {
        start:
            //Intro();
            AddPlayer(new Player("111"));
            AddPlayer(new Player("222"));
            AddPlayer(new Player("333"));
            AddPlayer(new Player("444"));
            CardForPlayers();
            Gameplay();
            Console.WriteLine("Play Again? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                goto start;
            }
        }

        private void CardForPlayers()
        {
            for (int i = 0; i < 13; i++)
            {
                players.ForEach(player => player.AddCard(deck.Deal()));
            }
            players.ForEach(player => player.Cardlist.Sort());

        }

        public void Intro()
        {
            Console.WriteLine("============= WELCOME TO SLAVE GAME =============\n");

            for (int i = 0; i < 4; i++)
            {
                Console.Write("Enter Player {0} Name : ", i + 1);
                string Name = Console.ReadLine();
                AddPlayer(new Player(Name));
            }
        }
        private void Gameplay()
        {
            round = 1;
            nextPlayer = 1;
            while (HaveWinner != true)
            {
                next:
                if (players[Queue].Pass)
                {
                    this.Queue = Queue + nextPlayer;
                    goto next;
                }
                if (CntPassed == 3)
                {
                    ClearFieldCard();
                }
                Console.Clear();
                Status();
                EndTurn();
                this.Queue = Queue + nextPlayer;
                
            }
        }

        public void PrintPlayers()
        {
            foreach (var item in players)
            {
                Console.WriteLine(item);
            }
        }

        private void ClearFieldCard()
        {
            CntPassed = 0;
            LastCard = null;
            foreach (var item in players)
            {
                item.Pass = false;
            }
            if (round % 2 == 0)
            {
                nextPlayer = 1;
            }
            else
            {
                nextPlayer = -1;
            }
            round++;
        }
        private void EndTurn()
        {
            if (players[Queue].NumberOfCard == 0)
            {
                HaveWinner = true;
                EndGame();
            }
            if (players[Queue].Pass)
            {
                CntPassed++;
            }
            if (players[Queue].DroppedCard != null)
            {
                LastCard = (players[Queue].DroppedCard);
            }

        }
        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine("Congratulations!!! The Winner is {0}\n\n\n",players[Queue].Name);
        }
        
        public void Status()
        {
            Console.WriteLine("Round : {0}", round);
            Console.WriteLine("\n****** Latest Card : {0}    ******\n", LastCard);
            players.ForEach(players => players.ShowTotalCard());
            //Console.WriteLine("\n1 = right, -1 = left : {0}", nextPlayer);
            //Console.WriteLine("Queue : {0}", Queue);
            //Console.WriteLine("Passed : {0}", CntPassed);
            PlayerQueueStatus();
            Console.WriteLine("");
            Console.WriteLine(players[Queue]);
            Console.WriteLine("");
            players[Queue].PlayerOption();
        }

        public void PlayerQueueStatus()
        {
            Console.WriteLine("");
            if (Queue == 0 && nextPlayer == 1)
            {
                Console.WriteLine("           ===============>>>>");
                Console.WriteLine("           { [1] , 2 , 3 , 4 }");
            }
            else if ((Queue == 1 && nextPlayer == 1))
            {
                Console.WriteLine("           ===============>>>>");
                Console.WriteLine("           { 1 , [2] , 3 , 4 }");
            }
            else if ((Queue == 2 && nextPlayer == 1))
            {
                Console.WriteLine("           ===============>>>>");
                Console.WriteLine("           { 1 , 2 , [3] , 4 }");
            }
            else if ((Queue == 3 && nextPlayer == 1))
            {
                Console.WriteLine("           ===============>>>>");
                Console.WriteLine("           { 1 , 2 , 3 , [4] }");
            }
            else if ((Queue == 0 && nextPlayer == -1))
            {
                Console.WriteLine("           <<<<===============");
                Console.WriteLine("           { [1] , 2 , 3 , 4 }");
            }
            else if ((Queue == 1 && nextPlayer == -1))
            {
                Console.WriteLine("           <<<<===============");
                Console.WriteLine("           { 1 , [2] , 3 , 4 }");
            }
            else if ((Queue == 2 && nextPlayer == -1))
            {
                Console.WriteLine("           <<<<===============");
                Console.WriteLine("           { 1 , 2 , [3] , 4 }");
            }
            else if ((Queue == 3 && nextPlayer == -1))
            {
                Console.WriteLine("           <<<<===============");
                Console.WriteLine("           { 1 , 2 , 3 , [4] }");
            }
        }
    }
}
