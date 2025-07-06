using MiniETicaret.API.Hubs;
using MiniETicaret.API.Middlewares;
using MiniETicaret.API.Services;
using MiniETicaret.Application;
using MiniETicaret.Application.Interfaces.Hubs;
using MiniETicaret.Infrastructure;
using MiniETicaret.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddScoped<IProductHubService, ProductHubService>();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true) // T�m kaynaklara izin ver (sadece geli�tirme i�in)
                   .AllowCredentials();
        });
});

// Program.cs
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// YEN� EKLENEN SATIR:
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();
// Controller'lar� kullanabilmek i�in gerekli middleware'i ekliyoruz.
app.MapControllers();

// YEN� EKLENEN SATIR:
app.MapHub<ProductHub>("/product-hub");

// Program.cs'in sonuna, app.Run()'dan �nce eklenecek.
// Ba�lang��ta In-Memory DB'ye veri eklemek i�in.
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    if (!dbContext.Products.Any())
//    {
//        dbContext.Products.Add(new MiniETicaret.Domain.Entities.Product { Id = Guid.NewGuid(), Name = "Laptop", Price = 35000, Stock = 150 });
//        dbContext.Products.Add(new MiniETicaret.Domain.Entities.Product { Id = Guid.NewGuid(), Name = "Mouse", Price = 450, Stock = 500 });
//        dbContext.Products.Add(new MiniETicaret.Domain.Entities.Product { Id = Guid.NewGuid(), Name = "Klavye", Price = 950, Stock = 300 });
//        await dbContext.SaveChangesAsync();
//    }
//}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Yeni, temiz �a�r�m�z:
    await MiniETicaret.Infrastructure.Persistence.Seed.ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);
}


app.Run();
