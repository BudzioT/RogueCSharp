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
    }
}
