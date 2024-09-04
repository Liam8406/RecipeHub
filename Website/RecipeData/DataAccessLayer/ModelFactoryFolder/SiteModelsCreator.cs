using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data;
using RecipeData.DataAccessLayer.Models;
using System.Diagnostics;


namespace RecipeData.DataAccessLayer.ModelFactoryFolder
{
    public class RecipesCreator: IOleDbModelCreator<RecipesModel>
    {
        public RecipesModel CreateModel(IDataReader src)
        {
            RecipesModel recipe = new RecipesModel();
            recipe.Id = Convert.ToString(src["RecipeID"]);
            recipe.RecipeName = Convert.ToString(src["RecipeName"]);
            recipe.Time = Convert.ToInt32(src["Time"]);
            recipe.Calories = Convert.ToInt32(src["Calories"]);
            recipe.Description = Convert.ToString(src["Description"]);
            recipe.Ingredients = Convert.ToString(src["Ingredients"]);
            recipe.Instructions = Convert.ToString(src["Instructions"]);
            recipe.RecipeImage = Convert.ToString(src["RecipeImage"]);
            recipe.TagID = Convert.ToString(src["TagID"]);
            return recipe;
        }
    }
    public class UserCreator : IOleDbModelCreator<UserModel>
    {
        public UserModel CreateModel(IDataReader src)
        {
            //src.Read();
            UserModel user = new UserModel();
            user.Id = Convert.ToString(src["UserID"]);
            user.UserName = Convert.ToString(src["UserName"]);
            user.UserEmail = Convert.ToString(src["UserEmail"]);
            user.UserPassword = Convert.ToString(src["UserPassword"]);
            return user;         
        }
    }
    public class ReviewsCreator : IOleDbModelCreator<ReviewModel>
    {
        public ReviewModel CreateModel(IDataReader src)
        {
            ReviewModel review = new ReviewModel();
            review.Id = Convert.ToString(src["ReviewID"]);
            review.RecipeID = Convert.ToString(src["RecipeID"]);
            review.ReviewDescription = Convert.ToString(src["ReviewDescription"]);
            review.UserID = Convert.ToString(src["UserID"]);
            review.DatePosted = Convert.ToDateTime(src["DatePosted"]);
            review.Rating = Convert.ToInt32(src["rating"]);
            return review;
        }
    }

    public class TagCreator : IOleDbModelCreator<TagModel>
    {
        public TagModel CreateModel(IDataReader src)
        {
            TagModel tag = new TagModel();
            tag.TagName = Convert.ToString(src["TagName"]);
            return tag;
        }
    }
    public class FavouritesCreator : IOleDbModelCreator<FavouritesModel>
    {
        public FavouritesModel CreateModel(IDataReader src)
        {
            FavouritesModel favourites = new FavouritesModel()
            {
               UserID = Convert.ToString(src["UserID"]),
               RecipeID = Convert.ToString(src["RecipeID"]),
            };
            return favourites;
        }
    }
    public class AdminCreator : IOleDbModelCreator<Admin>
    {
        public Admin CreateModel(IDataReader src)
        {
            //src.Read();
            Admin admin = new Admin();
            admin.Id = Convert.ToString(src["AdminID"]);
            admin.UserName = Convert.ToString(src["UserName"]);
            admin.UserPassword = Convert.ToString(src["UserPassword"]);
            return admin;
        }
    }

}