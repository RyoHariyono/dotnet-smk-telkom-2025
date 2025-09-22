using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Services;
using dotnet_smk_telkom_2025.Repositories;
using dotnet_smk_telkom_2025.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<InMemoryDbContext>();

/* ------------------------------ Repositories ------------------------------ */
builder.Services.AddScoped<BookQueryRepository>();
builder.Services.AddScoped<BookStoreRepository>();
builder.Services.AddScoped<CustomerQueryRepository>();
builder.Services.AddScoped<CustomerStoreRepository>();
builder.Services.AddScoped<PurchaseQueryRepository>();
builder.Services.AddScoped<PurchaseStoreRepository>();

/* -------------------------------- Services -------------------------------- */
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<PurchaseService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();