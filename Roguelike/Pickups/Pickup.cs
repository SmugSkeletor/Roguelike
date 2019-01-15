using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Interfaces;
using RogueSharp;
using RLNET;
using Roguelike.Core;

namespace Roguelike.Pickups
{
    public abstract class Pickup
    {
        private char _symbol;
        private RLColor _color;
        private string _name;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual RLColor Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        public virtual char Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
            }
        }

        public virtual void Draw(RLConsole console, IMap map, Point location)
        {
            if (!map.GetCell(location.X, location.Y).IsExplored)
            {
                return;
            }
            if (map.IsInFov(location.X, location.Y))
            {
                console.Set(location.X, location.Y, Color, Colors.FloorBGFov, Symbol);
            }
            else
            {
                console.Set(location.X, location.Y, Colors.Floor, Colors.FloorBG, '.');
            }
        }

        public abstract void OnPickup();

    }
}
