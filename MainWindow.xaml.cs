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
        public string fileLocation =  System.IO.Path.GetFullPath(@"data\USA_cars_datasets.csv");
        public MainWindow()
        {
            InitializeComponent();
            LoadIntoListBox(fileLocation);
        }
        private void LoadIntoListBox(string filePath)
        {
            LoadYearsIntoListBox();
            LoadMakesIntoListBox(filePath);
        }

        private void LoadMakesIntoListBox(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MakeOnlyMap>();

                var listings = csv.GetRecords<MakeListing>().ToList();

                var uniqueMakes = listings
                    .Select(l => l.Make)
                    .Where(b => !string.IsNullOrWhiteSpace(b))
                    .Distinct()
                    .OrderBy(b => b)
                    .ToList();

                MakeListBox.ItemsSource = uniqueMakes;
            }
        }

        private void LoadYearsIntoListBox()
        {
            List<int> years = Enumerable.Range(2005, 18).ToList();
            YearListBox.ItemsSource = years;
        }

        private void RandomVin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetVin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}