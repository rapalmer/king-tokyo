-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: 10.25.71.66    Database: db309yt01
-- ------------------------------------------------------
-- Server version	5.7.16

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `User_List`
--

DROP TABLE IF EXISTS `User_List`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `User_List` (
  `Player_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(45) DEFAULT NULL,
  `Password` varchar(128) DEFAULT NULL,
  `Local_IP` varchar(45) DEFAULT NULL,
  `_Character` varchar(45) DEFAULT NULL,
  `IsBanned` int(11) NOT NULL DEFAULT '0',
  `IsAdmin` int(11) NOT NULL DEFAULT '0',
  `Online` varchar(20) NOT NULL DEFAULT 'Offline',
  `Friends` varchar(49) DEFAULT '0',
  PRIMARY KEY (`Player_ID`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User_List`
--

LOCK TABLES `User_List` WRITE;
/*!40000 ALTER TABLE `User_List` DISABLE KEYS */;
INSERT INTO `User_List` VALUES (33,'thomas','2EytiG/MmE4pN2jgdyryoHgErZgHs/PLyvodqjTZupkVusXWY4lyfET0LSI7JJ0zz7PoAZauFJniIpgOZsy7zwcw0mgbhPYvcOJphOHeftG87Rmy5UxsKi+7WGuwWjed','10.65.219.66',NULL,0,1,'In Lobby','38,40'),(38,'rich','ZnfWX9+mFHa1xacZxhaBzsM2XKCZ9pI13nZpqmtViymtQb6ijbpFf3k+alBdL+8ARla2FR3gViCWdlQ9in6HH+xpEnmJ74PuKzb5tj71gRzGZmRdFuVIIoWGNLUXWroP','10.65.179.124',NULL,0,0,'Offline','33'),(40,'nick','51RlG1H7771HrL8BCcO0j0aCyJfXAJ/yXLHBoxP5jChO30xEvajPozdcvTguuDEulM2pLQbIs/frh+WSS0tm/9p5nxt9+C5Egc71eoHKDSQBYXTtxy+X+Eeh5fXkiHEU','10.65.211.228',NULL,0,1,'Offline','33'),(41,'test','OyPqlFfM5cGnnOplnIGW24/EhrhOpbJlHCBTSeRqBZSTKAbS9VkAK+YVzn1J76vxxWGDth4D9HK0v5IsXvsEr6bAjo2XT0mfIHF3887dOVtKa1Vfa9unokXRaTYqYDoD','10.65.211.228',NULL,0,0,'Offline','0'),(57,'thomas3','9Onvv+Dml8M99Yc29JY9stq4UbV1EuEG73W2AbJK+6i3jjqo64eOedSTu62oKjCNrV0y3opjuTHohqonqat9bV/ELKygiF3aZpLTk3Ho+bFRRbrV3HzSNWuX+Gb8HWp4','10.65.219.66',NULL,0,0,'Offline','33'),(58,'thomas4','MBi6A5rgkmfEgbDDYnn2fF/uXkwuw0+w9POpGQUuT7UW8JthBmapSVlc5OQf7iGMns/loS/Cy00/XpjuP2C6TQJzr4K51j0w+bXFqG0EPeSR3ouWs61e78HtwDErJWkm','10.65.219.66',NULL,0,0,'Offline','0'),(59,'thomas5','wvZYT47UqtuQRxFNRrovWUOG60hWt3bk45EFEXP8hAzUFHOZUQdsEPqIQoOVe+uJjtBNupyth7TA1OdLkmFEQkKs+AL/D4FsquF7o4Tjrc8r+gpWVYNEUt3KN/1ard/O','10.65.219.66',NULL,0,0,'Offline','0'),(60,'thomas6','Y58ombCRglDY8uNKMSVsRQSTTtP62F4Ity/6ZvKyxyBFedCjP7cPkigWpKmIAT7mhaHvgBkz9prQd9abbbMTHTdEPbjUniI0fqI8dJg7BlyC3SyfqOdbkJHR9GzTWdwo','10.65.219.66',NULL,0,0,'Offline','0'),(61,'thomas7','xEk+zkEU3xYRjxMGc20LiwjpGBHtWGyLMMGDeBgA1cijl+T8f3tK8OFIVy0O4cinub3gE/Xag4aOC2t7rKsC14UB7vjAyto4bVV4mA5aF9U8Vv+/w5S6wu5iCZ4xzjLX','10.65.219.66',NULL,0,0,'Offline','0'),(62,'thomas8','lu6xer8Rur+fE4ukwHaq06fJo4foiCjYp7YIL2llodS4sp1JpQlOVjkqR9pAKG3gS7hZNjl/ykS4cc1TGT3lD2noNC+mCM/o1+FDxY3/oBtUGK3WWrvXwyzntErvJldN','10.65.219.66',NULL,0,0,'Offline','0'),(63,'thomas9','scXx46TZQGpm3RpgH5IlESwmV3a3syNZ7+tHBR+tB5+zbV0su+a//p9LMuCz0zPCt0nlPtEwAwTj4PAMOpB4+SRYf0q1y3gC1MaZm4WImj/lQc5tbpbZJ46+i4dvWUYb','10.65.219.66',NULL,0,0,'Offline','0'),(64,'thomas10','HypGPFVUUDptyUfIM6nQ5L3YPBJqSi2+1Yfr1Fzgswy9ALWOpoNAMTv0R0xH/g5/JOBDo7ch2GitexoNNyfR6phoshveHd3McRvIh6mRns0n4zQa2RtFf9plAO0mDDhB','10.65.219.66',NULL,0,0,'Offline','0'),(65,'chungus','nPjFuBFBILPWc1KoG4WjWUVlmQGmfE+787UXC2zl1ajWS6fczX2sidJWD9oZP6C+QbWZZghkRYd/rrVDxoGhZ8ev9QrEjyVQ4jtLqmzD8ETpIcwpBXI52MvaRAV8aqk3','10.26.49.165',NULL,0,0,'Offline','40'),(66,'jon','QktTI5/PHKrSaI6DX22vGl46cA8um0so0qakz6DDzXBE1r4P5oWOgYMNEvua/9Iapx6cw+pRRfgUt9FdsOjStI4ipj7kUrLpg5DQSbkor1iob32Gko0tMDc1AdNuB6n5','10.65.219.73','Meka Dragon',0,0,'In Game','33');
/*!40000 ALTER TABLE `User_List` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-12-16 18:57:17
