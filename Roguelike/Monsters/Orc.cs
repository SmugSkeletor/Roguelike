using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.Monsters
{
    public class Orc : Monster
    {
        public static Orc Create(int level)
        {
            int health = Dice.Roll("2D5") + level * Dice.Roll("1D2");
            return new Orc
            {
                Attack = Dice.Roll("1D6") + level / 2,
                AtkChance = Dice.Roll("25D3"),
                FOVValue = 10,
                Gems = 0,
                Color = Colors.OrcColor,
                Defense = Dice.Roll("1D3") + level / 2,
                DefChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("8D5"),
                Health = health,
                MaxHealth = health,
                Name = "Ork",
                Speed = 11,
                Symbol = 'o',
                ExpValue = 2 + level
            };
        }
    }
}