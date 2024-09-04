using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Diagnostics;
using System.Data.OleDb;

namespace RecipeData.DataAccessLayer.DBcontext
{
    public abstract class DbContext:IDbContext
    {
        protected IDbConnection connection;
        protected IDbCommand command;
        protected IDbTransaction transaction;

        public void OpenConnection()
        {
            if(connection != null && connection.State == ConnectionState.Closed)
                connection.Open();
            this.command.Connection = this.connection;
        }
        public void CloseConnection()
        {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
                if (command != null)
                    command.Dispose();
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                }          
        }
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                command.CommandText = sql;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log the exception message for debugging purposes
                Trace.WriteLine($"SQL Execution Error: {ex.Message}");
                return -1; // Indicate an error occurred
            }
        }
        //public IDataReader ExecuteReader(string sql)
        //{
        //    try
        //    {
        //        command.CommandText = sql;
        //        IDataReader reader = command.ExecuteReader();
        //        return reader;
        //    }
        //    catch(Exception ex)
        //    {
        //        Trace.WriteLine($"The problem is {ex.Message}");
        //        return null;
        //    }
        //}
        public object ExecuteScalar(string sql)
        {
            command.CommandText = sql;
            return command.ExecuteScalar();
        }
        public object GetLastCreatedId()
        {
            string sql = "Select @Identity";
            return this.ExecuteScalar(sql);
        }
        public abstract void AddParameters(string name, Object value);
        public int Create(string sql)
        {
            throw new NotImplementedException();
        }

        public int Delete(string sql)
        {
            throw new NotImplementedException();
        }
        public IDataReader Read(string sql)
        {
            try
            {
                command.CommandText = sql;
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"{ex.Message}");
                return null;
            }

        }

        public object ReadValue(string sql)
        {
            throw new NotImplementedException();
        }

        public int Update(string sql)
        {
            return this.ChangeDb(sql);
        }
        private int ChangeDb(string sql)
        {
            this.command.CommandText = sql;
            return this.command.ExecuteNonQuery();
        }
        
        public void SaveChanges()
        {
            if(transaction != null)
            {
                transaction.Commit();
            }
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }
}