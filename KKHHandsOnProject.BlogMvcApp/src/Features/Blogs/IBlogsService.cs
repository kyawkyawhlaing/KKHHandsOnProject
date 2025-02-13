using KKHHandsOnProject.BlogMvcApp.src.Models;
using KKHHandsOnProject.BlogMvcApp.src.Models.RequestModels;
using KKHHandsOnProject.BlogMvcApp.src.Models.ViewModels;
using KKHHandsOnProject.BlogMvcApp.src.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace KKHHandsOnProject.BlogMvcApp.src.Features.Blogs
{
    public interface IBlogsService
    {
        Result<BlogViewModel> GetBlog(int id);
        Result<List<BlogViewModel>> GetBlogs();
        Result<BlogViewModel> CreateBlog(BlogRequestModel blog);
        Result<BlogViewModel> UpdateBlog(int id, BlogRequestModel blog);
        Result<BlogViewModel> DeleteBlog(int id);
        PaginationModel<BlogViewModel> GetBlogDataTable(HttpRequest request);
    }
}