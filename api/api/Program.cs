using LinqToDB;
using LinqToDB.Data;
using sposko;

var builder = WebApplication.CreateBuilder(args);

var dataOptions = new DataOptions<SposkoDb>(
    new DataOptions().UsePostgreSQL("Host=localhost;Username=user;Password=password;Database=sposko;IncludeErrorDetail=true")
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ISposkoDb>(_ => new SposkoDb(dataOptions));
builder.Services.AddScoped(typeof(IServiceHelper<,,,>), typeof(ServiceHelper<,,,>));
builder.Services.AddScoped<ISportService>(
    ctx => new SportService(
      ctx.GetService<ISposkoDb>(),
      ctx.GetService<IServiceHelper<Sport, SportDTO, CreateSportDTO, CreateSportDTO>>()
    )
);
builder.Services.AddScoped<IUserService>(
    ctx => new UserService(
      ctx.GetService<ISposkoDb>(),
      ctx.GetService<IServiceHelper<User, UserDTO, CreateUserDTO, CreateUserDTO>>()
    )
);
builder.Services.AddScoped<ISportGroupService>(
    ctx => new SportGroupService(
      ctx.GetService<ISposkoDb>(),
      ctx.GetService<IServiceHelper<SportGroup, SportGroupDTO, CreateSportGroupDTO, CreateSportGroupDTO>>()
    )
);
builder.Services.AddScoped<ITrainerService>(
    ctx => new TrainerService(
      ctx.GetService<ISposkoDb>(),
      ctx.GetService<IServiceHelper<Trainer, TrainerDTO, CreateTrainerDTO, CreateTrainerDTO>>()
    )
);
builder.Services.AddScoped<ISportTrainingService>(
    ctx => new SportTrainingService(
      ctx.GetService<ISposkoDb>(),
      ctx.GetService<IServiceHelper<SportTraining, SportTrainingDTO, CreateSportTrainingDTO, UpdateSportTrainingDTO>>()
    )
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowAll");

app.Run();
