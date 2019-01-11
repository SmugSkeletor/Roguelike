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
    class BeholderFactory : MonsterFactory
    {
        public override Monster Create(CreationType type)
        {
            switch (type)
            {
                case CreationType.NORMAL:
                    return new Beholder();
                case CreationType.WEAK:
                    return new MakeWeakling(new Beholder());
                case CreationType.MINI_BOSS:
                    return new MakeMiniBoss(new Beholder());
                case CreationType.BOSS:
                    return new MakeBoss(new Beholder());
                default:
                    return new Beholder();

            }
        }
    }
}
