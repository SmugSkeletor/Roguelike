using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.MonsterDecorator;
using Roguelike.Core;

namespace Roguelike.MonsterFactories
{
    public enum CreationType
    {
        NORMAL,
        WEAK,
        MINI_BOSS,
        BOSS
    }


    public abstract class MonsterFactory
    {
        public abstract Monster Create(CreationType type);
    }
}
