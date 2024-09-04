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
    public class ReviewsRepository : Repository, IRepository<ReviewModel>
    {
        public ReviewsRepository(DbContext dbContext, ModelFactory reviewModelFactory) : base(dbContext, reviewModelFactory)
        {
        }

        public bool Create(ReviewModel model)
        {
            string sql = $@"INSERT INTO [Reviews] ([RecipeID], [UserID], [DatePosted], [ReviewDescription], [Rating]) 
                        VALUES (@RecipeID, @UserID, @DatePosted, @ReviewDescription, @Rating)";
            base.dbContext.AddParameters("@RecipeID", model.RecipeID);
            base.dbContext.AddParameters("@UserID", model.UserID);
            base.dbContext.AddParameters("@DatePosted", model.DatePosted);
            base.dbContext.AddParameters("@ReviewDescription", model.ReviewDescription);
            base.dbContext.AddParameters("@Rating", model.Rating);
            //base.dbContext.ExecuteNonQuery(sql);
            model.Id = Convert.ToString(base.dbContext.GetLastCreatedId());
            return this.dbContext.ExecuteNonQuery(sql) > 0;
        }

        public void Update(ReviewModel model)
        {
            string sql = $@"UPDATE Reviews 
                    SET RecipeID = @RecipeID, 
                        UserID = @UserID, 
                        DatePosted = @DatePosted, 
                        ReviewDescription = @ReviewDescription, 
                        Rating = @Rating 
                    WHERE ReviewID = @ReviewID";

            base.dbContext.AddParameters("@RecipeID", model.RecipeID);
            base.dbContext.AddParameters("@UserID", model.UserID);
            base.dbContext.AddParameters("@DatePosted", model.DatePosted);
            base.dbContext.AddParameters("@ReviewDescription", model.ReviewDescription);
            base.dbContext.AddParameters("@Rating", model.Rating);
            base.dbContext.AddParameters("@ReviewID", model.Id);
            base.dbContext.ExecuteNonQuery(sql);
        }


        public void Delete(string id)
        {
            string sql = @"DELETE FROM Reviews WHERE ReviewID = @ReviewID";
            base.dbContext.AddParameters("@ReviewID", id);
            base.dbContext.ExecuteNonQuery(sql);
        }

        public ReviewModel GetT(string id)
        {
            string sql = @"SELECT * FROM Reviews WHERE ReviewID = @ReviewID";
            IDataReader dataReader = base.dbContext.Read(sql);
            ReviewModel review = recipeSiteModelFactory.ReviewsCreator.CreateModel(dataReader);
            return review;
        }

        public IEnumerable<ReviewModel> ReadAll()
        {
            List<ReviewModel> reviews = new List<ReviewModel>();
            string sql = @"SELECT * FROM Reviews";
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    reviews.Add(recipeSiteModelFactory.ReviewsCreator.CreateModel(dataReader));
                }
            }
            return reviews;
        }

        public List<ReviewModel> GetReviewsByRecipeId(string RecipeID)
        {
            List<ReviewModel> reviews = new List<ReviewModel>();
            string sql = @"SELECT * FROM [Reviews] WHERE [RecipeID] = @RecipeID";
            dbContext.AddParameters("@RecipeID", RecipeID);

            using (IDataReader dataReader = dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    ReviewModel review = recipeSiteModelFactory.ReviewsCreator.CreateModel(dataReader);
                    reviews.Add(review);
                }
            }

            return reviews;
        }
        public string GetReviewIDByRecipeIdAndUserID(string UserID, string RecipeID)
        {
            string sql = "SELECT ReviewID FROM Reviews WHERE UserID = @UserID AND RecipeID = @RecipeID";
            base.dbContext.AddParameters("@UserID", UserID);
            base.dbContext.AddParameters("@RecipeID", RecipeID);

                using (IDataReader dataReader = base.dbContext.Read(sql))
                {
                    if (dataReader.Read())
                    {
                        return dataReader["ReviewID"].ToString();
                    }
                }
            return null;
        }


        public bool DidUserAlreadyPostReview(string UserID, string RecipeID)
        {
            string sql = "SELECT * FROM Reviews WHERE UserID = @UserID AND RecipeID = @RecipeID";
            base.dbContext.AddParameters("@UserID", UserID);
            base.dbContext.AddParameters("@RecipeID", RecipeID);

            using (IDataReader dataReader = dbContext.Read(sql))
            {
                return dataReader.Read();
            }
        }

    }
}

