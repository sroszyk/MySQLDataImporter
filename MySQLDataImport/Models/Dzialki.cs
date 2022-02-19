using System;
using System.Collections.Generic;

#nullable disable

namespace MySQLDataImport.Models
{
    public partial class Dzialki
    {
        public int Iddzialki { get; set; }
        public string NrDzialki { get; set; }
        public double? PowierzchOgolem { get; set; }
        public double? PowOdwodnRowami { get; set; }
        public double? PowierzchnNawadn { get; set; }
        public double? UdzialProcentowy { get; set; }
        public double? OgolemZmeliorowane { get; set; }
        public double? PowerzchnDrenowana { get; set; }
        public int? PozRej { get; set; }

        public Gwtable Gwtable { get; set; }
    }
}
