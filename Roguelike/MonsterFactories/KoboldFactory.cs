﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.MonsterDecorator;
using Roguelike.Core;
using Roguelike.Monsters;

namespace Roguelike.MonsterFactories
{
    class KoboldFactory : MonsterFactory
    {
        public override Monster Create(CreationType type)
        {
            switch (type)
            {
                case CreationType.NORMAL:
                    return new Kobold();
                case CreationType.WEAK:
                    return new MakeWeakling(new Kobold());
                case CreationType.MINI_BOSS:
                    return new MakeMiniBoss(new Kobold());
                case CreationType.BOSS:
                    return new MakeBoss(new Kobold());
                default:
                    return new Kobold();

            }
        }
    }
}
