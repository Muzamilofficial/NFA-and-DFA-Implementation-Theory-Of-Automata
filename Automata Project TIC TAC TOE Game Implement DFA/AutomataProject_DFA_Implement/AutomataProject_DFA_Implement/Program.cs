using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataProject_DFA_Implement
{
    enum GameState
    {
        InProgress,
        PlayerAWins,
        PlayerBWins,
        Draw
    }

    class Program
    {
        char[,] arr = new char[3, 3];
        bool playerATurn = true;
        int playerAScore = 0, playerBScore = 0;

        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.Setup();
        }

        void Setup()
        {
            Console.Clear();
            int i, j, z = 1;
            DateTime dt = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine("\t\t\t" + dt);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Player A: " + playerAScore);
            Console.WriteLine("\t\t\t\t\t\tPlayer B: " + playerBScore);
            for (i = 0; i < 3; i++)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("\t");
                for (j = 0; j < 3; j++)
                {
                    arr[i, j] = (char)(z + '0');
                    Console.Write("\t {0}\t", arr[i, j]);
                    z++;
                }
                Console.WriteLine();
            }
            TurnChanging();
        }

        void Checking()
        {
            GameState gameState = GetGameState();
            switch (gameState)
            {
                case GameState.PlayerAWins:
                    Console.Clear();
                    Console.WriteLine("================================================================================");
                    Console.WriteLine("Player A is the winner!");
                    playerAScore++;
                    Console.WriteLine("================================================================================");
                    break;
                case GameState.PlayerBWins:
                    Console.Clear();
                    Console.WriteLine("================================================================================");
                    Console.WriteLine("Player B is the winner!");
                    playerBScore++;
                    Console.WriteLine("================================================================================");
                    break;
                case GameState.Draw:
                    Console.Clear();
                    Console.WriteLine("================================================================================");
                    Console.WriteLine("Match Draw!");
                    playerAScore++;
                    playerBScore++;
                    Console.WriteLine("================================================================================");
                    break;
                case GameState.InProgress:
                    PrintBox();
                    TurnChanging();
                    break;
            }
            DecisionMaker();
            Console.ReadKey();
        }

        GameState GetGameState()
        {
            for (int i = 0; i < 3; i++)
            {
                if (arr[i, 0] == arr[i, 1] && arr[i, 1] == arr[i, 2])
                {
                    if (arr[i, 0] == 'A')
                        return GameState.PlayerAWins;
                    else if (arr[i, 0] == 'B')
                        return GameState.PlayerBWins;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (arr[0, i] == arr[1, i] && arr[1, i] == arr[2, i])
                {
                    if (arr[0, i] == 'A')
                        return GameState.PlayerAWins;
                    else if (arr[0, i] == 'B')
                        return GameState.PlayerBWins;
                }
            }

            if (arr[0, 0] == arr[1, 1] && arr[1, 1] == arr[2, 2])
            {
                if (arr[0, 0] == 'A')
                    return GameState.PlayerAWins;
                else if (arr[0, 0] == 'B')
                    return GameState.PlayerBWins;
            }

            if (arr[0, 2] == arr[1, 1] && arr[1, 1] == arr[2, 0])
            {
                if (arr[0, 2] == 'A')
                    return GameState.PlayerAWins;
                else if (arr[0, 2] == 'B')
                    return GameState.PlayerBWins;
            }

            if (!IsBoardFull())
                return GameState.InProgress;

            return GameState.Draw;
        }

        bool IsBoardFull()
        {
            foreach (char cell in arr)
            {
                if (cell != 'A' && cell != 'B')
                    return false;
            }
            return true;
        }

        void PrintBox()
        {
            Console.Clear();
            int i, j;
            DateTime dt = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine("\t\t\t" + dt);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Player A: " + playerAScore);
            Console.WriteLine("\t\t\t\t\t\tPlayer B: " + playerBScore);

            for (i = 0; i < 3; i++)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("\t");
                for (j = 0; j < 3; j++)
                {
                    Console.Write("\t {0}\t", arr[i, j]);
                }
                Console.WriteLine();
            }
            TurnChanging();
        }

        void Box(int x)
        {
            char playerChar = playerATurn ? 'A' : 'B';
            switch (x)
            {
                case 1:
                    arr[0, 0] = playerChar;
                    break;
                case 2:
                    arr[0, 1] = playerChar;
                    break;
                case 3:
                    arr[0, 2] = playerChar;
                    break;
                case 4:
                    arr[1, 0] = playerChar;
                    break;
                case 5:
                    arr[1, 1] = playerChar;
                    break;
                case 6:
                    arr[1, 2] = playerChar;
                    break;
                case 7:
                    arr[2, 0] = playerChar;
                    break;
                case 8:
                    arr[2, 1] = playerChar;
                    break;
                case 9:
                    arr[2, 2] = playerChar;
                    break;
                default:
                    break;
            }
            playerATurn = !playerATurn;
            Checking();
        }

        void TurnChanging()
        {
            int x;
            if (playerATurn)
            {
            tryAgain:
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Player A Turn");
                x = Convert.ToInt32(Console.ReadLine());
                if (TurnNoChecker(x))
                    Box(x);
                else
                {
                    Console.WriteLine("\nWrong Choice! Choose from the given numbers.\n");
                    goto tryAgain;
                }
            }
            else
            {
            tryAgain1:
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Player B Turn");
                x = Convert.ToInt32(Console.ReadLine());
                if (TurnNoChecker(x))
                    Box(x);
                else
                {
                    Console.WriteLine("Choice Already Made! Try a different one.");
                    goto tryAgain1;
                }
            }
        }

        bool TurnNoChecker(int x)
        {
            char targetChar = (char)(x + '0');
            foreach (char cell in arr)
            {
                if (cell == targetChar)
                    return true;
            }
            return false;
        }

        void DecisionMaker()
        {
            int x;
        tryAgain2:
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press 1 for a New Game");
            Console.WriteLine("");
            Console.WriteLine("Press 2 for the Next Match");
            Console.WriteLine("");
            Console.WriteLine("Press 3 to Exit");
            Console.WriteLine();
            x = Convert.ToInt32(Console.ReadLine());
            if (x == 1)
            {
                playerAScore = 0;
                playerBScore = 0;
                Setup();
            }
            else if (x == 2)
            {
                Setup();
            }
            else if (x == 3)
            {
                Console.Clear();
                Console.WriteLine("Press any key to Exit!!!!!!!!!!!!!!!!!");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Choice! Try Again!!!");
                goto tryAgain2;
            }
        }

        public void Run()
        {
            Setup();
            Console.ReadKey();
        }
    }
}