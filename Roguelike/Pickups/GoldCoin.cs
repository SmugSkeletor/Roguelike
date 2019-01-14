using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;
using RogueSharp.DiceNotation;

namespace Roguelike.Pickups
{
    public class GoldCoin : Pickup
    {
        public GoldCoin()
        {
            Symbol = (char)184;
            Color = Colors.GoldCoinColor;
            Name = "Sztuka zlota";
        }
        public override void OnPickup()
        {
            Player.GetInstance().Gold += Dice.Roll("1D100");
        }
    }
}
