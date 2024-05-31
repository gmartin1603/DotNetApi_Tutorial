var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddPolicy("DevCors", builder =>
  {
    builder.WithOrigins("http://localhost:3000", "http://localhost:8080")
    .AllowAnyMethod()
    .AllowAnyHeader();
  });

  options.AddPolicy("ProdCors", builder =>
  {
    builder.WithOrigins("https://example.com")
    .AllowAnyMethod()
    .AllowAnyHeader();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseCors("DevCors");
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  app.UseCors("ProdCors");
  app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();

