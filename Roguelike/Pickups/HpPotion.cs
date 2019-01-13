using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;

namespace Roguelike.Pickups
{
    public class HpPotion : Potion
    {
        public HpPotion()
        {
            Symbol = (char)169;
            Color = Colors.HpPotionColor;
            Name = "Mikstura zycia";
        }
        public override void OnPickup()
        {
        }
    }
}
