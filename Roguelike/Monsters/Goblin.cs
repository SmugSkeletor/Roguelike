using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.Monsters
{
    public class Goblin : Monster
    {
        public static Goblin Create(int level)
        {
            int health = Dice.Roll("1D5") + level * Dice.Roll("1D2");
            return new Goblin
            {
                Attack = Dice.Roll("2D2") + level / 2,
                AtkChance = Dice.Roll("30D3"),
                FOVValue = 10,
                Gems = 0,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("1D2") + level / 2,
                DefChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("6D5"),
                Health = health,
                MaxHealth = health,
                Name = "Goblin",
                Speed = 6,
                Symbol = 'g',
                ExpValue = 2 + level
            };
        }
    }
}