using API.Services.Abstract;
using API.Services.Concrete;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Controller
builder.Services.AddControllers();
#endregion
#region DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
#region DependencyInjection
builder.Services.AddScoped<ICoffeeShopService, CoffeeShopService>();
#endregion


var app = builder.Build();

app.MapControllers();

app.Run();
