using Microsoft.Extensions.Configuration;
using MySQLDataImport.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


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
List<string> koperty = new List<string>();

using (var reader = new StreamReader("Koperta.txt"))
{
    while (!reader.EndOfStream)
    {
        koperty.Add(Regex.Replace(reader.ReadLine().Trim(), @"\s+", " "));
    }
}

string postCode;
string cityName;
string streetName;
string homeNubmer;

using (var reader = new StreamReader("DaneDoBazy.txt"))
{
    while (!reader.EndOfStream)
    {
        string line = reader.ReadLine();
        var dataTable = line.Split("│");
        postCode = string.Empty;
        cityName = string.Empty;
        streetName = string.Empty;
        homeNubmer = string.Empty;


        if (!FindDataByCountyName(dataTable))
        {
            postCode = string.Empty;
            cityName = string.Empty;
            streetName = string.Empty;
            homeNubmer = string.Empty;

            if (!FindDataByStreetName(dataTable))
            {
            }
        }


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
            KodPocztowy = postCode,
            Miasto = cityName,

        });
    }
}

var properties = new List<Dzialki>();
using (var reader = new StreamReader("Dzialki.txt"))
{
    Dzialki dzialka = null;

    while (!reader.EndOfStream)
    {
        string line = Regex.Replace(reader.ReadLine().Trim(), @"\s+", " ").Trim();

        if (Regex.IsMatch(line, "│ [0-9]+(/)?([0-9]+)?(, )?([0-9])? │"))
        {
            var propertyTable = line.Split("│").Select(x => x.Trim()).ToArray();

            dzialka = new Dzialki()
            {
                OgolemZmeliorowane = double.Parse(propertyTable[7]),
                UdzialProcentowy = double.Parse(propertyTable[6]),
                PowierzchnNawadn = double.Parse(propertyTable[5]),
                PowerzchnDrenowana = double.Parse(propertyTable[4]),
                PowOdwodnRowami = double.Parse(propertyTable[3]),
                PowierzchOgolem = double.Parse(propertyTable[2]),
                NrDzialki = propertyTable[1]
            };
        }
        if (Regex.IsMatch(line, "│[0-9]+ [A-Z]+"))
        {
            var pozRej = int.Parse(line.Split(" ")[0].Replace("│","").Trim());
            if (data.Any(x => x.PozRej == pozRej))
            {
                var p = data.FirstOrDefault(x => x.PozRej == pozRej);
                dzialka.PozRej = pozRej;
                p.Dzialki.Add(dzialka);
            }
        }
    }
}

dbContext.Gwtables.AddRange(data);

dbContext.SaveChanges();

bool FindDataByCountyName(string[] dataTable)
{
    string cleanCountyName = Regex.Replace(dataTable[4].Trim(), @"\s+", " ").Replace("-", "").Trim();

    homeNubmer = Regex.Match(cleanCountyName, "[0-9]+(/[0-9]+)?( )?[A-Z]?").Value;
    if (!string.IsNullOrEmpty(homeNubmer))
    {
        cleanCountyName = cleanCountyName.Replace(homeNubmer, "").Trim();
    }

    var addressLineIdx = koperty.FindIndex(x => Regex.IsMatch(x, "([0-9]{2}-[0-9]{3}) " + cleanCountyName));
    if (addressLineIdx == -1)
    {
        return false;
    }

    postCode = Regex.Match(koperty[addressLineIdx], "[0-9]{2}-[0-9]{3}").Value;
    cityName = cleanCountyName;
    string tmpHomeNumber = Regex.Match(koperty[addressLineIdx - 2], "[0-9]+(/[0-9]+)?( )?[A-Z]?").Value;

    string cleanName = Regex.Replace(dataTable[3].Trim(), @"\s+", " ");
    if (cleanName.Contains("S."))
    {
        cleanName = cleanName.Split("S.")[0].Trim();
    }
    else if (cleanName.Contains("C."))
    {
        cleanName = cleanName.Split("C.")[0].Trim();
    }

    var nameIndex = koperty.FindIndex(x => x.Contains(cleanName));
    if (nameIndex != -1)
    {
        streetName = Regex.Replace(koperty[nameIndex + 4], @"\s+", " ").Trim();
        string streetHomeNubmer = Regex.Match(streetName, "[0-9]+(/[0-9]+)?( )?[A-Z]?").Value;
        streetName = streetName.Replace(streetHomeNubmer, "").Trim();
    }


    return true;
}

bool FindDataByStreetName(string[] dataTable)
{
    string cleanStreetName = Regex.Replace(dataTable[4].Trim(), @"\s+", " ").Replace("-", "").Trim();

    if (cleanStreetName == "-" || string.IsNullOrEmpty(cleanStreetName))
    {
        return false;
    }

    homeNubmer = Regex.Match(cleanStreetName, "[0-9]+(/[0-9]+)?( )?[A-Z]?").Value;

    if (!string.IsNullOrEmpty(homeNubmer))
    {
        cleanStreetName = cleanStreetName.Replace(homeNubmer, "").Trim();
    }



    var addressLineIdx = koperty.FindIndex(x => x.Contains(cleanStreetName));

    if (addressLineIdx == -1)
    {
        return false;
    }

    postCode = Regex.Match(koperty[addressLineIdx + 2], "[0-9]{2}-[0-9]{3}").Value;
    cityName = koperty[addressLineIdx + 2].Replace(postCode, "");
    streetName = cleanStreetName.Replace(homeNubmer, "").Replace("UL.", "");

    return true;
}