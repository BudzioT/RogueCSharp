using OpenTK.Graphics.ES11;
using RogueGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Systems
{
    // Command manager
    public class CommandSystem
    {
        public bool MovePlayer(Direction direction)
        {
            // Copy player's position
            int posX = Game.Player.X;
            int posY = Game.Player.Y;

            // Move him based of direction
            switch (direction)
            {
                // Move left
                case Direction.Left:
                    posX -= 1;
                    break;
                // Move right
                case Direction.Right:
                    posX += 1;
                    break;
                // Move up
                case Direction.Up:
                    posY -= 1;
                    break;
                // Move down
                case Direction.Down:
                    posY += 1;
                    break;
                default:
                    return false;
            }

            // Try to move the player into new position
            if (Game.DungeonMap.SetActorPosition(Game.Player, posX, posY)) 
                return true;

            // Try to get monster at player's cell
            Monster monster = Game.DungeonMap.GetMonsterAt(posX, posY);
            // If there is one, make the player attack it
            if (monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }
            return false;
        }

        // Combat system
        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMsg = new StringBuilder();
            StringBuilder defMsg = new StringBuilder();

            
        }

        // Attack action 
        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder msg)
        {
            int hits = 0;
        }
    }
}
