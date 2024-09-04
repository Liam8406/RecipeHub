using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiClient;
using RecipeData.DataAccessLayer.ViewModels;
using RecipeData.DataAccessLayer.Models;

namespace RecipeSite.Controllers
{
    public class RecipesController : Controller
    {
        // GET: Recipes
        [HttpGet]
        public ActionResult ViewHomePage(int page = 1)
        {
            int recipesPerPage = 8;

            // Fetch the recipe list
            WebClient<RecipeListViewModel> client = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipeListViewModel");
            RecipeListViewModel recipeListViewModel = client.Get();

            // Pagination logic
            int totalRecipes = recipeListViewModel.Recipes.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecipes / recipesPerPage);
            page = Math.Max(1, Math.Min(page, totalPages));
            int startIndex = (page - 1) * recipesPerPage;
            recipeListViewModel.Recipes = recipeListViewModel.Recipes.Skip(startIndex).Take(recipesPerPage).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(recipeListViewModel);
        }





        [HttpGet]
        public ActionResult ViewGetRecipe(string RecipeName, string time, string calories, string[] tags, int page = 1)
        {
            int recipesPerPage = 8;
            RecipeListViewModel model;

            if (string.IsNullOrEmpty(RecipeName) && string.IsNullOrEmpty(time) && string.IsNullOrEmpty(calories))
            {
                WebClient<RecipeListViewModel> client = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipeListViewModel");
                model = client.Get();
            }

            if (string.IsNullOrEmpty(RecipeName) && string.IsNullOrEmpty(time) && string.IsNullOrEmpty(calories))
            {
                // Fetch all recipes
                WebClient<RecipeListViewModel> client = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipeListViewModel");
                model = client.Get();
            }
            else if (string.IsNullOrEmpty(time) && string.IsNullOrEmpty(calories))
            {
                // Fetch recipes by name
                WebClient<RecipeListViewModel> client = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipesByName");
                client.AddNewKeyValues("RecipeName", RecipeName);
                model = client.Get();
            }
            else if (string.IsNullOrEmpty(RecipeName) && string.IsNullOrEmpty(calories))
            {
                // Fetch recipes by time
                WebClient<RecipeListViewModel> clientTime = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipesByTime");
                clientTime.AddNewKeyValues("time", time);
                model = clientTime.Get();
            }
            else if (string.IsNullOrEmpty(RecipeName) && string.IsNullOrEmpty(time))
            {
                // Fetch recipes by calories
                WebClient<RecipeListViewModel> clientCalories = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipesByCalories");
                clientCalories.AddNewKeyValues("calories", calories);
                model = clientCalories.Get();
            }
            else if (string.IsNullOrEmpty(time))
            {
                // Fetch recipes by name and calories
                WebClient<RecipeListViewModel> clientNameCalories = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipesByNameAndCalories");
                clientNameCalories.AddNewKeyValues("RecipeName", RecipeName);
                clientNameCalories.AddNewKeyValues("calories", calories);
                model = clientNameCalories.Get();
            }
            else if (string.IsNullOrEmpty(RecipeName))
            {
                // Fetch recipes by time and calories
                WebClient<RecipeListViewModel> clientNameCalories = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipesByTimeAndCalories");
                clientNameCalories.AddNewKeyValues("time", time);
                clientNameCalories.AddNewKeyValues("calories", calories);
                model = clientNameCalories.Get();
            }
            else if (string.IsNullOrEmpty(calories))
            {
                // Fetch recipes by name and time
                WebClient<RecipeListViewModel> clientTimeName = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipesByTimeAndName");
                clientTimeName.AddNewKeyValues("time", time);
                clientTimeName.AddNewKeyValues("RecipeName", RecipeName);
                model = clientTimeName.Get();
            }
            else
            {
                // Fetch recipes by time, name, and calories
                WebClient<RecipeListViewModel> clientTimeNameCalories = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipesByTimeNameAndCalories");
                clientTimeNameCalories.AddNewKeyValues("time", time);
                clientTimeNameCalories.AddNewKeyValues("RecipeName", RecipeName);
                clientTimeNameCalories.AddNewKeyValues("calories", calories);
                model = clientTimeNameCalories.Get();
            }

            int totalRecipes = model.Recipes.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecipes / recipesPerPage);
            page = Math.Max(1, Math.Min(page, totalPages));
            int startIndex = (page - 1) * recipesPerPage;
            model.Recipes = model.Recipes.Skip(startIndex).Take(recipesPerPage).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.RecipeName = RecipeName;
            ViewBag.Time = time;
            ViewBag.Calories = calories;

            return View("ViewHomePage", model);
        }

        [HttpGet]
        public ActionResult ViewRecipe(string RecipeId)
        {
            // Fetch the recipe
            WebClient<RecipesModel> client = new WebClient<RecipesModel>("localhost:53513", "Guest", "GetRecipeById");
            client.AddNewKeyValues("RecipeID", RecipeId);
            RecipesModel recipe = client.Get();
            Session["recipe"] = recipe;

            // Fetch the reviews for the recipe
            WebClient<List<ReviewModel>> reviewClient = new WebClient<List<ReviewModel>>("localhost:53513", "User", "GetReviewsByRecipeId");
            reviewClient.AddNewKeyValues("RecipeID", RecipeId);
            List<ReviewModel> reviews = reviewClient.Get();

            // Fetch usernames for each review and set Session["UserName"]
            foreach (var review in reviews)
            {
                WebClient<UserModel> userClient = new WebClient<UserModel>("localhost:53513", "Guest", "GetUserById");
                userClient.AddNewKeyValues("UserID", review.UserID);
                UserModel user = userClient.Get();
                Session[$"UserName_{review.UserID}"] = user.UserName;
                userClient.ClearKeyValues();
            }

            // Check if the recipe is already favourited by the user
            UserModel currentUser = (UserModel)Session["user"];
            bool isFavourite = false;
            if (currentUser != null)
            {
                WebClient<bool> favouriteClient = new WebClient<bool>("localhost:53513", "User", "IsRecipeFavourited");
                favouriteClient.AddNewKeyValues("UserID", currentUser.Id);
                favouriteClient.AddNewKeyValues("RecipeID", RecipeId);
                isFavourite = favouriteClient.Get();
            }

            ViewBag.IsFavourite = isFavourite;
            ViewBag.Reviews = reviews;
            return View();
        }

        [HttpGet]
        public ActionResult ViewUploadRecipe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadRecipe(RecipesModel recipe)
        {
            if (Session["user"] == null)
            {
                ViewBag.Errors = "You must be logged in to upload a recipe.";
                return View("UploadRecipe", recipe);
            }

            WebClient<RecipesModel> client = new WebClient<RecipesModel>("localhost:53513", "User", "AddNewRecipe");
            if (client.Post(recipe))
            {
                ViewBag.Message = "Recipe uploaded successfully.";
                return RedirectToAction("ViewHomePage");
            }

            ViewBag.Errors = "There was an error uploading your recipe. Please try again.";
            return View("ViewUploadRecipe", recipe);
        }

        [HttpGet]
        public ActionResult ViewFavouritePage(int page = 1)
        {
            // Recipes per page
            int recipesPerPage = 8;

            // Check if user session exists
            if (Session["user"] == null)
            {
                return RedirectToAction("ViewLogIn", "User");
            }

            UserModel user = (UserModel)Session["user"];
            string userID = user.Id;

            // Fetch and initialize tags if not already done
            if (Session["Tags"] == null)
            {
                WebClient<List<TagModel>> tagClient = new WebClient<List<TagModel>>("localhost:53513", "Guest", "GetAllTags");
                Session["Tags"] = tagClient.Get();
            }

            // Fetch favorite recipes
            WebClient<List<RecipesModel>> client = new WebClient<List<RecipesModel>>("localhost:53513", "User", "GetFavouriteRecipes");
            client.AddNewKeyValues("UserID", userID);
            List<RecipesModel> favouriteRecipes = client.Get();

            // Ensure favouriteRecipes is not null
            if (favouriteRecipes == null)
            {
                favouriteRecipes = new List<RecipesModel>();
            }

            int totalRecipes = favouriteRecipes.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecipes / recipesPerPage);
            page = Math.Max(1, Math.Min(page, totalPages));
            int startIndex = (page - 1) * recipesPerPage;
            favouriteRecipes = favouriteRecipes.Skip(startIndex).Take(recipesPerPage).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            RecipeListViewModel model = new RecipeListViewModel
            {
                Recipes = favouriteRecipes
            };

            return View(model);
        }



        [HttpPost]
        public ActionResult FavouriteRecipe(FavouritesModel favourites)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("ViewLogIn", "User");
            }

            WebClient<FavouritesModel> client = new WebClient<FavouritesModel>("localhost:53513", "User", "AddToFavorites");
            
            if (client.Post(favourites))
            {
                TempData["Message"] = "Recipe saved successfully.";
            }
            else
            {
                TempData["Error"] = "There was an error saving the recipe. Please try again.";
            }

            return RedirectToAction("ViewRecipe", new { RecipeId = favourites.RecipeID });
        }

        


        //[HttpPost]
        //public ActionResult UploadRecipe(string RecipeName, string Time, string Ingredients, string Description, string Instructions, string RecipeImage)
        //{
        //    WebClient<RecipesModel> client = new WebClient<RecipesModel>("localhost:53513", "Guest", "GetRecipeById");
        //    client.AddNewKeyValues("RecipeID", RecipeId);
        //    RecipesModel recipe = client.Get();
        //    Session["recipe"] = recipe;
        //    return View();
        //}
    }
}
