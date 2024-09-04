using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RecipeData.DataAccessLayer.ModelFactoryFolder
{
   
        public interface IOleDbModelCreator<T> : IModelCreator<T, IDataReader>
        {
        }
}