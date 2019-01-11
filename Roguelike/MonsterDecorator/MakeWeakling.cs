using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.MonsterDecorator
{
    public class MakeWeakling : MonsterDecorator
    {
        public MakeWeakling(Monster monster):base(monster)
        {
           
        }

        public override void SetAttributes(int level)
        {
            base.SetAttributes(level);
            monster.MaxHealth -= Dice.Roll("1D5");
            monster.Attack -= Dice.Roll("1D5");
            if (monster.Attack < 0)
                monster.Attack = 0;
            monster.Health = monster.MaxHealth;
            monster.Color = Colors.WeakingColor;
            monster.Name += " - Slabeusz";
        }
    }
}
