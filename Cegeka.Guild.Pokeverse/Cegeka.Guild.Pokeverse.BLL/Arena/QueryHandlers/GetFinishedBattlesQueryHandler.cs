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
    internal sealed class GetFinishedBattlesQueryHandler : IRequestHandler<GetFinishedBattlesQuery, IEnumerable<FinishedBattleModel>>
    {
        private readonly IRepository<Battle> battleRepository;

        public GetFinishedBattlesQueryHandler(IRepository<Battle> battleRepository)
        {
            this.battleRepository = battleRepository;
        }

        public Task<IEnumerable<FinishedBattleModel>> Handle(GetFinishedBattlesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(battleRepository.GetAll()
                .Where(b => b.Winner != null)
                .Select(b => new FinishedBattleModel
                {
                    Attacker = b.Attacker.Pokemon.Name,
                    Defender = b.Defender.Pokemon.Name,
                    StartedAt = b.StartedAt,
                    FinishedAt = b.FinishedAt,
                    Winner = b.Winner.Name
                }));
        }
    }
}