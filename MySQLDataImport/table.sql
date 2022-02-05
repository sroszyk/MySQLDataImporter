CREATE TABLE `gwtable` (
  `idgwtable` int NOT NULL AUTO_INCREMENT,
  `Nr.kolejny` int DEFAULT NULL,
  `Poz.rej` int DEFAULT NULL,
  `NazwiskoImieDoOplatyNaleznosci` varchar(150) DEFAULT NULL,
  `NazwaWsiLubUlicyNumerDomu` varchar(150) DEFAULT NULL,
  `Pow.meliorowana` double DEFAULT NULL,
  `UdzialUlamkowyWPoz.rej` varchar(10) DEFAULT NULL,
  `NaleznoscOgolemDoZaplaty` double DEFAULT NULL,
  `ZalegloscZLatUbieglych` double DEFAULT NULL,
  `NaliczoneOdsetkiZaZwloke` double DEFAULT NULL,
  `KosztyUpomnieniaDoZaplaty` double DEFAULT NULL,
  `Umorzenie` double DEFAULT NULL,
  `DodatkoweObciazenieNalezn.` double DEFAULT NULL,
  `WysokoscWplatyOgolem` double DEFAULT NULL,
  `WysokoscWplatyWT.Zalegl` double DEFAULT NULL,
  `SALDO` double DEFAULT NULL,
  PRIMARY KEY (`idgwtable`)
) ENGINE=InnoDB AUTO_INCREMENT=156 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
