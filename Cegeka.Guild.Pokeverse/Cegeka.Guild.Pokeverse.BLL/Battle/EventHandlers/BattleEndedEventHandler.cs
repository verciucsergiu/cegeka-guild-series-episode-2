using System;
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
        private static int MaxGainedExperience = 50;
        private static int MinGainedExperience = 40;
        private static double WinningFactor = 0.5;
        private readonly IRepository<Domain.Entities.Battle> battleRepository;
        private readonly IRepository<Pokemon> pokemonRepository;
        private readonly IMediator mediator;

        public BattleEndedEventHandler(IRepository<Domain.Entities.Battle> battleRepository, IRepository<Pokemon> pokemonRepository, IMediator mediator)
        {
            this.battleRepository = battleRepository;
            this.pokemonRepository = pokemonRepository;
            this.mediator = mediator;
        }

        public async Task Handle(BattleEndedEvent notification, CancellationToken cancellationToken)
        {
            var battle = battleRepository.GetById(notification.BattleId);
            var random = new Random(DateTime.Now.Millisecond);
            var experienceGained = random.Next(MinGainedExperience, MaxGainedExperience);
            
            var winner = battle.Winner;
            winner.Experience += (int)Math.Round(experienceGained * WinningFactor);

            var loser = battle.Loser;
            loser.Experience += experienceGained;

            pokemonRepository.Update(winner);
            await mediator.Publish(new ExperienceGainedEvent(winner.Id), cancellationToken);

            pokemonRepository.Update(loser);
            await mediator.Publish(new ExperienceGainedEvent(loser.Id), cancellationToken);
        }
    }
}