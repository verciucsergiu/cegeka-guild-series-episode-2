using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.Events
{
    internal sealed class TrainerRegisteredEvent : INotification
    {
        public Guid TrainerId { get; }

        public TrainerRegisteredEvent(Guid trainerId)
        {
            TrainerId = trainerId;
        }
    }
}