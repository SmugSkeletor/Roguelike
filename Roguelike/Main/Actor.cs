using RLNET;
using Roguelike.Interfaces;
using RogueSharp;

namespace Roguelike.Core
{
    public class Actor : IActor, ISymbol, ITurnQueue
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

        public int Gems
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

        public int Time
        {
            get
            {
                return Speed;
            }
        }

        public int ExpToLevel
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

        public int Experience
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

        public int ExpValue
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

        public int Level
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

        public int Attack
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

        public int AtkChance
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

        public int FOVValue
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

        public int Defense
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

        public int DefChance
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

        public int Gold
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

        public int Health
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

        public int MaxHealth
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

        public string Name
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

        public int Speed
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

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
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