using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vail_Engineering.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Vail_EngineeringContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Vail_EngineeringContext") ?? throw new InvalidOperationException("Connection string 'Vail_EngineeringContext' not found.")));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Vail_EngineeringContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
