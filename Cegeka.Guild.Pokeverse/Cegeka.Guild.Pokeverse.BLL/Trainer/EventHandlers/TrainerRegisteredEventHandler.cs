using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Trainer.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.EventHandlers
{
    internal sealed class TrainerRegisteredEventHandler : INotificationHandler<TrainerRegisteredEvent>
    {
        private const int RandomPokemonsOnRegister = 2;
        private readonly IRepository<Domain.Entities.Trainer> trainerRepository;
        private readonly IRepository<PokemonDefinition> definitionsRepository;

        public TrainerRegisteredEventHandler(IRepository<Domain.Entities.Trainer> trainerRepository, IRepository<PokemonDefinition> definitionsRepository)
        {
            this.trainerRepository = trainerRepository;
            this.definitionsRepository = definitionsRepository;
        }

        public Task Handle(TrainerRegisteredEvent request, CancellationToken cancellationToken)
        {
            var trainer = trainerRepository.GetById(request.TrainerId);

            var random = new Random(DateTime.Now.Millisecond);
            var pokemons = this.definitionsRepository.GetAll();
            Enumerable.Range(1, RandomPokemonsOnRegister)
                .Select(_ => random.Next(0, pokemons.Count()))
                .Select(randomIndex => pokemons.ElementAt(randomIndex))
                .Select(definition => new Pokemon(trainer, definition))
                .ToList()
                .ForEach(p =>
                {
                    trainer.Pokemons.Add(p);
                });

            return Task.CompletedTask;
        }
    }
}