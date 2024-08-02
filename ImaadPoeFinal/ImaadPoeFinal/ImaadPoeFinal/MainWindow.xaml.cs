using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImaadPoeFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes; // List to store all recipes
        private List<Recipe> filteredRecipes; // List to store filtered recipes based on user criteria

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>(); // Initialize the recipes list
            filteredRecipes = new List<Recipe>(); // Initialize the filtered recipes list

            // Set window startup location to center
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Handles the event when the user clicks the "Enter Recipe Details" button
        private void EnterRecipeDetails_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe(); // Create a new recipe instance

            // Loop until a valid and unique recipe name is provided
            while (true)
            {
                recipe.Name = ShowInputDialog("Enter recipe name: ", "Recipe Name");
                if (string.IsNullOrWhiteSpace(recipe.Name))
                {
                    MessageBox.Show("Canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Check if recipe name already exists
                if (recipes.Any(r => r.Name.ToLower() == recipe.Name.ToLower()))
                {
                    MessageBox.Show(" Already exists. Enter a new recipe name.", "Same Name", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    break; // Break the loop if the name is valid and unique
                }
            }

            int numIngredients;
            // Loop until a valid number of ingredients is provided
            while (true)
            {
                string numIngredientsStr = ShowInputDialog("Enter number of ingredients:", "Number of Ingredients");
                if (int.TryParse(numIngredientsStr, out numIngredients) && numIngredients > 0)
                {
                    break;
                }
                else if (string.IsNullOrWhiteSpace(numIngredientsStr))
                {
                    MessageBox.Show("Entry canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Please enter a valid Number. (More Than 1)", "Input Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Define valid units of measurement
            string[] validUnits = {
                "g", "kg", "ml", "l", "cups", "tbsp", "tsp",
                "oz", "lb", "fl oz", "pt", "qt", "gal",
                "st", "t", "mg", "dl", "pinch", "dash",
                "cup (UK)", "tbsp (UK)"
            };

            // Loop to enter details for each ingredient
            for (int i = 0; i < numIngredients; i++)
            {
                Ingredient ingredient = new Ingredient();

                ingredient.Name = ShowInputDialog($"Name of Ingredient {i + 1}", $"Ingredient {i + 1} Name");
                if (string.IsNullOrWhiteSpace(ingredient.Name))
                {
                    MessageBox.Show("Recipe entry canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                double quantity;
                // Loop until a valid quantity is provided
                while (true)
                {
                    string quantityStr = ShowInputDialog($"Enter Quantity of {ingredient.Name}", $"Quantity of {ingredient.Name}");
                    if (double.TryParse(quantityStr, out quantity) && quantity > 0)
                    {
                        break;
                    }
                    else if (string.IsNullOrWhiteSpace(quantityStr))
                    {
                        MessageBox.Show("Recipe entry canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid Number. (More Than 1)", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                ingredient.Quantity = quantity;

                // Loop until a valid unit of measurement is provided
                while (true)
                {
                    string unit = ShowInputDialog($"Enter Unit of measurement for {ingredient.Name} ", $"Unit for {ingredient.Name}");
                    if (string.IsNullOrWhiteSpace(unit))
                    {
                        MessageBox.Show("Recipe entry canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else if (validUnits.Contains(unit.ToLower()))
                    {
                        ingredient.Unit = unit;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Invalid unit of measurement. Please enter one of the following units: g, kg, ml, l, cups, tbsp, tsp, oz, lb, fl oz, pt, qt, gal, st, t, mg, dl, pinch, dash, cup (UK), tbsp (UK).",
                            "Invalid Unit",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }

                int calories;
                // Loop until valid calorie information is provided
                while (true)
                {
                    string caloriesStr = ShowInputDialog($"Enter Calories for {ingredient.Name}", $"Calories for {ingredient.Name}");
                    if (int.TryParse(caloriesStr, out calories) && calories >= 0)
                    {
                        break;
                    }
                    else if (string.IsNullOrWhiteSpace(caloriesStr))
                    {
                        MessageBox.Show("Recipe entry canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid number for the calories.", " Input Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                ingredient.Calories = calories;

                ingredient.FoodGroup = ShowInputDialog($"Enter Food Group for {ingredient.Name}", $"Food Group for {ingredient.Name}");

                recipe.Ingredients.Add(ingredient);
            }

            int numSteps;
            // Loop until a valid number of steps is provided
            while (true)
            {
                string numStepsStr = ShowInputDialog("Enter Number of Steps", "Number of Steps");
                if (int.TryParse(numStepsStr, out numSteps) && numSteps > 0)
                {
                    break;
                }
                else if (string.IsNullOrWhiteSpace(numStepsStr))
                {
                    MessageBox.Show("Recipe entry canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Please enter a valid Number. (More Than 1)", "Input Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Loop to enter details for each step
            for (int i = 0; i < numSteps; i++)
            {
                string stepDescription = ShowInputDialog($"Enter Description for Step {i + 1}", $"Step {i + 1} Description");
                if (string.IsNullOrWhiteSpace(stepDescription))
                {
                    MessageBox.Show("Recipe entry canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                recipe.Steps.Add(new Step { Description = stepDescription });
            }

            // Prompt to save the recipe
            MessageBoxResult result = MessageBox.Show("Save the recipe?", "Save Recipe", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                recipes.Add(recipe);
                MessageBox.Show(" Captured and Saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Discarded.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Displays the list of recipes in the RecipeListBox
        private void DisplayRecipeList_Click(object sender, RoutedEventArgs e)
        {
            RecipeListBox.ItemsSource = recipes.OrderBy(r => r.Name);
        }

        // Filters recipes based on user criteria
        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            FilterRecipesDialog filterDialog = new FilterRecipesDialog();
            if (filterDialog.ShowDialog() == true)
            {
                string ingredientName = filterDialog.IngredientName;
                string foodGroup = filterDialog.FoodGroup;
                int maxCalories = filterDialog.MaxCalories;

                filteredRecipes = recipes.Where(r =>
                    (string.IsNullOrWhiteSpace(ingredientName) || r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientName.ToLower()))) &&
                    (string.IsNullOrWhiteSpace(foodGroup) || r.Ingredients.Any(i => i.FoodGroup.ToLower() == foodGroup.ToLower())) &&
                    (maxCalories == 0 || r.Ingredients.Sum(i => i.Calories) <= maxCalories))
                    .ToList();

                RecipeListBox.ItemsSource = filteredRecipes.OrderBy(r => r.Name);
            }
        }

        // Clears the filters and displays all recipes
        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            RecipeListBox.ItemsSource = recipes.OrderBy(r => r.Name);
        }

        // Closes the application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Handles the selection change event in the RecipeListBox
        private void RecipeListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem != null)
            {
                Recipe selectedRecipe = (Recipe)RecipeListBox.SelectedItem;
                DisplayRecipe(selectedRecipe);
            }
        }

        // Displays detailed information about the selected recipe
        private void DisplayRecipe(Recipe recipe)
        {
            int totalCalories = recipe.Ingredients.Sum(i => i.Calories);

            // Check if total calories exceed 300
            if (totalCalories > 300)
            {
                MessageBox.Show($"Warning: Recipe has {totalCalories} total calories, which is more than 300 calories.", "Calorie Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Clear previous inlines
            RecipeDetailsTextBlock.Inlines.Clear();

            // Add recipe name
            RecipeDetailsTextBlock.Inlines.Add(new Run("Recipe Name: ") { Foreground = new SolidColorBrush(Colors.Green) });
            RecipeDetailsTextBlock.Inlines.Add(new Run(recipe.Name + "\n\n") { Foreground = new SolidColorBrush(Colors.DarkSlateGray) });

            // Add ingredients
            RecipeDetailsTextBlock.Inlines.Add(new Run("Ingredients:\n") { Foreground = new SolidColorBrush(Colors.Green) });
            foreach (var ingredient in recipe.Ingredients)
            {
                RecipeDetailsTextBlock.Inlines.Add(new Run($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}\n") { Foreground = new SolidColorBrush(Colors.DarkSlateGray) });
                RecipeDetailsTextBlock.Inlines.Add(new Run($"Calories: ") { Foreground = new SolidColorBrush(Colors.Green) });
                RecipeDetailsTextBlock.Inlines.Add(new Run($"{ingredient.Calories}\n") { Foreground = new SolidColorBrush(Colors.DarkSlateGray) });
                RecipeDetailsTextBlock.Inlines.Add(new Run($"Food Group: ") { Foreground = new SolidColorBrush(Colors.Green) });
                RecipeDetailsTextBlock.Inlines.Add(new Run($"{ingredient.FoodGroup}\n\n") { Foreground = new SolidColorBrush(Colors.DarkSlateGray) });
            }

            // Add steps
            RecipeDetailsTextBlock.Inlines.Add(new Run("Steps:\n") { Foreground = new SolidColorBrush(Colors.Green) });
            int stepNumber = 1;
            foreach (var step in recipe.Steps)
            {
                RecipeDetailsTextBlock.Inlines.Add(new Run($"{stepNumber}. ") { Foreground = new SolidColorBrush(Colors.Green) });
                RecipeDetailsTextBlock.Inlines.Add(new Run(step.Description + "\n") { Foreground = new SolidColorBrush(Colors.DarkSlateGray) });
                stepNumber++;
            }

            // Add total calories
            RecipeDetailsTextBlock.Inlines.Add(new Run("\nTotal Calories: ") { Foreground = new SolidColorBrush(Colors.Green) });
            RecipeDetailsTextBlock.Inlines.Add(new Run(totalCalories.ToString()) { Foreground = new SolidColorBrush(Colors.DarkSlateGray) });
        }

        // Scales the selected recipe based on the user-provided factor
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Prompt user to select a recipe
            string recipeName = ShowRecipeSelectionDialog("Input a Recipe to Scale", "Recipe Selection");

            // Find the selected recipe
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name == recipeName);

            if (selectedRecipe == null)
            {
                MessageBox.Show("Recipe cannot be found or no recipe was selected.", "Error!!!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double factor = GetValidScalingFactor("Enter Scaling Factor (0.5 for half, 2 for double, 3 for triple)", "Scaling");
            if (factor == 0)
                return;

            foreach (var ingredient in selectedRecipe.Ingredients)
            {
                // Save original values if not already scaled
                if (ingredient.OriginalQuantity == 0)
                {
                    ingredient.OriginalQuantity = ingredient.Quantity;
                    ingredient.OriginalCalories = ingredient.Calories;
                }

                // Scale the quantity and calories
                ingredient.Quantity *= factor;
                ingredient.Calories = (int)(ingredient.OriginalCalories * factor);
            }

            DisplayRecipe(selectedRecipe);
        }

        // Prompts user for a valid scaling factor
        private double GetValidScalingFactor(string prompt, string caption)
        {
            double[] validFactors = { 0.5, 2, 3 }; // Valid scaling factors

            while (true)
            {
                string input = ShowInputDialog(prompt, caption);
                double factor;
                if (double.TryParse(input, out factor) && validFactors.Contains(factor))
                    return factor;

                MessageBox.Show("Input Invalid. Please enter a valid scaling factor (0.5 for half, 2 for double, 3 for triple).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Resets the scaling of the selected recipe to its original values
        private void ResetScale_Click(object sender, RoutedEventArgs e)
        {
            // Prompt user to select a recipe
            string recipeName = ShowRecipeSelectionDialog("Enter Recipe Name to Reset Scale", "Recipe Selection");

            // Find the selected recipe
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name == recipeName);

            if (selectedRecipe == null)
            {
                MessageBox.Show("Recipe cannot be found or no recipe was selected.", "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var ingredient in selectedRecipe.Ingredients)
            {
                if (ingredient.OriginalQuantity != 0 && ingredient.OriginalCalories != 0)
                {
                    ingredient.Quantity = ingredient.OriginalQuantity;
                    ingredient.Calories = ingredient.OriginalCalories;

                    // Reset original values
                    ingredient.OriginalQuantity = 0;
                    ingredient.OriginalCalories = 0;
                }
            }

            DisplayRecipe(selectedRecipe);
        }

        // Deletes a recipe from the list
        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Prompt user to enter the name of the recipe to delete
            string recipeNameToDelete = ShowInputDialog("Enter Recipe Name to Remove", "Recipe deleted");

            // Find the recipe in the list
            Recipe recipeToDelete = recipes.FirstOrDefault(r => r.Name == recipeNameToDelete);

            if (recipeToDelete == null)
            {
                MessageBox.Show("Recipe not found or no recipe selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Do you want to clear the recipe '{recipeToDelete.Name}' and enter a new one?", "Confirm Clear Recipe", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Remove the selected recipe
                recipes.Remove(recipeToDelete);
                RecipeListBox.ItemsSource = recipes.OrderBy(r => r.Name);

                // Clear the recipe details display
                RecipeDetailsTextBlock.Text = string.Empty;

                // Prompt user to enter a new recipe
                EnterRecipeDetails_Click(sender, e);
            }
        }

        // Shows an input dialog to the user
        private string ShowInputDialog(string prompt, string caption)
        {
            InputDialog inputDialog = new InputDialog(prompt, caption);
            if (inputDialog.ShowDialog() == true)
                return inputDialog.InputText;
            else
                return string.Empty;
        }

        // Gets a valid double input from the user
        private double GetValidDoubleInput(string prompt, string caption)
        {
            double value;
            while (true)
            {
                string input = ShowInputDialog(prompt, caption);
                if (double.TryParse(input, out value) && value > 0)
                    return value;

                MessageBox.Show("Invalid input. Please enter a valid positive number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Shows a dialog for recipe selection
        private string ShowRecipeSelectionDialog(string prompt, string caption)
        {
            // Prepare and show dialog to select recipe name
            InputDialog inputDialog = new InputDialog(prompt, caption);
            inputDialog.InputText = RecipeListBox.SelectedItem != null ? ((Recipe)RecipeListBox.SelectedItem).Name : "";

            if (inputDialog.ShowDialog() == true)
                return inputDialog.InputText;
            else
                return string.Empty;
        }
    }
}
