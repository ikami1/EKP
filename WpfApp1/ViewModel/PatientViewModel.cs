using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace WpfApp1.ViewModel
{
    class PatientViewModel: ViewModelBase
    {
        private MedicationStatement _medicationStatment;
        public MedicationStatement MedicationStatement
        {
            get => _medicationStatment;
            set
            {
                _medicationStatment = value;
            }
        }

        private Medication _medication;
        public Medication Medication
        {
            get => _medication;
            set
            {
                _medication = value;
            }
        }

        private Observation _observation;
        public Observation Observation
        {
            get => _observation;
            set
            {
                _observation = value;
            }
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        public string Name
        {
            get
            {
                string returnName = "";
                foreach(HumanName name in _patient.Name)
                {
                    returnName = returnName + name;
                }
                return returnName;
            }
        }

        public string Id
        {
            get
            {
                return _patient.Identifier[0].Value.ToString();
            }
        }

        public string Gender
        {
            get
            {
                return _patient.Gender.Value.ToString();
            }
        }

        public string BirthDate
        {
            get
            {
                if (_patient.BirthDate != null)
                    return _patient.BirthDate.ToString();
                else
                    return "";
            }
        }

        public string Address
        {
            get
            {
                if (_patient.Address.Count > 0)
                    return _patient.Address[0].Text;
                else
                    return "";
            }
        }

        public PatientViewModel(Patient p)
        {
            Patient = p;


            var client = new FhirClient("http://test.fhir.org/r4");
            client.PreferredFormat = ResourceFormat.Json;

            var bundle = client.Search("Patient/" + Id + "/$everything");

            foreach (var entry in bundle.Entry)
            {
                try
                {
                    Console.WriteLine(entry.Resource.TypeName);
                }
                catch (Exception e)
                {
                }

            }
        }

    }
}
