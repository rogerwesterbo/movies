using MongoDB.Driver;
using Movies.WebApi.Extensions;
using Movies.WebApi.GraphQL;
using Movies.WebApi.Interfaces;
using Movies.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomCors(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped<IMongoService, MongoService>();
builder.Services
        .AddGraphQLServer()
        .AddQueryType<MovieQuery>()
        .AddMongoDbFiltering()
        .AddMongoDbSorting()
        .AddMongoDbProjections();
builder.Services.AddCompression();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

// kickstart seeding to mongodb
app.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IMongoService>();

await app.RunAsync();
