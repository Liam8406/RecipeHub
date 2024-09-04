using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RecipeData.DataAccessLayer.DBcontext;
using RecipeData.DataAccessLayer.Models;
using RecipeData.DataAccessLayer.ModelFactoryFolder;


namespace RecipeData.DataAccessLayer.Repositories
{
    public class TagRepository : Repository, IRepository<TagModel>
    {
        public TagRepository(DbContext dbContext, ModelFactory tagModelFactory) : base(dbContext, tagModelFactory)
        {
        }

        public bool Create(TagModel model)
        {
            string sql = $@"INSERT INTO Tags(TagName) VALUES (@TagName)";
            base.dbContext.AddParameters("@TagName", model.TagName);
            base.dbContext.ExecuteNonQuery(sql);
            model.Id = Convert.ToString(base.dbContext.GetLastCreatedId());
            return this.dbContext.ExecuteNonQuery(sql) > 0;
        }

        public void Update(TagModel model)
        {
            string sql = $@"UPDATE Tags SET TagName = @TagName WHERE TagID = @TagID";
            base.dbContext.AddParameters("@TagID", model.Id);
            base.dbContext.AddParameters("@TagName", model.TagName);
            base.dbContext.ExecuteNonQuery(sql);
        }

        public void Delete(string id)
        {
            string sql = @"DELETE FROM Tags WHERE TagID = @TagID";
            base.dbContext.AddParameters("@TagID", id);
            base.dbContext.ExecuteNonQuery(sql);
        }

        public TagModel GetT(string id)
        {
            string sql = @"SELECT * FROM Tags WHERE TagID = @TagID";
            IDataReader dataReader = base.dbContext.Read(sql);
            TagModel tag = recipeSiteModelFactory.TagCreator.CreateModel(dataReader);
            return tag;
        }
        public string GetTagNameByTagID(string TagID)
        {
            string sql = "SELECT TagName FROM Tags WHERE TagID = @TagID";
            base.dbContext.AddParameters("@TagID", TagID);
            IDataReader dataReader = base.dbContext.Read(sql);

            if (dataReader.Read())
            {
                return dataReader["TagName"].ToString();
            }
            else
            {
                return null;
            }
        }

        //public List<string> GetTagNamesByTagID(string tagIDs)
        //{
        //    List<string> tagNames = new List<string>();

        //    if (string.IsNullOrEmpty(tagIDs))
        //    {
        //        return tagNames;
        //    }

        //    // Split the tagIDs by comma
        //    string[] tagIDArray = tagIDs.Split(',');

        //    // Build the SQL query
        //    string sql = "SELECT TagName FROM Tags WHERE TagID IN (" + string.Join(",", tagIDArray.Select(id => "@" + id)) + ")";

        //    // Add parameters for each tag ID
        //    foreach (var tagID in tagIDArray)
        //    {
        //        base.dbContext.AddParameters("@" + tagID, tagID);
        //    }

        //    // Execute the query
        //    using (IDataReader dataReader = base.dbContext.Read(sql))
        //    {
        //        while (dataReader.Read())
        //        {
        //            tagNames.Add(dataReader["TagName"].ToString());
        //        }
        //    }

        //    return tagNames;
        //}

        public IEnumerable<TagModel> ReadAll()
        {
            List<TagModel> tags = new List<TagModel>();
            string sql = @"SELECT * FROM Tags";
            using (IDataReader dataReader = base.dbContext.Read(sql))
            {
                while (dataReader.Read())
                {
                    tags.Add(recipeSiteModelFactory.TagCreator.CreateModel(dataReader));
                }
            }
            return tags;
        }
    }
}