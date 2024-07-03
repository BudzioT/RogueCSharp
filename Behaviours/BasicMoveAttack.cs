using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueGame;
using RogueGame.Core;
using RogueGame.Interfaces;
using RogueGame.Systems;
using RogueSharp;


namespace RogueGame.Behaviours
{
    // Basic move and attack behaviours
    public class BasicMoveAttack : IBehaviour
    {
        // Act based off properties
        public bool Act(Monster monster, CommandSystem cmdSystem)
        {
            // Get references to things
            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;
            RogueSharp.FieldOfView monsterFov = new RogueSharp.FieldOfView(dungeonMap);

            // If monster isn't alert of the player
            if (!monster.Alerted.HasValue)
            {
                // Calculate its FOV
                monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
                // If monster sees the player now, alert him
                if (monsterFov.IsInFov(player.X, player.Y))
                {
                    Game.MessageLog.Add($"{monster.Name} is approaching {player.Name}");
                    monster.Alerted = 1;
                }
            }

            // If monster is alerted about player
            if (monster.Alerted.HasValue)
            {
                // Make player and monster cells walkable, to check for a path
                dungeonMap.SetIsWalkable(player.X, player.Y, true);
                dungeonMap.SetIsWalkable(monster.X, monster.Y, true);

                
                // Try to find the shortest path from the monster to the player
                PathFinder pathFinder = new PathFinder(dungeonMap);
                RogueSharp.Path path = null;
                try
                {
                    path = pathFinder.ShortestPath(
                        dungeonMap.GetCell(monster.X, monster.Y),
                        dungeonMap.GetCell(player.X, player.Y));
                }

                // If there isn't any path, don't do anything, just print a message
                catch (PathNotFoundException)
                {
                    Game.MessageLog.Add($"{monster.Name} is chilling");
                }

                // Make player and monster cells not walkable anymore
                dungeonMap.SetIsWalkable(player.X, player.Y, false);
                dungeonMap.SetIsWalkable(monster.X, monster.Y, false);

                // If there is a path between player and monster
                if (path != null)
                {
                    // Try to move in direction of the player
                    try
                    {
                        cmdSystem.MoveMonster(monster, (RogueSharp.Cell)path.Steps.First());
                    }
                    // If movement was unsuccesful, write a message
                    catch (NoMoreStepsException)
                    {
                        Game.MessageLog.Add($"{monster.Name} just forgot something");
                    }
                }
                // Increase alerted count
                monster.Alerted++;
                // If monster is alerted for more than 13 turns, make him stop
                if (monster.Alerted > 13)
                {
                    monster.Alerted = null;
                }
            }

            return true;
        }
    }
}
