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

namespace ClientGUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VelibClient.Service1Client velibClient = new VelibClient.Service1Client();
        private string citySelected;
       


        public MainWindow()
        {
            InitializeComponent();
            List<VelibClient.City> cities = velibClient.GetCities().ToList();
            
            
            List<String> citiesName = new List<string>();
            foreach (VelibClient.City city in cities)
            {
                citiesName.Add(city.name);
            }
            citiesName.Sort();
            foreach (string s in citiesName)
            { 
                CityBox.Items.Add(s);
            }
            

        }

        

        private void CityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityBox.SelectedItem != null)
            {
                StationsList.Items.Clear();
                citySelected = CityBox.SelectedItem.ToString();
                List<VelibClient.Station> stationsList = velibClient.GetStations(citySelected).ToList();
                List<string> stations = new List<string>();
                foreach (VelibClient.Station station in stationsList)
                {
                    
                    StationsList.Items.Add(station.name);
                }
            }
        }

        private void StationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StationsList.SelectedItem != null)
            {
                string stationSelected = StationsList.SelectedItem.ToString();
                string infos = velibClient.GetStationData(stationSelected, citySelected);
                AvailableBikesLabel.Content = infos;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CityBox.SelectedItem != null)
            {
                StationsList.Items.Clear();
                citySelected = CityBox.SelectedItem.ToString();
                List<VelibClient.Station> stationsList = velibClient.GetStations(citySelected).ToList();
                string research = SearchStationTextBox.Text;
                foreach (VelibClient.Station station in stationsList)
                {
                    string name = station.name;

                    if (name.ToLower().Contains(research.ToLower()))
                    {
                        
                        StationsList.Items.Add(name);
                    }
                    
                }
            }
        }

        }
}
