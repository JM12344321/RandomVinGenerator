using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVinGenerator
{
    public sealed class AutoListingMap : ClassMap<ProcessAutoData>
    {
        public AutoListingMap()
        {
            Map(m => m.ID).Name("id");
            Map(m => m.Price).Name("price");
            Map(m => m.Make).Name("brand");
            Map(m => m.Model).Name("model");
            Map(m => m.Year).Name("year");
            Map(m => m.TitleStatus).Name("title_status");
            Map(m => m.Mileage).Name("mileage");
            Map(m => m.Color).Name("color");
            Map(m => m.VIN).Name("vin");
            Map(m => m.Lot).Name("lot");
            Map(m => m.State).Name("state");
            Map(m => m.Country).Name("country");
            Map(m => m.Condition).Name("condition");
        }
    }
    public class ProcessAutoData
    {
        public int ID { get; set; }
        public decimal Price { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string TitleStatus { get; set; }
        public double Mileage { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public long Lot { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Condition { get; set; }

    }
}
