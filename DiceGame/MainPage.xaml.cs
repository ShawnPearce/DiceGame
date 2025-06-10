//Shawn Pearce
//Dice Game

using DiceGame.Resources.BusinessLogic;

namespace DiceGame;

public partial class MainPage : ContentPage
{
     // Create an instance of the Game class
       private Game game;

        public MainPage()
        {
            InitializeComponent();
            
            // Initialize the game
            game = new Game();
            
            // Set default game mode
            GameModePicker.SelectedIndex = 0; // Two dice mode
            
            // Update the display
            UpdateDisplay();
        }

        // Method called when game mode is changed
        private void OnGameModeChanged(object sender, EventArgs e)
        {
            if (GameModePicker.SelectedIndex == 0)
            {
                // Two dice mode
                game.SetThreeDiceMode(false);
                TitleLabel.Text = "Two Dice Game";
                RulesLabel.Text = "Win with sum of 7 or 11";
                Dice3Image.IsVisible = false;
            }
            else
            {
                // Three dice mode
                game.SetThreeDiceMode(true);
                TitleLabel.Text = "Three Dice Game (Bonus)";
                RulesLabel.Text = "Win with sum of 11 or 18";
                Dice3Image.IsVisible = true;
            }
            
            UpdateDisplay();
            RollButton.IsEnabled = true;
        }

        // Method called when Roll Dice button is clicked
        private void OnRollDiceClicked(object sender, EventArgs e)
        {
            
            if (game.IsGameOver())
            {
                StatusLabel.Text = "Game is over! Click Reset to play again.";
                return;
            }

            // Roll the dice
            int[] diceValues = game.RollDice();
            
            // Update the dice images
            Dice1Image.Source = game.GetDiceImageName(diceValues[0]);
            Dice2Image.Source = game.GetDiceImageName(diceValues[1]);
            
            // Update third dice if in three dice mode
            if (game.IsThreeDiceMode() && diceValues.Length > 2)
            {
                Dice3Image.Source = game.GetDiceImageName(diceValues[2]);
            }
            
            // Calculate and show the sum
            int sum = game.GetSum(diceValues);
            SumLabel.Text = $"Sum: {sum}";
            
            // Update attempts counter
            AttemptsLabel.Text = $"Attempts: {game.GetAttempts()} / 3";
            
            // Check game result and update status message
            if (game.IsGameWon())
            {
                StatusLabel.Text = "You win!";
                StatusLabel.TextColor = Colors.Green;
                RollButton.IsEnabled = false;
            }
            else if (game.IsGameOver())
            {
                StatusLabel.Text = "Computer wins!";
                StatusLabel.TextColor = Colors.Red;
                RollButton.IsEnabled = false;
            }
            else
            {
                StatusLabel.Text = "Try again!";
                StatusLabel.TextColor = Colors.Orange;
            }
        }

        // Method called when Reset button is clicked
        private void OnResetClicked(object sender, EventArgs e)
        {
            // Reset the game
            game.ResetGame();
            
            // Reset the display
            UpdateDisplay();
            
            // Enable the roll button
            RollButton.IsEnabled = true;
        }

        // Method to update the display to initial state
        private void UpdateDisplay()
        {
            // Reset dice images
            Dice1Image.Source = "side_1.png";
            Dice2Image.Source = "side_2.png";
            Dice3Image.Source = "side_3.png";
            
            // Reset sum display
            SumLabel.Text = "Sum: --";
            
            // Reset status message
            StatusLabel.Text = "Click 'Roll Dice' to start playing!";
            StatusLabel.TextColor = Colors.Black;
            
            // Reset attempts counter
            AttemptsLabel.Text = $"Attempts: {game.GetAttempts()} / 3";
        }
    }