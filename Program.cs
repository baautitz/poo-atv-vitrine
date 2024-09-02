using Atividade_06_Vitrine;
using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Atividade_06_Vitrine.Repositories.MySQL;

namespace Atividade_06_Vitrine;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Retrieve connection string from configuration
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Register repositories with connection string
        builder.Services.AddScoped<ICategoriaRepository>(provider => new CategoriaRepository(connectionString!));
        builder.Services.AddScoped<IProdutoRepository>(provider => new ProdutoRepository(connectionString!));

        // Add services to the container
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
