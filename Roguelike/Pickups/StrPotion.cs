using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;
using RogueLike;
using Roguelike.Sys;

namespace Roguelike.Pickups
{
    public class StrPotion : Potion
    {
        public StrPotion()
        {
            Name = "Mikstura sily";
            Symbol = (char)169;
            Color = Colors.StrPotionColor;
        }

        public override void OnPickup()
        {
            Player.GetInstance().Attack += 1;
            Facade.Log.Add($"Podnosisz miksture sily");
        }
    }
}
