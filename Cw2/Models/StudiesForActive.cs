using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{
    public class StudiesForActive
    {

        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("numberOfStudies")]
        public int number { get; set; }
    }
}
