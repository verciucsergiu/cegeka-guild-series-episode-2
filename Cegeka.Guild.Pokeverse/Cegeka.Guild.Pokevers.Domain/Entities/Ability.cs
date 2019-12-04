using System;

namespace Cegeka.Guild.Pokeverse.Domain.Entities
{
    public class Ability : Entity
    {
        public Guid PokemonDefinitionId { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }

        public int RequiredLevel { get; set; }
    }
}