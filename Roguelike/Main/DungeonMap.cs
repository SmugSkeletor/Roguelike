﻿using RLNET;
using RogueLike;
using RogueSharp;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Iterators;
using Roguelike.Pickups;

namespace Roguelike.Core
{
    public class DungeonMap : Map

    {
        private readonly List<Monster> _monsters;
        private readonly Dictionary<Point, Pickup> _pickups;
        public List<Rectangle> Rooms;
        public Stairs StairsUp { get; set; }
        public Stairs StairsDown { get; set; }
        public Rectangle this[int index]
        {
            get { return Rooms[index]; }
            set { Rooms.Insert(index, value); }
        }
        public int Count
        {
            get { return Rooms.Count; }
        }
        public RoomIterator CreateIterator()
        {
            return new RoomIterator(Rooms);
        }
        public DungeonMap()
        {
            Game.TurnQueue.Clear();
            Rooms = new List<Rectangle>();
            _monsters = new List<Monster>();
            _pickups = new Dictionary<Point, Pickup>();

        }

        public void AddPlayer(Player player)
        {
            SetIsWalkable(Player.GetInstance().X, Player.GetInstance().Y, false);
            UpdatePlayerFOV();
            Game.TurnQueue.Add(player);
        }

        public bool SetActorPosition(Actor actor, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                SetIsWalkable(actor.X, actor.Y, true);
                actor.X = x;
                actor.Y = y;
                SetIsWalkable(actor.X, actor.Y, false);
                if (actor is Player)
                {
                    UpdatePlayerFOV();
                }
                return true;
            }
            return false;
        }

        public bool CanGoDownStairs()
        {
            return StairsDown.X == Player.GetInstance().X && StairsDown.Y == Player.GetInstance().Y;
        }

        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
            SetIsWalkable(monster.X, monster.Y, false);
            Game.TurnQueue.Add(monster);
        }

        public void AddPickup(Point point,Pickup pickup)
        {
            _pickups.Add(point,pickup);
        }

        public void DeleteMonster(Monster monster)
        {
            _monsters.Remove(monster);
            SetIsWalkable(monster.X, monster.Y, true);
            Game.TurnQueue.Remove(monster);
        }

        public void DeletePickup(int x,int y)
        {
            Point point=null;
            foreach (KeyValuePair<Point, Pickup> entry in _pickups)
            {
                if (entry.Key.X == x && entry.Key.Y == y)
                {
                    point = entry.Key;
                }
            }
            if (point != null)
            {
                _pickups.Remove(point);
            }
        }

        public Monster GetMobAt(int x, int y)
        {
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        public Pickup GetPickupAt(int x, int y)
        {
            foreach (KeyValuePair<Point, Pickup> entry in _pickups)
            {
                if(entry.Key.X==x && entry.Key.Y==y)
                {
                    return entry.Value;
                }
            } 
            return null;
        }

        public Point GetRandomFreeTile(Rectangle room)
        {
            if (RoomFreeTile(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if (IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }
            return null;
        }

        public bool RoomFreeTile(Rectangle room)
        {
            for (int x = 1; x <= room.Width - 2; x++)
            {
                for (int y = 1; y <= room.Height - 2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        public void UpdatePlayerFOV()
        {
            ComputeFov(Player.GetInstance().X, Player.GetInstance().Y, Player.GetInstance().FOVValue, true);
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);
            }

            foreach (KeyValuePair<Point, Pickup> pickup in _pickups)
            {
                pickup.Value.Draw(mapConsole, this, pickup.Key);
            }
            StairsDown.Draw(mapConsole, this);
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            if (!cell.IsExplored)
            {
                return;
            }

            if (IsInFov(cell.X, cell.Y))
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBGFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBGFov, '#');
                }
            }
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBG, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBG, '#');
                }
            }
        }
    }
}