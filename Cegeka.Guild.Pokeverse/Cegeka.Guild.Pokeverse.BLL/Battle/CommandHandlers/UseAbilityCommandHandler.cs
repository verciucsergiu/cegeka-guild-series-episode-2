using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Battle.Commands;
using Cegeka.Guild.Pokeverse.Business.Battle.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battle.CommandHandlers
{
    internal sealed class UseAbilityCommandHandler : IRequestHandler<UseAbilityCommand>
    {
        private readonly IRepository<Domain.Entities.Battle> battlesRepository;
        private readonly IRepository<Pokemon> pokemonsRepository;
        private readonly IMediator mediator;

        public UseAbilityCommandHandler(IRepository<Domain.Entities.Battle> battlesRepository, IRepository<Pokemon> pokemonsRepository, IMediator mediator)
        {
            this.battlesRepository = battlesRepository;
            this.pokemonsRepository = pokemonsRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(UseAbilityCommand request, CancellationToken cancellationToken)
        {
            var battle = this.battlesRepository.GetById(request.BattleId);
            ValidateBattle(request, battle);

            var pokemonDealingDamage = this.pokemonsRepository.GetById(request.PokemonId);
            var ability = pokemonDealingDamage.Abilities.FirstOrDefault(a => a.Id == request.AbilityId);

            ValidateAbilityUsage(ability, pokemonDealingDamage);

            var pokemonTakingDamage = battle.Attacker;
            if (request.PokemonId == battle.AttackerId)
            {
                pokemonTakingDamage = battle.Defender;
            }

            pokemonTakingDamage.Health -= ability.Damage;
            battle.ActivePlayer = pokemonTakingDamage.Pokemon.Id;
            if (pokemonTakingDamage.Health <= 0)
            {
                battle.Winner = pokemonDealingDamage;
                battle.Loser = pokemonTakingDamage.Pokemon;
                battle.FinishedAt = DateTime.Now;

                await mediator.Publish(new BattleEndedEvent(battle.Id), cancellationToken);
            }

            return Unit.Value;
        }

        private static void ValidateAbilityUsage(Ability ability, Pokemon pokemonDealingDamage)
        {
            if (ability == null)
            {
                throw new InvalidOperationException("Unknown ability!");
            }

            if (ability.RequiredLevel > pokemonDealingDamage.CurrentLevel)
            {
                throw new InvalidOperationException("You cannot use this ability yet!");
            }
        }

        private static void ValidateBattle(UseAbilityCommand request, Domain.Entities.Battle battle)
        {
            if (battle == null)
            {
                throw new InvalidOperationException("Battle not found!");
            }

            if (battle.Winner != null)
            {
                throw new InvalidOperationException("Battle has already ended!");
            }

            if (battle.ActivePlayer != request.PokemonId)
            {
                throw new InvalidOperationException("You are not the active player, wait for your turn!");
            }
        }
    }
}