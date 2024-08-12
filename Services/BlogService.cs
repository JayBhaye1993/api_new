using BlogMgtAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace BlogMgtAPI.Services
{
    public class BlogService : IBlogService
    {
        private const string filePath = "Models//Data//BlogData.json";
        public async Task<BlogModel> GetBlogById(int Id)
        {
            BlogModel response = null;
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            List<BlogModel> lstblogs = JsonConvert.DeserializeObject<List<BlogModel>>(json);
            if (lstblogs != null && lstblogs.Count > 0)
                response = lstblogs.Where(x => x.Id == Id).FirstOrDefault();
            return response;
        }

        public async Task<IEnumerable<BlogModel>> GetBlog()
        {
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            List<BlogModel> blogs = JsonConvert.DeserializeObject<List<BlogModel>>(json);
            return blogs;
        }

        public BlogModel PostBlog(BlogModel blogModel)
        {
            try
            {
                var jsonData = System.IO.File.ReadAllText(filePath);

                var blogList = JsonConvert.DeserializeObject<List<BlogModel>>(jsonData)
                                      ?? new List<BlogModel>();

                if (blogList.Count > 0)
                {
                    blogModel.Id = blogList.Count + 1;
                }
                else
                {
                    blogModel.Id = 1;
                }

                blogModel.DateCreated = DateTime.Now.Date;
                blogList.Add(blogModel);

                jsonData = JsonConvert.SerializeObject(blogList);
                System.IO.File.WriteAllText(filePath, jsonData);
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                file.Close();

                return blogModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<BlogModel> DeleteBlog(int Id)
        {
            BlogModel blogModel = null;
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            reader.Close();
            List<BlogModel> lstblogs = JsonConvert.DeserializeObject<List<BlogModel>>(json);
            if (lstblogs != null && lstblogs.Count > 0)
                blogModel = lstblogs.FirstOrDefault(x => x.Id == Id);

            if (blogModel != null)
            {
                lstblogs.Remove(blogModel);
                var jsonData = JsonConvert.SerializeObject(lstblogs);
                System.IO.File.WriteAllText(filePath, jsonData);

            }

            return lstblogs;
        }

        public async Task<BlogModel> UpdateBlogById(BlogModel blogModel)
        {
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            reader.Close();
            List<BlogModel> lstblogs = JsonConvert.DeserializeObject<List<BlogModel>>(json);
            foreach (var blog in lstblogs.Where(w => w.Id == blogModel.Id))
            {
                blog.DateCreated = DateTime.Now;
                blog.Text = blogModel.Text;
                blog.UserName = blogModel.UserName;


            }

            var jsonData = JsonConvert.SerializeObject(lstblogs);
            System.IO.File.WriteAllText(filePath, jsonData);

            var response = lstblogs.Where(x => x.Id == blogModel.Id).FirstOrDefault();
            return await Task.FromResult<BlogModel>(response);
        }
    }
}
