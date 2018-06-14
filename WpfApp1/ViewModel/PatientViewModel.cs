using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    class PatientViewModel: ViewModelBase
    {
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
                return _patient.Id.ToString();
            }
        }

        public string Gender
        {
            get
            {
                return _patient.Gender.ToString();
            }
        }

        public string BirthDate
        {
            get
            {
                return _patient.BirthDate.ToString();
            }
        }

        public PatientViewModel(Patient p)
        {
            Patient = p;
        }

    }
}
