using Roguelike.Core;
using Roguelike.Monsters;
using RogueLike;
using RogueSharp;
using RogueSharp.DiceNotation;
using System;
using System.Linq;
using Roguelike.MonsterDecorator;
using Roguelike.Iterators;
using Roguelike.Pickups;
using Roguelike.MonsterFactories;
using System.Collections.Generic;

namespace Roguelike.Systems
{
    public class MapGen
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;
        public RoomIterator iterator;
        private List<MonsterFactory> factoryList;

        private readonly DungeonMap _map;

        public MapGen(int width, int height,
        int maxRooms, int roomMaxSize, int roomMinSize, int mapLevel, List<MonsterFactory> list)
        {
            factoryList = list;
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new DungeonMap();
            iterator = _map.CreateIterator();
        }

        private void PlaceMobs()
        {
            for(Rectangle room = iterator.First(); !iterator.IsDone; room = iterator.Next())
            //foreach (var room in _map.Rooms)
            {
                Point randomPickupPoint = _map.GetRandomFreeTile(room);
                if (randomPickupPoint != null)
                {
                    GoldCoin pot = new GoldCoin();
                    pot.X = randomPickupPoint.X;
                    pot.Y = randomPickupPoint.Y;
                    _map.AddPickup(pot);
                }
                if (Dice.Roll("1D10") < 7)
                {
                    var numberOfMonsters = Dice.Roll("1D4");
                    if (Game.DanteMustDie) numberOfMonsters = 7;
                    else if (Game.KoboldKarnage) numberOfMonsters = room.Height * room.Width - 10;
                    if (numberOfMonsters < 1) numberOfMonsters = 1;
                    for (int i = 0; i < numberOfMonsters; i++)
                    {
                        CreationType type;
                        var typeDecider = Dice.Roll("1D100");
                        if (typeDecider <= 60)
                        {
                            type = CreationType.NORMAL;
                        }
                        else if(typeDecider<=80)
                        {
                            type = CreationType.WEAK;
                        }
                        else if(typeDecider<=92)
                        {
                            type = CreationType.MINI_BOSS;
                        }
                        else
                        {
                            type = CreationType.BOSS;
                        }



                        Point randomRoomLocation = _map.GetRandomFreeTile(room);
                        if (randomRoomLocation != null)
                        {
                            int level = Game.Player.Level + Game._mapLevel + Dice.Roll("1D5") - Dice.Roll("1D5");
                            if (level < 1) level = 1;
                            int whatMonster = Dice.Roll("1D100");
                            if (Game.KoboldKarnage) whatMonster = 1;
                            if (whatMonster <= 25)
                            {
                                //var monster = Kobold.Create(level);
                                //var monster1 = new Kobold();
                                //monster1.SetAttributes(level);
                                Monster monster = factoryList[0].Create(type);
                                monster.SetAttributes(level);
                                //Kobold monster = new Kobold();
                                monster.X = randomRoomLocation.X;
                                monster.Y = randomRoomLocation.Y;
                                _map.AddMonster(monster);
                            }
                            else if (whatMonster <= 50)
                            {
                                Monster monster = factoryList[1].Create(type);
                                monster.SetAttributes(level);
                                monster.X = randomRoomLocation.X;
                                monster.Y = randomRoomLocation.Y;
                                _map.AddMonster(monster);
                            }
                            else if (whatMonster <= 90)
                            {
                                Monster monster = factoryList[2].Create(type);
                                monster.SetAttributes(level);
                                monster.X = randomRoomLocation.X;
                                monster.Y = randomRoomLocation.Y;
                                _map.AddMonster(monster);
                            }
                            else if (whatMonster <= 95)
                            {
                                Monster monster = factoryList[3].Create(type);
                                monster.SetAttributes(level);
                                monster.X = randomRoomLocation.X;
                                monster.Y = randomRoomLocation.Y;
                                _map.AddMonster(monster);
                            }
                            else if (whatMonster <= 100)
                            {
                                Monster monster = factoryList[4].Create(type);
                                monster.SetAttributes(level);
                                monster.X = randomRoomLocation.X;
                                monster.Y = randomRoomLocation.Y;
                                _map.AddMonster(monster);
                            }
                        }
                    }
                }
            }
        }

        private void PlacePlayer()
        {
            Player player = Player.GetInstance();
            if (player == null)
            {
                player = Player.GetInstance();
            }
            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;
            _map.AddPlayer(player);
        }

        private void CreateXTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }

        private void CreateYTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }

        public DungeonMap GenerateMap()
        {
            _map.Initialize(_width, _height);

            for (int r = _maxRooms; r > 0; r--)
            {
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);
                var newRoom = new Rectangle(roomXPosition, roomYPosition,
                  roomWidth, roomHeight);
                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));
                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);
                }
            }
            foreach (Rectangle room in _map.Rooms)
            {
                GenerateRoom(room);
            }
            for (int r = 1; r < _map.Rooms.Count; r++)
            {
                int prevRoomCenterX = _map.Rooms[r - 1].Center.X;
                int prevRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int thisRoomCenterX = _map.Rooms[r].Center.X;
                int thisRoomCenterY = _map.Rooms[r].Center.Y;
                if (Game.Random.Next(1, 2) == 1)
                {
                    CreateXTunnel(prevRoomCenterX, thisRoomCenterX, prevRoomCenterY);
                    CreateYTunnel(prevRoomCenterY, thisRoomCenterY, thisRoomCenterX);
                }
                else
                {
                    CreateYTunnel(prevRoomCenterY, thisRoomCenterY, prevRoomCenterX);
                    CreateXTunnel(prevRoomCenterX, thisRoomCenterX, thisRoomCenterY);
                }
            }
            GenerateStairs();
            PlacePlayer();
            PlaceMobs();
            return _map;
        }

        private void GenerateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    _map.SetCellProperties(x, y, true, true, true);
                }
            }
        }

        private void GenerateStairs()
        {
            _map.StairsUp = new Stairs
            {
                X = _map.Rooms.First().Center.X + 1,
                Y = _map.Rooms.First().Center.Y,
                IsUp = true
            };
            _map.StairsDown = new Stairs
            {
                X = _map.Rooms.Last().Center.X,
                Y = _map.Rooms.Last().Center.Y,
                IsUp = false
            };
        }
    }
}