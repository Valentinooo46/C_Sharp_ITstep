using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace RecipeApp
{
    class Recipe
    {
        public string Title { get; set; }
        public string Cuisine { get; set; }
        public List<string> Ingredients { get; set; }
        public int CookingTime { get; set; } // in minutes
        public string Instructions { get; set; }

        public override string ToString()
        {
            return $"Title: {Title}\nCuisine: {Cuisine}\nIngredients: {string.Join(", ", Ingredients)}\nCooking Time: {CookingTime} minutes\nInstructions: {Instructions}\n";
        }

        virtual public string  ToFileFormat()
        {
            return $"{Title}|{Cuisine}|{string.Join(",", Ingredients)}|{CookingTime}|{Instructions}";
        }

        public static Recipe FromFileFormat(string fileLine)
        {
            var parts = fileLine.Split('|');
            return new Recipe
            {
                Title = parts[0],
                Cuisine = parts[1],
                Ingredients = new List<string>(parts[2].Split(',')),
                CookingTime = int.Parse(parts[3]),
                Instructions = parts[4]
            };
        }
    }
    
    //task3
    class Recipe2 : Recipe
    {
        public string Type {  get; set; }
        public int Calories { get; set; } // in joules

        public override string ToString()
        {
            return $"Title: {Title}\nCuisine: {Cuisine}\nIngredients: {string.Join(", ", Ingredients)}\nCooking Time: {CookingTime} minutes\nInstructions: {Instructions}\nCalories: {Calories}\nType: {Type}";
        }

        override public string  ToFileFormat()
        {
            return $"{Title}|{Cuisine}|{string.Join(",", Ingredients)}|{CookingTime}|{Instructions}|{Calories}|{Type}";
        }

        public static Recipe2 FromFileFormat(string fileLine)
        {
            var parts = fileLine.Split('|');
            return new Recipe2
            {
                Title = parts[0],
                Cuisine = parts[1],
                Ingredients = new List<string>(parts[2].Split(',')),
                CookingTime = int.Parse(parts[3]),
                Instructions = parts[4],
                Calories = int.Parse(parts[5]),
                Type = parts[6]

            };
        }
    }

    class RecipeManager
    {
        protected List<Recipe> recipes = new List<Recipe>();

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        public void RemoveRecipe(string title)
        {
            recipes.RemoveAll(r => r.Title.Equals(title));  //лямбда функція(предикат)
        }

        public void UpdateRecipe(string title, Recipe updatedRecipe)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].Title.Equals(title))
                {
                    recipes[i] = updatedRecipe;
                    break;
                }
            }
        }

        public List<Recipe> SearchRecipes(string keyword)
        {
            return recipes.FindAll(r =>
                r.Title.IndexOf(keyword) >= 0 ||
                r.Cuisine.IndexOf(keyword) >= 0);
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var recipe in recipes)
                {
                    writer.WriteLine(recipe.ToFileFormat());
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        recipes.Add(Recipe.FromFileFormat(line));
                    }
                }
            }
        }

        public void PrintAllRecipes()
        {
            foreach (var recipe in recipes)
            {
                Console.WriteLine(recipe);
            }
        }
    }
    class RecipeManager2
    {
        protected List<Recipe2> recipes = new List<Recipe2>();

        public void AddRecipe(Recipe2 recipe)
        {
            recipes.Add(recipe);
        }

        public void RemoveRecipe(string title)
        {
            recipes.RemoveAll(r => r.Title.Equals(title));  //лямбда функція(предикат)
        }

        public void UpdateRecipe(string title, Recipe2 updatedRecipe)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].Title.Equals(title))
                {
                    recipes[i] = updatedRecipe;
                    break;
                }
            }
        }

        public List<Recipe2> SearchRecipes(string keyword)
        {
            return recipes.FindAll(r =>
                r.Title.IndexOf(keyword) >= 0 ||
                r.Cuisine.IndexOf(keyword) >= 0);
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var recipe in recipes)
                {
                    writer.WriteLine(recipe.ToFileFormat());
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        recipes.Add(Recipe2.FromFileFormat(line));
                    }
                }
            }
        }

        public void PrintAllRecipes()
        {
            foreach (var recipe in recipes)
            {
                Console.WriteLine(recipe);
            }
        }
    }
    
        



        
        

        class RecipeManagerExt : RecipeManager
        {
            // Звіт за видом кухні
            public void ReportByCuisine(string filePath = null)
            {
                Dictionary<string, List<Recipe>> report = new Dictionary<string, List<Recipe>>();

                foreach (var recipe in recipes)
                {
                    if (!report.ContainsKey(recipe.Cuisine))
                    {
                        report[recipe.Cuisine] = new List<Recipe>();
                    }
                    report[recipe.Cuisine].Add(recipe);
                }

                if (filePath != null)
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var group in report)
                        {
                            writer.WriteLine($"Cuisine: {group.Key}");
                            foreach (var recipe in group.Value)
                            {
                                writer.WriteLine(recipe);
                            }
                            writer.WriteLine();
                        }
                    }
                }
                else
                {
                    foreach (var group in report)
                    {
                        Console.WriteLine($"Cuisine: {group.Key}");
                        foreach (var recipe in group.Value)
                        {
                            Console.WriteLine(recipe);
                        }
                        Console.WriteLine();
                    }
                }
            }

            // Звіт за часом приготування
            public void ReportByCookingTime(string filePath = null)
            {
                SortedDictionary<int, List<Recipe>> report = new SortedDictionary<int, List<Recipe>>();

                foreach (var recipe in recipes)
                {
                    if (!report.ContainsKey(recipe.CookingTime))
                    {
                        report[recipe.CookingTime] = new List<Recipe>();
                    }
                    report[recipe.CookingTime].Add(recipe);
                }

                if (filePath != null)
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var group in report)
                        {
                            writer.WriteLine($"Cooking Time: {group.Key} minutes");
                            foreach (var recipe in group.Value)
                            {
                                writer.WriteLine(recipe);
                            }
                            writer.WriteLine();
                        }
                    }
                }
                else
                {
                    foreach (var group in report)
                    {
                        Console.WriteLine($"Cooking Time: {group.Key} minutes");
                        foreach (var recipe in group.Value)
                        {
                            Console.WriteLine(recipe);
                        }
                        Console.WriteLine();
                    }
                }
            }

            // Звіт за назвами інгредієнтів
            public void ReportByIngredients(string ingredient, string filePath = null)
            {
                List<Recipe> report = new List<Recipe>();

                foreach (var recipe in recipes)
                {
                    if (recipe.Ingredients.Contains(ingredient))
                    {
                        report.Add(recipe);
                    }
                }

                if (filePath != null)
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Recipes with ingredient: {ingredient}");
                        foreach (var recipe in report)
                        {
                            writer.WriteLine(recipe);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Recipes with ingredient: {ingredient}");
                    foreach (var recipe in report)
                    {
                        Console.WriteLine(recipe);
                    }
                }
            }

            // Звіт за назвою рецепта
            public void ReportByTitle(string title, string filePath = null)
            {
                List<Recipe> report = new List<Recipe>();

                foreach (var recipe in recipes)
                {
                    if (recipe.Title.Equals(title))
                    {
                        report.Add(recipe);
                    }
                }

                if (filePath != null)
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Recipes with title: {title}");
                        foreach (var recipe in report)
                        {
                            writer.WriteLine(recipe);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Recipes with title: {title}");
                    foreach (var recipe in report)
                    {
                        Console.WriteLine(recipe);
                    }
                }
            }
        }
    class RecipeManagerExt2 : RecipeManager2
    {
        // за типом страви
        public void ReportByType(string Type, string filePath = null) { 
            List<Recipe2> report = new List<Recipe2>();
            foreach (var recipe in recipes)
            {
                if (recipe.Type.Equals(Type))
                {
                    report.Add(recipe);
                }
            }
            if(filePath != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Recipes with type: {Type}");
                    foreach (var recipe in report)
                    {
                        writer.WriteLine(recipe);
                    }
                }
            }
        }
        // за сумою калорій
        public void ReportByenergy(uint Energy, string filePath = null)
        {
            List<Recipe2> report = new List<Recipe2>();
            foreach (var recipe in recipes)
            {
                if (recipe.Calories <= Energy)
                {
                    report.Add(recipe);
                }
            }
            if (filePath != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Recipes with energy equals or lower: {Energy}");
                    foreach (var recipe in report)
                    {
                        writer.WriteLine(recipe);
                    }
                }
            }
        }
        public void ReportBySet(string filePath = null)
        {
            List<Recipe2> report = new List<Recipe2>();
            foreach (var recipe in recipes)
            {
                if (report.Find(r => r.Type == recipe.Type) == null)
                {
                    report.Add(recipe);
                }
            }
            if (filePath != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Set of recipes:");
                    foreach (var recipe in report)
                    {
                        writer.WriteLine(recipe);
                    }
                }
            }
        }


    }


}






