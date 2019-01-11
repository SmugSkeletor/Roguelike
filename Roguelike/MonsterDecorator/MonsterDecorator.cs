using Roguelike.Core;
using RogueSharp.DiceNotation;
using RLNET;
using Roguelike.Interfaces;
using RogueSharp;

namespace Roguelike.MonsterDecorator
{
    public abstract class MonsterDecorator : Monster
    {
        protected Monster monster;

        protected MonsterDecorator(Monster monster)
        {
            this.monster = monster;
        }

        public override void SetAttributes(int level)
        {
            monster.SetAttributes(level);
           
        }

        public override int Gems
        {
            get
            {
                return monster.Gems;
            }
            set
            {
                monster.Gems = value;
            }
        }

        public override int Time
        {
            get
            {
                return monster.Speed;
            }
        }

        public override int ExpToLevel
        {
            get
            {
                return monster.ExpToLevel;
            }
            set
            {
                monster.ExpToLevel = value;
            }
        }

        public override int Experience
        {
            get
            {
                return monster.Experience;
            }
            set
            {
                monster.Experience = value;
            }
        }

        public override int ExpValue
        {
            get
            {
                return monster.ExpValue;
            }
            set
            {
                monster.ExpValue = value;
            }
        }

        public override int Level
        {
            get
            {
                return monster.Level;
            }
            set
            {
                monster.Level = value;
            }
        }

        public override int Attack
        {
            get
            {
                return monster.Attack;
            }
            set
            {
                monster.Attack = value;
            }
        }

        public override int AtkChance
        {
            get
            {
                return monster.AtkChance;
            }
            set
            {
                monster.AtkChance = value;
            }
        }

        public override int FOVValue
        {
            get
            {
                return monster.FOVValue;
            }
            set
            {
                monster.FOVValue = value;
            }
        }

        public override int Defense
        {
            get
            {
                return monster.Defense;
            }
            set
            {
                monster.Defense = value;
            }
        }

        public override int DefChance
        {
            get
            {
                return monster.DefChance;
            }
            set
            {
                monster.DefChance = value;
            }
        }

        public override int Gold
        {
            get
            {
                return monster.Gold;
            }
            set
            {
                monster.Gold = value;
            }
        }

        public override int Health
        {
            get
            {
                return monster.Health;
            }
            set
            {
                monster.Health = value;
            }
        }

        public override int MaxHealth
        {
            get
            {
                return monster.MaxHealth;
            }
            set
            {
                monster.MaxHealth = value;
            }
        }

        public override string Name
        {
            get
            {
                return monster.Name;
            }
            set
            {
                monster.Name = value;
            }
        }

        public override int Speed
        {
            get
            {
                return monster.Speed;
            }
            set
            {
                monster.Speed = value;
            }
        }

        public override RLColor Color
        {
            get
            {
                return monster.Color;
            }
            set
            {
                monster.Color = value;
            }
        }
        public override char Symbol
        {
            get
            {
                return monster.Symbol;
            }
            set
            {
                monster.Symbol = value;
            }
        }
        public override int X
        {
            get
            {
                return monster.X;
            }
            set
            {
                monster.X = value;
            }
        }
        public override int Y
        {
            get
            {
                return monster.Y;
            }
            set
            {
                monster.Y = value;
            }
        }

        public override void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(monster.X, monster.Y).IsExplored)
            {
                return;
            }
            if (map.IsInFov(monster.X, monster.Y))
            {
                console.Set(monster.X, monster.Y, monster.Color, Colors.FloorBGFov, monster.Symbol);
            }
            else
            {
                console.Set(monster.X, monster.Y, Colors.Floor, Colors.FloorBG, '.');
            }
        }
    }
}
