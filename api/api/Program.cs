using LinqToDB;
using LinqToDB.Data;
using sposko;

var builder = WebApplication.CreateBuilder(args);

var dataOptions = new DataOptions<SposkoDb>(
    new DataOptions().UsePostgreSQL("Host=localhost;Username=user;Password=password;Database=sposko")
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ISposkoDb>(_ => new SposkoDb(dataOptions));
builder.Services.AddScoped(typeof(IServiceHelper<,,>), typeof(ServiceHelper<,,>));
builder.Services.AddScoped<ISportService>(
    ctx => new SportService(
      ctx.GetService<ISposkoDb>(),
      ctx.GetService<IServiceHelper<Sport, SportDTO, CreateSportDTO>>()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
