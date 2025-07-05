using MiniETicaret.API.Middlewares;
using MiniETicaret.Application;
using MiniETicaret.Infrastructure;
using MiniETicaret.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

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

app.UseHttpsRedirection();

app.UseAuthorization();
// Controller'larý kullanabilmek için gerekli middleware'i ekliyoruz.
app.MapControllers();

// Program.cs'in sonuna, app.Run()'dan önce eklenecek.
// Baþlangýçta In-Memory DB'ye veri eklemek için.
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
    // Yeni, temiz çaðrýmýz:
    await MiniETicaret.Infrastructure.Persistence.Seed.ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);
}


app.Run();
