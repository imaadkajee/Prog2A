using System;

namespace ImaadPoeFinal
{
    public class Ingredient
    {
        // Gets or sets the name of the ingredient.
        public string Name { get; set; }

        // Gets or sets the quantity of the ingredient.
        public double Quantity { get; set; }

        // Gets or sets the original quantity of the ingredient before scaling.
        public double OriginalQuantity { get; set; }

        // Gets or sets the original calories of the ingredient before scaling.
        public int OriginalCalories { get; set; }

        // Gets or sets the unit of measurement for the ingredient.
        public string Unit { get; set; }

        // Gets or sets the calories of the ingredient.
        public int Calories { get; set; }

        // Gets or sets the food group of the ingredient.
        public string FoodGroup { get; set; }

        // Gets or sets the scaling factor applied to the ingredient's quantity and calories.
        // This property is internal to limit its access to within the assembly.
        internal double ScaleFactor { get; set; }
    }
}
