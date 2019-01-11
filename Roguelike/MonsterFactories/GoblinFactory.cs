using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.MonsterDecorator;
using Roguelike.Core;
using Roguelike.Monsters;

namespace Roguelike.MonsterFactories
{
    class GoblinFactory : MonsterFactory
    {
        public override Monster Create(CreationType type)
        {
            switch (type)
            {
                case CreationType.NORMAL:
                    return new Goblin();
                case CreationType.WEAK:
                    return new MakeWeakling(new Goblin());
                case CreationType.MINI_BOSS:
                    return new MakeMiniBoss(new Goblin());
                case CreationType.BOSS:
                    return new MakeBoss(new Goblin());
                default:
                    return new Goblin();

            }
        }
    }
}
