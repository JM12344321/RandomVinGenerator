using CsvHelper;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomVinGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\USA_cars_datasets.csv");

        public MainWindow()
        {
            InitializeComponent();
            LoadIntoListBox(fileLocation);
        }
        private readonly CarDataService _carDataService = new CarDataService();

        private void LoadIntoListBox(string filePath)
        {
            YearListBox.ItemsSource = _carDataService.LoadYears();
            MakeListBox.ItemsSource = _carDataService.LoadUniqueMakes(filePath);
        }

        private void RandomVin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetVin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}