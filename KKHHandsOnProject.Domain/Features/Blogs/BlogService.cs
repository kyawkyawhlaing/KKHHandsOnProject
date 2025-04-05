using System.Linq.Expressions;
using KKHHandsOnProject.Database.AppData;
using KKHHandsOnProject.Shared.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KKHHandsOnProject.Domain.Features.Blogs;

public class BlogService
{
    private static Result<BlogViewModel> _model = new Result<BlogViewModel>();
    private readonly AppDbContext _dbContext;

    public BlogService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Result<BlogViewModel> CreateBlog(BlogRequestModel blog)
    {
        try
        {
            _dbContext.Blogs!.Add(new BlogDataModel
            {
                BlogTitle   = blog.BlogTitle!,
                BlogAuthor  = blog.BlogAuthor!,
                BlogContent = blog.BlogContent!,
                BlogImagePath   = FilePathConstants.ImagePath + blog.BlogImage!.FileName,
            });
            //_dbContext.SaveChanges();
            _model = Result<BlogViewModel>.Success(null, "Blog has created successfully");
            goto Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _model = Result<BlogViewModel>.SystemError("Internal Server Error");
            goto Result;
        }

        Result:
        return _model;
    }

    public Result<BlogViewModel> DeleteBlog(int id)
    {
        var item = _dbContext.Blogs!.AsNoTracking().FirstOrDefault(x => x.BlogId == id)!;
        _dbContext.Entry(item).State = EntityState.Deleted;
        int result = _dbContext.SaveChanges();
        if (result is 1)
        {
            _model = Result<BlogViewModel>.Success(null, "Blog has deleted successfully");
            goto Result;
        }
        else
        {
            _model = Result<BlogViewModel>.SystemError("Blog has Fail to delete");
            goto Result;
        }

        Result:
        return _model;
    }

    public Result<BlogViewModel> GetBlog(int id)
    {
        var item = _dbContext.Blogs!.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            _model = Result<BlogViewModel>.NotFound("Blog not found");
            goto Result;
        }
        else
        {
            _model = Result<BlogViewModel>.Success(new BlogViewModel()
            {
                BlogId = item.BlogId,
                BlogTitle = item.BlogTitle,
                BlogAuthor = item.BlogAuthor,
                BlogContent = item.BlogContent
            });
            goto Result;
        }

        Result:
        return _model;
    }

    public JQueryPaginationResponseModel<BlogViewModel> GetBlogDataTable(HttpRequest request)
    {
        int draw = int.Parse(request.Form["draw"].FirstOrDefault() ?? "0");
        int pageNumber = int.Parse(request.Form["start"].FirstOrDefault() ?? "0");
        int pageSize = int.Parse(request.Form["length"].FirstOrDefault() ?? "10");
        string searchValue = request.Form["search[value]"].FirstOrDefault() ?? "";
        int orderColumn = int.Parse(request.Form["order[0][column]"].FirstOrDefault() ?? "0");
        string sortColumn = request.Form[$@"columns[{orderColumn}][data]"].FirstOrDefault()! ??
                            request.Form["columns[0][data]"].FirstOrDefault()!;
        string sortDir = request.Form["order[0][dir]"].FirstOrDefault() ?? "asc";
        var items = _dbContext.Blogs!.AsNoTracking().Select(x => new BlogViewModel
            {
                BlogId = x.BlogId,
                BlogTitle = x.BlogTitle,
                BlogAuthor = x.BlogAuthor,
                BlogContent = x.BlogContent
            })
            .Where(x => x.BlogAuthor!.Contains(searchValue) || x.BlogTitle!.Contains(searchValue))
            .Skip(pageNumber)
            .Take(pageSize);

        // Dynamic sorting based on column name
        var parameter = Expression.Parameter(typeof(BlogViewModel), "x");
        var property = Expression.Property(parameter, sortColumn);
        var lambda =
            Expression.Lambda<Func<BlogViewModel, object>>(Expression.Convert(property, typeof(object)), parameter);

        // Apply dynamic order by
        var orderedQuery = sortDir == "asc"
            ? items.Distinct().OrderBy(lambda).ToList()!
            : items.Distinct().OrderByDescending(lambda).ToList()!;
        return new JQueryPaginationResponseModel<BlogViewModel>
        {
            Draw = draw,
            RecordsTotal = _dbContext.Blogs!.AsNoTracking().Count(),
            RecordsFiltered = searchValue.Length > 0 ? items.ToList().Count : _dbContext.Blogs!.AsNoTracking().Count(),
            Data = orderedQuery
        };
    }

    public Result<List<BlogViewModel>> GetBlogs()
    {
        var items = _dbContext.Blogs!.AsNoTracking().Select(x => new BlogViewModel
        {
            BlogId = x.BlogId,
            BlogTitle = x.BlogTitle,
            BlogAuthor = x.BlogAuthor,
            BlogContent = x.BlogContent
        }).ToList();
        Result<List<BlogViewModel>> model = Result<List<BlogViewModel>>.Success(items);
        return model;
    }

    public Result<BlogViewModel> UpdateBlog(int id, BlogRequestModel blog)
    {
        var item = _dbContext.Blogs!.AsNoTracking().FirstOrDefault(x => x.BlogId == id)!;
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

        _dbContext.Entry(item).State = EntityState.Modified;
        int result = _dbContext.SaveChanges();
        if (result is 1)
        {
            _model = Result<BlogViewModel>.Success(null, "Blog has updated successfully");
            goto Result;
        }
        else
        {
            _model = Result<BlogViewModel>.SystemError("Blog has Failed to Update");
            goto Result;
        }

        Result:
        return _model;
    }

}