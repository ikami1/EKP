using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            var endpoint = new Uri("http://vonk.fire.ly");
            var client = new FhirClient(endpoint);

            //var query = new string[] { "name=Rob" };
            //var bundle = client.Search("Patient", query);

            var bundle = client.Search("Patient");

            button1.Content = "Got " + bundle.Entry.Count() + " records!";

            label1.Content = "";
            foreach (var entry in bundle.Entry)
            {
                try
                {
                    Patient p = (Patient)entry.Resource;
                    label1.Content = label1.Content + p.Id + " " + p.Name.First().Text + "\r\n";
                }
                catch (Exception exeption) { }
            }
        }*/
    }
}
