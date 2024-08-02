using System;
using System.Collections.Generic;

namespace ImaadPoeFinal
{
    public class Recipe
    {
        // Gets or sets the name of the recipe.
        public string Name { get; set; }

        // Gets or sets the list of ingredients used in the recipe.
        public List<Ingredient> Ingredients { get; set; }

        // Gets or sets the list of steps to prepare the recipe.
        public List<Step> Steps { get; set; }

        // Initializes a new instance of the Recipe class.
        public Recipe()
        {
            // Initialize the lists for ingredients and steps
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }
    }
}
