using Roguelike.Core;
using Roguelike.Interfaces;
using Roguelike.Pickups;
using RogueLike;
using RogueSharp;
using RogueSharp.DiceNotation;
using System.Text;

namespace Roguelike.Systems
{
    public class Commands
    {
        public bool MovePlayer(Direction direction)
        {
            int x = Player.GetInstance().X;
            int y = Player.GetInstance().Y;

            switch (direction)
            {
                case Direction.Up:
                    {
                        y = Player.GetInstance().Y - 1;
                        break;
                    }
                case Direction.Down:
                    {
                        y = Player.GetInstance().Y + 1;
                        break;
                    }
                case Direction.Left:
                    {
                        x = Player.GetInstance().X - 1;
                        break;
                    }
                case Direction.Right:
                    {
                        x = Player.GetInstance().X + 1;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }

            Pickup pickup = Game.DMap.GetPickupAt(x, y);
            if (Game.DMap.SetActorPosition(Player.GetInstance(), x, y))
            {
                if (pickup != null)
                {
                    pickup.OnPickup();
                    Game.DMap.DeletePickup(x,y);
                }
                return true;
            }
            Monster monster = Game.DMap.GetMobAt(x, y);

            if (monster != null)
            {
                Attack(Player.GetInstance(), monster);
                return true;
            }
            return false;
        }

        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder atkLog = new StringBuilder();
            StringBuilder defLog = new StringBuilder();
            int hits = CountAtk(attacker, defender, atkLog);
            int blocks = CountDef(defender, hits, atkLog, defLog);
            Game.Log.Add(atkLog.ToString());
            if (!string.IsNullOrWhiteSpace(defLog.ToString()))
            {
                Game.Log.Add(defLog.ToString());
            }
            int dmg = hits - blocks;

            CountDmg(defender, dmg);
        }

        private static int CountAtk(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;
            DiceExpression atkRoll = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult atk = atkRoll.Roll();

            foreach (TermResult termResult in atk.Results)
            {
                if (termResult.Value >= 100 - attacker.AtkChance)
                {
                    hits++;
                }
            }

            return hits;
        }

        private static int CountDef(Actor defender, int hits, StringBuilder atkLog, StringBuilder defLog)
        {
            int blocks = 0;

            if (hits > 0)
            {
                DiceExpression defRoll = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult def = defRoll.Roll();

                foreach (TermResult termResult in def.Results)
                {
                    if (termResult.Value >= 100 - defender.DefChance)
                    {
                        blocks++;
                    }
                }
            }
            else
            {
            }

            return blocks;
        }

        private static void CountDmg(Actor defender, int dmg)
        {
            if (dmg > 0)
            {
                defender.Health = defender.Health - dmg;

                Game.Log.Add($"{defender.Name} otrzymuje {dmg} obrazen");

                if (defender.Health <= 0)
                {
                    Death(defender);
                }
            }
            else
            {
                Game.Log.Add($"{defender.Name} nie otrzymuje obrazen");
            }
        }

        private static void Death(Actor defender)
        {
            if (defender is Player)
            {
                Game.Log.Add($"{defender.Name} ginie! Koniec gry!");
                Game._gameOver = true;
            }
            else if (defender is Monster)
            {
                Game.DMap.DeleteMonster((Monster)defender);

                Game.Log.Add($"{defender.Name} ginie i wyrzuca {defender.Gold} zlota");
                if (defender.Gems > 0) Game.Log.Add($" oraz {defender.Gems} klejnotow");
                Player.GetInstance().Gold += defender.Gold;
                Player.GetInstance().Gems += defender.Gems;
                Player.GetInstance().Experience += defender.ExpValue;
                if (Player.GetInstance().Experience >= Player.GetInstance().ExpToLevel) { Player.GetInstance().LevelUp(); }
            }
        }

        public bool IsPlayerTurn { get; set; }

        public void EndPlayerTurn()
        {
            IsPlayerTurn = false;
        }

        public void ActivateMobs()
        {
            ITurnQueue turnQueue = Game.TurnQueue.Get();
            if (turnQueue is Player)
            {
                IsPlayerTurn = true;
                Game.TurnQueue.Add(Player.GetInstance());
            }
            else
            {
                Monster monster = turnQueue as Monster;

                if (monster != null)
                {
                    monster.PerformAction(this);
                    Game.TurnQueue.Add(monster);
                }

                ActivateMobs();
            }
        }

        public void MoveMob(Monster monster, Cell cell)
        {
            if (!Game.DMap.SetActorPosition(monster, cell.X, cell.Y))
            {
                if (Player.GetInstance().X == cell.X && Player.GetInstance().Y == cell.Y)
                {
                    Attack(monster, Player.GetInstance());
                }
            }
        }
    }
}