using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVinGenerator
{
    public class CarDataService
    {
        public List<string> LoadUniqueMakes(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<MakeOnlyMap>();
            return csv.GetRecords<MakeListing>()
                      .Select(l => l.Make)
                      .Where(b => !string.IsNullOrWhiteSpace(b))
                      .Distinct()
                      .OrderBy(b => b)
                      .ToList();
        }

        public List<int> LoadYears() => Enumerable.Range(2005, 18).ToList();
    }

}
