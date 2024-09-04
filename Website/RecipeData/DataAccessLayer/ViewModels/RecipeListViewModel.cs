using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeData.DataAccessLayer.Models;

namespace RecipeData.DataAccessLayer.ViewModels
{
    public class RecipeListViewModel
    {
        public List<RecipesModel> Recipes { get; set; }
        public List<TagModel> Tags { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public List<UserModel> Users { get; set; }
    }
}