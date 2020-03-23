using Cw2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cw2
{
    public class Program
    {
        static void Main(string[] args)
        {
           
            var set = new HashSet<Studnet>();

            // 1.1 Przyjmowanie parametrów: 
            var input = args.Length > 0 ? args[0] : @"Files\dane.csv";
            var output = args.Length > 1 ? args[1] : @"Files\Result.xml";
            var dataType = args.Length > 2 ? args[2] : "xml";

            //var input = @"C:\Users\kacpe\Desktop\Programowanie\Polsko-Japońska\ABD\Cw2\Cw2\Files\dane.csv";
            //var output = @"C:\Users\kacpe\Desktop\Programowanie\Polsko-Japońska\ABD\Cw2\Cw2\Files\Result.json";
            //var dataType = "json";

            //1.2 Obsługa błedów:
            try
            {
                if (!File.Exists(input))
                    throw new FileNotFoundException("Error", input.Split("\\")[^1]);

                foreach (var line in File.ReadAllLines(input))
                {

                    string[] kolumna = line.Split(",");
                    if (kolumna.Length < 9 || line.Contains(",,"))
                    {
                        File.AppendAllText("Log.txt", $"{DateTime.UtcNow } Nieprawidłowa liczba wartości");
                        continue;
                    }

                    var studnet = new Studnet
                    {
                        Imie = kolumna[0],
                        Nazwisko = kolumna[1],
                        Studies = new Studies
                        {
                            name = kolumna[2],
                            mode = kolumna[3]
                        },
                        BirthDate = kolumna[5],
                        MothersName = kolumna[7],
                        FathersName = kolumna[8],
                        IndexNumber = "s" + kolumna[4],
                        Email = kolumna[6]
                    };
                    set.Add(studnet);
                }

            }
            catch (FileNotFoundException e)
            {
                File.AppendAllText("Log.txt", $"{DateTime.UtcNow } {e.Message} {e.FileName} ");
            } catch (ArgumentException e)
            {
                File.AppendAllText("Log.txt", $"{DateTime.UtcNow } {e.Message} Podana scierzka jest niepoprawna! ");
            }


            //1.3 DISTINCT
            HashSet<Studnet> distinctSet = set
                .GroupBy(p => new { p.Imie, p.Nazwisko })
                .Select(g => g.First())
                .ToHashSet();

            ActiveStudies bigg = new ActiveStudies()
            {
                ListActive = new HashSet<StudiesForActive>()
            };
            //Active Studies Set Up
            foreach (var a in distinctSet)
            {
                    var stud = new StudiesForActive()
                    {
                        name = a.Studies.name,
                        number = 0
                    };
                    bigg.ListActive.Add(stud);
            }  

            ActiveStudies small = new ActiveStudies()
            {
                ListActive = bigg.ListActive.GroupBy(p => p.name)
                .Select(g => g.First())
                .ToHashSet()
        };

            for (int i = 0; i < small.ListActive.Count; i++)
            {
                for (int j = 0; j < bigg.ListActive.Count; j++)
                {
                    if(String.Equals(small.ListActive.ElementAt(i).name, bigg.ListActive.ElementAt(j).name))
                    {
                        small.ListActive.ElementAt(i).number = small.ListActive.ElementAt(i).number + 1;
                    }
                    
                }
            }

            //Uczelnia Set Up
            Uczelnia uni = new Uczelnia()
            {
                Author = "Kacper Dobosz",
                Studnets = distinctSet,
                activeStudies = small
            };


            if(dataType == "xml")
            {
                //XML
                FileStream writer = new FileStream(output, FileMode.Create);
                XmlSerializerNamespaces xml = new XmlSerializerNamespaces();
                xml.Add("", "");
                XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));
                serializer.Serialize(writer, uni, xml);
            }
            if (dataType == "json")
            {
                //JSON
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var jsonFile = JsonSerializer.Serialize<Uczelnia>(uni,options);
                File.WriteAllText(output, jsonFile);
            }
        }
    }
}
