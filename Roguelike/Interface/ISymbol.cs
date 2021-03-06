﻿using RLNET;
using RogueSharp;

namespace Roguelike.Interfaces
{
    internal interface ISymbol
    {
        RLColor Color { get; set; }
        char Symbol { get; set; }
        int X { get; set; }
        int Y { get; set; }

        void Draw(RLConsole console, IMap map);
    }
}