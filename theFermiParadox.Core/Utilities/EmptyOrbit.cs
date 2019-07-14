using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class EmptyOrbit : IOrbit
    {
        private Vector3 _position;

        ABody _mainBody;
        ABody _body;

        public EmptyOrbit() 
        {
            _mainBody = null;
            _body = null;
        }

        public Vector3 Position { get { return Vector3.Zero; } }

        public ABody MainBody { get => _mainBody; }
        public ABody Body { get => _body; }
    }
}
