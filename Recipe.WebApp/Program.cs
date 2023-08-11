using Recipe.Library.Services;
using Recipe.WebApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IApiService, ApiService>();

// Register the RecipeDbContext
builder.Services.AddDbContext<RecipeDbContext>(
    options => options.UseSqlite("Data Source=/Users/yuenshan/Documents/Programming/WEB_project/Data/recipe.db;")
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapControllerRoute(
        name: "create",
        pattern: "Create",
        defaults: new { controller = "Create", action = "Create" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "recipeDetails",
    pattern: "Home/Details/{id}",
    defaults: new { controller = "Home", action = "Details" });

});



app.Run();

