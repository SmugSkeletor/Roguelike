using RLNET;
using Roguelike.Interfaces;
using RogueSharp;

namespace Roguelike.Core
{
    public abstract class Actor : IActor, ISymbol, ITurnQueue
    {
        private int _level;
        private int _attack;
        private int _atkChance;
        private int _fovValue;
        private int _defense;
        private int _defChance;
        private int _gold;
        private int _health;
        private int _maxHealth;
        private string _name;
        private int _speed;
        private int _expValue;
        private int _experience;
        private int _expToLevel;
        private int _gems;
        private int _x;
        private int _y;
        private char _symbol;
        private RLColor _color;

        public virtual int Gems
        {
            get
            {
                return _gems;
            }
            set
            {
                _gems = value;
            }
        }

        public virtual int Time
        {
            get
            {
                return Speed;
            }
        }

        public virtual int ExpToLevel
        {
            get
            {
                return _expToLevel;
            }
            set
            {
                _expToLevel = value;
            }
        }

        public virtual int Experience
        {
            get
            {
                return _experience;
            }
            set
            {
                _experience = value;
            }
        }

        public virtual int ExpValue
        {
            get
            {
                return _expValue;
            }
            set
            {
                _expValue = value;
            }
        }

        public virtual int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        public virtual int Attack
        {
            get
            {
                return _attack;
            }
            set
            {
                _attack = value;
            }
        }

        public virtual int AtkChance
        {
            get
            {
                return _atkChance;
            }
            set
            {
                _atkChance = value;
            }
        }

        public virtual int FOVValue
        {
            get
            {
                return _fovValue;
            }
            set
            {
                _fovValue = value;
            }
        }

        public virtual int Defense
        {
            get
            {
                return _defense;
            }
            set
            {
                _defense = value;
            }
        }

        public virtual int DefChance
        {
            get
            {
                return _defChance;
            }
            set
            {
                _defChance = value;
            }
        }

        public virtual int Gold
        {
            get
            {
                return _gold;
            }
            set
            {
                _gold = value;
            }
        }

        public virtual int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }

        public virtual int MaxHealth
        {
            get
            {
                return _maxHealth;
            }
            set
            {
                _maxHealth = value;
            }
        }

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

        public virtual int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
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
    }
}