using Helpers;
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
        private readonly string _name;
        private readonly Guid _uuid;
        private readonly StellarSystem _stellarSystem;

        public ABody(string name, StellarSystem stellarSystem)
        {
            _name = name;
            _stellarSystem = stellarSystem;
            _uuid = new Guid();
        }
        [XmlElement("Uuid")]
        public Guid Uuid => _uuid;

        [XmlElement("Name")]
        public string Name => _name;

        [XmlElement("Denomination")]
        public abstract string Denomination { get; }

        [XmlElement("IsVirtual")]
        public bool IsVirtual { get; set; }

        [XmlElement("Position")]
        public Vector3 Position { get; set; }
        
        //ROTATION

        public bool IsReady()
        {
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                if (this.GetType().GetProperty(p.Name).GetValue(this, null)==null) return false;
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
    }
}
