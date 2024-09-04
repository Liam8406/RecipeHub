using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiClient;
using RecipeData.DataAccessLayer.Models;

namespace RecipeSite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult ViewSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserModel user)
        {
            WebClient<UserModel> client = new WebClient<UserModel>("localhost:53513", "Guest", "SignUp");
            if (client.Post(user))
            {
                Session["user"] = user;
                return RedirectToAction("ViewHomePage", "Recipes");
            }
            ViewBag.Errors = true;
            return View("ViewSignUp", user);
        }

        [HttpGet]
        public ActionResult ViewLogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string UserPassword)
        {
            if(UserName == "" || UserPassword == "")
            {
                ViewBag.Error = true;
                return View("ViewLogIn");
            }

            WebClient<string> clientAdmin = new WebClient<string>("localhost:53513", "Guest", "AdminLogIn");
            clientAdmin.AddNewKeyValues("UserName", UserName);
            clientAdmin.AddNewKeyValues("UserPassword", UserPassword);
            string AdminID = clientAdmin.Get();

            WebClient<string> client = new WebClient<string>("localhost:53513", "Guest", "LogIn");
            client.AddNewKeyValues("UserName", UserName);
            client.AddNewKeyValues("UserPassword", UserPassword);
            string UserID = client.Get();

            WebClient<UserModel> userFromIdClient = new WebClient<UserModel>("localhost:53513", "Guest", "GetUserById");
            userFromIdClient.AddNewKeyValues("UserID", UserID);
            UserModel user = userFromIdClient.Get();
            if(AdminID != null)
            {
                WebClient<Admin>aClient = new WebClient<Admin>("localhost:53513", "Guest", "GetAdminById");
                aClient.AddNewKeyValues("adminId", AdminID);
                Admin admin = aClient.Get();
                aClient.ClearKeyValues();
                Session["Admin"] = admin;
                Session["user"] = user;
                Session["UserID"] = UserID;
                return RedirectToAction("ViewHomePage", "Recipes");

            }
            else
            {
                Session["Admin"] = null;
                Session["UserID"] = UserID;
            }

            if (UserID == null)
            {
                ViewBag.Error = true;
                return View("ViewLogIn");
            }

            Session["UserID"] = UserID;
            userFromIdClient.ClearKeyValues();
            Session["user"] = user;

            client.ClearKeyValues();
            clientAdmin.ClearKeyValues();
            System.Diagnostics.Debug.WriteLine("User stored in session: " + user.Id);


            return RedirectToAction("ViewHomePage", "Recipes");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("ViewHomePage", "Recipes");
        }
    }
}