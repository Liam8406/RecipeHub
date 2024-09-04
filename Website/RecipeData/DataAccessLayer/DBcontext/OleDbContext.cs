using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace RecipeData.DataAccessLayer.DBcontext
{
    public class OleDbContext:DbContext
    {
        static OleDbContext oleDbContext;
        static object obj = new object();
        private OleDbContext()
        {
            this.connection = new OleDbConnection();
            this.connection.ConnectionString = CommonParameters.ConnectionString;
            this.command = this.connection.CreateCommand();
        }
        public static OleDbContext GetInstance()
        {
            lock (obj)
            {
                if (oleDbContext == null)
                    oleDbContext = new OleDbContext();
                return oleDbContext;
            }
        }
        public override void AddParameters(string name, Object value)
        {
            OleDbParameter param = new OleDbParameter(name, value);
            base.command.Parameters.Add(param);
        }
    }

}