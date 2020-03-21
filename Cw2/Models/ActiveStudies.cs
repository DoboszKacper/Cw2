using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{
    public class ActiveStudies
    {

        [XmlElement("studies")]
        public HashSet<StudiesForActive> ListActive { get; set; }

    }
}
