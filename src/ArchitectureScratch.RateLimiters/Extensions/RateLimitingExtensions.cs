using System.Threading.RateLimiting;

namespace ArchitectureScratch.RateLimiters.Extensions;

public static class RateLimitingExtensions
{
    public static IServiceCollection ConfigureRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            /*
             * ** Fixed window limiter
             * Set how many limited requests can be performed in a given window of time (period of time)
             * For example here we are allowing 5 requests in 30 seconds for each IP address
             * After 30 seconds, the request limit is reset
             */
            options.AddPolicy("fixed", httpContext =>
            {
                return RateLimitPartition.GetFixedWindowLimiter(httpContext.Connection.RemoteIpAddress?.ToString(), _ =>
                {
                    return new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromSeconds(30),
                        AutoReplenishment = false,
                    };
                });
            });
            
            /*
             * Sliding window limiter
             * Set how many limited request can be performed in a segment of a window of time
             * For example 
             */
            options.AddPolicy("sliding", httpContext =>
            {
                return RateLimitPartition.GetSlidingWindowLimiter(httpContext.Connection.RemoteIpAddress?.ToString(), _ =>
                {
                    return new SlidingWindowRateLimiterOptions()
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromSeconds(15),
                        SegmentsPerWindow = 3
                    };
                });
            });

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });
        return services;
    } 
}