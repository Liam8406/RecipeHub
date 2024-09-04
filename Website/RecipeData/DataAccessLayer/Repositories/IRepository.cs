using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeData.DataAccessLayer.Repositories
{
    public interface IRepository<T>
    {
        bool Create(T model);
        //change to bool update and delete
        void Update(T model);
        void Delete(string id);
        IEnumerable<T> ReadAll();
        //IEnumerable<T> ReadAll(string criteria);   
        T GetT(string id);
    }
}
