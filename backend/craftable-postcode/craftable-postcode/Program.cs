using craftable_postcode.Data;
using craftable_postcode.Respositories;
using craftable_postcode.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AddressApiDbContext>(options => options.UseInMemoryDatabase("AddressDb"));

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressServices, AddressServices>();
builder.Services.AddScoped<ICalculateCoordinate, CalculateCoordinate>();
builder.Services.AddScoped<IAddressApiDbContext, AddressApiDbContext>();

builder.Services.AddHttpClient<IAddressRepository, AddressRepository>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
                                                //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
            .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
