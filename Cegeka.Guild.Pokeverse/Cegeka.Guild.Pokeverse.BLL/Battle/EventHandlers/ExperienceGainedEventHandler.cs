using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Battle.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battle.EventHandlers
{
    internal sealed class ExperienceGainedEventHandler : INotificationHandler<ExperienceGainedEvent>
    {
        private static int LevelThreshold = 1500;
        private readonly IRepository<Pokemon> pokemonRepository;

        public ExperienceGainedEventHandler(IRepository<Pokemon> pokemonRepository)
        {
            this.pokemonRepository = pokemonRepository;
        }

        public Task Handle(ExperienceGainedEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}