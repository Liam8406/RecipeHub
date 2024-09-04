using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.ModelFactoryFolder;
using RecipeData.DataAccessLayer.Repositories;

namespace RecipeData.DataAccessLayer.Repositories
{
    public class RecipeUnitOfWork
    {
       
        RecipeRepository recipeRepository;
        ReviewsRepository reviewsRepository;
        FavouritesRepository favouritesRepository;
        TagRepository tagRepository;
        UserRepository userRepository;

        DbContext dbContext;
        ModelFactory modelFactory;
        
      
        public RecipeRepository RecipeRepository
        {
            get
            {
                if (recipeRepository == null)
                    recipeRepository = new RecipeRepository(dbContext, modelFactory);
                return recipeRepository;
            }

        }
        public FavouritesRepository FavouritesRepository
        {
            get
            {
                if (favouritesRepository == null)
                    favouritesRepository = new FavouritesRepository(dbContext, modelFactory);
                return favouritesRepository;
            }
        }
        public TagRepository TagsRepository
        {
            get
            {
                if (tagRepository == null)
                    tagRepository = new TagRepository(dbContext, modelFactory);
                return tagRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(dbContext, modelFactory);
                return userRepository;
            }
        }
        public ReviewsRepository ReviewsRepository
        {
            get
            {
                if(reviewsRepository == null)
                    reviewsRepository = new ReviewsRepository(dbContext, modelFactory);
                return reviewsRepository;
            }
        }

        public RecipeUnitOfWork()
        {
            this.dbContext = OleDbContext.GetInstance();
            this.modelFactory = new ModelFactory();
            this.modelFactory = new ModelFactory();
        }

        public RecipeUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}