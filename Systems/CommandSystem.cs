using OpenTK.Graphics.ES11;
using RogueGame.Core;
using RogueGame.Interfaces;
using RogueSharp;
using RogueSharp.DiceNotation;
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
        // Player turn flag
        public bool PlayerTurn { get; set; }

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

            int hits = ResolveAttack(attacker, defender, attackMsg);
            int blocks = ResolveDefense(defender, hits, defMsg, attackMsg);

            Game.MessageLog.Add(attackMsg.ToString());
            if (!string.IsNullOrWhiteSpace(defMsg.ToString()) )
                Game.MessageLog.Add(defMsg.ToString());

            int dmg = hits - blocks;
            HandleDamage(defender, dmg);
        }

        // Attack action, return completed hits
        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder msg)
        {
            int hits = 0;
            
            // Prepare the message
            msg.AppendFormat("{0} hits {1} and gets a roll of", attacker.Name, defender.Name);

            // Roll a 100-sided dice based off attacker's attack
            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            // Go through each roll result
            foreach(TermResult termResult in attackResult.Results)
            {
                // Write the 
                msg.Append(", " + termResult.Value);
                // Add a hit, if the roll is higher than attack percent chance
                if (termResult.Value >= 100 - attacker.AttackChance) 
                {
                    ++hits;
                }
            }

            return hits;
        }

        // Defend action, return completed blocks
        private static int ResolveDefense(Actor defender, int hits, StringBuilder defMsg, StringBuilder atkMsg)
        {
            int blocks = 0;

            // If there was any hit, try to block it
            if (hits > 0)
            {
                // Update messages
                atkMsg.AppendFormat(" and hits {0} times.", hits);
                defMsg.AppendFormat("   {0} defends with a roll of", defender.Name);

                // Create defense dice based off defender's defense and roll it
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseResult = defenseDice.Roll();

                // Go through each result
                foreach (TermResult termResult in defenseResult.Results)
                {
                    // Update the message
                    defMsg.Append(", " + termResult.Value);
                    // If value is high enough, increase succesful blocks
                    if (termResult.Value >= 100 - defender.DefenseChance)
                        ++blocks;
                }
                // Update blocking message
                defMsg.AppendFormat(" blocking {0} times", blocks);
            }
            // If there were 0 hits, add a fail message
            else
            {
                atkMsg.Append("... and falls on the ground");
            }

            return blocks;
        }

        // Apply all the damage that wasn't block to the defender
        private static void HandleDamage(Actor defender, int dmg)
        {
            // If there was any damage, decrease defender's HP
            if (dmg > 0)
            {
                defender.Health = defender.Health - dmg;
                // Write message
                Game.MessageLog.Add($"  {defender.Name} got damaged by {dmg} points");
                
                // If defender's HP i equal to or below 0, handle his death
                if (defender.Health <= 0)
                    HandleDeath(defender);
            }
            // If defender blocked all hits, don't decrease his HP and write message
            else
                Game.MessageLog.Add($"  {defender.Name} turned invisible!");
        }

        // Handle actor's death
        private static void HandleDeath(Actor defender)
        {
            // If player died, write a message
            if (defender is Player)
                Game.MessageLog.Add($"  {defender.Name}... It's sad to watch...");
            // Otherwise, if monster died, remove it
            else if (defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster)defender);
                Game.MessageLog.Add($"  {defender.Name} was annihilated, dropped {defender.Gold} gold");
            }
        }

        // End the player's turn
        public void EndPlayerTurn()
        {
            PlayerTurn = false;
        }

        // Move the monster, attack if it is possible
        public void MoveMonster(Monster monster, ICell cell)
        {
            // If monster can't move anymore
            if (!Game.DungeonMap.SetActorPosition(monster, cell.X, cell.Y))
            {
                // Check if there is a player, if so, attack him
                if (Game.Player.X == cell.X && Game.Player.Y == cell.Y)
                    Attack(monster, Game.Player);
            }
        }

        // Activate the monster turn
        public void ActivateMonsters()
        {
            // Get the turn schedule
            IScheduable scheduable = Game.SchedulingSystem.Get();
            // If it's players turn, set it and add player to the schedule
            if (scheduable is Player)
            {
                PlayerTurn = true;
                Game.SchedulingSystem.Add(Game.Player);
            }
            // Otherwise perform monster action and add it to the schedule
            else
            {
                Monster monster = scheduable as Monster;
                if (monster != null)
                {
                    monster.PerformAction(this);
                    Game.SchedulingSystem.Add(monster);
                }
                // Deactivate monsters until player's turn is over
                ActivateMonsters();
            }
        }
    }
}
