using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeData.DataAccessLayer.Models
{
    public class ReviewModel : BaseModel
    {
        public string RecipeID { get; set; }
        public string UserID{ get; set; }
        public DateTime DatePosted { get; set; }
        public string ReviewDescription { get; set; }
        public int Rating { get; set; }
    }
}