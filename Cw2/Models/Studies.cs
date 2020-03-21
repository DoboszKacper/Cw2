using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{
    public class Studies
    {
        [XmlElement("name")]
        public string name { get; set; }

        [XmlElement("mode")]
        public string mode { get; set; }

    }
}
