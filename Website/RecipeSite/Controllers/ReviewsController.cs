using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiClient;
using RecipeData.DataAccessLayer.Models;

namespace RecipeSite.Controllers
{
    public class ReviewsController : Controller
    {
        // POST: Post a review
        [HttpPost]
        public ActionResult PostReview(ReviewModel review)
        {
            if (Session["user"] == null)
            {
                System.Diagnostics.Debug.WriteLine("User session is null");
                return RedirectToAction("ViewLogIn", "User");
            }

            string userId = (string)Session["UserID"];
            review.UserID = userId;
            review.DatePosted = DateTime.Now;

            RecipesModel recipe = (RecipesModel)Session["recipe"];
            review.RecipeID = recipe.Id;

            WebClient<ReviewModel> client = new WebClient<ReviewModel>("localhost:53513", "User", "AddReview");
            if (client.Post(review))
            {
                TempData["Message"] = "Review posted successfully.";
            }
            else
            {
                TempData["Error"] = "You have already posted a review for this recipe.";
            }

            string redirectUrl = Url.Action("ViewRecipe", "Recipes", new { RecipeId = recipe.Id });
            return Redirect(redirectUrl);
        }
        [HttpPost]
        public ActionResult DeleteReview(string UserID, string RecipeID)
        {
            ReviewModel review = new ReviewModel();
            review.UserID = UserID;
            review.RecipeID = RecipeID;
            WebClient<ReviewModel> deleteClient = new WebClient<ReviewModel>("localhost:53513", "User", "RemoveComment");
            deleteClient.AddNewKeyValues("UserID", UserID);
            deleteClient.AddNewKeyValues("RecipeID", RecipeID);
            
            if (deleteClient.Post(review))
            {
                TempData["Message"] = "Review deleted successfully.";
            }
            else
            {
                TempData["Error"] = "There was an internal server error.";
            }

            string redirectUrl = Url.Action("ViewRecipe", "Recipes", new { RecipeId = RecipeID });
            return Redirect(redirectUrl);
        }







    }
}