using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeData.DataAccessLayer.ModelFactoryFolder
{
    public interface IModelCreator<T, TSource>
    {
        T CreateModel(TSource src);
    }
}