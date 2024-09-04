using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.Models;
using RecipeData.DataAccessLayer.ModelFactoryFolder;


namespace RecipeData.DataAccessLayer.Repositories
{
    public class FavouritesRepository : Repository, IRepository<FavouritesModel>
    {
        public FavouritesRepository(DbContext dbContext, ModelFactory favouritesModelFactory) : base(dbContext, favouritesModelFactory)
        {
        }

        public bool Create(FavouritesModel model)
        {
            string sql = $@"INSERT INTO Favourites (RecipeID, UserID) 
                        VALUES (@RecipeID, @UserID)";
            base.dbContext.AddParameters("@RecipeID", model.RecipeID);
            base.dbContext.AddParameters("@UserID", model.UserID);
            model.Id = Convert.ToString(base.dbContext.GetLastCreatedId());
            return this.dbContext.ExecuteNonQuery(sql) > 0;
        }
        public void Update(FavouritesModel model)
        {
            //Does not require
        }

        public void Delete(string id)
        {
            string sql = @"DELETE FROM Favourites WHERE FavouriteID = @FavouriteID";
            base.dbContext.AddParameters("@FavouriteID", id);
            base.dbContext.ExecuteNonQuery(sql);
        }

        public FavouritesModel GetT(string id)
        {
            string sql = @"SELECT * FROM Favourites WHERE FavouriteID = @FavouriteID";
            IDataReader dataReader = base.dbContext.Read(sql);
            FavouritesModel favourite = recipeSiteModelFactory.FavouritesCreator.CreateModel(dataReader);
            return favourite;
        }

        public IEnumerable<FavouritesModel> ReadAll()
        {
            List<FavouritesModel> favourites = new List<FavouritesModel>();
            string sql = @"SELECT * FROM Favourites";
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    favourites.Add(recipeSiteModelFactory.FavouritesCreator.CreateModel(dataReader));
                }
            }
            return favourites;
        }
        public List<RecipesModel> GetFavouriteRecipes(string userID)
        {
            List<RecipesModel> favouriteRecipes = new List<RecipesModel>();

            // SQL query to fetch favorite recipes
            string sql = @"
        SELECT r.*
        FROM [Favourites] AS f
        INNER JOIN [Recipes] AS r ON f.[RecipeID] = r.[RecipeID]
        WHERE f.[UserID] = @UserID";

            base.dbContext.AddParameters("@UserID", userID);

            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    RecipesModel recipe = recipeSiteModelFactory.RecipesCreator.CreateModel(dataReader);
                    favouriteRecipes.Add(recipe);
                }
            }

            return favouriteRecipes;
        }
        public bool IsRecipeFavourited(string UserID, string RecipeID)
        {
                string sql = "SELECT COUNT(*) FROM Favourites WHERE UserID = @UserID AND RecipeID = @RecipeID";
                base.dbContext.AddParameters("@UserID", UserID);
                base.dbContext.AddParameters("@RecipeID", RecipeID);
                int count = Convert.ToInt32(base.dbContext.ExecuteScalar(sql));
                return count > 0;
        }

    }
}