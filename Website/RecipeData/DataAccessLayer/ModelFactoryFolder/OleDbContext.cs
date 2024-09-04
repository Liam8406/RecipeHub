using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RecipeData.DataAccessLayer
{
    public class OleDbContext : DbContext
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
        public override void AddParameters(string name, string value)
        {
            OleDbParameter param = new OleDbParameter(name, value);
            base.command.Parameters.Add(param);
        }
    }
}