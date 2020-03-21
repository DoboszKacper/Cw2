using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Cw2.Models;

namespace Cw2.Models
{
    [XmlRoot("Uczelnia",IsNullable =false)]
    public class Uczelnia
    {

        public Uczelnia()
        {
            DateOfCreation = DateTime.Now.ToString("yyyy-mm-dd"); 
        }

        [XmlAttribute]
        public string Author { get; set; }

        [XmlAttribute(AttributeName = "CreatedAt")]
        public string DateOfCreation { get; set; }

        [XmlArray ("Studenci")]
        public HashSet<Studnet> Studnets { set; get; }

        [XmlElement("activeStudies")]
        public ActiveStudies activeStudies { set; get; }
    }
}
