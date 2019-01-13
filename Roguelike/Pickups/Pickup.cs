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
    public abstract class Pickup : ISymbol
    {
        private int _x;
        private int _y;
        private char _symbol;
        private RLColor _color;

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
        public virtual int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public virtual int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public virtual void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBGFov, Symbol);
            }
            else
            {
                console.Set(X, Y, Colors.Floor, Colors.FloorBG, '.');
            }
        }

        public abstract void OnPickup();

    }
}
