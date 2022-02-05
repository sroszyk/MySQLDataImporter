using Microsoft.Extensions.Configuration;
using MySQLDataImport.Models;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

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
            PowMeliorowana = double.Parse(dataTable[5].Trim()),
            UdzialUlamkowyWpozRej = dataTable[6].Trim(),
            NaleznoscOgolemDoZaplaty = double.Parse(dataTable[7].Trim()),
            ZalegloscZlatUbieglych = double.Parse(dataTable[8].Trim()),
            NaliczoneOdsetkiZaZwloke = double.Parse(dataTable[9].Trim()),
            KosztyUpomnieniaDoZaplaty = double.Parse(dataTable[10].Trim()),
            Umorzenie = double.Parse(dataTable[11].Trim()),
            DodatkoweObciazenieNalezn = double.Parse(dataTable[12].Trim()),
            WysokoscWplatyOgolem = double.Parse(dataTable[13].Trim()),
            WysokoscWplatyWtZalegl = double.Parse(dataTable[14].Trim()),
            Saldo = double.Parse(dataTable[15].Trim()),
        });;
    }
}

dbContext.Gwtables.AddRange(data);

dbContext.SaveChanges();
