using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core.Abstracts
{
    public abstract class ABody: APrintable, INode
    {
        private string _name;
        private readonly Guid _uuid;
        private readonly StellarSystem _stellarSystem;
        private List<Orbit> _childOrbits;
        private readonly bool _isVirtual;
        private int _bodyIndex=-1;
        public ABody(StellarSystem stellarSystem, bool isVirtual)
        {
            _stellarSystem = stellarSystem;
            _uuid = Guid.NewGuid();
            _childOrbits = new List<Orbit>();
            _isVirtual = isVirtual;
        }

        public Guid Uuid => _uuid;

        public string Name {
            get {
                if(string.IsNullOrWhiteSpace(_name))
                {
                    _name = IsVirtual? _stellarSystem.Name:$"{_stellarSystem.Name} {Physic.StarLetter(BodyIndex-1)}";
                    return _name;
                }
                else
                {
                    return _name;
                }
            }
            set { _name = value; } }

        public abstract string Denomination { get; }

        public abstract double Mass { get; set; }

        public bool IsVirtual { get { return _isVirtual; } }

        /// <summary>
        /// Gets or sets the position in meters.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 Position { get; set; }

        [NonPrintable]
        public Orbit ParentOrbit { get; set; }

        [NonPrintable]
        public List<Orbit> ChildOrbits { get => _childOrbits ; set => _childOrbits = value; }

        //TODO : avoid virtual in get index, (this cause offset) should be lazy
        public int BodyIndex {
            get {
                if (_bodyIndex != -1)
                {
                    return _bodyIndex;
                }
                else
                { 
                    _bodyIndex = _stellarSystem.GetIndexOf(this);
                    return _bodyIndex;
                }
            } 
        }

        //TODO self ROTATION

        public bool IsReady()
        {
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                if (this.GetType().GetProperty(p.Name).GetValue(this, null) == null) return false;
            }
            return true;
        }

        public abstract void Accept(Visitor v);
        public abstract INode Accept(MutationVisitor v);


        /*
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }*/
    }
}
