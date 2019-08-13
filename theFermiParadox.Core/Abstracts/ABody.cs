﻿using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace theFermiParadox.Core.Abstracts
{
    public abstract class ABody
    {
        private string _name;
        private readonly Guid _uuid;
        private readonly StellarSystem _stellarSystem;
        private List<Orbit> _childOrbits;
        private readonly bool _isVirtual;
        //private int bodyIndex;


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
                    return $"{_stellarSystem.Name} {Physic.latinNumber(BodyIndex)}";
                }
                else
                {
                    return _name;
                }
            }
            set { _name = value; } }

        public abstract string Denomination { get; }

        public bool IsVirtual { get { return _isVirtual; } }

        /// <summary>
        /// Gets or sets the position in meters.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 Position { get; set; }

        public Orbit ParentOrbit { get; set; }

        public List<Orbit> ChildOrbits { get => _childOrbits ; set => _childOrbits = value; }

        public int BodyIndex { get => _stellarSystem.GetIndexOf(this); }

        //TODO self ROTATION

        public bool IsReady()
        {
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                if (this.GetType().GetProperty(p.Name).GetValue(this, null) == null) return false;
            }
            return true;
        }

        public override string ToString()
        {
            string rslt = "";
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                rslt += "  " + p.Name + " : " + (this.GetType().GetProperty(p.Name).GetValue(this, null) ?? "none").ToString() + "\n";
            }
            return rslt;
        }
        /*
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }*/

        public abstract void Accept(BodyVisitor v);

        //internal abstract ABody Accept(MutationVisitor v);

    }
}
