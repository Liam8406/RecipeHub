using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeData.DataAccessLayer.ModelFactoryFolder;

namespace RecipeData.DataAccessLayer.ModelFactoryFolder
{
    public class ModelFactory
    {
        //מפעל של המודלים. כדי ליצור מודל צריך לפנות למפעל והוא מייצר אותו
        RecipesCreator recipeCreator;
        TagCreator tagCreator;
        UserCreator userCreator;
        ReviewsCreator reviewsCreator;
        FavouritesCreator favouritesCreator;
        AdminCreator adminCreator;
        

        public RecipesCreator RecipesCreator
        {
            get
            {
                if (this.recipeCreator == null)
                    this.recipeCreator = new RecipesCreator();
                return this.recipeCreator;
            }
        }
        public UserCreator UserCreator
        {
            get
            {
                if (this.userCreator == null)
                    this.userCreator = new UserCreator();
                return this.userCreator;
            }
        }
        public ReviewsCreator ReviewsCreator
        {
            get
            {
                if (this.reviewsCreator == null)
                    this.reviewsCreator = new ReviewsCreator();
                return this.reviewsCreator;
            }
        }
        public TagCreator TagCreator
        {
            get
            {
                if (this.tagCreator == null)
                    this.tagCreator = new TagCreator();
                return this.tagCreator;
            }
        }
        public FavouritesCreator FavouritesCreator
        {
            get
            {
                if (this.favouritesCreator == null)
                    this.favouritesCreator = new FavouritesCreator();
                return this.favouritesCreator;
            }
        }
        public AdminCreator AdminCreator
        {
            get
            {
                if (this.adminCreator == null)
                    this.adminCreator = new AdminCreator();
                return this.adminCreator;
            }
        }

    }
}
