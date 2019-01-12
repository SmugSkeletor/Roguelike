using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;

namespace Roguelike.Pickups
{
    public class StrPotion : Potion
    {
        public StrPotion()
        {
            Symbol = (char)169;
            Color = Colors.StrPotionColor;
            Name = "Mikstura sily";
        }
        public override void OnPickupEffect()
        {

        }
    }
}
