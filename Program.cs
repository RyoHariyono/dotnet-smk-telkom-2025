using dotnet_smk_telkom_2025.Infrastructure.Databases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<InMemoryDbContext>();

/* ------------------------------ Repositories ------------------------------ */
// Add repositories here when needed
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IBookRepository, BookRepository>();
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

/* -------------------------------- Services -------------------------------- */
// Add services here when needed
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IBookService, BookService>();
// builder.Services.AddScoped<ICustomerService, CustomerService>();
// builder.Services.AddScoped<IPurchaseService, PurchaseService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();