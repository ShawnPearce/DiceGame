namespace DiceGame.Resources.BusinessLogic;

public class Game
    {
        // fields
        public Random random;
        public int attempts;
        public bool gameWon;
        public bool gameOver;
        public bool isThreeDiceMode; 

        // Constructor
        public Game()
        {
            random = new Random();
            isThreeDiceMode = false; // Start with two dice mode
            ResetGame();
        }

        // Properties to get game state
        public int GetAttempts()
        {
            return attempts;
        }

        public bool IsGameWon()
        {
            return gameWon;
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

        public bool IsThreeDiceMode()
        {
            return isThreeDiceMode;
        }

        // Method to set game mode
        public void SetThreeDiceMode(bool useThreeDice)
        {
            isThreeDiceMode = useThreeDice;
            ResetGame();
        }

        // Method to roll dice (2 or 3 depending on mode)
        public int[] RollDice()
        {
            if (gameOver)
                return null;

            int[] diceValues;
            
            if (isThreeDiceMode)
            {
                // Roll three dice
                diceValues = new int[3];
                diceValues[0] = random.Next(1, 7);
                diceValues[1] = random.Next(1, 7);
                diceValues[2] = random.Next(1, 7);
            }
            else
            {
                // Roll two dice
                diceValues = new int[2];
                diceValues[0] = random.Next(1, 7);
                diceValues[1] = random.Next(1, 7);
            }

            // Increase attempt counter
            attempts++;

            // Check if player wins
            int sum = GetSum(diceValues);
            
            if (isThreeDiceMode)
            {
                // Three dice mode: win with sum of 11 or 18
                if (sum == 11 || sum == 18)
                {
                    gameWon = true;
                    gameOver = true;
                }
            }
            else
            {
                // Two dice mode: win with sum of 7 or 11
                if (sum == 7 || sum == 11)
                {
                    gameWon = true;
                    gameOver = true;
                }
            }

            // Check if all attempts used
            if (!gameWon && attempts >= 3)
            {
                gameOver = true;
            }

            return diceValues;
        }

        // Method to calculate sum of dice
        public int GetSum(int[] diceValues)
        {
            int sum = 0;
            for (int i = 0; i < diceValues.Length; i++)
            {
                sum += diceValues[i];
            }
            return sum;
        }

        // Method to reset the game
        public void ResetGame()
        {
            attempts = 0;
            gameWon = false;
            gameOver = false;
        }

        // Method to get dice image filename
        public string GetDiceImageName(int diceValue)
        {
            return $"side_{diceValue}.png";
        }
    }
