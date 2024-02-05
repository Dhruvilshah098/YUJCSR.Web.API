using YUJCSR.Infrastructure.Repositories.Implementation;
using YUJCSR.Infrastructure.Repositories.Interface;

namespace YUJCSR.API.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            return services;
        }
    }
}
