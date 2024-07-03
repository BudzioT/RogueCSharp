using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueGame.Core
{
    // Monster class
    public class Monster : Actor
    {
        public void DrawStats(RLConsole statConsole, int position)
        {
            // Start drawing below the player stats, create a margin between stats
            int posY = 13 + (position * 2);
            statConsole.Print(1, posY, Symbol.ToString(), Color);

            // Calculate width of the health bar
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int widthLeft = 16 - width;

            // Draw health of the monster
            statConsole.SetBackColor(3, posY, width, 1, Swatch.Primary[2]);
            // Draw background of the health, to see how much he has left
            statConsole.SetBackColor(3 + width, posY, widthLeft, 1, Swatch.Primary[4]);

            // Draw its name before the healthbar
            statConsole.Print(2, posY, $": {Name}", Swatch.Light);
        }
    }
}
