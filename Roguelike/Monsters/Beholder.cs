using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.Monsters
{
    public class Beholder : Monster
    {
        public static Beholder Create(int level)
        {
            int health = Dice.Roll("5D5") + level * Dice.Roll("1D4");
            return new Beholder
            {
                Attack = Dice.Roll("4D5") + level / 2,
                AtkChance = Dice.Roll("30D3"),
                FOVValue = 10,
                Color = Colors.BeholderColor,
                Defense = Dice.Roll("2D5") + level / 2,
                DefChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("10D5"),
                Gems = Dice.Roll("1D4"),
                Health = health,
                MaxHealth = health,
                Name = "Beholder",
                Speed = 12,
                Symbol = 'B',
                ExpValue = 10 + level
            };
        }
    }
}