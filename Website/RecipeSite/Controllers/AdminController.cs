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
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult ViewAdminPage()
        { 

            // Fetch the recipe list
            WebClient<RecipeListViewModel> client = new WebClient<RecipeListViewModel>("localhost:53513", "Guest", "GetRecipeListViewModel");
            RecipeListViewModel recipeListViewModel = client.Get();

            // Ensure recipeListViewModel is not null
            if (recipeListViewModel == null)
            {
                recipeListViewModel = new RecipeListViewModel
                {
                    Recipes = new List<RecipesModel>()
                };
            }

            return View(recipeListViewModel);
        }


        [HttpPost]
        public ActionResult DeleteRecipe(RecipesModel recipe)
        {
            WebClient<RecipesModel> client = new WebClient<RecipesModel>("localhost:53513", "Admin", "DeleteRecipe");
            if (client.Post(recipe))
            {
                TempData["Message"] = "Recipe deleted successfully.";
            }
            else
            {
                TempData["Error"] = "There was an internal server error.";
            }
            return RedirectToAction("ViewAdminPage");
        }

        [HttpPost]
        public ActionResult UpdateRecipe(RecipesModel recipe)
        {
            WebClient<RecipesModel> client = new WebClient<RecipesModel>("localhost:53513", "Admin", "UpdateRecipe");
            if (client.Post(recipe))
            {
                TempData["Message"] = "Recipe updated successfully.";
            }
            else
            {
                TempData["Error"] = "There was an internal server error.";
            }
            return RedirectToAction("ViewAdminPage");
        }
        [HttpPost]
        public ActionResult DeleteReview(ReviewModel review)
        {
            WebClient<ReviewModel> client = new WebClient<ReviewModel>("localhost:53513", "Admin", "DeleteReview");
            if (client.Post(review))
            {
                TempData["Message"] = "Review deleted successfully.";
            }
            else
            {
                TempData["Error"] = "There was an internal server error.";
            }
            return RedirectToAction("ViewAdminPage");
        }

        [HttpPost]
        public ActionResult UpdateReview(ReviewModel review)
        {
            WebClient<ReviewModel> client = new WebClient<ReviewModel>("localhost:53513", "Admin", "UpdateReview");
            if (client.Post(review))
            {
                TempData["Message"] = "Review updated successfully.";
            }
            else
            {
                TempData["Error"] = "There was an internal server error.";
            }
            return RedirectToAction("ViewAdminPage");
        }

        [HttpPost]
        public ActionResult UpdateUser(UserModel user)
        {
            WebClient<UserModel> client = new WebClient<UserModel>("localhost:53513", "Admin", "UpdateUser");
            if (client.Post(user))
            {
                TempData["Message"] = "User updated successfully.";
            }
            else
            {
                TempData["Error"] = "There was an internal server error.";
            }
            return RedirectToAction("ViewAdminPage");
        }

        [HttpPost]
        public ActionResult DeleteUser(UserModel user)
        {
            WebClient<UserModel> client = new WebClient<UserModel>("localhost:53513", "Admin", "DeleteUser");
            if (client.Post(user))
            {
                TempData["Message"] = "User deleted successfully.";
            }
            else
            {
                TempData["Error"] = "There was an internal server error.";
            }
            return RedirectToAction("ViewAdminPage");
        }


    }
}