using DemoASPMVC.Services;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Enregistrement des services au niveau global de l'application
//Singleton => Garder une seule et unique instance pour la durée de vie de l'application
// Service ré-instancier à chaque fois qu'on redémarre l'application

builder.Services.AddSingleton<GameService>();

builder.Services.AddTransient<SqlConnection>(pc => new SqlConnection(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<IGameService, GameDBService>();
builder.Services.AddScoped<IUserDBService, UserDBService>();


//builder.Services.AddScoped<IGameService, GameService>();

//Scoped => Je garde l'instance pour tout la durée d'un appel http
// Dés qu'une nouvelle requête HTTP est produite => Service Ré-instancier
//builder.Services.AddScoped<GameService>();

//Tansient => a chaque appel du service => nouvelle instance
//builder.Services.AddTransient<GameService>();



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
