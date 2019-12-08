using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battle.Events
{
    internal sealed class ExperienceGainedEvent : INotification
    {
        public Guid PokemonId { get; }

        public ExperienceGainedEvent(Guid pokemonId)
        {
            PokemonId = pokemonId;
        }
    }
}