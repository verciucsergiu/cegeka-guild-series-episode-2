using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Battle.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battle.EventHandlers
{
    internal sealed class BattleEndedEventHandler : INotificationHandler<BattleEndedEvent>
    {
        private static int ExperienceGainedWinning = 500;
        private static int ExperienceGainedLosing = 50;
        private readonly IRepository<Domain.Entities.Battle> battleRepository;
        private readonly IRepository<Pokemon> pokemonRepository;

        public BattleEndedEventHandler(IRepository<Domain.Entities.Battle> battleRepository, IRepository<Pokemon> pokemonRepository)
        {
            this.battleRepository = battleRepository;
            this.pokemonRepository = pokemonRepository;
        }

        public Task Handle(BattleEndedEvent notification, CancellationToken cancellationToken)
        {
            var battle = battleRepository.GetById(notification.BattleId);
            var winner = battle.Winner;
            winner.Experience += ExperienceGainedWinning;
            var loser = battle.Loser;
            loser.Experience += ExperienceGainedLosing;

            pokemonRepository.Update(winner);
            pokemonRepository.Update(loser);

            return Task.CompletedTask;
        }
    }
}