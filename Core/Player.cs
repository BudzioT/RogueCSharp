using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Core
{
    // Class representing player
    public class Player : Actor
    {
        // Initialize the player
        public Player() 
        {
            // Set player stats
            Attack = 2;
            AttackChance = 40;
            Defense = 2;
            DefenseChance = 40;
            Health = 100;
            MaxHealth = 100;
            Speed = 10;
            Gold = 0;
            // Set awareness to 15 cells
            Awareness = 15;
            // Set the name
            Name = "Player";
            // Set variables to help draw the player
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }

        // Print player's statistics
        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:     {Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:   {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:   {Attack} ({AttackChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense:  {Defense} ({DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Gold:     {Gold}", Colors.Gold);
        }
    }
}
