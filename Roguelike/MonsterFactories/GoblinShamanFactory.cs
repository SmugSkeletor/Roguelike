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
    class GoblinShamanFactory : MonsterFactory
    {
        public override Monster Create(CreationType type)
        {
            switch (type)
            {
                case CreationType.NORMAL:
                    return new GoblinShaman();
                case CreationType.WEAK:
                    return new MakeWeakling(new GoblinShaman());
                case CreationType.MINI_BOSS:
                    return new MakeMiniBoss(new GoblinShaman());
                case CreationType.BOSS:
                    return new MakeBoss(new GoblinShaman());
                default:
                    return new GoblinShaman();

            }
        }
    }
}
