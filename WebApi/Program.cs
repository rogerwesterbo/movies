using MongoDB.Driver;
using Movies.WebApi.GraphQL;
using Movies.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped<MongoService>();

builder.Services
        .AddGraphQLServer()
        .AddQueryType<MovieQuery>()
        .AddMongoDbFiltering()
        .AddMongoDbSorting()
        .AddMongoDbProjections();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

app.Run();
