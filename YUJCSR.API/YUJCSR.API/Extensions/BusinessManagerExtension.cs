using YUJCSR.Business.Implementation;
using YUJCSR.Business.Interface;

namespace YUJCSR.API.Extensions
{
    public static class BusinessManagerExtension
    {
        public static IServiceCollection RegisterBusinessManager(this IServiceCollection services)
        {
            services.AddScoped<IAdminUserBusinessManager, AdminUserBusinessManager>();
            services.AddScoped<ICSOBusinessManager, CSOBusinessManager>();
            services.AddScoped<IProjectBusinessManager, ProjectBusinessManager>();
            services.AddScoped<IUNSDGBusinessManager, UNSDGBusinessManager>();
            services.AddScoped<IImpactBusinessManager, ImpactBusinessManager>();

            return services;

        }
    }
}
