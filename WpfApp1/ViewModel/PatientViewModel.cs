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
        private List<MedicationRequest> _medicationRequests;
        public List<MedicationRequest> MedicationRequests
        {
            get => _medicationRequests;
            set
            {
                _medicationRequests = value;
            }
        }

        private List<Medication> _medications;
        public List<Medication> Medications
        {
            get => _medications;
            set
            {
                _medications = value;
            }
        }

        private List<Observation> _observations;
        public List<Observation> Observations
        {
            get => _observations;
            set
            {
                _observations = value;
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
                return _patient.Id;
                //return _patient.Identifier[0].Value.ToString();
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
            HistoryCount = 0;
        }

        public int HistoryCount { get; set; }

        public void GetHistoryData()
        {
            HistoryCount = 1;
            var client = new FhirClient("https://api-stu3.hspconsortium.org/STU301withSynthea/open");
            client.PreferredFormat = ResourceFormat.Json;

            var bundle = client.Search("Patient/" + Id + "/$everything");

            foreach (var entry in bundle.Entry)
            {
                try
                {
                    switch (entry.Resource.TypeName)
                    {
                        case "MedicationRequests":
                            MedicationRequests.Add((MedicationRequest)entry.Resource);
                            break;
                        case "Medication":
                            Medications.Add((Medication)entry.Resource);
                            break;
                        case "Observation":
                            Observations.Add((Observation)entry.Resource);
                            break;
                    }
                }
                catch (Exception e)
                {
                }

            }
            
            /*var client = new FhirClient("http://test.fhir.org/r4");
            client.PreferredFormat = ResourceFormat.Json;
            var query = new string[] { "subject._id="+_patient.Id };
            var bundle = client.Search("MedicationRequest", query);
            HistoryCount += bundle.Entry.Count;
            foreach (var entry in bundle.Entry)
            {
                MedicationRequests.Add((MedicationRequest) entry.Resource);
            }

            bundle = client.Search("Observation/_search?subject._id=" + _patient.Id);
            HistoryCount += bundle.Entry.Count;
            foreach (var entry in bundle.Entry)
            {
                Observations.Add((Observation)entry.Resource);
            }*/
        }

    }
}
