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
            Player.GetInstance().Health += ((int)Player.GetInstance().MaxHealth) / 2;
            if (Player.GetInstance().Health > Player.GetInstance().MaxHealth) Player.GetInstance().Health = Player.GetInstance().MaxHealth;
            Game.Log.Add($"Podnosisz miksture zycia");
        }
    }
}
