using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueGame.Core;
using RogueGame.Systems;


namespace RogueGame.Interfaces
{
    // Interface representing actor's behaviour
    public interface IBehaviour
    {
        // Make the monster act
        bool Act(Monster monster, CommandSystem cmdSystem);
    }
}
