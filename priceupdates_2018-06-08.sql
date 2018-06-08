# ************************************************************
# Sequel Pro SQL dump
# Version 4541
#
# http://www.sequelpro.com/
# https://github.com/sequelpro/sequelpro
#
# Host: 127.0.0.1 (MySQL 5.7.20)
# Database: priceupdates
# Generation Time: 2018-06-08 09:12:33 +0000
# ************************************************************


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


# Dump of table field_definitions
# ------------------------------------------------------------

DROP TABLE IF EXISTS `field_definitions`;

CREATE TABLE `field_definitions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `definition_id` int(11) DEFAULT NULL,
  `column` varchar(45) NOT NULL,
  `maps_to` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`,`column`)
) ENGINE=MyISAM AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;

LOCK TABLES `field_definitions` WRITE;
/*!40000 ALTER TABLE `field_definitions` DISABLE KEYS */;

INSERT INTO `field_definitions` (`id`, `definition_id`, `column`, `maps_to`)
VALUES
	(1,1,'1','partnumber'),
	(2,1,'4','description'),
	(3,1,'5','retail_price'),
	(17,1,'0','barcode'),
	(5,1,'7','buying_category'),
	(6,1,'11','weight');

/*!40000 ALTER TABLE `field_definitions` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table field_mappings
# ------------------------------------------------------------

DROP TABLE IF EXISTS `field_mappings`;

CREATE TABLE `field_mappings` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `manufacturer_id` int(11) DEFAULT NULL,
  `column` varchar(45) NOT NULL,
  `maps_to` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`,`column`)
) ENGINE=MyISAM AUTO_INCREMENT=309 DEFAULT CHARSET=latin1;

LOCK TABLES `field_mappings` WRITE;
/*!40000 ALTER TABLE `field_mappings` DISABLE KEYS */;

INSERT INTO `field_mappings` (`id`, `manufacturer_id`, `column`, `maps_to`)
VALUES
	(55,1,'1','pricecode'),
	(23,1,'1','partnumber'),
	(24,1,'4','description'),
	(25,1,'5','retail_price'),
	(53,1,'1','pricecode'),
	(202,117,'5','retail_price'),
	(203,117,'0','barcode'),
	(200,117,'1','partnumber'),
	(201,117,'4','description'),
	(208,118,'0','barcode'),
	(205,118,'1','partnumber'),
	(206,118,'4','description'),
	(207,118,'5','retail_price'),
	(213,119,'0','barcode'),
	(210,119,'1','partnumber'),
	(211,119,'4','description'),
	(212,119,'5','retail_price'),
	(218,120,'0','barcode'),
	(215,120,'1','partnumber'),
	(216,120,'4','description'),
	(217,120,'5','retail_price'),
	(223,121,'0','barcode'),
	(220,121,'1','partnumber'),
	(221,121,'4','description'),
	(222,121,'5','retail_price'),
	(228,122,'0','barcode'),
	(225,122,'1','partnumber'),
	(226,122,'4','description'),
	(227,122,'5','retail_price'),
	(233,123,'0','barcode'),
	(230,123,'1','partnumber'),
	(231,123,'4','description'),
	(232,123,'5','retail_price'),
	(238,124,'0','barcode'),
	(235,124,'1','partnumber'),
	(236,124,'4','description'),
	(237,124,'5','retail_price'),
	(243,125,'0','barcode'),
	(240,125,'1','partnumber'),
	(241,125,'4','description'),
	(242,125,'5','retail_price'),
	(248,126,'0','barcode'),
	(245,126,'1','partnumber'),
	(246,126,'4','description'),
	(247,126,'5','retail_price'),
	(253,127,'0','barcode'),
	(250,127,'1','partnumber'),
	(251,127,'4','description'),
	(252,127,'5','retail_price'),
	(258,128,'0','barcode'),
	(255,128,'1','partnumber'),
	(256,128,'4','description'),
	(257,128,'5','retail_price'),
	(263,129,'0','barcode'),
	(260,129,'1','partnumber'),
	(261,129,'4','description'),
	(262,129,'5','retail_price'),
	(268,130,'0','barcode'),
	(265,130,'1','partnumber'),
	(266,130,'4','description'),
	(267,130,'5','retail_price'),
	(273,131,'0','barcode'),
	(270,131,'1','partnumber'),
	(271,131,'4','description'),
	(272,131,'5','retail_price'),
	(278,132,'0','barcode'),
	(275,132,'1','partnumber'),
	(276,132,'4','description'),
	(277,132,'5','retail_price'),
	(283,133,'0','barcode'),
	(280,133,'1','partnumber'),
	(281,133,'4','description'),
	(282,133,'5','retail_price'),
	(288,134,'0','barcode'),
	(285,134,'1','partnumber'),
	(286,134,'4','description'),
	(287,134,'5','retail_price'),
	(293,135,'0','barcode'),
	(290,135,'1','partnumber'),
	(291,135,'4','description'),
	(292,135,'5','retail_price'),
	(298,136,'0','barcode'),
	(295,136,'1','partnumber'),
	(296,136,'4','description'),
	(297,136,'5','retail_price'),
	(303,137,'0','barcode'),
	(300,137,'1','partnumber'),
	(301,137,'4','description'),
	(302,137,'5','retail_price'),
	(308,138,'0','barcode'),
	(305,138,'1','partnumber'),
	(306,138,'4','description'),
	(307,138,'5','retail_price');

/*!40000 ALTER TABLE `field_mappings` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table full_imports
# ------------------------------------------------------------

DROP VIEW IF EXISTS `full_imports`;

CREATE TABLE `full_imports` (
   `id` INT(11) NOT NULL DEFAULT '0',
   `import_id` INT(11) NULL DEFAULT NULL,
   `file_name` VARCHAR(100) NULL DEFAULT NULL,
   `processed_date` DATE NULL DEFAULT NULL,
   `manufacturer_id` INT(11) NULL DEFAULT NULL,
   `user_id` INT(11) NULL DEFAULT NULL,
   `processed` TINYINT(4) NULL DEFAULT NULL,
   `rows` INT(11) NULL DEFAULT NULL,
   `uploaded_file_name` VARCHAR(45) NULL DEFAULT NULL,
   `effective_date` DATE NULL DEFAULT NULL,
   `manufacurer_name` VARCHAR(45) NULL DEFAULT NULL,
   `manufacturer_code` VARCHAR(15) NULL DEFAULT NULL,
   `user_name` VARCHAR(45) NULL DEFAULT NULL,
   `user_email` VARCHAR(45) NULL DEFAULT NULL
) ENGINE=MyISAM;



# Dump of table imports
# ------------------------------------------------------------

DROP TABLE IF EXISTS `imports`;

CREATE TABLE `imports` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `import_id` int(11) DEFAULT NULL,
  `file_name` varchar(100) DEFAULT NULL,
  `processed_date` date DEFAULT NULL,
  `manufacturer_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `processed` tinyint(4) DEFAULT NULL,
  `rows` int(11) DEFAULT NULL,
  `uploaded_file_name` varchar(45) DEFAULT NULL,
  `effective_date` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=69 DEFAULT CHARSET=latin1;

LOCK TABLES `imports` WRITE;
/*!40000 ALTER TABLE `imports` DISABLE KEYS */;

INSERT INTO `imports` (`id`, `import_id`, `file_name`, `processed_date`, `manufacturer_id`, `user_id`, `processed`, `rows`, `uploaded_file_name`, `effective_date`)
VALUES
	(1,1,'draper2017.txt','2018-05-16',1,1,1,12132,'2018-05-16-10-58.txt','2018-05-16'),
	(2,2,'draper2018.txt','2018-05-16',1,1,1,9881,'2018-05-16-11-19.txt','2018-05-16'),
	(25,2,'draper2018.txt','2018-05-17',117,1,1,9881,'2018-05-17-17-14-28.txt','2018-05-17'),
	(24,1,'draper2017.txt','2018-05-17',117,1,1,12132,'2018-05-17-17-14-22.txt','2018-05-17'),
	(26,1,'draper2017.txt','2018-05-17',119,1,0,0,'2018-05-17-19-25-27.txt','2018-05-17'),
	(27,1,'draper2017.txt','2018-05-17',120,1,0,0,'2018-05-17-19-32-03.txt','2018-05-17'),
	(28,1,'draper2017.txt','2018-05-17',121,1,1,12132,'2018-05-17-19-38-11.txt','2018-05-17'),
	(29,2,'draper2018.txt','2018-05-17',121,1,1,9881,'2018-05-17-19-38-16.txt','2018-05-17'),
	(30,1,'draper2017.txt','2018-05-17',122,1,1,12132,'2018-05-17-19-53-39.txt','2018-05-17'),
	(31,2,'draper2018.txt','2018-05-17',122,1,1,9881,'2018-05-17-19-53-42.txt','2018-05-17'),
	(32,1,'draper2017.txt','2018-05-17',123,1,1,12132,'2018-05-17-19-55-00.txt','2018-05-17'),
	(33,2,'draper2018.txt','2018-05-17',123,1,1,9881,'2018-05-17-19-55-04.txt','2018-05-17'),
	(34,1,'draper2017.txt','2018-05-17',124,1,1,12132,'2018-05-17-19-56-05.txt','2018-05-17'),
	(35,2,'draper2018.txt','2018-05-17',124,1,1,9881,'2018-05-17-19-56-09.txt','2018-05-17'),
	(36,1,'draper2017.txt','2018-05-17',125,1,1,12132,'2018-05-17-19-57-28.txt','2018-05-17'),
	(37,2,'draper2018.txt','2018-05-17',125,1,1,9881,'2018-05-17-19-57-31.txt','2018-05-17'),
	(38,1,'draper2017.txt','2018-05-17',126,1,1,12132,'2018-05-17-20-02-33.txt','2018-05-17'),
	(39,2,'draper2018.txt','2018-05-17',126,1,1,9881,'2018-05-17-20-02-36.txt','2018-05-17'),
	(40,1,'draper2017.txt','2018-05-17',127,1,1,12132,'2018-05-17-20-56-36.txt','2018-05-17'),
	(41,2,'draper2018.txt','2018-05-17',127,1,1,9881,'2018-05-17-20-56-40.txt','2018-05-17'),
	(42,1,'draper2017.txt','2018-05-17',128,1,1,12132,'2018-05-17-21-02-19.txt','2018-05-17'),
	(43,2,'draper2018.txt','2018-05-17',128,1,1,9881,'2018-05-17-21-02-24.txt','2018-05-17'),
	(44,1,'draper2017.txt','2018-05-17',129,1,1,12132,'2018-05-17-21-05-24.txt','2018-05-17'),
	(45,2,'draper2018.txt','2018-05-17',129,1,1,9881,'2018-05-17-21-05-28.txt','2018-05-17'),
	(46,1,'draper2017.txt','2018-05-17',130,1,1,12132,'2018-05-17-21-07-07.txt','2018-05-17'),
	(47,2,'draper2018.txt','2018-05-17',130,1,1,9881,'2018-05-17-21-07-11.txt','2018-05-17'),
	(48,1,'draper2017.txt','2018-05-17',131,1,1,12132,'2018-05-17-21-09-35.txt','2018-05-17'),
	(49,2,'draper2018.txt','2018-05-17',131,1,1,9881,'2018-05-17-21-09-38.txt','2018-05-17'),
	(50,1,'draper2017.txt','2018-05-17',132,1,1,12132,'2018-05-17-21-17-34.txt','2018-05-17'),
	(51,2,'draper2018.txt','2018-05-17',132,1,1,9881,'2018-05-17-21-17-38.txt','2018-05-17'),
	(52,1,'draper2017.txt','2018-05-17',133,1,1,12132,'2018-05-17-21-18-28.txt','2018-05-17'),
	(53,2,'draper2018.txt','2018-05-17',133,1,1,9881,'2018-05-17-21-18-32.txt','2018-05-17'),
	(54,1,'draper2017.txt','2018-05-17',134,1,1,12132,'2018-05-17-21-26-28.txt','2018-05-17'),
	(55,2,'draper2018.txt','2018-05-17',134,1,1,9881,'2018-05-17-21-26-33.txt','2018-05-17'),
	(56,1,'draper2017.txt','2018-05-17',135,1,1,12132,'2018-05-17-21-28-06.txt','2018-05-17'),
	(57,2,'draper2018.txt','2018-05-17',135,1,1,9881,'2018-05-17-21-28-10.txt','2018-05-17'),
	(58,1,'draper2017.txt','2018-05-18',136,1,1,12132,'2018-05-18-11-32-19.txt','2018-05-18'),
	(59,2,'draper2018.txt','2018-05-18',136,1,1,9881,'2018-05-18-11-32-23.txt','2018-05-18'),
	(60,1,'draper2017.txt','2018-05-18',137,1,1,12132,'2018-05-18-12-29-40.txt','2018-05-18'),
	(61,2,'draper2018.txt','2018-05-18',137,1,1,9881,'2018-05-18-12-29-43.txt','2018-05-18'),
	(62,1,'draper2017.txt','2018-05-18',138,1,1,12132,'2018-05-18-20-26-09.txt','2018-05-18'),
	(63,2,'draper2018.txt','2018-05-18',138,1,1,9881,'2018-05-18-20-26-12.txt','2018-05-18'),
	(64,3,'test.txt','2018-05-31',1,1,0,0,'2018-05-31-20-41-03.txt','2018-05-31'),
	(65,4,'test.txt','2018-05-31',1,1,0,0,'2018-05-31-20-44-03.txt','2018-05-31'),
	(66,5,'test.txt','2018-05-31',1,1,0,0,'2018-05-31-21-08-17.txt','2018-05-31'),
	(67,6,'test.txt','2018-05-31',1,1,0,0,'2018-05-31-21-13-46.txt','2018-05-31'),
	(68,7,'test.txt','2018-05-31',1,1,0,0,'2018-05-31-21-14-29.txt','2018-05-31');

/*!40000 ALTER TABLE `imports` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table manufacturers
# ------------------------------------------------------------

DROP TABLE IF EXISTS `manufacturers`;

CREATE TABLE `manufacturers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `code` varchar(15) DEFAULT NULL,
  `inputtype` varchar(45) DEFAULT NULL,
  `headers` int(11) DEFAULT NULL,
  `trailers` int(11) DEFAULT NULL,
  `prepared_date` datetime DEFAULT NULL,
  `base_date` datetime DEFAULT NULL,
  `effective_date` datetime DEFAULT NULL,
  `currency_code` varchar(10) DEFAULT NULL,
  `decimal_places` int(11) DEFAULT NULL,
  `country_of_use` varchar(45) DEFAULT NULL,
  `original_currency` varchar(10) DEFAULT NULL,
  `continental_format` tinyint(4) DEFAULT NULL,
  `conversion_factor` varchar(10) DEFAULT NULL,
  `convert_pack_to_unit` tinyint(3) unsigned DEFAULT NULL,
  `convert_weight_to_unit` tinyint(4) DEFAULT NULL,
  `convert_from_vat` tinyint(4) DEFAULT NULL,
  `reformat_partnumber` tinyint(4) DEFAULT NULL,
  `reformat_description` tinyint(4) DEFAULT NULL,
  `pad_record` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `inputtype_UNIQUE` (`inputtype`)
) ENGINE=MyISAM AUTO_INCREMENT=146 DEFAULT CHARSET=latin1;

LOCK TABLES `manufacturers` WRITE;
/*!40000 ALTER TABLE `manufacturers` DISABLE KEYS */;

INSERT INTO `manufacturers` (`id`, `name`, `code`, `inputtype`, `headers`, `trailers`, `prepared_date`, `base_date`, `effective_date`, `currency_code`, `decimal_places`, `country_of_use`, `original_currency`, `continental_format`, `conversion_factor`, `convert_pack_to_unit`, `convert_weight_to_unit`, `convert_from_vat`, `reformat_partnumber`, `reformat_description`, `pad_record`)
VALUES
	(1,'changed','DR','TSV',1,0,NULL,NULL,NULL,'GBP',2,'GB','GBP',1,'5',0,0,0,1,1,1),
	(117,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(118,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(119,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(120,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(121,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(122,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(123,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(124,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(125,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(126,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(127,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(128,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(129,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(130,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(131,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(132,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(133,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(134,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(135,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(136,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(137,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(138,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(139,'changed','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(140,'Test Manufacturer','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(141,'changed','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(142,'changed','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(143,'changed','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(144,'changed','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0),
	(145,'changed','TC',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,NULL,0,0,0,0,0,0);

/*!40000 ALTER TABLE `manufacturers` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table prices
# ------------------------------------------------------------

DROP TABLE IF EXISTS `prices`;

CREATE TABLE `prices` (
  `import_id` int(11) DEFAULT NULL,
  `partnumber` varchar(20) NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  `retail_price` float DEFAULT NULL,
  `barcode` varchar(45) DEFAULT NULL,
  `buying_category` varchar(45) DEFAULT NULL,
  `weight` varchar(45) DEFAULT NULL,
  `pack_quantity` varchar(45) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

LOCK TABLES `prices` WRITE;
/*!40000 ALTER TABLE `prices` DISABLE KEYS */;

INSERT INTO `prices` (`import_id`, `partnumber`, `description`, `retail_price`, `barcode`, `buying_category`, `weight`, `pack_quantity`)
VALUES
	(1,'1','same prices',10,NULL,NULL,NULL,NULL),
	(2,'1','same prices',10,NULL,NULL,NULL,NULL),
	(1,'2','changed prices',10,NULL,NULL,NULL,NULL),
	(2,'2','changed prices',20,NULL,NULL,NULL,NULL),
	(2,'3','added',10,NULL,NULL,NULL,NULL),
	(1,'4','deleted',20,NULL,NULL,NULL,NULL);

/*!40000 ALTER TABLE `prices` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table users
# ------------------------------------------------------------

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COMMENT='				';

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;

INSERT INTO `users` (`id`, `name`, `email`)
VALUES
	(1,'Steve Lamb','steve.lamb@ibcos.co.uk');

/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;




# Replace placeholder table for full_imports with correct view syntax
# ------------------------------------------------------------

DROP TABLE `full_imports`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `full_imports`
AS SELECT
   `i`.`id` AS `id`,
   `i`.`import_id` AS `import_id`,
   `i`.`file_name` AS `file_name`,
   `i`.`processed_date` AS `processed_date`,
   `i`.`manufacturer_id` AS `manufacturer_id`,
   `i`.`user_id` AS `user_id`,
   `i`.`processed` AS `processed`,
   `i`.`rows` AS `rows`,
   `i`.`uploaded_file_name` AS `uploaded_file_name`,
   `i`.`effective_date` AS `effective_date`,
   `m`.`name` AS `manufacurer_name`,
   `m`.`code` AS `manufacturer_code`,
   `u`.`name` AS `user_name`,
   `u`.`email` AS `user_email`
FROM ((`imports` `i` join `manufacturers` `m` on((`m`.`id` = `i`.`manufacturer_id`))) join `users` `u` on((`u`.`id` = `i`.`user_id`)));

/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
