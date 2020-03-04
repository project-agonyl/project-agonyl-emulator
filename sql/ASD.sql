-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: ASD
-- Source Schemata: ASD
-- Created: Wed Mar  4 12:04:40 2020
-- Workbench Version: 8.0.19
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema ASD
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `ASD` ;
CREATE SCHEMA IF NOT EXISTS `ASD` ;

-- ----------------------------------------------------------------------------
-- Table ASD.v_beta
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`v_beta` (
  `AccountID` CHAR(20) NOT NULL,
  `Motive` VARCHAR(200) NULL,
  `Result` CHAR(1) NOT NULL,
  `UserName` CHAR(20) NOT NULL,
  `SCN` CHAR(15) NOT NULL,
  `ResultDate` DATETIME NULL,
  `ResultUser` CHAR(20) NULL,
  `ResultNo` INT NOT NULL,
  `ResultDesc` VARCHAR(2000) NULL,
  `AuthType` CHAR(1) NOT NULL,
  `RegistDate` DATETIME NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.UserTicket
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`UserTicket` (
  `AccountID` CHAR(20) NOT NULL,
  `TicketNo` CHAR(12) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.UpdateLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`UpdateLog` (
  `UpdateLogID` INT NOT NULL,
  `ManageID` CHAR(20) NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `UpdateContent` VARCHAR(3000) NOT NULL,
  `LogDate` DATETIME NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.StatusLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`StatusLog` (
  `StatusLogID` INT NOT NULL,
  `ManageID` CHAR(20) NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `Status` CHAR(1) NOT NULL,
  `StartDate` DATETIME NOT NULL,
  `EndDate` DATETIME NOT NULL,
  `Content` VARCHAR(1000) NOT NULL,
  `LogDate` DATETIME NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Sheet1
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Sheet1` (
  `F1` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.RestoreRequest
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`RestoreRequest` (
  `RestoreRequestID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `SCN` CHAR(14) NOT NULL,
  `Result` CHAR(1) NOT NULL,
  `RequestDate` DATETIME NOT NULL,
  `ResultDate` DATETIME NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.ReservedPresent
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`ReservedPresent` (
  `AccountID` CHAR(20) NOT NULL,
  `SeatName` VARCHAR(49) NULL,
  `PresentType` VARCHAR(20) NOT NULL,
  `Present` VARCHAR(100) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.ReservedChar
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`ReservedChar` (
  `AccountID` CHAR(20) NOT NULL,
  `CharName` VARCHAR(50) NOT NULL,
  `ServerIdx` TINYINT UNSIGNED NOT NULL,
  `CharClass` TINYINT UNSIGNED NOT NULL,
  `Nation` TINYINT UNSIGNED NOT NULL,
  `GroupSeatID` INT NULL,
  `RegistDate` DATETIME NOT NULL,
  `Sex` TINYINT UNSIGNED NOT NULL,
  `Name` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.RandChar
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`RandChar` (
  `RandNo` INT NOT NULL,
  `Rand` INT NULL,
  `AccountID` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.QuestResponse
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`QuestResponse` (
  `QuestNo` TINYINT UNSIGNED NOT NULL,
  `AnswerNo` TINYINT UNSIGNED NOT NULL,
  `AccountID` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.QuestList
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`QuestList` (
  `QuestNo` TINYINT UNSIGNED NOT NULL,
  `Content` VARCHAR(50) NOT NULL,
  `QuestFlag` TINYINT(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.PcbangList
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`PcbangList` (
  `AccountID` CHAR(20) NOT NULL,
  `PcbangCode` VARCHAR(12) NOT NULL,
  `PcbangName` VARCHAR(50) NOT NULL,
  `Owner` VARCHAR(20) NOT NULL,
  `SCN` CHAR(14) NOT NULL,
  `PcbangAddress` VARCHAR(255) NOT NULL,
  `PcbangZipcode` CHAR(7) NOT NULL,
  `OwnerAddress` VARCHAR(255) NOT NULL,
  `OwnerZipcode` CHAR(7) NOT NULL,
  `PcbangTel` CHAR(14) NOT NULL,
  `Uptae` VARCHAR(100) NOT NULL,
  `OpenDate` VARCHAR(10) NOT NULL,
  `Upzong` VARCHAR(100) NOT NULL,
  `Semuser` VARCHAR(30) NOT NULL,
  `RequestDate` DATETIME NOT NULL,
  `Result` CHAR(1) NOT NULL,
  `ResultDate` DATETIME NULL,
  `ResultDesc` VARCHAR(1000) NOT NULL,
  `ResultUser` CHAR(20) NOT NULL,
  `ResultNo` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.OutAccount
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`OutAccount` (
  `OutAccountID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `OutDate` DATETIME NOT NULL,
  `Result` CHAR(1) NOT NULL,
  `ResultUser` VARCHAR(20) NULL,
  `ResultDesc` VARCHAR(4000) NULL,
  `Reason` VARCHAR(1000) NULL,
  `RestoreDate` DATETIME NULL,
  `SCN` VARCHAR(14) NOT NULL,
  `PrevStatus` CHAR(1) NOT NULL,
  `ResultDate` DATETIME NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.LottoEvent
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`LottoEvent` (
  `LottoEventID` SMALLINT NOT NULL,
  `EventName` VARCHAR(30) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Lotto
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Lotto` (
  `LottoEventID` SMALLINT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `SelectNum1` TINYINT UNSIGNED NOT NULL,
  `SelectNum2` TINYINT UNSIGNED NOT NULL,
  `SelectNum3` TINYINT UNSIGNED NOT NULL,
  `SelectNum4` TINYINT UNSIGNED NOT NULL,
  `SelectNum5` TINYINT UNSIGNED NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.LotteryTicket
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`LotteryTicket` (
  `LotteryTicketID` BIGINT NOT NULL,
  `IsUsed` CHAR(1) NOT NULL,
  `TicketNo` CHAR(12) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.LoginLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`LoginLog` (
  `SCN` CHAR(13) NOT NULL,
  `LoginDate` DATETIME NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Job
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Job` (
  `JobID` CHAR(1) NOT NULL,
  `JobName` VARCHAR(100) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.ItemStorage0
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`ItemStorage0` (
  `c_id` CHAR(20) NOT NULL,
  `c_sheadera` VARCHAR(255) NOT NULL,
  `c_sheaderb` VARCHAR(255) NOT NULL,
  `c_sheaderc` VARCHAR(255) NOT NULL,
  `c_headera` VARCHAR(255) NOT NULL,
  `c_headerb` VARCHAR(255) NOT NULL,
  `c_headerc` VARCHAR(255) NOT NULL,
  `d_cdate` DATETIME NULL,
  `d_udate` DATETIME NULL,
  `c_status` CHAR(1) NOT NULL,
  `m_body` LONGTEXT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.InnerAccount
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`InnerAccount` (
  `AccountID` CHAR(20) NOT NULL,
  `Desc` VARCHAR(500) NOT NULL,
  `CreateDate` DATETIME NOT NULL,
  `Creater` CHAR(8) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.GroupSeat
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`GroupSeat` (
  `GroupSeatID` INT NOT NULL,
  `Master` CHAR(20) NOT NULL,
  `SeatName` VARCHAR(40) NOT NULL,
  `SeatType` CHAR(1) NOT NULL,
  `SeatPassword` VARCHAR(15) NOT NULL,
  `ServerIdx` TINYINT UNSIGNED NOT NULL,
  `CntRegist` TINYINT UNSIGNED NOT NULL,
  `Name` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.GiftInfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`GiftInfo` (
  `account` CHAR(50) NULL,
  `gift` CHAR(50) NULL,
  `time` VARBINARY(8) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.GameServer
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`GameServer` (
  `ServerIdx` TINYINT UNSIGNED NOT NULL,
  `ServerName` CHAR(16) NOT NULL,
  `CntRegist` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.GameLoginLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`GameLoginLog` (
  `LoginIdx` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `LoginIP` VARCHAR(15) NOT NULL,
  `LoginDate` DATETIME NOT NULL,
  `PayMode` CHAR(3) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.GameBroadcast
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`GameBroadcast` (
  `GameBroadcastID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `RequestDate` DATETIME NOT NULL,
  `Job` VARCHAR(200) NOT NULL,
  `Motive` VARCHAR(2000) NOT NULL,
  `Intro` VARCHAR(2000) NOT NULL,
  `FilePath` VARCHAR(200) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Activation
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Activation` (
  `act_id` VARCHAR(50) NOT NULL,
  `account` VARCHAR(50) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.FRIEND0
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`FRIEND0` (
  `CharName` CHAR(20) NOT NULL,
  `GroupInfo` VARCHAR(1024) NOT NULL,
  `FriendInfo` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.FaultMailAccount
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`FaultMailAccount` (
  `AccountID` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.onlinerecords
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`onlinerecords` (
  `charname` VARCHAR(25) NULL,
  `charid` VARCHAR(25) NULL,
  `login_time` VARCHAR(25) NULL,
  `logout_time` VARCHAR(25) NULL,
  `online_time` VARCHAR(25) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.playerpkinfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`playerpkinfo` (
  `pker` VARCHAR(50) NULL,
  `pker_rb` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `pker_lvl` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `pked` VARCHAR(50) NULL,
  `pked_rb` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `pked_lvl` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `loc` VARCHAR(50) NULL,
  `time` VARCHAR(50) NULL,
  `pker_nation` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `pked_nation` CHAR(10) CHARACTER SET 'utf8mb4' NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.DenyChar
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`DenyChar` (
  `DenyCharID` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Count
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Count` (
  `count` INT NOT NULL DEFAULT 0);

-- ----------------------------------------------------------------------------
-- Table ASD.ClanMember
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`ClanMember` (
  `ClanID` INT NULL,
  `ServerID` INT NULL,
  `CharName` VARCHAR(50) CHARACTER SET 'utf8mb4' NOT NULL,
  `Level` INT NULL,
  `Class` INT NULL,
  `Rank` INT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.ClanInfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`ClanInfo` (
  `ClanID` INT NOT NULL,
  `ServerID` VARCHAR(20) NULL,
  `ClanName` VARCHAR(20) CHARACTER SET 'utf8mb4' NULL,
  `MasterName` VARCHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `Nation` VARCHAR(20) NULL,
  `MarkID` VARCHAR(20) NULL,
  `CDate` DATETIME NULL,
  `DDate` DATETIME NULL,
  `ClanPasswd` VARCHAR(20) NULL,
  `ClanRank` VARCHAR(20) NULL,
  `ClanStatus` VARCHAR(20) NULL,
  `StorageID` VARCHAR(20) NULL,
  `AgitID` INT NULL,
  `WinCount` INT NULL,
  `LoseCount` INT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.clan
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`clan` (
  `ClanID` CHAR(2) NULL,
  `ServerID` CHAR(1) NULL,
  `ClanName` CHAR(10) NULL,
  `Nation` CHAR(10) NULL,
  `MarkID` CHAR(10) NULL,
  `CDate` DATETIME NULL,
  `DDate` DATETIME NULL,
  `ClanPasswd` CHAR(10) NULL,
  `ClanRank` CHAR(10) NULL,
  `ClanStatus` CHAR(1) NULL,
  `StorageID` CHAR(10) NULL,
  `AgitID` CHAR(10) NULL,
  `WinCount` CHAR(10) NULL,
  `LoseCount` CHAR(10) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.CharInfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`CharInfo` (
  `AccountID` CHAR(21) NULL,
  `ServerIdx` CHAR(2) NULL,
  `CharName` CHAR(21) NULL,
  `Class` CHAR(2) NULL,
  `Nation` CHAR(2) NULL,
  `default` CHAR(10) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.charac0
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`charac0` (
  `c_id` CHAR(20) NOT NULL,
  `c_sheadera` VARCHAR(255) NOT NULL,
  `c_sheaderb` VARCHAR(255) NOT NULL,
  `c_sheaderc` VARCHAR(255) NOT NULL,
  `c_headera` VARCHAR(255) NOT NULL,
  `c_headerb` VARCHAR(255) NOT NULL,
  `c_headerc` VARCHAR(255) NOT NULL,
  `d_cdate` DATETIME NULL,
  `d_udate` DATETIME NULL,
  `c_status` CHAR(1) NOT NULL,
  `m_body` VARCHAR(4000) NOT NULL,
  `rb` INT NOT NULL DEFAULT '0',
  `set_gift` INT NOT NULL DEFAULT '0',
  `online` CHAR(10) NULL,
  `c_reset` INT NOT NULL DEFAULT 1,
  `rc_event` INT NOT NULL DEFAULT 1);

-- ----------------------------------------------------------------------------
-- Table ASD.cdg_mondrops
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`cdg_mondrops` (
  `monsterid` INT NOT NULL DEFAULT 0,
  `itemcode` INT NOT NULL DEFAULT 0,
  `dropcount` BIGINT NOT NULL DEFAULT 0,
  `monstername` VARCHAR(20) NULL DEFAULT '');

-- ----------------------------------------------------------------------------
-- Table ASD.cdg_icdata
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`cdg_icdata` (
  `item1` INT NOT NULL DEFAULT 0,
  `item2` INT NOT NULL DEFAULT 0,
  `item3` INT NOT NULL DEFAULT 0,
  `item4` INT NOT NULL DEFAULT 0,
  `item5` INT NOT NULL DEFAULT 0,
  `item6` INT NOT NULL DEFAULT 0,
  `item7` INT NOT NULL DEFAULT 0,
  `item8` INT NOT NULL DEFAULT 0,
  `item9` INT NOT NULL DEFAULT 0,
  `item10` INT NOT NULL DEFAULT 0,
  `item11` INT NOT NULL DEFAULT 0,
  `item12` INT NOT NULL DEFAULT 0,
  `item13` INT NOT NULL DEFAULT 0,
  `item14` INT NOT NULL DEFAULT 0,
  `item15` INT NOT NULL DEFAULT 0,
  `item16` INT NOT NULL DEFAULT 0);

-- ----------------------------------------------------------------------------
-- Table ASD.ChristmasGiftInfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`ChristmasGiftInfo` (
  `gift_code` INT NOT NULL,
  `gift_name` VARCHAR(50) NOT NULL,
  `gift_item_code` VARCHAR(100) NOT NULL,
  `gift_points` INT NOT NULL,
  `gift_took` INT NOT NULL,
  `gift_taken` VARCHAR(50) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.CurrentGift
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`CurrentGift` (
  `account` VARCHAR(50) NOT NULL,
  `current_gift` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BlackList
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BlackList` (
  `BlackListID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `BlockStartDate` DATETIME NOT NULL,
  `BlockEndDate` DATETIME NOT NULL,
  `AccountStatus` CHAR(1) NOT NULL,
  `Status` CHAR(1) NOT NULL,
  `Content` VARCHAR(1000) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaWebLoginLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaWebLoginLog` (
  `BetaWebLoginLogID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `IpAddr` CHAR(15) NOT NULL,
  `LoginDate` DATETIME NOT NULL,
  `LoginCheck` TINYINT UNSIGNED NOT NULL,
  `AccessDeny` TINYINT(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaTester4
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaTester4` (
  `BetaID` INT NOT NULL,
  `BetaRandNum` INT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `Scn` VARCHAR(20) NULL,
  `ZipCode` VARCHAR(10) NULL,
  `Region` VARCHAR(41) NULL,
  `Age` VARCHAR(5) NULL,
  `Sex` TINYINT UNSIGNED NULL,
  `Result` CHAR(1) NULL,
  `BetaNum` TINYINT UNSIGNED NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaTester3
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaTester3` (
  `BetaID` INT NOT NULL,
  `BetaRandNum` INT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `Scn` VARCHAR(20) NULL,
  `ZipCode` VARCHAR(10) NULL,
  `Region` VARCHAR(41) NULL,
  `Age` VARCHAR(5) NULL,
  `Sex` TINYINT UNSIGNED NULL,
  `Result` CHAR(1) NULL,
  `BetaNum` TINYINT UNSIGNED NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaTester2
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaTester2` (
  `BetaID` INT NOT NULL,
  `BetaRandNum` INT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `Scn` VARCHAR(20) NULL,
  `ZipCode` VARCHAR(10) NULL,
  `Region` VARCHAR(41) NULL,
  `Age` VARCHAR(5) NULL,
  `Sex` TINYINT UNSIGNED NULL,
  `Result` CHAR(1) NULL,
  `BetaNum` TINYINT UNSIGNED NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaTester1
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaTester1` (
  `BetaID` INT NOT NULL,
  `BetaRandNum` INT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `Scn` VARCHAR(20) NULL,
  `Zipcode` VARCHAR(10) NULL,
  `Region` VARCHAR(10) NULL,
  `Age` VARCHAR(5) NULL,
  `Sex` TINYINT UNSIGNED NULL,
  `Result` CHAR(1) NULL,
  `BetaNum` TINYINT UNSIGNED NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaTester
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaTester` (
  `AccountID` CHAR(20) NOT NULL,
  `Motive` VARCHAR(200) NULL,
  `Result` CHAR(1) NOT NULL,
  `UserName` CHAR(20) NOT NULL,
  `SCN` CHAR(15) NOT NULL,
  `ResultDate` DATETIME NULL,
  `ResultUser` CHAR(20) NULL,
  `ResultNo` INT NOT NULL,
  `ResultDesc` VARCHAR(2000) NULL,
  `AuthType` CHAR(1) NOT NULL,
  `RegistDate` DATETIME NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaPresent
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaPresent` (
  `AccountID` CHAR(20) NOT NULL,
  `Present` CHAR(1) NOT NULL,
  `PresentType` CHAR(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaPcbangIP
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaPcbangIP` (
  `AccountID` CHAR(20) NOT NULL,
  `IP` VARCHAR(15) NOT NULL,
  `Result` CHAR(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.charloginlog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`charloginlog` (
  `c_id` VARCHAR(50) NOT NULL,
  `rb` INT NULL,
  `lvl` INT NULL,
  `datetime` VARCHAR(25) NULL,
  `class` LONGTEXT NULL,
  `Nation` INT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaPcbang
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaPcbang` (
  `AccountID` CHAR(20) NOT NULL,
  `PcbangCode` VARCHAR(12) NULL,
  `PcbangName` VARCHAR(50) NULL,
  `Owner` VARCHAR(20) NULL,
  `Scn` CHAR(14) NULL,
  `OpenDate` VARCHAR(10) NULL,
  `PcbangAddress` VARCHAR(255) NULL,
  `PcbangZipcode` CHAR(7) NULL,
  `OwnerAddress` VARCHAR(255) NULL,
  `OwnerZipcode` CHAR(7) NULL,
  `Uptae` VARCHAR(100) NULL,
  `Upzong` VARCHAR(100) NULL,
  `Semuser` VARCHAR(30) NULL,
  `AccountIDStatus` CHAR(1) NOT NULL,
  `RequestDate` DATETIME NOT NULL,
  `Result` CHAR(1) NOT NULL,
  `ResultDesc` VARCHAR(1000) NULL,
  `ResultUser` CHAR(20) NULL,
  `ResultDate` DATETIME NULL,
  `ResultNo` INT NOT NULL,
  `ResultType` CHAR(1) NOT NULL,
  `Tel` VARCHAR(20) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.BetaArgee
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`BetaArgee` (
  `AccountID` CHAR(20) NOT NULL,
  `AgreeStatus` TINYINT(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Charrecord
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Charrecord` (
  `c_id` VARCHAR(50) NOT NULL,
  `info` VARCHAR(600) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Beta4
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Beta4` (
  `AccountID` CHAR(20) NOT NULL,
  `CntLogin` INT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Itemlog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Itemlog` (
  `charname` VARCHAR(25) NOT NULL,
  `tocharname` VARCHAR(250) NOT NULL,
  `ip` VARCHAR(20) NULL,
  `toip` VARCHAR(20) NULL,
  `itemname` VARCHAR(50) NULL,
  `itemcode` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `uniqcode` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `loc` CHAR(12) NULL,
  `date` DATETIME NULL,
  `shopname` CHAR(20) NULL,
  `event` CHAR(25) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Beta3_1
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Beta3_1` (
  `BetaID` INT NOT NULL,
  `BetaRandNum` INT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `Scn` VARCHAR(20) NULL,
  `Zipcode` VARCHAR(10) NULL,
  `Region` VARCHAR(10) NULL,
  `Age` VARCHAR(5) NULL,
  `Sex` TINYINT UNSIGNED NULL,
  `Result` CHAR(1) NULL,
  `BetaNum` TINYINT UNSIGNED NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.AccountInfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`AccountInfo` (
  `account` CHAR(99) NULL,
  `contact` CHAR(99) NULL,
  `name` CHAR(99) NULL,
  `email` CHAR(100) NULL,
  `ip` CHAR(50) NOT NULL DEFAULT '127.0.0.1',
  `login_ip` CHAR(50) NOT NULL DEFAULT '127.0.0.1',
  `event_points` INT NOT NULL DEFAULT 0,
  `cevent_points` INT NOT NULL,
  `refresh_count` INT NOT NULL,
  `ref_add_allow` INT NOT NULL,
  `referer` CHAR(99) NULL,
  `flamez_coins` DOUBLE NOT NULL DEFAULT 0);

-- ----------------------------------------------------------------------------
-- Table ASD.Banlist
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Banlist` (
  `c_id` VARCHAR(50) NOT NULL,
  `account` VARCHAR(50) NOT NULL,
  `tim` BIGINT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.sysdiagrams
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`sysdiagrams` (
  `name` VARCHAR(160) NOT NULL,
  `principal_id` INT NOT NULL,
  `diagram_id` INT NOT NULL,
  `version` INT NULL,
  `definition` LONGBLOB NULL,
  PRIMARY KEY (`diagram_id`),
  UNIQUE INDEX `UK_principal_name` (`principal_id` ASC, `name` ASC) VISIBLE);

-- ----------------------------------------------------------------------------
-- Table ASD.Ban_IP
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Ban_IP` (
  `List_IP` VARCHAR(50) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.bak
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`bak` (
  `c_id` CHAR(20) NOT NULL,
  `c_sheadera` VARCHAR(255) NOT NULL,
  `c_sheaderb` VARCHAR(255) NOT NULL,
  `c_sheaderc` VARCHAR(255) NOT NULL,
  `c_headera` VARCHAR(255) NOT NULL,
  `c_headerb` VARCHAR(255) NOT NULL,
  `c_headerc` VARCHAR(255) NOT NULL,
  `d_cdate` DATETIME NULL,
  `d_udate` DATETIME NULL,
  `c_status` CHAR(1) NOT NULL,
  `m_body` VARCHAR(4000) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.AuthLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`AuthLog` (
  `AuthLogID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `AuthType` CHAR(1) NOT NULL,
  `AuthDate` DATETIME NOT NULL,
  `Result` CHAR(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Answer
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Answer` (
  `QuestNo` TINYINT UNSIGNED NOT NULL,
  `AnswerNo` TINYINT UNSIGNED NOT NULL,
  `Content` VARCHAR(100) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.AdultCheck
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`AdultCheck` (
  `Name` CHAR(20) NOT NULL,
  `SCN` CHAR(13) NOT NULL,
  `RegDate` DATETIME NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.AccountFailAuth
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`AccountFailAuth` (
  `FailAuthID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `SCN1` CHAR(15) NOT NULL,
  `SCN2` CHAR(15) NOT NULL,
  `AuthDate` DATETIME NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.AccountExt
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`AccountExt` (
  `AccountID` CHAR(20) NOT NULL,
  `Job` CHAR(1) NOT NULL,
  `RecomID` CHAR(20) NOT NULL,
  `EmailStatus` TINYINT(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.AccountAuth
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`AccountAuth` (
  `AccountID` CHAR(20) NOT NULL,
  `AuthType` CHAR(1) NOT NULL,
  `AuthDate` DATETIME NOT NULL,
  `Result` CHAR(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.account
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`account` (
  `c_id` CHAR(20) NOT NULL,
  `c_sheadera` VARCHAR(255) NOT NULL,
  `c_sheaderb` VARCHAR(255) NOT NULL,
  `c_sheaderc` VARCHAR(255) NOT NULL,
  `c_headera` VARCHAR(255) NOT NULL,
  `c_headerb` VARCHAR(255) NOT NULL,
  `c_headerc` VARCHAR(255) NOT NULL,
  `d_cdate` DATETIME NULL,
  `d_udate` DATETIME NULL,
  `c_status` CHAR(1) NOT NULL,
  `m_body` VARCHAR(255) NULL,
  `acc_status` VARCHAR(50) NOT NULL DEFAULT 'Normal',
  `salary` DATETIME NOT NULL,
  `last_salary` DATETIME NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.Deals
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`Deals` (
  `deal_id` INT NOT NULL,
  `character` VARCHAR(50) NOT NULL,
  `item_name` VARCHAR(100) NOT NULL,
  `item_code` VARCHAR(4000) NOT NULL,
  `flamez_coins` DOUBLE NOT NULL DEFAULT 0,
  `deal_status` INT NOT NULL DEFAULT 1,
  `bcharacter` VARCHAR(100) NULL DEFAULT 'none',
  `deal_time` VARCHAR(50) NULL,
  `seller_ip` VARCHAR(50) NOT NULL DEFAULT '127.0.0.1');

-- ----------------------------------------------------------------------------
-- Table ASD.ZipCode
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`ZipCode` (
  `zipidx` INT NOT NULL,
  `zipcode` CHAR(7) NOT NULL,
  `sido` VARCHAR(8) NOT NULL,
  `gugun` VARCHAR(11) NOT NULL,
  `dong` VARCHAR(41) NOT NULL,
  `note1` VARCHAR(26) NULL,
  `note2` VARCHAR(18) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.WebLoginReport
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`WebLoginReport` (
  `LoginYear` SMALLINT NOT NULL,
  `LoginMonth` TINYINT UNSIGNED NOT NULL,
  `LoginDay` TINYINT UNSIGNED NOT NULL,
  `LoginHour` TINYINT UNSIGNED NOT NULL,
  `CntSuccess` INT NOT NULL,
  `CntFailure` INT NOT NULL,
  `CntAccessDeny` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.WebLoginRecentLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`WebLoginRecentLog` (
  `AccountID` CHAR(20) NOT NULL,
  `LoginIP` CHAR(15) NOT NULL,
  `CntLoginFailure` INT NOT NULL,
  `CheckDate` DATETIME NOT NULL,
  `AccessDenyDate` DATETIME NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.WebLoginLog
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`WebLoginLog` (
  `WebLoginLogID` INT NOT NULL,
  `AccountID` CHAR(20) NOT NULL,
  `LoginIP` CHAR(15) NOT NULL,
  `LoginDate` DATETIME NOT NULL,
  `LoginSuccess` CHAR(1) NOT NULL,
  `AccessDeny` TINYINT(1) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vStorage
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vStorage` (
  `HistoryDate` DATETIME NOT NULL,
  `ServerID` INT NOT NULL,
  `AccountID` CHAR(20) NULL,
  `Money` VARCHAR(255) NULL,
  `CreateDate` DATETIME NULL,
  `LastDate` DATETIME NULL,
  `Status` CHAR(1) NULL,
  `BodyInfo` LONGTEXT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vStatPcbang2
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vStatPcbang2` (
  `Sido` VARCHAR(4) NULL,
  `TypeA` INT NULL,
  `TypeO` INT NULL,
  `Total` INT NULL,
  `Ratio` DOUBLE NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vStatPcbang1
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vStatPcbang1` (
  `Sido` VARCHAR(4) NULL,
  `Gugun` VARCHAR(6) NULL,
  `TypeA` INT NULL,
  `TypeO` INT NULL,
  `Total` INT NULL,
  `Ratio` DOUBLE NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vStatAuth2
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vStatAuth2` (
  `Result` CHAR(1) NULL,
  `AuthTypeB` INT NOT NULL,
  `AuthTypeC` INT NOT NULL,
  `AuthTypeD` INT NOT NULL,
  `AuthTypeE` INT NOT NULL,
  `Total` INT NOT NULL,
  `ResultName` VARCHAR(3) NOT NULL,
  `AuthTypeBP` DECIMAL(28,12) NULL,
  `AuthTypeCP` DECIMAL(28,12) NULL,
  `AuthTypeDP` DECIMAL(28,12) NULL,
  `AuthTypeEP` DECIMAL(28,12) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vStatAuth1
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vStatAuth1` (
  `AuthType` CHAR(1) NULL,
  `ResultO` INT NOT NULL,
  `ResultA` INT NOT NULL,
  `Total` INT NOT NULL,
  `AuthTypeName` VARCHAR(7) NOT NULL,
  `ResultOP` DECIMAL(28,12) NULL,
  `ResultAP` DECIMAL(28,12) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vItemStorage
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vItemStorage` (
  `c_id` CHAR(20) NULL,
  `c_sheadera` VARCHAR(255) NULL,
  `c_sheaderb` VARCHAR(255) NULL,
  `c_sheaderc` VARCHAR(255) NULL,
  `c_headera` VARCHAR(255) NULL,
  `c_headerb` VARCHAR(255) NULL,
  `c_headerc` VARCHAR(255) NULL,
  `d_cdate` DATETIME NULL,
  `d_udate` DATETIME NULL,
  `c_status` CHAR(1) NULL,
  `m_body` LONGTEXT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vcharac
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vcharac` (
  `c_id` CHAR(20) NULL,
  `c_sheadera` VARCHAR(255) NULL,
  `c_sheaderb` VARCHAR(255) NULL,
  `c_sheaderc` VARCHAR(255) NULL,
  `c_headera` VARCHAR(255) NULL,
  `c_headerb` VARCHAR(255) NULL,
  `c_headerc` VARCHAR(255) NULL,
  `d_cdate` DATETIME NULL,
  `d_udate` DATETIME NULL,
  `c_status` CHAR(1) NULL,
  `m_body` VARCHAR(4000) NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.vAdultAge
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`vAdultAge` (
  `age` INT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.v_vga
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`v_vga` (
  `QuestNo` TINYINT UNSIGNED NOT NULL,
  `AnswerNo` TINYINT UNSIGNED NOT NULL,
  `AccountID` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.v_ram
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`v_ram` (
  `QuestNo` TINYINT UNSIGNED NOT NULL,
  `AnswerNo` TINYINT UNSIGNED NOT NULL,
  `AccountID` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.v_os
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`v_os` (
  `QuestNo` TINYINT UNSIGNED NOT NULL,
  `AnswerNo` TINYINT UNSIGNED NOT NULL,
  `AccountID` CHAR(20) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table ASD.v_cpu
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `ASD`.`v_cpu` (
  `QuestNo` TINYINT UNSIGNED NOT NULL,
  `AnswerNo` TINYINT UNSIGNED NOT NULL,
  `AccountID` CHAR(20) NOT NULL);
SET FOREIGN_KEY_CHECKS = 1;
