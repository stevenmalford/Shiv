using RogueSharp;
using Shiv.Interfaces;
using Shiv.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiv.Core.Behaviors
{
    public class StandardMoveAndAttack : IBehavior
    {
        public bool Act(Monster monster, Commands commandSystem)
        {
            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            if(!monster.TurnsAlerted.HasValue)
            {
                monsterFov.ComputeFov(monster.X, monster.Y, monster.Fov, true);
                if(monsterFov.IsInFov(player.X, player.Y))
                {
                    monster.TurnsAlerted = 1;
                }
            }

            if(monster.TurnsAlerted.HasValue)
            {
                dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
                dungeonMap.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(dungeonMap);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(dungeonMap.GetCell(monster.X, monster.Y), dungeonMap.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {
                    
                }

                dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
                dungeonMap.SetIsWalkable(player.X, player.Y, false);

                if(path != null)
                {
                    try
                    {
                        commandSystem.MoveMonster(monster, path.Steps.First());
                    }
                    catch(NoMoreStepsException)
                    {

                    }
                }

                monster.TurnsAlerted++;

                if(monster.TurnsAlerted > 15)
                {
                    monster.TurnsAlerted = null;
                }
            }

            return true;
        }
    }
}
