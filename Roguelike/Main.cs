using RLNET;
using Roguelike.Core;
using Roguelike.Systems;
using RogueSharp.Random;
using System;
using System.Media;

namespace RogueLike
{
    public static class Game
    {
        public static void GameOver()
        {
            //niewazne
        }

        public static bool DanteMustDie = false;
        public static bool KoboldKarnage = false;
        public static Log Log { get; private set; }
        public static SoundPlayer soundplayer = new SoundPlayer();
        public static TurnQueue TurnQueue { get; private set; }
        public static IRandom Random { get; private set; }
        private static bool _renderChange = true;
        public static Commands Commands { get; private set; }
        public static Player Player { get; set; }
        public static DungeonMap DMap { get; set; }
        public static int _mapLevel = 1;
        public static int regen { get; set; }
        public static bool _gameOver = false;
        public static bool _start = true;
        public static RLConsole _startConsole;
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static RLRootConsole _mainConsole;
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static RLConsole _mapConsole;
        private static readonly int _logWidth = 80;
        private static readonly int _logHeight = 11;
        public static RLConsole _logConsole;
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;
        private static readonly int _lootWidth = 80;
        private static readonly int _lootHeight = 11;
        private static RLConsole _lootConsole;
        private static RLConsole _gameOverConsole;

        public static void Main()
        {
            TurnQueue = new TurnQueue();
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);
            Commands = new Commands();
            MapGen mapGen = new MapGen(_mapWidth, _mapHeight, 50, 20, 5, _mapLevel);
            DMap = mapGen.GenerateMap();
            DMap.UpdatePlayerFOV();
            string bitmap = "terminal8x8.png";
            string Title = $"RPG - Poziom {_mapLevel}";
            _mainConsole = new RLRootConsole(bitmap, _screenWidth, _screenHeight, 8, 8, 1f, Title);
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _logConsole = new RLConsole(_logWidth, _logHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _lootConsole = new RLConsole(_lootWidth, _lootHeight);
            _mainConsole.Title = $"RPG - Poziom {_mapLevel}";
            _mainConsole.Update += OnRootConsoleUpdate;
            _mainConsole.Render += OnRootConsoleRender;
            _mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBG);
            _mapConsole.Print(1, 1, "Mapa", Colors.TextH);
            Log = new Log();
            Log.Add("Poziom 1");
            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Palette.Red2);
            _statConsole.Print(1, 1, "Statystyki", Colors.TextH);

            _lootConsole.SetBackColor(0, 0, _lootWidth, _lootHeight, Palette.Wood);
            _lootConsole.Print(1, 1, "Ekwipunek", Colors.TextH);
            soundplayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\muzyka.wav";
            soundplayer.PlayLooping();
            _mainConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool playerAct = false;
            RLKeyPress keyPress = _mainConsole.Keyboard.GetKeyPress();

            if (Commands.IsPlayerTurn)
            {
                if (keyPress != null)
                {
                    if (keyPress.Key == RLKey.Number1)
                    {
                        Game.Player.Attack += 1;
                        _renderChange = true;
                    }

                    if (keyPress.Key == RLKey.Number2)
                    {
                        Game.Player.Defense += 1;
                        _renderChange = true;
                    }

                    if (keyPress.Key == RLKey.K)
                    {
                        KoboldKarnage = true;
                        DanteMustDie = false;
                        MapGen mapGen = new MapGen(_mapWidth, _mapHeight, 50, 10, 5, _mapLevel);
                        DMap = mapGen.GenerateMap();
                        Log = new Log();
                        Commands = new Commands();
                        _mainConsole.Title = $"RPG - Poziom {_mapLevel}";
                        playerAct = true;
                    }
                    if (keyPress.Key == RLKey.D)
                    {
                        DanteMustDie = true;
                        KoboldKarnage = false;
                        MapGen mapGen = new MapGen(_mapWidth, _mapHeight, 50, 10, 5, _mapLevel);
                        DMap = mapGen.GenerateMap();
                        Log = new Log();
                        Commands = new Commands();
                        _mainConsole.Title = $"RPG - Poziom {_mapLevel}";
                        playerAct = true;
                    }
                    if (keyPress.Key == RLKey.Up)
                    {
                        playerAct = Commands.MovePlayer(Direction.Up);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Down)
                    {
                        playerAct = Commands.MovePlayer(Direction.Down);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Left)
                    {
                        playerAct = Commands.MovePlayer(Direction.Left);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Right)
                    {
                        playerAct = Commands.MovePlayer(Direction.Right);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Escape)
                    {
                        _mainConsole.Close();
                    }
                    else if (keyPress.Key == RLKey.Period)
                    {
                        if (DMap.CanGoDownStairs())
                        {
                            MapGen mapGen = new MapGen(_mapWidth, _mapHeight, 50, 20, 5, ++_mapLevel);
                            DMap = mapGen.GenerateMap();
                            Log = new Log();
                            Commands = new Commands();
                            _mainConsole.Title = $"RPG - Poziom {_mapLevel}";
                            playerAct = true;
                        }
                    }
                }

                if (playerAct)
                {
                    if (regen > 7 && Game.Player.Health < Game.Player.MaxHealth) { Game.Player.Health++; regen = 0; }
                    _renderChange = true;
                    Commands.EndPlayerTurn();
                }
            }
            else
            {
                Commands.ActivateMobs();
                _renderChange = true;
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (_start)
            {
                RLKeyPress keyPress = _mainConsole.Keyboard.GetKeyPress();
                if (keyPress != null) { _start = false; _renderChange = true; }
                _startConsole = new RLConsole(_screenWidth, _screenHeight);
                _startConsole.SetBackColor(0, 0, _screenWidth, _screenHeight, Colors.FloorBGFov);
                _startConsole.Print(22, 28, "R            R", Colors.MenuColor);
                _startConsole.Print(22, 27, "R           R", Colors.MenuColor);
                _startConsole.Print(22, 26, "R          R", Colors.MenuColor);
                _startConsole.Print(22, 25, "R         R", Colors.MenuColor);
                _startConsole.Print(22, 24, "R        R", Colors.MenuColor);
                _startConsole.Print(22, 23, "R       R", Colors.MenuColor);
                _startConsole.Print(22, 22, "R      R", Colors.MenuColor);
                _startConsole.Print(22, 21, "R     R", Colors.MenuColor);
                _startConsole.Print(22, 20, "R    R", Colors.MenuColor);
                _startConsole.Print(22, 19, "R   R", Colors.MenuColor);
                _startConsole.Print(22, 18, "R  R", Colors.MenuColor);
                _startConsole.Print(22, 17, "RRR", Colors.MenuColor);
                _startConsole.Print(22, 16, "RR", Colors.MenuColor);
                _startConsole.Print(22, 15, "RRRRRRRRRRRRR", Colors.MenuColor);
                _startConsole.Print(22, 14, "RR          RR", Colors.MenuColor);
                _startConsole.Print(22, 13, "R            RR", Colors.MenuColor);
                _startConsole.Print(22, 12, "R             R", Colors.MenuColor);
                _startConsole.Print(22, 11, "R             R", Colors.MenuColor);
                _startConsole.Print(22, 10, "R             R", Colors.MenuColor);
                _startConsole.Print(22, 9, "R            RR", Colors.MenuColor);
                _startConsole.Print(22, 8, "RR          RR", Colors.MenuColor);
                _startConsole.Print(22, 7, "RRRRRRRRRRRRR", Colors.MenuColor);

                _startConsole.Print(42, 28, "P", Colors.MenuColor);
                _startConsole.Print(42, 27, "P", Colors.MenuColor);
                _startConsole.Print(42, 26, "P", Colors.MenuColor);
                _startConsole.Print(42, 25, "P", Colors.MenuColor);
                _startConsole.Print(42, 24, "P", Colors.MenuColor);
                _startConsole.Print(42, 23, "P", Colors.MenuColor);
                _startConsole.Print(42, 22, "P", Colors.MenuColor);
                _startConsole.Print(42, 21, "P", Colors.MenuColor);
                _startConsole.Print(42, 20, "P", Colors.MenuColor);
                _startConsole.Print(42, 19, "P", Colors.MenuColor);
                _startConsole.Print(42, 18, "P", Colors.MenuColor);
                _startConsole.Print(42, 17, "P", Colors.MenuColor);
                _startConsole.Print(42, 16, "P", Colors.MenuColor);
                _startConsole.Print(42, 15, "PPPPPPPPPPPPP", Colors.MenuColor);
                _startConsole.Print(42, 14, "PP          PP", Colors.MenuColor);
                _startConsole.Print(42, 13, "P            PP", Colors.MenuColor);
                _startConsole.Print(42, 12, "P             P", Colors.MenuColor);
                _startConsole.Print(42, 11, "P             P", Colors.MenuColor);
                _startConsole.Print(42, 10, "P             P", Colors.MenuColor);
                _startConsole.Print(42, 9, "P            PP", Colors.MenuColor);
                _startConsole.Print(42, 8, "PP          PP", Colors.MenuColor);
                _startConsole.Print(42, 7, "PPPPPPPPPPPPP", Colors.MenuColor);

                _startConsole.Print(62, 28, "GGGGGGGGGGGGGGG", Colors.MenuColor);
                _startConsole.Print(62, 27, "GG           GG", Colors.MenuColor);
                _startConsole.Print(62, 26, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 25, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 24, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 23, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 22, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 21, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 20, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 19, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 18, "G            GG", Colors.MenuColor);
                _startConsole.Print(62, 17, "G      GGGGGGGG", Colors.MenuColor);
                _startConsole.Print(62, 16, "G", Colors.MenuColor);
                _startConsole.Print(62, 15, "G", Colors.MenuColor);
                _startConsole.Print(62, 14, "G", Colors.MenuColor);
                _startConsole.Print(62, 13, "G", Colors.MenuColor);
                _startConsole.Print(62, 12, "G", Colors.MenuColor);
                _startConsole.Print(62, 11, "G", Colors.MenuColor);
                _startConsole.Print(62, 10, "G", Colors.MenuColor);
                _startConsole.Print(62, 9, "G             G", Colors.MenuColor);
                _startConsole.Print(62, 8, "GG           GG", Colors.MenuColor);
                _startConsole.Print(62, 7, "GGGGGGGGGGGGGGG", Colors.MenuColor);

                _startConsole.Print(36, 40, "Autor: Adam Bartulewicz", Colors.Gold);
                _startConsole.Print(40, 44, "Klawiszologia:", Colors.Gold);
                _startConsole.Print(30, 48, "Poruszanie sie i atakowanie wrogow", Colors.Text);
                _startConsole.Print(32, 50, "odbywa sie za pomoca strzalek", Colors.Text);
                _startConsole.Print(34, 52, "By zejsc po schodach ( ", Colors.Text);
                _startConsole.Print(57, 52, ">", Colors.GameOverColor);
                _startConsole.Print(58, 52, " )", Colors.Text);
                _startConsole.Print(21, 54, "nalezy stojac na nich nacisnac symbol je oznaczajacy ", Colors.Text);
                _startConsole.Print(74, 54, ">", Colors.GameOverColor);
                _startConsole.Print(40, 56, "@", Colors.Player);
                _startConsole.Print(42, 56, "- Gracz", Colors.Text);

                _startConsole.Print(36, 58, "g", Colors.KoboldColor);
                _startConsole.Print(38, 58, "k", Colors.KoboldColor);
                _startConsole.Print(40, 58, "o", Colors.OrcColor);
                _startConsole.Print(42, 58, "- slabi wrogowie", Colors.Text);
                _startConsole.Print(38, 60, "G", Colors.KoboldColor);
                _startConsole.Print(40, 60, "B", Colors.BeholderColor);
                _startConsole.Print(42, 60, "- silni wrogowie", Colors.Text);
                _startConsole.Print(18, 63, "ZBIERZ JAK NAJWIECEJ ZLOTA PRZED SWOJA NIEUCHRONNA SMIERCIA", Colors.Gold);
                _startConsole.Print(26, 65, "NACISNIJ DOWOLNA STRZALKE ABY ZACZAC PRZYGODE", Colors.Gold);

                RLConsole.Blit(_startConsole, 0, 0, _screenWidth, _screenHeight, _mainConsole, 0, 0);
                _mainConsole.Draw();
            }
            if (_gameOver)
            {
                _mainConsole.Update -= OnRootConsoleUpdate;
                _gameOverConsole = new RLConsole(_mapWidth, _mapHeight);
                _gameOverConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBG);
                _gameOverConsole.Print(36, 24, "KONIEC GRY", Colors.GameOverColor);
                _gameOverConsole.Print(29, 26, $"TWOJ WYNIK KONCOWY TO {Player.Gold + (Player.Gems * 100)}", Colors.Gold);
                _gameOverConsole.Print(28, 30, "WCISNIJ ESC ABY WYJSC Z GRY", Colors.GameOverColor);
                RLConsole.Blit(_gameOverConsole, 0, 0, _mapWidth, _mapHeight, _mainConsole, 0, _lootHeight);
                _mainConsole.Draw();
                _renderChange = false;
                RLKeyPress keyPress = _mainConsole.Keyboard.GetKeyPress();
                if (keyPress != null)
                {
                    if (keyPress.Key == RLKey.Escape) _mainConsole.Close();
                }
            }

            if (_renderChange)
            {
                DMap.Draw(_mapConsole);
                Player.Draw(_mapConsole, DMap);
                Player.DrawStats(_statConsole);
                Player.DrawLoot(_lootConsole);
                Log.Draw(_logConsole);
                RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _mainConsole, 0, _lootHeight);
                RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _mainConsole, _mapWidth, 0);
                RLConsole.Blit(_logConsole, 0, 0, _logWidth, _logHeight, _mainConsole, 0, _screenHeight - _logHeight);
                RLConsole.Blit(_lootConsole, 0, 0, _lootWidth, _lootHeight, _mainConsole, 0, 0);
                _mainConsole.Draw();
                _renderChange = false;
            }
        }
    }
}