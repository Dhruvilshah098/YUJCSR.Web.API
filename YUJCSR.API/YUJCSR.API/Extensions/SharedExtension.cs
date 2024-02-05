using YUJCSR.Common.Implementation;
using YUJCSR.Common.Interface;

namespace YUJCSR.API.Extensions
{
    public static class SharedExtension
    {
        public static IServiceCollection RegisterShared(this IServiceCollection services)
        {
            services.AddScoped<IHttpHelper, HttpHelper>();
            return services;
        }
    }
}
