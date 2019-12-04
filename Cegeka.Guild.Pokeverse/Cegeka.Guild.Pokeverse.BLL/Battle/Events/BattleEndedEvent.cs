using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battle.Events
{
    internal sealed class BattleEndedEvent : INotification
    {
        public BattleEndedEvent(Guid battleId)
        {
            BattleId = battleId;
        }

        public Guid BattleId { get; }
    }
}