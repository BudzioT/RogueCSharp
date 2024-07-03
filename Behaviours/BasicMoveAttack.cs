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
                // Make player and monster cells walkable
                dungeonMap.SetIsWalkable(player.X, player.Y, true);
                dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
            }

            return true;
        }
    }
}
