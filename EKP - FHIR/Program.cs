using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace EKP___FHIR
{
    class Program
    {
        static void Main(string[] args)
        {
            connection();
        }

        public static void connection()
        {
            var client = new FhirClient("http://test.fhir.org/r4");
            client.PreferredFormat = ResourceFormat.Json;

            //var query = new string[] { "gender=male" };
            //var bundle = client.Search("Patient", query);

            var bundle = client.Search("Patient");

            Console.WriteLine( "Got " + bundle.Entry.Count() + " records!");

            foreach (var entry in bundle.Entry)
            {
                try
                {
                    Patient p = (Patient)entry.Resource;
                    Console.WriteLine(p.Id + " " + p.Name.First().Text + "\r\n");
                } catch (Exception e) {
                }
                
            }

            Console.WriteLine(bundle.Entry.Count());

        }
    }
}
