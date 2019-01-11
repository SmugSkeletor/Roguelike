using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.Monsters
{
    public class Kobold : Monster
    {
        public Kobold() { }

        public override void SetAttributes(int level)
        {
            int health = Dice.Roll("2D5") + level * Dice.Roll("1D2");
            Attack = Dice.Roll("1D4") + level / 2;
            AtkChance = Dice.Roll("25D3");
            FOVValue = 10;
            Gems = 0;
            Color = Colors.KoboldColor;
            Defense = Dice.Roll("1D3") + level / 2;
            DefChance = Dice.Roll("10D4");
            Gold = Dice.Roll("5D5");
            Health = health;
            MaxHealth = health;
            Name = "Kobold";
            Speed = 14;
            Symbol = 'k';
            ExpValue = 1 + level;

        }



    }
}