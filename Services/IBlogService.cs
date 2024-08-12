using BlogMgtAPI.Models;

namespace BlogMgtAPI.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogModel>> GetBlog();

        Task<BlogModel> GetBlogById(int Id);

        BlogModel PostBlog(BlogModel blog);

        IEnumerable<BlogModel> DeleteBlog(int Id);

        Task<BlogModel> UpdateBlogById(BlogModel blogModel);
    }
}
