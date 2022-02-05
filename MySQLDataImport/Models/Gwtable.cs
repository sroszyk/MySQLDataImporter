using System;
using System.Collections.Generic;

#nullable disable

namespace MySQLDataImport.Models
{
    public partial class Gwtable
    {
        public int Idgwtable { get; set; }
        public int? NrKolejny { get; set; }
        public int? PozRej { get; set; }
        public string NazwiskoImieDoOplatyNaleznosci { get; set; }
        public string NazwaWsiLubUlicyNumerDomu { get; set; }
        public double? PowMeliorowana { get; set; }
        public string UdzialUlamkowyWpozRej { get; set; }
        public double? NaleznoscOgolemDoZaplaty { get; set; }
        public double? ZalegloscZlatUbieglych { get; set; }
        public double? NaliczoneOdsetkiZaZwloke { get; set; }
        public double? KosztyUpomnieniaDoZaplaty { get; set; }
        public double? Umorzenie { get; set; }
        public double? DodatkoweObciazenieNalezn { get; set; }
        public double? WysokoscWplatyOgolem { get; set; }
        public double? WysokoscWplatyWtZalegl { get; set; }
        public double? Saldo { get; set; }
    }
}
