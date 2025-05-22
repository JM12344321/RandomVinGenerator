using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace RandomVinGenerator
{
    public class VinService
    {
        private readonly string _filePath;
        private readonly List<ProcessAutoData> _autoData;

        public VinService(string filePath)
        {
            _filePath = filePath;
            _autoData = LoadAutoData();
        }

        private List<ProcessAutoData> LoadAutoData()
        {
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<AutoListingMap>();
            return csv.GetRecords<ProcessAutoData>()
                      .Where(r => !string.IsNullOrWhiteSpace(r.VIN))
                      .ToList();
        }

        public ProcessAutoData? GetRandomAuto(string? year, string? make)
        {
            var random = new Random();

            bool isYearBlank = string.IsNullOrWhiteSpace(year);
            bool isMakeBlank = string.IsNullOrWhiteSpace(make);

            if (isYearBlank && isMakeBlank)
            {
                return _autoData.Count == 0 ? null : _autoData[random.Next(_autoData.Count)];
            }

            int targetYear = isYearBlank ? random.Next(2005, 2023) : int.TryParse(year, out int y) ? y : 0;
            string? targetMake = isMakeBlank
                ? _autoData.Select(a => a.Make).Distinct().OrderBy(_ => Guid.NewGuid()).FirstOrDefault()
                : make;

            var matching = _autoData
                .Where(a =>
                    a.Year == targetYear &&
                    string.Equals(a.Make, targetMake, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return matching.Any() ? matching[random.Next(matching.Count)] : null;
        }
        
    }
}
