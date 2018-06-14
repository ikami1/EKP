using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace WpfApp1.ViewModel
{
    class PatientListViewModel : ViewModelBase {
        public ObservableCollection<PatientViewModel> Patients { get; set; } = new ObservableCollection<PatientViewModel>();

        private ListCollectionView _view;

        private string _fetchedPatientsNumber;
        public string FetchedPatientsNumber
        {
            get => _fetchedPatientsNumber;
            set
            {
                _fetchedPatientsNumber = value;
                OnPropertyChanged(nameof(FetchedPatientsNumber));
            }
        }

        private PatientViewModel _selectedPatient;
        public PatientViewModel SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                if((_selectedPatient!=null)&&(_selectedPatient.HistoryCount==0))
                    _selectedPatient.GetHistoryData();
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public string FilterValue { get; set; }
        private RelayCommand _filterDataCommand;
        public RelayCommand FilterDataCommand { get => _filterDataCommand; }
        private void FilterData()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                _view.Filter = null;
            }
            else
            {
                _view.Filter = (c) => ((PatientViewModel)c).Name.Contains(FilterValue);
            }
        }

        public PatientListViewModel()
        {
            OnPropertyChanged("Patients");

            FetchPatientList();
            SelectedPatient = Patients[0];

            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Patients);
            _filterDataCommand = new RelayCommand(param => this.FilterData());
        }

        private void FetchPatientList()
        {
            var client = new FhirClient("http://test.fhir.org/r4");
            client.PreferredFormat = ResourceFormat.Json;

            //var query = new string[] { "gender=male" };
            //var bundle = client.Search("Patient", query);

            var bundle = client.Search("Patient");

            FetchedPatientsNumber = bundle.Entry.Count().ToString();

            foreach (var entry in bundle.Entry)
            {
                try
                {
                    Patient p = (Patient)entry.Resource;
                    Patients.Add(new PatientViewModel(p));
                }
                catch (Exception e)
                {
                }

            }
            bundle = client.Search("Patient/_search?_id=pat1");
            foreach (var entry in bundle.Entry)
            {
                try
                {
                    Patient p = (Patient)entry.Resource;
                    Patients.Add(new PatientViewModel(p));
                }
                catch (Exception e)
                {
                }

            }
        }

    }
}
