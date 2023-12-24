using carvedrock_admin.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace carvedrock_admin.Data;

public class AdminContext : IdentityDbContext<AdminUser>
{
    private readonly string _dbPath;

    public AdminContext(IConfiguration config)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _dbPath = Path.Join(path, config.GetConnectionString("UserDbFilename"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");
}
