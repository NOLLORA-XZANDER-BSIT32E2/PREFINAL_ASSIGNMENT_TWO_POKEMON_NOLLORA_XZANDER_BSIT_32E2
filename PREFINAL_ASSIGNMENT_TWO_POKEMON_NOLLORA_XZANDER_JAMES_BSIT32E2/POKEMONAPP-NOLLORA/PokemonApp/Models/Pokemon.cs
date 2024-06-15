namespace PokemonApp.Models
{
    public class Pokemon
    {
        public Pokemon()
        {
            Abilities = new List<PokemonAbility>();
            Moves = new List<PokemonMove>();
        }

        public string Name { get; set; }
        public string SpriteUrl { get; set; }
        public List<PokemonAbility> Abilities { get; set; }
        public List<PokemonMove> Moves { get; set; }

        public class PokemonAbility
        {
            public Ability Ability { get; set; }
        }

        public class PokemonMove
        {
            public Move Move { get; set; }
        }

        public class Ability
        {
            public string Name { get; set; }
        }

        public class Move
        {
            public string Name { get; set; }
        }
    }
}
