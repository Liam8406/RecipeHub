using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.ModelFactoryFolder;

namespace RecipeData.DataAccessLayer.Repositories
{
    public abstract class Repository
    {
        protected DBcontext.DbContext dbContext;
        protected ModelFactory recipeSiteModelFactory;

        public Repository(DBcontext.DbContext dbContext)
        {
            this.dbContext = dbContext;
            recipeSiteModelFactory = new ModelFactory();
        }
        //mmmm
        public Repository(DbContext dbContext, ModelFactory recipeModelFactory) : this(dbContext)
        {
        }
        //mmmm
        public void Save()
        {
            this.dbContext.SaveChanges();
        }

    }
}