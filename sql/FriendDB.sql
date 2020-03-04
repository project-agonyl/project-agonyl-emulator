-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: FriendDB
-- Source Schemata: FriendDB
-- Created: Wed Mar  4 12:15:20 2020
-- Workbench Version: 8.0.19
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema FriendDB
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `FriendDB` ;
CREATE SCHEMA IF NOT EXISTS `FriendDB` ;

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB3
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB3` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB3` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB4
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB4` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB4` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB5
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB5` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB5` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB6
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB6` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB6` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB7
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB7` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB7` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB8
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB8` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB8` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB9
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB9` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB9` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB10
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB10` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB10` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB11
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB11` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB11` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB12
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB12` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB12` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB13
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB13` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB13` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB14
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB14` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB14` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB15
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB15` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB15` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.FRIEND0
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`FRIEND0` (
  `CharName` CHAR(20) NOT NULL,
  `GroupInfo` VARCHAR(1024) NOT NULL,
  `FriendInfo` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table FriendDB.dtproperties
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`dtproperties` (
  `id` INT NOT NULL,
  `objectid` INT NULL,
  `property` VARCHAR(64) NOT NULL,
  `value` VARCHAR(255) NULL,
  `uvalue` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `lvalue` LONGBLOB NULL,
  `version` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`, `property`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.ClanInfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`ClanInfo` (
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
-- Table FriendDB.ClanMember
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`ClanMember` (
  `ClanID` INT NULL,
  `ServerID` INT NULL,
  `CharName` VARCHAR(50) CHARACTER SET 'utf8mb4' NOT NULL,
  `Level` INT NULL,
  `Class` INT NULL,
  `Rank` INT NULL);

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB0
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB0` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB0` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB1
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB1` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB1` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));

-- ----------------------------------------------------------------------------
-- Table FriendDB.LETTERDB2
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `FriendDB`.`LETTERDB2` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Keeping` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL,
  INDEX `IX_LETTERDB2` (`Receiver` ASC) VISIBLE,
  PRIMARY KEY (`LetterIdx`));
SET FOREIGN_KEY_CHECKS = 1;
