using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueGame.Interfaces;
using RogueSharp;


namespace RogueGame.Core
{
    public class Actor : IActor, IDrawable
    {
        // IActor variables
        private string _name;
        private int _awareness;
        private int _attack;
        private int _attackChance;
        private int _defense;
        private int _defenseChance;
        private int _health;
        private int _maxHealth;
        private int _speed;
        private int _gold;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public int Awareness
        {
            get
            {
                return _awareness;
            }

            set
            {
                _awareness = value;
            }
        }

        public int Attack
        {
            get
            {
                return _attack;
            }

            set
            {
                _attack = value;
            }
        }

        public int AttackChance
        {
            get
            {
                return _attackChance;
            }

            set
            {
                _attackChance = value;
            }
        }

        public int Defense
        {
            get
            {
                return _defense;
            }

            set
            {
                _defense = value;
            }
        }

        public int DefenseChance
        {
            get
            {
                return _defenseChance;
            }

            set
            {
                _defenseChance = value;
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                _health = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return _maxHealth;
            }

            set
            {
                _maxHealth = value;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }
        
        public int Gold
        {
            get
            {
                return _gold;
            }

            set
            {
                _gold = value;
            }
        }

        // IDrawable variables
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        // Draw the Actor
        public void Draw(RLConsole console, IMap map)
        {
            // Don't draw it, if cell isn't yet explored
            if (!map.GetCell(X, Y).IsExplored)
                return;

            // Draw the actor when in FOV
            if (map.IsInFov(X, Y))
                console.Set(X, Y, Color, Colors.FloorBgFov, Symbol);
            // When not in FOV, draw normal floor
            else
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
        }
    }
}
