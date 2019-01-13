using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;

namespace Roguelike.Pickups
{
    public class DefPotion : Potion
    {
        public DefPotion()
        {
            Symbol = (char)169;
            Color = Colors.DefPotionColor;
            Name = "Mikstura obrony";
        }
        public override void OnPickup()
        {

        }
    }
}
