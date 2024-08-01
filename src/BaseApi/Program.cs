using BaseApi.Data;
using BaseApi.Repositories;
using BaseApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new SqliteConnectionFactory(
        builder.Configuration.GetValue<string>("Database:ConnectionString")!));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/users", async (IUserService userService) => {
	return await userService.GetAllAsync();
});

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
