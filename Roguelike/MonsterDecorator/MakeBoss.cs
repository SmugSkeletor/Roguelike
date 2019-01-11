using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.MonsterDecorator
{
    class MakeBoss : MonsterDecorator
    {
        public MakeBoss(Monster monster):base(monster)
        {

        }
        public override void SetAttributes(int level)
        {
            base.SetAttributes(level);
            monster.MaxHealth += Dice.Roll("2D5");
            monster.Attack += Dice.Roll("2D5");
            monster.Health = monster.MaxHealth;
            monster.Color = Colors.BossColor;
            monster.Name += "- Boss";
        }
    }
}
