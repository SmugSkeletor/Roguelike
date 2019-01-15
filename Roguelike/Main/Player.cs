using RLNET;
using RogueLike;
using Roguelike;
using RogueSharp.DiceNotation;
using Roguelike.Systems;

namespace Roguelike.Core
{
    public sealed class Player : Actor
    {
        private static Player instance = null;
        private Log log = new Log();
        private Player()
        {
            Level = 1;
            Experience = 0;
            ExpToLevel = 10;
            Attack = 3;
            AtkChance = 50;
            FOVValue = 15;
            Color = Colors.Player;
            Defense = 3; //klasa pancerza :^)
            DefChance = 40;
            Gold = 0;
            Gems = 0;
            MaxHealth = 50 + Dice.Roll("2D8");
            Health = MaxHealth;
            Name = "Gracz";
            Speed = 10;
            Symbol = '@';
        }

        public static Player GetInstance()
        {
            if (instance == null)
                instance = new Player();
            return instance;
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Poziom:    {Level}", Colors.Text);
            statConsole.Print(1, 3, $"Zycie:  {Health} / {MaxHealth} ", Colors.Text);
            statConsole.Print(1, 5, $"Atak:  {Attack} ({AtkChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Obrona: {Defense} ({DefChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Exp: {Experience}/{ExpToLevel} ", Colors.Text);
        }

        public void DrawLoot(RLConsole inventoryConsole)
        {
            inventoryConsole.Print(1, 3, $"Zloto: {Gold}", Colors.Gold);
            inventoryConsole.Print(1, 5, $"Klejnoty: {Gems}", Colors.BeholderColor);
        }

        public void LevelUp()
        {
            Level++;
            Experience = 0;
            ExpToLevel *= 2;
            Attack += Dice.Roll("1D2");
            Defense += Dice.Roll("1D2");
            MaxHealth += Dice.Roll("1D3");
            Health = MaxHealth;
            Game.Log.Add($"--------------------------------------");
            Game.Log.Add($"WKRACZASZ NA {Level} POZIOM DOSWIADCZENIA");
            Game.Log.Add($"--------------------------------------");
        }
    }
}