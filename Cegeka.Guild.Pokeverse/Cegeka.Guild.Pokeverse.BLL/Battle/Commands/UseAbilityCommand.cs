using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battle.Commands
{
    public sealed class UseAbilityCommand : IRequest
    {
        public UseAbilityCommand(Guid abilityId, Guid pokemonId, Guid battleId)
        {
            AbilityId = abilityId;
            PokemonId = pokemonId;
            BattleId = battleId;
        }
        
        public Guid AbilityId { get; }
        public Guid PokemonId { get; }
        public Guid BattleId { get; }
    }
}