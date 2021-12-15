using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

namespace Movies.WebApi.Extensions;

public static class CompressionExtension
{
    public static IServiceCollection AddCompression(
        this IServiceCollection services)
    {
        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
        });
        return services;
    }
}