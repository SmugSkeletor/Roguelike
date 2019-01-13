using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;
using RogueLike;

namespace Roguelike.Pickups
{
    public class StrPotion : Potion
    {
        public StrPotion()
        {
            Symbol = '!';
            Color = RLColor.LightRed;
        }

        public override void OnPickup()
        {
            Player.GetInstance().Attack += 1;
        }
    }
}
