using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueGame.Core;
using RogueSharp.DiceNotation;


namespace RogueGame.Monsters
{
    // KEnemy, a simple, most basic type of enemy
    public class KEnemy : Monster
    {
        // Create the KEnemy
        public static KEnemy Create(int level)
        {
            // Roll the amount of health and max health
            int health = Dice.Roll("2D5");

            // Create KEnemy with random stats, that increase based off current level
            return new KEnemy
            {
                Awareness = 10,
                Name = "KEnemy",

                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),

                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),

                Health = health,
                MaxHealth = health,

                Speed = 14,
                Gold = Dice.Roll("5D5"),

                Color = Colors.KEnemy,
                Symbol = 'K'
            };
        }
    }
}
