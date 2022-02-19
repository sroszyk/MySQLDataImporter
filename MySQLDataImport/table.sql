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
  `Miasto` varchar(150) DEFAULT NULL,
  `Ulica` varchar(150) DEFAULT NULL,
  `KodPocztowy` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`idgwtable`),
  KEY `part_of_name` (`Nr.kolejny`),
  KEY `poz_rej_idx` (`Poz.rej`)
);

CREATE TABLE `dzialki` (
  `iddzialki` int NOT NULL,
  `NrDzialki` varchar(45) DEFAULT NULL,
  `PowierzchOgolem` double DEFAULT NULL,
  `PowOdwodnRowami` double DEFAULT NULL,
  `PowierzchnNawadn` double DEFAULT NULL,
  `UdzialProcentowy` double DEFAULT NULL,
  `OgolemZmeliorowane` double DEFAULT NULL,
  `PozRej` int DEFAULT NULL,
  PRIMARY KEY (`iddzialki`)
);
ALTER TABLE `mydatabase`.`dzialki` 
ADD COLUMN `PowerzchnDrenowana` DOUBLE NULL AFTER `PozRej`;
ALTER TABLE `mydatabase`.`dzialki` 
CHANGE COLUMN `iddzialki` `iddzialki` INT NOT NULL AUTO_INCREMENT ;