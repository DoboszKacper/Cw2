using System;
using System.Xml.Serialization;

namespace Cw2.Models
{
    public class Studnet {


        [XmlElement("fname")]
        public string Imie { get; set; }

        [XmlElement("lname")]
        public string Nazwisko { get; set; }

        [XmlElement("birthdate")]
        public string BirthDate { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("mothersName")]
        public string MothersName { get; set; }

        [XmlElement("fathersName")]
        public string FathersName { get; set; }


        [XmlElement("studies")]
        public Studies Studies { get; set; }
        
        

        [XmlAttribute("indexNumber")]
        public string IndexNumber { get; set; }

    }
}