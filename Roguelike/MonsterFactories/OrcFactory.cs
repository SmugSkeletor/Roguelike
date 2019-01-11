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
    class OrcFactory : MonsterFactory
    {
        public override Monster Create(CreationType type)
        {
            switch (type)
            {
                case CreationType.NORMAL:
                    return new Orc();
                case CreationType.WEAK:
                    return new MakeWeakling(new Orc());
                case CreationType.MINI_BOSS:
                    return new MakeMiniBoss(new Orc());
                case CreationType.BOSS:
                    return new MakeBoss(new Orc());
                default:
                    return new Orc();

            }
        }
    }
}
