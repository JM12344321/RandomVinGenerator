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
            if (_autoData.Count == 0) return null;

            var random = new Random();
            var ReturnedAuto = new ProcessAutoData();

            bool hasYear = int.TryParse(year, out int parsedYear);
            string? normalizedMake = string.IsNullOrWhiteSpace(make) ? null : make.Trim();

            if (!hasYear && normalizedMake == null)
            {
                ReturnedAuto = _autoData[random.Next(_autoData.Count)];
                return ReturnedAuto;
            }

            int targetYear = hasYear ? parsedYear : random.Next(2005, 2023);

            string targetMake = normalizedMake ?? _autoData
                .Select(auto => auto.Make)
                .Where(make => !string.IsNullOrWhiteSpace(make))
                .Distinct()
                .OrderBy(_ => Guid.NewGuid())
                .First();

            var filtered = _autoData
                .Where(auto => auto.Year == targetYear &&
                            string.Equals(auto.Make, targetMake, StringComparison.OrdinalIgnoreCase))
                .ToList();

            ReturnedAuto = filtered.Count == 0 ? null : filtered[random.Next(filtered.Count)];
            return ReturnedAuto;
        }
       
    }
}
