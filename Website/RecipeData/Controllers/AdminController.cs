using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeData.DataAccessLayer.Repositories;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.ViewModels;
using RecipeData.DataAccessLayer.Models;

namespace RecipeData.Controllers
{
    public class AdminController : ApiController
    {
        private readonly DbContext dbContext;
        private readonly RecipeUnitOfWork unitOfWork;
        private readonly RecipeListViewModel recipeListViewModel;

        public AdminController()
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
        public IHttpActionResult AddReview(ReviewModel review)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.ReviewsRepository.Create(review);
                return Ok();
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
        public IHttpActionResult AddToFavorites(string userId, string recipeId)
        {
            try
            {
                dbContext.OpenConnection();
                FavouritesModel favouritesModel = new FavouritesModel
                {
                    RecipeID = recipeId,
                    UserID = userId
                };
                unitOfWork.FavouritesRepository.Create(favouritesModel);
                return Ok();
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

        [Route("api/Admin/DeleteRecipe")]
        [HttpPost]
        public bool DeleteRecipe(RecipesModel Recipe)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.RecipeRepository.Delete(Recipe.Id);
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
        [Route("api/Admin/UpdateRecipe")]
        [HttpPost]
        public bool UpdateRecipe(RecipesModel Recipe)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.RecipeRepository.Update(Recipe);
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
        [Route("api/Admin/DeleteReview")]
        [HttpPost]
        public bool DeleteReview(ReviewModel Review)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.ReviewsRepository.Delete(Review.Id);
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
        [Route("api/Admin/UpdateReview")]
        [HttpPost]
        public bool UpdateReview(ReviewModel Review)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.ReviewsRepository.Update(Review);
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
        
            [Route("api/Admin/DeleteUser")]
        [HttpPost]
        public bool DeleteUser(UserModel user)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.UserRepository.Delete(user.Id);
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
        [Route("api/Admin/UpdateUser")]
        [HttpPost]
        public bool UpdateUser(UserModel user)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.UserRepository.Update(user);
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
        [HttpPost]
        public IHttpActionResult AddRecipe(RecipesModel recipe)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.RecipeRepository.Create(recipe);
                return Ok();
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
        public IHttpActionResult DeleteReview(string reviewId)
        {
            try
            {
                dbContext.OpenConnection();
                unitOfWork.ReviewsRepository.Delete(reviewId);
                return Ok();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return InternalServerError();
            }
            finally
            {
                dbContext.CloseConnection();
            }
        }
    }
}
