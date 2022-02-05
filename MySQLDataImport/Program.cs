using Microsoft.Extensions.Configuration;
using MySQLDataImport.Models;
using System.Collections.Generic;
using System.IO;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var currentCulture = System.Globalization.CultureInfo.InstalledUICulture;
var numberFormat = (System.Globalization.NumberFormatInfo)currentCulture.NumberFormat.Clone();
numberFormat.NumberDecimalSeparator = ".";

var connectionString = config.GetConnectionString("MyDatabase");

var dbContext = new mydatabaseContext(connectionString);

List<Gwtable> data = new List<Gwtable>();


using (var reader = new StreamReader("DaneDoBazy.txt"))
{
    while (!reader.EndOfStream)
    {
        string line = reader.ReadLine();
        var dataTable = line.Split("│");
        data.Add(new Gwtable
        {
            NrKolejny = int.Parse(dataTable[1].Trim()),
            PozRej = int.Parse(dataTable[2].Trim()),
            NazwiskoImieDoOplatyNaleznosci = dataTable[3].Trim(),
            NazwaWsiLubUlicyNumerDomu = dataTable[4].Trim(),
            PowMeliorowana = double.Parse(dataTable[5].Trim(), numberFormat),
            UdzialUlamkowyWpozRej = dataTable[6].Trim(),
            NaleznoscOgolemDoZaplaty = double.Parse(dataTable[7].Trim(), numberFormat),
            ZalegloscZlatUbieglych = double.Parse(dataTable[8].Trim(), numberFormat),
            NaliczoneOdsetkiZaZwloke = double.Parse(dataTable[9].Trim(), numberFormat),
            KosztyUpomnieniaDoZaplaty = double.Parse(dataTable[10].Trim(), numberFormat),
            Umorzenie = double.Parse(dataTable[11].Trim(), numberFormat),
            DodatkoweObciazenieNalezn = double.Parse(dataTable[12].Trim(), numberFormat),
            WysokoscWplatyOgolem = double.Parse(dataTable[13].Trim(), numberFormat),
            WysokoscWplatyWtZalegl = double.Parse(dataTable[14].Trim(), numberFormat),
            Saldo = double.Parse(dataTable[15].Trim(), numberFormat),
        });;
    }
}

dbContext.Gwtables.AddRange(data);

dbContext.SaveChanges();
