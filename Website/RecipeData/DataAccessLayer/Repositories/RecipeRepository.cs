using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.Models;
using RecipeData.DataAccessLayer.ModelFactoryFolder;
using System.Diagnostics;

namespace RecipeData.DataAccessLayer.Repositories
{
    public class RecipeRepository:Repository, IRepository<RecipesModel>
    {
        public RecipeRepository(DbContext dbContext, ModelFactory recipeModelFactory) : base(dbContext, recipeModelFactory)
        {
        }

        public bool Create(RecipesModel model) 
        {
            string sql = $@"INSERT INTO [Recipes] ([RecipeName],[Time],[Ingredients],[Calories],[Description],[Instructions], [RecipeImage])
            VALUES ([@RecipeName],[@Time],[@Ingredients],[@Calories],[@Description],[@Instructions], [@RecipeImage])";
            base.dbContext.AddParameters("@RecipeName", model.RecipeName);
            base.dbContext.AddParameters("@Time", model.Time);
            base.dbContext.AddParameters("@Ingredients", model.Ingredients);
            base.dbContext.AddParameters("@Calories", model.Calories);
            base.dbContext.AddParameters("@Description", model.Description);
            base.dbContext.AddParameters("@Instructions", model.Instructions);
            base.dbContext.AddParameters("@RecipeImage", model.RecipeImage);

            model.Id = Convert.ToString(base.dbContext.GetLastCreatedId());
            return this.dbContext.ExecuteNonQuery(sql) > 0;
        }
        public void Update(RecipesModel model)
        {
            string sql = @"UPDATE Recipes 
                   SET RecipeName = @RecipeName,
                       [Time] = @Time,
                       Ingredients = @Ingredients,
                       Calories = @Calories,
                       [Description] = @Description,
                       Instructions = @Instructions,
                       RecipeImage = @RecipeImage
                   WHERE RecipeID = @RecipeID";
            base.dbContext.AddParameters("@RecipeName", model.RecipeName);
            base.dbContext.AddParameters("@Time", model.Time);
            base.dbContext.AddParameters("@Ingredients", model.Ingredients);
            base.dbContext.AddParameters("@Calories", model.Calories);
            base.dbContext.AddParameters("@Description", model.Description);
            base.dbContext.AddParameters("@Instructions", model.Instructions);
            base.dbContext.AddParameters("@RecipeImage", model.RecipeImage);
            base.dbContext.AddParameters("@RecipeID", model.Id);
            base.dbContext.ExecuteNonQuery(sql);
        }



        public void Delete(string id)
        {
            // Delete associated favourites first
            string deleteFavouritesSql = "DELETE FROM Favourites WHERE RecipeID=@RecipeID";
            base.dbContext.AddParameters("@RecipeID", id);
            base.dbContext.ExecuteNonQuery(deleteFavouritesSql);

            // Delete associated reviews next
            string deleteReviewsSql = "DELETE FROM Reviews WHERE RecipeID=@RecipeID";
            base.dbContext.AddParameters("@RecipeID", id);
            base.dbContext.ExecuteNonQuery(deleteReviewsSql);

            // Delete the recipe
            string deleteRecipeSql = "DELETE FROM Recipes WHERE RecipeID=@RecipeID";
            base.dbContext.AddParameters("@RecipeID", id);
            base.dbContext.ExecuteNonQuery(deleteRecipeSql);
        }




        public RecipesModel GetT(string id)
        {
            RecipesModel recipe = null;
            string sql = $@"SELECT * FROM Recipes WHERE RecipeID=@RecipeID";
            dbContext.AddParameters("@RecipeID", id);

            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                if (dataReader.Read())
                {
                    recipe = recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader);
                }
            }

            return recipe;
        }
        public IEnumerable<RecipesModel> ReadAll()
        {
            List<RecipesModel> recipes = new List<RecipesModel>();
            string sql = @"SELECT * FROM Recipes";

            using (IDataReader dataReader = this.dbContext.Read(sql))
            {
                while (dataReader.Read())
                { 
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }


        public List<RecipesModel> GetRecipesByTag(string tag)
        {
            string sql = $"SELECT * FROM Recipes WHERE Tags LIKE '%{tag}%'";
            List<RecipesModel> recipes = new List<RecipesModel>();

            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        public List<RecipesModel> GetRecipesByName(string name)
        {
            string sql = $"SELECT * FROM Recipes WHERE RecipeName LIKE '%' + @RecipeName + '%'";
            base.dbContext.AddParameters("@RecipeName", name);
            List<RecipesModel> recipes = new List<RecipesModel>();
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        public List<RecipesModel> GetRecipesByTime(int time)
        {
            string sql = $"SELECT * FROM Recipes WHERE Time <= @Time";
            base.dbContext.AddParameters("@Time", time);
            List<RecipesModel> recipes = new List<RecipesModel>();
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        public List<RecipesModel> GetRecipesByCalories(int Calories)
        {
            string sql = $"SELECT * FROM Recipes WHERE Calories <= @Calories";
            base.dbContext.AddParameters("@Calories", Calories);
            List<RecipesModel> recipes = new List<RecipesModel>();
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        
        public List<RecipesModel> GetRecipesByNameAndCalories(string name, int calories)
        {
            string sql = $"SELECT * FROM Recipes WHERE Calories <= @Calories AND RecipeName LIKE '%' + @RecipeName + '%'";
            base.dbContext.AddParameters("@Calories", calories);
            base.dbContext.AddParameters("@RecipeName", name);
            List<RecipesModel> recipes = new List<RecipesModel>();
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        public List<RecipesModel> GetRecipesBytimeAndCalories(int time, int calories)
        {
            string sql = $"SELECT * FROM Recipes WHERE Calories <= @Calories AND Time <= @Time";
            base.dbContext.AddParameters("@Calories", calories);
            base.dbContext.AddParameters("@Time", time);
            List<RecipesModel> recipes = new List<RecipesModel>();
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        public List<RecipesModel> GetRecipesByTimeAndName(int time, string name)
        {  
            string sql = $"SELECT * FROM Recipes WHERE Time <= @Time AND RecipeName LIKE '%' + @RecipeName + '%'";
            base.dbContext.AddParameters("@Time", time);
            base.dbContext.AddParameters("@RecipeName", name);
            List<RecipesModel> recipes = new List<RecipesModel>();
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        public List<RecipesModel> GetRecipesByTimeAndCaloriesAndName(int time, int calories, string name)
        {
            string sql = $"SELECT * FROM Recipes WHERE Calories <= @Calories AND Time <= @Time AND RecipeName LIKE '%' + @RecipeName + '%'";
            base.dbContext.AddParameters("@Calories", calories);
            base.dbContext.AddParameters("@Time", time);
            base.dbContext.AddParameters("@RecipeName", name);
            List<RecipesModel> recipes = new List<RecipesModel>();
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    recipes.Add(recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader));
                }
            }

            return recipes;
        }
        //not sure if I need this
        //public IEnumerable<RecipesModel> ReadAll(string criteria)
        //{
        //    List<RecipesModel> recipes = new List<RecipesModel>();
        //    string sql = $@"select * from Recipes where {criteria}";
        //    using (IDataReader dataReader = base.dbContext.ExecuteReader(sql))
        //    {
        //        while (dataReader.Read())
        //        {
        //            recipes.Add(recipeModelFactory.RecipesCreator.CreateModel(dataReader));
        //        }
        //    }
        //    return recipes;
        //}
    }
}