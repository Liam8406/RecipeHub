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
    //[RoutePrefix("api/Guest")]
    public class GuestController : ApiController
    {
        DbContext DbContext;
        RecipeUnitOfWork UnitOfWork;
        RecipeListViewModel recipeListViewModel;
        public GuestController()
        {
            DbContext = OleDbContext.GetInstance();
            UnitOfWork = new RecipeUnitOfWork(DbContext);
            recipeListViewModel = new RecipeListViewModel();
        }
        [HttpGet]
        public RecipeListViewModel GetRecipeListViewModel()
        {
            try
            {
                RecipeViewModel recipeViewModel = new RecipeViewModel();
                this.DbContext.OpenConnection();
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.ReadAll().ToList();
                recipeListViewModel.Reviews = UnitOfWork.ReviewsRepository.ReadAll().ToList();
                recipeListViewModel.Users = UnitOfWork.UserRepository.ReadAll().ToList();
                //recipeListViewModel.Tags = UnitOfWork.TagsRepository.ReadAll().ToList();
                 return recipeListViewModel;
            }
           catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
                
            }
            finally
            {
                DbContext.CloseConnection();
            }
        }
        [HttpGet]
        [Route("api/Guest/GetAllTags")]
        public List<TagModel> GetAllTags()
        {
            try
            {
                this.DbContext.OpenConnection();
                return UnitOfWork.TagsRepository.ReadAll().ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;

            }
            finally
            {
                DbContext.CloseConnection();
            }
        }
        //[HttpGet]
        //public string GetTagNameByTagID(string TagID)
        //{
        //    try
        //    {
        //        this.DbContext.OpenConnection();
        //        return UnitOfWork.TagsRepository.GetTagNameByTagID(TagID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLine(ex.Message);
        //        return null;

        //    }
        //}
        
        //[HttpGet]
        //public RecipeListViewModel GetRecipesByTag(string tag)
        //{
        //    RecipeListViewModel recipeListViewModel = new RecipeListViewModel();

        //    try
        //    {
        //        this.DbContext.OpenConnection();

        //        // Retrieve recipes by tag using RecipeRepository
        //        recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesByTag(tag);

        //        // Retrieve all tags using TagsRepository
        //        recipeListViewModel.Tags = UnitOfWork.TagsRepository.ReadAll().ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        Trace.WriteLine(message);
        //        return null;
        //    }
        //    finally
        //    {
        //        DbContext.CloseConnection();
        //    }

        //    return recipeListViewModel;
        //}
        [HttpGet]
        public RecipeListViewModel GetRecipesByName(string RecipeName)
        {
            RecipeListViewModel recipeListViewModel = new RecipeListViewModel();

            try
            {
                this.DbContext.OpenConnection();
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesByName(RecipeName);
                //recipeListViewModel.Tags = UnitOfWork.TagsRepository.ReadAll().ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }

            return recipeListViewModel;
        }
        [HttpGet]
        public RecipeListViewModel GetRecipesByTime(string time)
        {
            RecipeListViewModel recipeListViewModel = new RecipeListViewModel();

            try
            {
                this.DbContext.OpenConnection();

                // Retrieve recipes by tag using RecipeRepository
                int intTime = int.Parse(time);
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesByTime(intTime);

                // Retrieve all tags using TagsRepository
                //recipeListViewModel.Tags = UnitOfWork.TagsRepository.ReadAll().ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }

            return recipeListViewModel;
        }
        [HttpGet]
        public RecipeListViewModel GetRecipesByCalories(string calories)
        {
            RecipeListViewModel recipeListViewModel = new RecipeListViewModel();

            try
            {
                this.DbContext.OpenConnection();

                // Retrieve recipes by tag using RecipeRepository
                int intCalories = int.Parse(calories);
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesByCalories(intCalories);

                // Retrieve all tags using TagsRepository
                //recipeListViewModel.Tags = UnitOfWork.TagsRepository.ReadAll().ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }

            return recipeListViewModel;
        }
        [HttpGet]
        public RecipeListViewModel GetRecipesByTimeAndCalories(string calories, string time)
        {
            RecipeListViewModel recipeListViewModel = new RecipeListViewModel();

            try
            {
                this.DbContext.OpenConnection();

                int intTime = int.Parse(time);
                int intCalories = int.Parse(calories);
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesBytimeAndCalories(intTime, intCalories);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }

            return recipeListViewModel;
        }
        [HttpGet]
        public RecipeListViewModel GetRecipesByNameAndCalories(string calories, string RecipeName)
        {
            RecipeListViewModel recipeListViewModel = new RecipeListViewModel();

            try
            {
                this.DbContext.OpenConnection();

                int intCalories = int.Parse(calories);
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesByNameAndCalories(RecipeName, intCalories);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }

            return recipeListViewModel;
        }
        [HttpGet]
        public RecipeListViewModel GetRecipesByNameTimeAndCalories(string time, string calories, string RecipeName)
        {
            RecipeListViewModel recipeListViewModel = new RecipeListViewModel();
            try
            {
                this.DbContext.OpenConnection();
                int intTime = int.Parse(time);
                int intCalories = int.Parse(calories);
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesByTimeAndCaloriesAndName(intTime, intCalories, RecipeName);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }

            return recipeListViewModel;
        }
        [HttpGet]
        public RecipesModel GetRecipeById(string RecipeID)
        {
            try
            {
                this.DbContext.OpenConnection();
                return UnitOfWork.RecipeRepository.GetT(RecipeID);
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }
        }
        [HttpGet]
        public RecipeListViewModel GetRecipesByTimeAndName(string RecipeName, string time)
        {
            RecipeListViewModel recipeListViewModel = new RecipeListViewModel();

            try
            {
                this.DbContext.OpenConnection();

                // Retrieve recipes by tag using RecipeRepository
                int intTime = int.Parse(time);
                recipeListViewModel.Recipes = UnitOfWork.RecipeRepository.GetRecipesByTimeAndName(intTime, RecipeName);

                // Retrieve all tags using TagsRepository
                //recipeListViewModel.Tags = UnitOfWork.TagsRepository.ReadAll().ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                DbContext.CloseConnection();
            }

            return recipeListViewModel;
        }
        [HttpPost]
        public bool SignUp(UserModel user)
        {
            try
            {

                this.DbContext.OpenConnection();
                return UnitOfWork.UserRepository.Create(user);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return false;

            }
            finally
            {
                DbContext.CloseConnection();
            }

        }
        [HttpGet]
        public string LogIn(string UserName, string UserPassword)
        {
            try
            {
                string username = UserName;
                string password = UserPassword;
                this.DbContext.OpenConnection();
                return UnitOfWork.UserRepository.LogIn(username, password);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;

            }
            finally
            {
                DbContext.CloseConnection();
            }
        }
        [Route("api/Guest/AdminLogIn")]
        [HttpGet]
        public string AdminLogIn(string UserName, string UserPassword)
        {
            try
            {
                string username = UserName;
                string password = UserPassword;
                this.DbContext.OpenConnection();
                return UnitOfWork.UserRepository.IsAdmin(username, password);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;

            }
            finally
            {
                DbContext.CloseConnection();
            }
        }

        [Route("api/Guest/GetAdminByID")]
        [HttpGet]
        public Admin GetAdminById(string adminId)
        {
            try
            {
                string id = adminId;
                this.DbContext.OpenConnection();
                return UnitOfWork.UserRepository.AdminGetById(id);
            }
            finally
            {
                DbContext.CloseConnection();
            }
        }
        [HttpGet]
        [Route("api/Guest/GetUserById")]
        public UserModel GetUserById(string UserID)
        {
            try
            {
                string id = UserID;
                this.DbContext.OpenConnection();
                UserModel user = UnitOfWork.UserRepository.GetT(id);
                return user;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Trace.WriteLine(message);
                return null;
            }
            finally
            {
                this.DbContext.CloseConnection();
            }
        }
        //[HttpGet]
        //public RecipeViewModel GetRecipeViewModel()
        //{
        //    try
        //    {
        //        RecipeViewModel recipeViewModel = new RecipeViewModel();
        //        DbContext.OpenConnection();
        //        recipeViewModel.R= UnitOfWork.RecipeRepository.ReadAll().ToList();
        //        return recipeViewModel;
        //    }
        //    catch(Exception ex)
        //    {
        //        Trace.WriteLine($"The problem is {ex.Message}");
        //        return null;
        //    }
        //    finally
        //    {
        //        DbContext.CloseConnection();
        //    }
        //}

    }
}
