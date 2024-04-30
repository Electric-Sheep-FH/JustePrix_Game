using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_JustePrix_Serialized
{
    public class GameManager
    {
        public Random RandomNumber { get; set; }
        public int NumberToGuess { get; set; }
        public Player player { get; set; }

        public GameManager(Player player)
        {
            RandomNumber = new Random();
            this.player = player;
        }

        public static GameManager InitiateGame(int difficultyChoice)
        {
            Player player = Player.AskPlayerName();
            GameManager game = new GameManager(player);

            if (difficultyChoice == 1)
            {
                game.NumberToGuess = game.RandomNumber.Next(1,101);
            } else if (difficultyChoice == 2)
            {
                game.NumberToGuess = game.RandomNumber.Next(1, 1001);
            } else
            {
                game.NumberToGuess = game.RandomNumber.Next(1, 10001);
            }

            return game;
        }

        public static int GuessingTurns(GameManager game)
        {
            bool win = false;

            while(!win)
            {
                Console.Write("Quel est votre nombre ? : ");
                string choice = Console.ReadLine();

                int parsedChoice = CheckingNumberValidity(choice);
                bool isNumberAddedToList = game.player.AddingAndCheckingTryedList(parsedChoice);
                if (isNumberAddedToList)
                {
                    if(parsedChoice == game.NumberToGuess)
                    {
                        win = true;
                    }
                    if (parsedChoice < game.NumberToGuess)
                    {
                        Console.WriteLine("Le nombre à deviner est plus grand !\n");
                    } else if (parsedChoice > game.NumberToGuess)
                    {
                        Console.WriteLine("Le nombre à deviner est plus petit !\n");
                    }
                    game.player.DisplayTriedNumbers();
                } else
                {
                    Console.Write("Vous avez déjà tenter ce nombre, veuillez recommencer.\n");
                }
            }
            Console.Write("\n###################################################");
            Console.WriteLine($"\nBravo, vous avez trouvé le nombre : {game.NumberToGuess} en {game.player.TryedNumbers.Count()} coups.");
            Console.Write("###################################################");

            return game.player.TryedNumbers.Count();
        }

        public static int CheckingNumberValidity(string userEntry)
        {
            bool correctChoice = false;
            int parsedChoice = 0;

            while (!correctChoice)
            {
                if (int.TryParse(userEntry, out parsedChoice))
                {
                    if (parsedChoice > 0)
                    {
                        correctChoice = true;
                    }
                }
                else
                {
                    Console.Write("Une erreur est survenue dans votre choix, veuillez ressayer : ");
                    userEntry = Console.ReadLine();
                }
            }
            return parsedChoice;
        }

        public static void LaunchGame()
        {
            bool gaming = true;

            while (gaming)
            {
                Console.Clear();

                int difficultyChoice = Difficulty.DifficultyChoice();
                Console.Clear();
                GameManager game = InitiateGame(difficultyChoice);
                Console.Clear();
                ScoreBoard.DisplayScoreboard();

                //CheatMode !
                //Console.WriteLine(game.NumberToGuess);
            
                int playerGuessedCount = GuessingTurns(game);

                game.player.Score = playerGuessedCount;

                ScoreBoard.RegisterScore(game);

                Console.Write("\n\nVoulez-vous rejouer ? (y/n) : ");
                string choice = Console.ReadLine();
                if (choice.Trim() == "n")
                {
                    gaming = false;
                    Console.WriteLine("\nMerci d'avoir joué !");
                }
            }
        }
    }
}
