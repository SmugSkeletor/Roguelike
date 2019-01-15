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
            Player.GetInstance().Defense += 1;
            Game.Log.Add($"Podnosisz miksture obrony");
        }
    }
}
