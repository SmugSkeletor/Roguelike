using RLNET;
using Roguelike.Core;
using Roguelike.Systems;
using RogueSharp.Random;
using System;
using System.Media;
using Roguelike.MonsterFactories;
using System.Collections.Generic;
using Roguelike.Sys;


namespace RogueLike
{
    public static class Game
    {
        public static void GameOver()
        {
            //niewazne
        }
        public static Facade facade = new Facade();
        public static bool DanteMustDie = false;
        public static bool KoboldKarnage = false;
        public static SoundPlayer soundplayer = new SoundPlayer();
        public static TurnQueue TurnQueue { get; private set; }
        public static IRandom Random { get; private set; }
        public static bool _renderChange = true;

        public static int regen { get; set; }
        public static bool _gameOver = false;
        public static bool _start = true;
        public static RLConsole _startConsole;
        public static readonly int _screenWidth = 100;
        public static readonly int _screenHeight = 70;
        public static RLRootConsole _mainConsole;
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
            string bitmap = "terminal8x8.png";
            string Title = $"RPG - Poziom {Facade._mapLevel}";
            _mainConsole = new RLRootConsole(bitmap, _screenWidth, _screenHeight, 8, 8, 1f, Title);
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _logConsole = new RLConsole(_logWidth, _logHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _lootConsole = new RLConsole(_lootWidth, _lootHeight);
            _mainConsole.Update += OnRootConsoleUpdate;
            _mainConsole.Render += OnRootConsoleRender;
            _mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBG);
            _mapConsole.Print(1, 1, "Mapa", Colors.TextH);
            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Palette.Red2);
            _statConsole.Print(1, 1, "Statystyki", Colors.TextH);
            _lootConsole.SetBackColor(0, 0, _lootWidth, _lootHeight, Palette.Wood);
            _lootConsole.Print(1, 1, "Ekwipunek", Colors.TextH);
            soundplayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\muzyka.wav";
            soundplayer.PlayLooping();
            facade.InitGame();
            _mainConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool playerAct = false;
            RLKeyPress keyPress = _mainConsole.Keyboard.GetKeyPress();

            if (Facade.Commands.IsPlayerTurn)
            {
                if (keyPress != null)
                {
                    if (keyPress.Key == RLKey.Number1)
                    {
                        Player.GetInstance().Attack += 1;
                        _renderChange = true;
                    }

                    if (keyPress.Key == RLKey.Number2)
                    {
                        Player.GetInstance().Defense += 1;
                        _renderChange = true;
                    }

                    if (keyPress.Key == RLKey.K)
                    {
                        KoboldKarnage = true;
                        DanteMustDie = false;
                        facade.InitGame();
                        playerAct = true;
                    }
                    if (keyPress.Key == RLKey.D)
                    {
                        DanteMustDie = true;
                        KoboldKarnage = false;
                        facade.InitGame();
                        playerAct = true;
                    }
                    if (keyPress.Key == RLKey.Up)
                    {
                        playerAct = Facade.Commands.MovePlayer(Direction.Up);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Down)
                    {
                        playerAct = Facade.Commands.MovePlayer(Direction.Down);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Left)
                    {
                        playerAct = Facade.Commands.MovePlayer(Direction.Left);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Right)
                    {
                        playerAct = Facade.Commands.MovePlayer(Direction.Right);
                        regen++;
                    }
                    else if (keyPress.Key == RLKey.Escape)
                    {
                        _mainConsole.Close();
                    }
                    else if (keyPress.Key == RLKey.Period)
                    {
                        if (Facade.DMap.CanGoDownStairs())
                        {
                            facade.InitGame();
                            playerAct = true;
                        }
                    }
                }

                if (playerAct)
                {
                    if (regen > 7 && Player.GetInstance().Health < Player.GetInstance().MaxHealth) { Player.GetInstance().Health++; regen = 0; }
                    _renderChange = true;
                    Facade.Commands.EndPlayerTurn();
                }
            }
            else
            {
                Facade.Commands.ActivateMobs();
                _renderChange = true;
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (_start)
            {
                Facade facade = new Facade();
                facade.ShowMenu();
            }
            if (_gameOver)
            {
                _mainConsole.Update -= OnRootConsoleUpdate;
                _gameOverConsole = new RLConsole(_mapWidth, _mapHeight);
                _gameOverConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBG);
                _gameOverConsole.Print(36, 24, "KONIEC GRY", Colors.GameOverColor);
                _gameOverConsole.Print(29, 26, $"TWOJ WYNIK KONCOWY TO {Player.GetInstance().Gold + (Player.GetInstance().Gems * 100)}", Colors.Gold);
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
                Facade.DMap.Draw(_mapConsole);
                Player.GetInstance().Draw(_mapConsole, Facade.DMap);
                Player.GetInstance().DrawStats(_statConsole);
                Player.GetInstance().DrawLoot(_lootConsole);
                Facade.Log.Draw(_logConsole);
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