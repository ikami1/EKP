using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
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
                if ((_selectedPatient != null) && (_selectedPatient.HistoryCount == 0))
                    _selectedPatient.GetHistoryData();
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        private RelayCommand _filterDataCommand;
        public RelayCommand FilterDataCommand { get => _filterDataCommand; }
        public string FilterValue { get; set; }
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

        private RelayCommand _plotCommand;
        public RelayCommand PlotCommand { get => _plotCommand; }
        private PointCollection _plotDataSet;
        public PointCollection PlotDataSet
        {
            get => _plotDataSet;
            set
            {
                _plotDataSet = value;
                OnPropertyChanged(nameof(PlotDataSet));
            }
        }
        private void PlotData()
        {
            PlotDataSet = SelectedPatient.GeneratePlotData();
        }

        public PatientListViewModel()
        {
            OnPropertyChanged("Patients");

            FetchPatientList();
            SelectedPatient = Patients[0];

            List<Point> test = new List<Point>();
            for (int i = 1; i < 5; i++)
                test.Add(new Point(i, i));

            _plotDataSet = new PointCollection(test);

            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Patients);

            _filterDataCommand = new RelayCommand(param => this.FilterData());
            _plotCommand = new RelayCommand(param => this.PlotData());
        }

        private void FetchPatientList()
        {
            var client = new FhirClient("https://api-stu3.hspconsortium.org/STU301withSynthea/open");
            client.PreferredFormat = ResourceFormat.Json;

            var query = new string[] { "_count=50" };
            var bundle = client.Search("Patient", query);

            //var bundle = client.Search("Patient");

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
            
        }

    }
}
