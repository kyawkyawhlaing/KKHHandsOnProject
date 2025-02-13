using Azure.Core;
using KKHHandsOnProject.BlogMvcApp.src.Models;
using KKHHandsOnProject.BlogMvcApp.src.Models.RequestModels;
using KKHHandsOnProject.BlogMvcApp.src.Models.ViewModels;
using KKHHandsOnProject.BlogMvcApp.src.Shared.Models;
using KKHHandsOnProject.Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace KKHHandsOnProject.BlogMvcApp.src.Features.Blogs
{
    public class BlogsService : IBlogsService
    {
        private static Result<BlogViewModel> model = new Result<BlogViewModel>();
        private readonly AppDbContext _db;

        public BlogsService(AppDbContext db)
        {
            _db = db;
        }

        public Result<BlogViewModel> CreateBlog(BlogRequestModel blog)
        {
            try
            {
                _db.Blogs.Add(new BlogDataModel
                {
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent,
                });
                _db.SaveChanges();
                model = Result<BlogViewModel>.Success(null, "Blog has created successfully");
                goto Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                model = Result<BlogViewModel>.SystemError("Internal Server Error");
                goto Result;
            }
        Result:
            return model;

        }

        public Result<BlogViewModel> DeleteBlog(int id)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id)!;
            _db.Entry(item).State = EntityState.Deleted;
            int result = _db.SaveChanges();
            if (result is 1)
            {
                model = Result<BlogViewModel>.Success(null, "Blog has deleted successfully");
                goto Result;
            }
            else
            {
                model = Result<BlogViewModel>.SystemError("Blog has Fail to delete");
                goto Result;
            }
        Result:
            return model;
        }

        public Result<BlogViewModel> GetBlog(int id)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                model = Result<BlogViewModel>.NotFound("Blog not found");
                goto Result;
            }
            else
            {
                model = Result<BlogViewModel>.Success(new BlogViewModel()
                {
                    BlogId = item.BlogId,
                    BlogTitle = item.BlogTitle,
                    BlogAuthor = item.BlogAuthor,
                    BlogContent = item.BlogContent
                });
                goto Result;
            }
        Result:
            return model;
        }

        public PaginationModel<BlogViewModel> GetBlogDataTable(HttpRequest request)
        {
            int pageNumber = int.Parse(request.Form["start"].FirstOrDefault() ?? "0");
            int pageSize = int.Parse(request.Form["length"].FirstOrDefault() ?? "10");
            string searchValue = request.Form["search[value]"].FirstOrDefault() ?? "";
            int orderColumn = int.Parse(request.Form["order[0][column]"].FirstOrDefault() ?? "0");
            string sortColumn = request.Form[$@"columns[{orderColumn}][data]"].FirstOrDefault()! ?? request.Form["columns[0][data]"].FirstOrDefault()!;
            string sortDir = request.Form["order[0][dir]"].FirstOrDefault() ?? "asc";
            var items = _db.Blogs.AsNoTracking().Select(x => new BlogViewModel
            {
                BlogId = x.BlogId,
                BlogTitle = x.BlogTitle,
                BlogAuthor = x.BlogAuthor,
                BlogContent = x.BlogContent
            })
                .Where(x => x.BlogAuthor.Contains(searchValue) || x.BlogTitle.Contains(searchValue))
                .Skip(pageNumber)
                .Take(pageSize);

            // Dynamic sorting based on column name
            var parameter = Expression.Parameter(typeof(BlogViewModel), "x");
            var property = Expression.Property(parameter, sortColumn);
            var lambda = Expression.Lambda<Func<BlogViewModel, object>>(Expression.Convert(property, typeof(object)), parameter);

            // Apply dynamic order by
            var orderedQuery = sortDir == "asc" ?
                items.Distinct().OrderBy(lambda).ToList()! :
                items.Distinct().OrderByDescending(lambda).ToList()!;
            return new PaginationModel<BlogViewModel>
            {
                RecordsTotal = _db.Blogs.AsNoTracking().Count(),
                RecordsFiltered = searchValue.Length>0 ? items.ToList().Count : _db.Blogs.AsNoTracking().Count(),
                Data = orderedQuery
            };
        }

        public Result<List<BlogViewModel>> GetBlogs()
        {
            Result<List<BlogViewModel>> model = new Result<List<BlogViewModel>>();
            var items = _db.Blogs.AsNoTracking().Select(x => new BlogViewModel
            {
                BlogId = x.BlogId,
                BlogTitle = x.BlogTitle,
                BlogAuthor = x.BlogAuthor,
                BlogContent = x.BlogContent
            }).ToList();
            if (items is not null)
            {
                model = Result<List<BlogViewModel>>.Success(items);
                goto Result;
            }
            else
            {
                model = Result<List<BlogViewModel>>.NotFound("Blogs not found");
                goto Result;
            }
        Result:
            return model;
        }

        public Result<BlogViewModel> UpdateBlog(int id, BlogRequestModel blog)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id)!;
            if (blog.BlogTitle is not null)
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (blog.BlogAuthor is not null)
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (blog.BlogContent is not null)
            {
                item.BlogContent = blog.BlogContent;
            }
            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            if (result is 1)
            {
                model = Result<BlogViewModel>.Success(null, "Blog has updated successfully");
                goto Result;
            }
            else
            {
                model = Result<BlogViewModel>.SystemError("Blog has Failed to Update");
                goto Result;
            }
        Result:
            return model;
        }
    }
}
