using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeData.DataAccessLayer.Models
{
    public class Admin:BaseModel
    {
        public string UserName { get; set; }
        public string UserPassword{ get; set; }
    }
}