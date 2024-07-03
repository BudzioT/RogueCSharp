using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;


// Field Of View classes
namespace RogueGame.Core
{
    public class FieldOfView : FieldOfView<Cell>
    {
        public FieldOfView(IMap map) : base(map)
        {

        }
    }

    public class FieldOfView<TCell> where TCell : ICell
    {
        // Map and FOV
        private readonly IMap _map;
        private readonly HashSet<int> _inFov;

        // Initialize FOV
        public FieldOfView(IMap map)
        {
            _map = map;
            _inFov = new HashSet<int>();
        }

        // Initialize FOV with Hash
        internal FieldOfView(IMap map, HashSet<int> inFov)
        {
            _map = map;
            _inFov = inFov;
        }
    }
}
