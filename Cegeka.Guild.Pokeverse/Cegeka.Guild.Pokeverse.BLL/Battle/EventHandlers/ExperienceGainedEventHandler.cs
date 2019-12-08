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
        private static int LevelUpThreshold = 100;
        private readonly IRepository<Pokemon> pokemonRepository;

        public ExperienceGainedEventHandler(IRepository<Pokemon> pokemonRepository)
        {
            this.pokemonRepository = pokemonRepository;
        }

        public Task Handle(ExperienceGainedEvent notification, CancellationToken cancellationToken)
        {
            var pokemon = pokemonRepository.GetById(notification.PokemonId);

            if (pokemon.Experience >= pokemon.CurrentLevel * LevelUpThreshold)
            {
                pokemon.CurrentLevel++;
                pokemonRepository.Update(pokemon);
            }

            return Task.CompletedTask;
        }
    }
}