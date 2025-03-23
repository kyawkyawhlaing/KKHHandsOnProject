using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KKHHandsOnProject.Database.DapperData
{
    public static class DapperServiceExtensions
    {
        public static void AddDapperServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(x => new DapperContext(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddScoped<IDapperRepository, DapperRepository>();
            builder.Services.AddScoped<DapperService>();
        }
    }
}
