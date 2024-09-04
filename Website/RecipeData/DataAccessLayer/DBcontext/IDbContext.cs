using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace RecipeData.DataAccessLayer.DBcontext
{
    internal interface IDbContext
    {
        //CRUD - Create Read Update Delete
        //read <- DB
        //CUD -> DB
        IDataReader Read(string sql);
        object ReadValue(string sql);
        int Create(string sql);
        int Update(string sql);
        int Delete(string sql);
        void OpenConnection();
        void CloseConnection();
        void BeginTransaction();
    }
}