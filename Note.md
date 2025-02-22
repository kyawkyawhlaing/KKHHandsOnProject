# KKHDotNetCore
### DB First Approach
- dotnet ef dbcontext scaffold "Server=.;Database=KKHDotNetCore;User Id=sa;Password=sa;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -t Table_Name1,Table_Name2 -f
- dotnet ef dbcontext scaffold "Server=.;Database=KKHDotNetCore;User Id=sa;Password=sa;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Data -c AppDbContext  -f