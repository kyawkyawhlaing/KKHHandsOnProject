using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KKHHandsOnProject.Domain.Features.Blogs;
public static class BlogServiceExtension
{
    public static void AddBlogServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<BlogService>();
    }
}