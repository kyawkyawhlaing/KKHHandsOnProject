# KKHDotNetCore
### DB First Approach
- dotnet ef dbcontext scaffold "Server=.;Database=KKHDotNetCore;User Id=sa;Password=sa;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -t Table_Name1,Table_Name2 -f
- dotnet ef dbcontext scaffold "Server=.;Database=KKHDotNetCore;User Id=sa;Password=sa;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Data -c AppDbContext  -f

### Serilog Connection String
- "ConnectionString":  "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=CRUDLogs; Integrated Security=True; Connect Timeout=30; Encrypt=False;TrustServerCertificate=False;Application Intent=ReadWrite;MultiSubnetFailover=False;"