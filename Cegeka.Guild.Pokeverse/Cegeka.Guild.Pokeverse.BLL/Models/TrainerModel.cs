using System;
using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Business.Arena.Models;

namespace Cegeka.Guild.Pokeverse.Business.Models
{
    public class TrainerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<PokemonModel> Pokemons { get; set; }
    }
}