using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;

namespace Roguelike.Pickups
{
    public abstract class Pickup
    {
        private string _name;
        private char _symbol;
        private RLColor _color;

        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
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
        public abstract void OnPickupEffect();
    }
}
