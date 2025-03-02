using Microsoft.AspNetCore.Http;

namespace KKHHandsOnProject.Domain.Features.Blogs;

public class BlogRequestModel
{
    public int? BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
    public IFormFile? BlogImage { get; set; }
}