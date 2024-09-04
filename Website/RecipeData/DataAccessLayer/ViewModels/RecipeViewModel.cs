using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeData.DataAccessLayer.Models;

namespace RecipeData.DataAccessLayer.ViewModels
{
    public class RecipeViewModel
    {
        public string RecipeName { get; set; }
        public int Time { get; set; }
        public List<TagModel> Tags { get; set; }
        public string Ingredients { get; set; }
        public string RecipeImage { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}