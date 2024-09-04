using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.Models;
using RecipeData.DataAccessLayer.ModelFactoryFolder;
using System.Diagnostics;

namespace RecipeData.DataAccessLayer.Repositories
{
    public class UserRepository : Repository, IRepository<UserModel>
    {
        public UserRepository(DbContext dbContext, ModelFactory userModelFactory) : base(dbContext, userModelFactory)
        {
        }

        public bool Create(UserModel model)
        {
            string sql = @"INSERT INTO [User] ([UserName], [UserEmail], [UserPassword]) 
                   VALUES (@UserName, @UserEmail, @UserPassword)";
            base.dbContext.AddParameters("@UserName", model.UserName);
            base.dbContext.AddParameters("@UserEmail", model.UserEmail);
            base.dbContext.AddParameters("@UserPassword", model.UserPassword);
            //base.dbContext.ExecuteNonQuery(sql);
            model.Id = Convert.ToString(base.dbContext.GetLastCreatedId());
            return this.dbContext.ExecuteNonQuery(sql) > 0;
        }
        public void Update(UserModel model)
        {
            string sql = @"UPDATE [User]
                   SET UserName = @UserName, UserEmail = @UserEmail
                   WHERE UserID = @UserID";
            base.dbContext.AddParameters("@UserName", model.UserName);
            base.dbContext.AddParameters("@UserEmail", model.UserEmail);
            base.dbContext.AddParameters("@UserID", model.Id);
            base.dbContext.ExecuteNonQuery(sql);
        }

        public void Delete(string userId)
        {
            string deleteFavouritesSql = "DELETE FROM Favourites WHERE UserID=@UserID";
            base.dbContext.AddParameters("@UserID", userId);
            base.dbContext.ExecuteNonQuery(deleteFavouritesSql);

            string deleteReviewsSql = "DELETE FROM Reviews WHERE UserID=@UserID";
            base.dbContext.AddParameters("@UserID", userId);
            base.dbContext.ExecuteNonQuery(deleteReviewsSql);

            string sql = @"DELETE FROM [User] WHERE UserID = @UserID";
            base.dbContext.AddParameters("@UserID", userId);
            base.dbContext.ExecuteNonQuery(sql);
        }



        public UserModel GetT(string UserID)
        {
            UserModel user = null;
            string sql = @"SELECT * FROM [User] WHERE [UserID] = @UserID";
            base.dbContext.AddParameters("@UserID", UserID);

            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                if (dataReader.Read())
                {
                    user = recipeSiteModelFactory.UserCreator.CreateModel(dataReader);
                }
            }

            return user;

        }

        public IEnumerable<UserModel> ReadAll()
        {
            List<UserModel> users = new List<UserModel>();
            string sql = @"SELECT * FROM [User]";
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    users.Add(recipeSiteModelFactory.UserCreator.CreateModel(dataReader));
                }
            }
            return users;
        }
        public string LogIn(string username, string password)
        {
            string sql = @"SELECT [UserID] FROM [User] WHERE [UserName]=@UserName AND [UserPassword]=@UserPassword";
            base.dbContext.AddParameters("@UserName", username);
            base.dbContext.AddParameters("@UserPassword", password);
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                dataReader.Read();
                return dataReader["UserID"].ToString();
            }
        }
        public string IsAdmin(string username, string password)
        {
            string sql = @"SELECT * FROM [Admin] WHERE [UserName]=@UserName AND [UserPassword]=@UserPassword";
            base.dbContext.AddParameters("@UserName", username);
            base.dbContext.AddParameters("@UserPassword", password);
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                dataReader.Read();
                return dataReader["AdminID"].ToString();
            }
        }
        public Admin AdminGetById(string id)
        {
            string sql = "Select * from Admin where AdminID=@AdminID";
            this.dbContext.AddParameters("@AdminID", id);
            using (IDataReader datareader = this.dbContext.Read(sql))
            {
                datareader.Read();
                return recipeSiteModelFactory.AdminCreator.CreateModel(datareader);
            }
        }
    }
}