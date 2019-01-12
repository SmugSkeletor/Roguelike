using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Core;
using RogueSharp;

namespace Roguelike.Iterators
{
    interface Iterator
    {
        Rectangle First();
        Rectangle Next();
        bool IsDone { get; }
        Rectangle CurrentItem { get; }
    }
}
