using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Interfaces
{
    // Interface representing Actor
    public interface IActor
    {
        // Name
        string Name { get; set; }
        // Awarness for calculating FOV
        int Awareness { get; set; }
        // Attack variables
        int Attack { get; set; }
        int AttackChance { get; set; }
        // Defense variables
        int Defense { get; set; }
        int DefenseChance { get; set; }
        // Health variables
        int Health { get; set; }
        int MaxHealth { get; set; }
        // Speed of an actor
        int Speed { get; set; }
        // Amount of gold
        int Gold { get; set; }
    }
}
