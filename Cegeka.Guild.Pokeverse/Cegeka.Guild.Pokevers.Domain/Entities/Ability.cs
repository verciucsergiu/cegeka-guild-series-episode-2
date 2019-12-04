namespace Cegeka.Guild.Pokevers.Domain.Entities
{
    public class Ability : Entity
    {
        public string Name { get; set; }

        public int Damage { get; set; }

        public int RequiredLevel { get; set; }
    }
}