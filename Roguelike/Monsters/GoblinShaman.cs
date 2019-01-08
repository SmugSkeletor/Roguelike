using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.Monsters
{
    public class GoblinShaman : Monster
    {
        public static GoblinShaman Create(int level)
        {
            int health = Dice.Roll("2D5") + level * Dice.Roll("1D5");
            return new GoblinShaman
            {
                Attack = Dice.Roll("3D6") + level / 2,
                AtkChance = Dice.Roll("25D3"),
                FOVValue = 10,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("2D5") + level / 2,
                DefChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("10D5"),
                Gems = Dice.Roll("1D3"),
                Health = health,
                MaxHealth = health,
                Name = "Goblinski Szaman",
                Speed = 8,
                Symbol = 'G',
                ExpValue = 8 + level
            };
        }
    }
}