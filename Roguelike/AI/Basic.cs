using Roguelike.Core;
using Roguelike.Interfaces;
using Roguelike.Systems;
using RogueLike;
using RogueSharp;
using System.Linq;

namespace Roguelike.Behaviors
{
    public class Basic : IAI
    {
        public bool Act(Monster monster, Commands command)
        {
            DungeonMap DMap = Game.DMap;
            Player player = Game.Player;
            FieldOfView mobFov = new FieldOfView(DMap);

            if (!monster.TurnsAlerted.HasValue)
            {
                mobFov.ComputeFov(monster.X, monster.Y, monster.FOVValue, true);
                if (mobFov.IsInFov(player.X, player.Y))
                {
                    Game.Log.Add($"{monster.Name} zauwaza {player.Name}");
                    monster.TurnsAlerted = 1;
                }
            }
            if (monster.TurnsAlerted.HasValue)
            {
                DMap.SetIsWalkable(monster.X, monster.Y, true);
                DMap.SetIsWalkable(player.X, player.Y, true);
                PathFinder pathFinder = new PathFinder(DMap);
                Path path = null;
                try
                {
                    path = pathFinder.ShortestPath(
                    DMap.GetCell(monster.X, monster.Y),
                    DMap.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {
                    Game.Log.Add($"{monster.Name} czeka na okazje do ataku");
                }
                DMap.SetIsWalkable(monster.X, monster.Y, false);
                DMap.SetIsWalkable(player.X, player.Y, false);
                if (path != null)
                {
                    try
                    {
                        command.MoveMob(monster, path.Steps.First());
                    }
                    catch (NoMoreStepsException)
                    {
                        Game.Log.Add($"{monster.Name} szuka drogi");
                    }
                }

                monster.TurnsAlerted++;

                if (monster.TurnsAlerted > 15)
                {
                    monster.TurnsAlerted = null;
                }
            }
            return true;
        }
    }
}