using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RandomVinGenerator
{
    public partial class MainWindow : Window
    {
        private readonly string _fileLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\USA_cars_datasets.csv");
        private readonly CarDataService _carDataService;
        private readonly VinService _vinService;
        private readonly Dictionary<string, Label> _fieldLabels;

        public MainWindow()
        {
            InitializeComponent();

            _carDataService = new CarDataService();
            _vinService = new VinService(_fileLocation);

            _fieldLabels = new()
            {
                ["VIN"] = VINResultVIN,
                ["Year"] = YearResultLabel,
                ["Make"] = MakeResultLabel,
                ["Model"] = ModelResultLabel,
                ["Mileage"] = MileageResultLabel,
                ["Price"] = PriceResultLabel
            };

            LoadIntoListBox();
        }

        private void LoadIntoListBox()
        {
            YearListBox.ItemsSource = _carDataService.LoadYears();
            MakeListBox.ItemsSource = _carDataService.LoadUniqueMakes(_fileLocation);
        }

        private void RandomVin_Click(object sender, RoutedEventArgs e)
        {
            DisplayRandomVin(null, null);
        }

        private void GetVin_Click(object sender, RoutedEventArgs e)
        {
            string? selectedYear = YearListBox.SelectedItem?.ToString();
            string? selectedMake = MakeListBox.SelectedItem?.ToString();
            DisplayRandomVin(selectedYear, selectedMake);
        }

        private void DisplayRandomVin(string? year, string? make)
        {
            var entry = _vinService.GetRandomAuto(year, make);

            if (entry == null)
            {
                SetLabels(new Dictionary<string, string>
                {
                    ["VIN"] = "No result, sorry!",
                    ["Year"] = "",
                    ["Make"] = "",
                    ["Model"] = "",
                    ["Mileage"] = "",
                    ["Price"] = ""
                });
                return;
            }

            SetLabels(new Dictionary<string, string>
            {
                ["VIN"] = entry.VIN.ToUpperInvariant().Replace(" ", ""),
                ["Year"] = entry.Year.ToString().Replace(" ", ""),
                ["Make"] = entry.Make.Replace(" ", ""),
                ["Model"] = entry.Model.Replace(" ", ""),
                ["Mileage"] = $"{entry.Mileage:N0}".Replace(" ", ""),
                ["Price"] = $"{entry.Price:N0}".Replace(" ", "")
            });
        }

        private void SetLabels(Dictionary<string, string> values)
        {
            foreach (var kvp in values)
                if (_fieldLabels.TryGetValue(kvp.Key, out var label))
                    label.Content = kvp.Value;
        }

        private void CopyToClipboard(string key)
        {
            if (_fieldLabels.TryGetValue(key, out var label))
            {
                var content = label.Content?.ToString();
                if (!string.IsNullOrWhiteSpace(content))
                    Clipboard.SetText(content);
            }
        }

        private void VINCopyButton_Click(object sender, RoutedEventArgs e) => CopyToClipboard("VIN");
        private void YearCopyButton_Click(object sender, RoutedEventArgs e) => CopyToClipboard("Year");
        private void MakeCopyButton_Click(object sender, RoutedEventArgs e) => CopyToClipboard("Make");
        private void ModelCopyButton_Click(object sender, RoutedEventArgs e) => CopyToClipboard("Model");
        private void MileageCopyButton_Click(object sender, RoutedEventArgs e) => CopyToClipboard("Mileage");
        private void PriceCopyButton_Click(object sender, RoutedEventArgs e) => CopyToClipboard("Price");

        private void CacheBuster_Click(object sender, RoutedEventArgs e)
        {
            if (MileageResultLabel.Content is string mileageStr &&
            double.TryParse(mileageStr.Replace(",", ""), out double mileage))
            {
                mileage += 10;
                MileageResultLabel.Content = $"{mileage:N0}";
            }

            if (PriceResultLabel.Content is string priceStr &&
                decimal.TryParse(priceStr.Replace(",", ""), out decimal price))
            {
                price += 10;
                PriceResultLabel.Content = $"{price:N0}";
            }
        }
    }
}
