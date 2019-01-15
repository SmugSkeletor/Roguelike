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
            FieldOfView mobFov = new FieldOfView(DMap);

            if (!monster.TurnsAlerted.HasValue)
            {
                mobFov.ComputeFov(monster.X, monster.Y, monster.FOVValue, true);
                if (mobFov.IsInFov(Player.GetInstance().X, Player.GetInstance().Y))
                {
                    Game.Log.Add($"{monster.Name} zauwaza {Player.GetInstance().Name}");
                    monster.TurnsAlerted = 1;
                }
            }
            if (monster.TurnsAlerted.HasValue)
            {
                DMap.SetIsWalkable(monster.X, monster.Y, true);
                DMap.SetIsWalkable(Player.GetInstance().X, Player.GetInstance().Y, true);
                PathFinder pathFinder = new PathFinder(DMap);
                Path path = null;
                try
                {
                    path = pathFinder.ShortestPath(
                    DMap.GetCell(monster.X, monster.Y),
                    DMap.GetCell(Player.GetInstance().X, Player.GetInstance().Y));
                }
                catch (PathNotFoundException)
                {
                    Game.Log.Add($"{monster.Name} czeka na okazje do ataku");
                }
                DMap.SetIsWalkable(monster.X, monster.Y, false);
                DMap.SetIsWalkable(Player.GetInstance().X, Player.GetInstance().Y, false);
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