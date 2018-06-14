using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Windows.Media;
using System.Windows;
using OxyPlot;

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

            _plotCommand = new RelayCommand(param => this.GeneratePlotData());
        }

        public int HistoryCount { get; set; }
        public List<Observation> HistoryList { get; set; } = new List<Observation>();

        public Dictionary<string, List<Observation>> PlotData { get; set; } = new Dictionary<string, List<Observation>>();
        public string[] PossibleValues { get; } = new string[] { "height", "heart_rate", "temperature", "weight", "bmi" };
        public string _chosenValue;
        public string ChosenValue {
            get => _chosenValue;
            set
            {
                _chosenValue = value;
                ChosenSet = PlotData[_chosenValue];
                if (ChosenSet.Count > 0)
                {
                    Start = PlotData[_chosenValue][0];
                    End = PlotData[_chosenValue][ChosenSet.Count - 1];
                }
                else
                {
                    Start = null;
                    End = null;
                }
                OnPropertyChanged(nameof(ChosenSet));
                OnPropertyChanged(nameof(Start));
                OnPropertyChanged(nameof(End));
            }
        }
        public Observation Start { get; set; }
        public Observation End { get; set; }
        public List<Observation> ChosenSet { get; set; }

        private RelayCommand _plotCommand;
        public RelayCommand PlotCommand { get => _plotCommand; }
        private List<DataPoint> _plotDataSet;
        public List<DataPoint> PlotDataSet
        {
            get => _plotDataSet;
            set
            {
                _plotDataSet = value;
                OnPropertyChanged(nameof(PlotDataSet));
            }
        }
        public void GeneratePlotData()
        {
            List<DataPoint> tempList = new List<DataPoint>();
            for(int i=ChosenSet.IndexOf(Start);i<=ChosenSet.IndexOf(End);i++)
            {
                SimpleQuantity temp = (SimpleQuantity) ChosenSet[i].Value;
                tempList.Add(new DataPoint(i, Convert.ToDouble(temp.Value)));
            }
            PlotDataSet = tempList;
        }

        public void GetHistoryData()
        {
            HistoryCount = 1;
            MedicationRequests = new List<MedicationRequest>();
            Medications = new List<Medication>();
            Observations = new List<Observation>();

            var client = new FhirClient("https://api-stu3.hspconsortium.org/STU301withSynthea/open");
            client.PreferredFormat = ResourceFormat.Json;

            var query = new string[] { "_count=50" };
            var bundle = client.Search("Patient/" + Id + "/$everything", query);

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
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                }

            }
            
            //HistoryList.AddRange(MedicationRequests);
            //HistoryList.AddRange(Medications);
            HistoryList.AddRange(Observations);
            HistoryCount = HistoryList.Count;
            HistoryList.OrderBy(f => f.Effective );

            PlotData.Add("height", new List<Observation>());
            PlotData.Add("heart_rate", new List<Observation>());
            PlotData.Add("temperature", new List<Observation>());
            PlotData.Add("weight", new List<Observation>());
            PlotData.Add("bmi", new List<Observation>());


            foreach (Observation o in Observations)
            {
                switch (o.Code.Text)
                {
                    case "height":
                        PlotData["height"].Add(o);
                        PlotData["height"].OrderBy(f => f.Effective);
                        break;
                    case "heart_rate":
                        PlotData["heart_rate"].Add(o);
                        PlotData["heart_rate"].OrderBy(f => f.Effective);
                        break;
                    case "temperature":
                        PlotData["temperature"].Add(o);
                        PlotData["temperature"].OrderBy(f => f.Effective);
                        break;
                    case "weight":
                        PlotData["weight"].Add(o);
                        PlotData["weight"].OrderBy(f => f.Effective);
                        break;
                    case "bmi":
                        PlotData["bmi"].Add(o);
                        PlotData["bmi"].OrderBy(f => f.Effective);
                        break;
                    default:
                        break;
                }
            }
            ChosenValue = PossibleValues[0];


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
