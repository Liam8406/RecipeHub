using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeData.DataAccessLayer.Models
{
    public class FavouritesModel:BaseModel
    {
        public string RecipeID{ get; set; }
        public string UserID { get; set; }
    }
}