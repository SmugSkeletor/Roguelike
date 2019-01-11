using Roguelike.Behaviors;
using Roguelike.Systems;
using Roguelike.Core;

namespace Roguelike.MonsterDecorator
{
    public abstract class MonsterDecorator : Monster
    {
        protected Monster monster;

        protected MonsterDecorator(Monster monster)
        {
            this.monster = monster;
        }


    }
}
