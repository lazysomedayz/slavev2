using System;
using System.Collections.Generic;

namespace slave
{
    class Field
    {
        private List<Player> players { get; set; }
        private Deck deck { get; set; }
        private bool HaveWinner { get; set; }
        private int queue { get; set; }
        private int CntPassed { get; set; }
        private Card LastCard { get; set; }
        private int nextPlayer { get; set; }
        private int round { get; set; }

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
            Intro();
            //AddPlayer(new Player("111"));
            //AddPlayer(new Player("222"));
            //AddPlayer(new Player("333"));
            //AddPlayer(new Player("444"));
            CardForPlayers();
            CheckPlayerWhoHave3Club();
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
            players.ForEach(player => player.GetCardlist().Sort());

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
                if (players[Queue].GetPass())
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

        public void CheckPlayerWhoHave3Club()
        {
            for (int i = 0; i < 4; i++)
            {
                if (players[i].Have3Club)
                {
                    Queue = i;
                }
            }
        }

        private void ClearFieldCard()
        {
            CntPassed = 0;
            LastCard = null;
            foreach (var item in players)
            {
                item.SetPass(false);
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
            if (players[Queue].GetTotalCard() == 0)
            {
                HaveWinner = true;
                EndGame();
            }
            if (players[Queue].GetPass())
            {
                CntPassed++;
            }
            if (players[Queue].GetDroppedCard() != null)
            {
                LastCard = (players[Queue].GetDroppedCard());
            }

        }
        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine("Congratulations!!! The Winner is {0}\n\n\n", players[Queue].GetName());
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
