using System;
using System.Windows;

namespace ImaadPoeFinal
{
    public partial class FilterRecipesDialog : Window
    {
        // Property to store the ingredient name entered by the user
        public string IngredientName { get; set; }

        // Property to store the food group entered by the user
        public string FoodGroup { get; set; }

        // Property to store the maximum calories entered by the user
        public int MaxCalories { get; set; }

        // Initializes a new instance of the FilterRecipesDialog class
        public FilterRecipesDialog()
        {
            InitializeComponent(); // Initialize the components in the dialog
            WindowStartupLocation = WindowStartupLocation.CenterScreen; // Center the dialog on the screen
        }

        // Handles the click event of the Filter button
        // Gathers input from the user and validates it
        // If the input is valid, it sets the properties and closes the dialog with a true result
        // If the input is invalid, it shows an error message
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Trim and assign user input
            IngredientName = IngredientNameTextBox.Text.Trim();
            FoodGroup = FoodGroupTextBox.Text.Trim();

            // Try to parse the MaxCalories input
            if (int.TryParse(MaxCaloriesTextBox.Text.Trim(), out int maxCalories))
            {
                MaxCalories = maxCalories; // Set the MaxCalories property
                DialogResult = true; // Close the dialog with a positive result
            }
            else
            {
                // Show an error message if the input is invalid
                MessageBox.Show("Enter a valid number for Max Calories.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                MaxCaloriesTextBox.Focus(); // Set focus back to the MaxCaloriesTextBox
            }
        }

        // Handles the click event of the Cancel button
        // Closes the dialog without applying any filters
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Close the dialog with a negative result
        }
    }
}
