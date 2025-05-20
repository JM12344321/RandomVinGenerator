using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomVinGenerator
{
    public class MakeListing
    {
        public string Make { get; set; }
    }
    public sealed class MakeOnlyMap : ClassMap<MakeListing>
    {
        public
            MakeOnlyMap()
        {
            Map(m => m.Make).Name("brand");
        }
    }
}
