using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.MonsterDecorator
{
    public class MakeMiniBoss : MonsterDecorator
    {
        public MakeMiniBoss(Monster monster) : base(monster)
        {

        }
        public override void SetAttributes(int level)
        {
            base.SetAttributes(level);
            monster.MaxHealth += Dice.Roll("1D5");
            monster.Attack += Dice.Roll("1D5");
            monster.Health = monster.MaxHealth;
            monster.Color = Colors.MiniBossColor;
            monster.Name += " - Mini Boss";

        }
    }
}
