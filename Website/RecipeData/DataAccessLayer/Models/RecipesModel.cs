using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeData.DataAccessLayer.Models
{
    public class RecipesModel:BaseModel
    {
        public string RecipeName { get; set; }
        public int Time{ get; set; }
        public string TagID { get; set; }
        public string Ingredients { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string RecipeImage { get; set; }

    }
}