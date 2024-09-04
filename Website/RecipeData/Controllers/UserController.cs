using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeData;
using System.Diagnostics;
using RecipeData.DataAccessLayer.Repositories;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.ViewModels;
using RecipeData.DataAccessLayer.Models;


namespace RecipeData.Controllers
{
    public class UserController : ApiController
    {
        private readonly DbContext dbContext;
        private readonly RecipeUnitOfWork unitOfWork;
        private readonly RecipeListViewModel recipeListViewModel;

        public UserController()
        {
            dbContext = OleDbContext.GetInstance();
            unitOfWork = new RecipeUnitOfWork(dbContext);
            recipeListViewModel = new RecipeListViewModel();
        }

        [HttpGet]
        public RecipeListViewModel GetRecipeListViewModel()
        {
            try
            {
                dbContext.OpenConnection();
                recipeListViewModel.Recipes = unitOfWork.RecipeRepository.ReadAll().ToList();
                recipeListViewModel.Tags = unitOfWork.TagsRepository.ReadAll().ToList();
                return recipeListViewModel;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }

        [HttpGet]
        public RecipeListViewModel GetRecipesByTag(string tag)
        {
            try
            {
                dbContext.OpenConnection();
                recipeListViewModel.Recipes = unitOfWork.RecipeRepository.GetRecipesByTag(tag);
                recipeListViewModel.Tags = unitOfWork.TagsRepository.ReadAll().ToList();
                return recipeListViewModel;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
        public string GetTagNameByTagID(string TagID)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.TagsRepository.GetTagNameByTagID(TagID);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
        [HttpGet]
        public RecipeListViewModel GetRecipesByName(string name)
        {
            try
            {
                dbContext.OpenConnection();
                recipeListViewModel.Recipes = unitOfWork.RecipeRepository.GetRecipesByName(name);
                recipeListViewModel.Tags = unitOfWork.TagsRepository.ReadAll().ToList();
                return recipeListViewModel;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }

        [HttpGet]
        public RecipeListViewModel GetRecipesByTime(int time)
        {
            try
            {
                dbContext.OpenConnection();
                recipeListViewModel.Recipes = unitOfWork.RecipeRepository.GetRecipesByTime(time);
                recipeListViewModel.Tags = unitOfWork.TagsRepository.ReadAll().ToList();
                return recipeListViewModel;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
        
        [HttpPost]
        [Route("api/User/AddNewRecipe")]
        public bool AddNewRecipe(RecipesModel recipe)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.RecipeRepository.Create(recipe);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            finally
            {
                dbContext.CloseConnection();
            }

        }
        [Route("api/User/AddToFavorites")]
        [HttpPost]
        public bool AddToFavorites(FavouritesModel favourite)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.FavouritesRepository.Create(favourite);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
        [Route("api/User/GetFavouriteRecipes")]
        [HttpGet]
        public List<RecipesModel> GetFavouriteRecipes(string UserID)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.FavouritesRepository.GetFavouriteRecipes(UserID);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }

        }
        [HttpPost]
        public bool DidUserAlreadyPostReview(string UserID, string RecipeID)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.ReviewsRepository.DidUserAlreadyPostReview(UserID, RecipeID);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            finally
            {
                dbContext.CloseConnection();
            }

        }
        
        [Route("api/User/GetReviewIDByRecipeIDAndUserID")]
        [HttpGet]
        public string GetReviewIDByRecipeIDAndUserID(string UserID, string RecipeID)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.ReviewsRepository.GetReviewIDByRecipeIdAndUserID(UserID, RecipeID);


            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }

        }
        [Route("api/User/AddReview")]
        [HttpPost]
        public bool AddReview(ReviewModel review)
        {
            try
            {

                if (DidUserAlreadyPostReview(review.UserID, review.RecipeID))
                {
                    return false;
                }
                dbContext.OpenConnection();
                return unitOfWork.ReviewsRepository.Create(review);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            finally
            {
                dbContext.CloseConnection();
            }

        }
        
        [HttpPost]
        [Route("api/User/RemoveComment")]
        public bool RemoveComment(ReviewModel review)
        {
            try
            {
                
                review.Id = GetReviewIDByRecipeIDAndUserID(review.UserID, review.RecipeID);
                dbContext.OpenConnection();
                unitOfWork.ReviewsRepository.Delete(review.Id);
                return true;

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            finally
            {
                dbContext.CloseConnection();
            }

        }
        [HttpGet]
        public bool IsRecipeFavourited(string UserID, string RecipeID)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.FavouritesRepository.IsRecipeFavourited(UserID, RecipeID);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
            [HttpGet]
        public List<ReviewModel> GetReviewsByRecipeId(string RecipeId)
        {
            try
            {
                dbContext.OpenConnection();
                return unitOfWork.ReviewsRepository.GetReviewsByRecipeId(RecipeId);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteOwnComment(string userId, string reviewId)
        {
            try
            {
                dbContext.OpenConnection();
                var reviews = unitOfWork.ReviewsRepository.GetT(reviewId);
                if (reviews != null && reviews.UserID == userId)
                {
                    unitOfWork.ReviewsRepository.Delete(reviewId);
                    return Ok();
                }
                else
                {
                    return BadRequest("Comment not found or you don't have permission to delete.");
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
        
    }
}


