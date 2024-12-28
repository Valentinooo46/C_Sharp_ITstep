using RecipeApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class File1
    {
        static void Main(string[] args)
        {
            RecipeManagerExt2 recipeManagerExt = new RecipeManagerExt2();
            recipeManagerExt.AddRecipe(new Recipe2 { Title = "Sushi", Type = "second", Calories = 900, Cuisine = "Japan", Ingredients = new List<string> { "Nori", "rice", "red fish" }, CookingTime = 14, Instructions = "Just boil rice and roll the sushi" });
            recipeManagerExt.AddRecipe(new Recipe2 { Title = "Rolls", Type = "second", Calories = 1200, Cuisine = "Japan", Ingredients = new List<string> { "Nori", "rice", "red fish","peanut","cheese" }, CookingTime = 20, Instructions = "Just boil rice and roll the rolls" });
            recipeManagerExt.AddRecipe(new Recipe2 { Title = "Pizza", Type = "first", Calories = 2000, Cuisine = "Italy", Ingredients = new List<string> { "dough", "ketchup", "cheese", "meat", "pinapple" }, CookingTime = 120, Instructions = "throw ingridienst on the disc of dough and bake it" });
            List<Recipe2> temp = recipeManagerExt.SearchRecipes("Japan");
            foreach (Recipe recipe in temp)
            {
                Console.WriteLine(recipe);
            }
            recipeManagerExt.ReportBySet("C:\\Users\\valea\\source\\repos\\Project2\\Project2\\temp.txt");
            
        }
        
    }
}
