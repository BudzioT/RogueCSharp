﻿using System;
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
    }
}
