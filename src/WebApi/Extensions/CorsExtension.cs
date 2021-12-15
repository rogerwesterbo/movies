namespace Movies.WebApi.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            return services;
        }
    }
}