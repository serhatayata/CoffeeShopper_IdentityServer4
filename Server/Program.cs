 using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server;
using Server.Data;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

var defaultConnString = builder.Configuration.GetConnectionString("DefaultConnection");
var assembly = typeof(Program).Assembly.GetName().Name;

#region SeedData
if (seed)
{
    SeedData.EnsureSeedData(defaultConnString);
}
#endregion
#region IdentityServer
builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseSqlServer(defaultConnString, b => b.MigrationsAssembly(assembly))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
                }).AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                    b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
                })
                .AddDeveloperSigningCredential();
#endregion

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(
    endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    }
);

app.Run();
