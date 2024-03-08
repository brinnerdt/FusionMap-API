using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "FusionMapAPI", Version = "v1" });

});

builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevCors", (corsBuilder) =>
    {
        corsBuilder
            // .WithOrigins("http://localhost:3000")
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    options.AddPolicy("ProdCors", (corsBuilder) =>
    {
        corsBuilder
            // .WithOrigins("https://myproductionapp.com")
            // .WithOrigins("http://172.19.0.4:3000", "http://localhost:3000", "http://web:3000")
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevCors");
}

else
{
    Console.WriteLine("Production");
    // app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "FusionMapAPI v1");
    });
    app.UseCors("ProdCors");
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

