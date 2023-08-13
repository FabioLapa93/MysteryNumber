namespace MysteryNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MysteryNumberGame mysteryNumberGame = new MysteryNumberGame();
            mysteryNumberGame.InicializeGame();
        }
    }

    public class MysteryNumberGame
    {
        sbyte Difficulty = 0;
        short MaxNumber = 0;
        sbyte MaxPlayers = 0;
        List<PlayerInfo> PlayerList = new List<PlayerInfo>();

        public void InicializeGame()
        {
            Welcome();
            SetDifficulty();
            SetDifficultyMaxNumber();
            SetPlayers();
            SetMatch();
        }

        public void Welcome()
        {
            Console.WriteLine("Welcome to Mystery Number Game!");
            Utils.RequestEnterKeyAndClear();
        }

        public void SetDifficulty()
        {
            Console.WriteLine("Choose the difficulty level [1-5]:");
            Console.WriteLine("1: Very Easy [1-9]");
            Console.WriteLine("2: Easy [1-25]");
            Console.WriteLine("3: Normal [1-50]");
            Console.WriteLine("4: Hard [1-125]");
            Console.WriteLine("5: Very Hard [1-500]");
            Difficulty = Utils.GetNumberMinMax(1, 5);
            Console.Clear();
        }

        public void SetDifficultyMaxNumber()
        {
            switch (Difficulty)
            {
                case 1:
                    MaxNumber = 9;
                    break;
                case 2:
                    MaxNumber = 25;
                    break;
                case 3:
                    MaxNumber = 50;
                    break;
                case 4:
                    MaxNumber = 125;
                    break;
                case 5:
                    MaxNumber = 500;
                    break;
            }
        }

        public void SetPlayers()
        {
            PlayerList = new List<PlayerInfo>();
            Console.WriteLine("Choose the number of players [1-4]:");
            MaxPlayers = Utils.GetNumberMinMax(1, 4);
            Console.Clear();

            for (int i = 1; i <= MaxPlayers; i++)
            {
                PlayerList.Add(Utils.CreatePlayer(i, MaxNumber));
                Console.Clear();
            }
        }

        public void SetMatch()
        {
            PlayerInfo? winPlayer = null;
            int RoundNumber = 0;

            while (winPlayer == null)
            {
                RoundNumber++;
                winPlayer = NewRound(RoundNumber);
            }

            if (PlayerList.Count() > 1)
            {
                Console.WriteLine($"The winner of this match is {winPlayer.Name}");
            }

            Console.WriteLine("Congratulations.");
            Console.WriteLine("Thank you for playing!");
            Utils.RequestEnterKeyAndClear();
            ShowStatisticsQuestion(RoundNumber);
            ShowRestartQuestion();
        }

        public PlayerInfo? NewRound(int roundNumber)
        {
            Console.WriteLine($"Round {roundNumber} Starting...");
            Utils.RequestEnterKeyAndClear();

            PlayerInfo? winPlayer = new PlayerInfo();

            foreach (PlayerInfo player in PlayerList)
            {
                Console.WriteLine($"{player.Name}, what is your Mystery Number? [1-{MaxNumber}]");
                short PossibleMysteryNumber = Utils.GetNumberMinMax(1, MaxNumber);
                winPlayer = PossibleMysteryNumber == player.MysteryNumber ? player : null;

                if (winPlayer != null)
                {
                    Console.WriteLine("CORRECT!!!");
                    Utils.RequestEnterKeyAndClear();
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect.");
                    if (player.MysteryNumber > PossibleMysteryNumber)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine($"HI: {player.Name} the mystery number is higher than your guess.");
                        Console.WriteLine("\n");
                    }
                    else if (player.MysteryNumber < PossibleMysteryNumber)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine($"LO: {player.Name} the mystery number is lower than your guess.");
                        Console.WriteLine("\n");
                    }
                }
                Utils.RequestEnterKeyAndClear();
            }

            return winPlayer;
        }

        public void ShowStatisticsQuestion(int RoundNumber)
        {
            if (Utils.BoolQuestion("Do you want to see the match statistics?"))
            {
                Console.WriteLine("Winner statistics:");
                Console.WriteLine("\n");
                Console.WriteLine($"Range of Numbers: 1-{MaxNumber}");
                Console.WriteLine($"Number of Attempts: {RoundNumber}");

                Console.WriteLine("\n");
                Console.WriteLine("\n");
                Console.WriteLine("Players match info:");
                Console.WriteLine("\n");

                foreach (var player in PlayerList)
                {
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine($"Id: Player {player.Id}");
                    Console.WriteLine($"Name: {player.Name}");
                    Console.WriteLine($"Mystery Number: {player.MysteryNumber}");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("\n");
                }
                Utils.RequestEnterKeyAndClear();
            }
        }

        public void ShowRestartQuestion()
        {
            if (Utils.BoolQuestion("Do you want to restart this match?"))
            {
                restartMatch();
            }
            else
            {
                if (Utils.BoolQuestion("How about the game, do you want to restart it?"))
                {
                    InicializeGame();
                }
                else
                {
                    Environment.Exit(1);
                }
            }
        }

        public void restartMatch()
        {
            foreach (PlayerInfo player in PlayerList)
            {
                player.MysteryNumber = new Random().Next(1, MaxNumber);
            }
            SetMatch();
        }
    }

    public class PlayerInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MysteryNumber { get; set; }
    }


    public static class Utils
    {
        public static PlayerInfo CreatePlayer(int newId, int maxNumber)
        {
            PlayerInfo newPlayer = new PlayerInfo();
            newPlayer.Id = newId;
            newPlayer.Name = GetString($"Please enter the name for Player {newPlayer.Id}:");
            newPlayer.MysteryNumber = new Random().Next(1, maxNumber);


            return newPlayer;
        }

        public static string GetString(string prompt)
        {
            string? value;
            bool validInput = false;

            Console.WriteLine(prompt);

            do
            {
                value = Console.ReadLine()?.Trim();
                validInput = !string.IsNullOrWhiteSpace(value);

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid value.");
                }

            } while (!validInput);

            return value;
        }

        public static sbyte GetNumberMinMax(sbyte min, sbyte max)
        {
            sbyte value;
            bool validInput = false;

            do
            {
                string? input = Console.ReadLine()?.Trim();
                validInput = sbyte.TryParse(input, out value) && value >= min && value <= max;

                if (!validInput)
                {
                    Console.WriteLine($"Invalid input. Please enter a valid number between {min} and {max}.");
                }

            } while (!validInput);

            return value;
        }

        public static short GetNumberMinMax(short min, short max)
        {
            short value;
            bool validInput = false;

            do
            {
                string? input = Console.ReadLine()?.Trim();
                validInput = short.TryParse(input, out value) && value >= min && value <= max;

                if (!validInput)
                {
                    Console.WriteLine($"Invalid input. Please enter a valid number between {min} and {max}.");
                }

            } while (!validInput);

            return value;
        }

        public static void RequestEnterKeyAndClear()
        {
            Console.WriteLine("Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Console.Clear();
        }

        public static bool BoolQuestion(string question)
        {
            bool? result = null;

            Console.WriteLine($"{question} (Y/N)");
            do
            {
                char response = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (response == 'Y' || response == 'y')
                {
                    result = true; // Exit the loop
                }
                else if (response == 'N' || response == 'n')
                {
                    result = false; // Exit the loop
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid response. Please enter Y or N.");
                }
            } while (result == null);

            RequestEnterKeyAndClear();

            return result.Value;
        }
    }
}