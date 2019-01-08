using Roguelike.Core;
using Roguelike.Systems;

namespace Roguelike.Interfaces
{
    public interface IAI
    {
        bool Act(Monster monster, Commands commandSystem);
    }
}