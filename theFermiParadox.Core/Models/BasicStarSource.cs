using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace theFermiParadox.Core
{ 
    [XmlRoot("BasicStarType")]
    public class BasicStarSource
    {
        [XmlElement("Star")]
        public List<Star> Stars { get; set; }
    }
}