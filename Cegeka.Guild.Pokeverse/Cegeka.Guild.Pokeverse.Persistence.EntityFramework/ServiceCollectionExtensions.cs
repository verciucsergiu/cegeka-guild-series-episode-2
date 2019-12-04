using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Persistence.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<PokeverseContext>(options => options.UseSqlServer(connectionString))
                .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }
    }
}
