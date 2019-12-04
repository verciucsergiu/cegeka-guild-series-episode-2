using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Arena.Queries;
using Cegeka.Guild.Pokeverse.Business.Models;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Arena.QueryHandlers
{
    internal sealed class GetOngoingBattlesQueryHandler : IRequestHandler<GetOngoingBattlesQuery, IEnumerable<OngoingBattleModel>>
    {
        private readonly IRepository<Battle> battleRepository;

        public GetOngoingBattlesQueryHandler(IRepository<Battle> battleRepository)
        {
            this.battleRepository = battleRepository;
        }

        public Task<IEnumerable<OngoingBattleModel>> Handle(GetOngoingBattlesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(battleRepository.GetAll()
                .Where(b => b.Winner == null)
                .Select(b => new OngoingBattleModel
                {
                    Id = b.Id,
                    Attacker = b.Attacker.Pokemon.Name,
                    AttackerHealth = b.Attacker.Health,
                    Defender = b.Defender.Pokemon.Name,
                    DefenderHealth = b.Defender.Health,
                    StartedAt = b.StartedAt
                }));
        }
    }
}