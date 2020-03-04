-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: Clan
-- Source Schemata: Clan
-- Created: Wed Mar  4 12:12:52 2020
-- Workbench Version: 8.0.19
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema Clan
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `Clan` ;
CREATE SCHEMA IF NOT EXISTS `Clan` ;

-- ----------------------------------------------------------------------------
-- Table Clan.ClanInfo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `Clan`.`ClanInfo` (
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
-- Table Clan.ClanMember
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `Clan`.`ClanMember` (
  `ClanID` INT NULL,
  `ServerID` INT NULL,
  `CharName` VARCHAR(50) CHARACTER SET 'utf8mb4' NOT NULL,
  `Level` INT NULL,
  `Class` INT NULL,
  `Rank` INT NULL);

-- ----------------------------------------------------------------------------
-- Table Clan.dtproperties
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `Clan`.`dtproperties` (
  `id` INT NOT NULL,
  `objectid` INT NULL,
  `property` VARCHAR(64) NOT NULL,
  `value` VARCHAR(255) NULL,
  `uvalue` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `lvalue` LONGBLOB NULL,
  `version` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`, `property`));
SET FOREIGN_KEY_CHECKS = 1;
