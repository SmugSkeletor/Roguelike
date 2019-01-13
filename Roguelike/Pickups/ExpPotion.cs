using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;

namespace Roguelike.Pickups
{
    public class ExpPotion : Potion
    {
        public ExpPotion()
        {
            Symbol = (char)169;
            Color = Colors.ExpPotionColor;
            Name = "Mikstura doswiadczenia";
        }
        public override void OnPickup()
        {

        }
    }
}
