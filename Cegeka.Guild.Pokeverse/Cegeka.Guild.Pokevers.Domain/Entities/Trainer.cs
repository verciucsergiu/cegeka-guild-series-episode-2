using System.Collections.Generic;

namespace Cegeka.Guild.Pokevers.Domain.Entities
{
    public class Trainer : Entity
    {
        public string Name { get; set; }

        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
    }
}