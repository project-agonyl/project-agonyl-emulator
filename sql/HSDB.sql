-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: HSDB
-- Source Schemata: HSDB
-- Created: Wed Mar  4 12:10:15 2020
-- Workbench Version: 8.0.19
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema HSDB
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `HSDB` ;
CREATE SCHEMA IF NOT EXISTS `HSDB` ;

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB0
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB0` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB1
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB1` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB10
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB10` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB11
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB11` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB12
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB12` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB13
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB13` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB14
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB14` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB15
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB15` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB2
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB2` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB3
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB3` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB4
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB4` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB5
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB5` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB6
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB6` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB7
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB7` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB8
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB8` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.LETTERDB9
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`LETTERDB9` (
  `LetterIdx` INT NOT NULL,
  `Receiver` CHAR(20) NOT NULL,
  `Sender` CHAR(20) NOT NULL,
  `SendDate` DATETIME NOT NULL,
  `Reading` TINYINT UNSIGNED NOT NULL,
  `Keeping` TINYINT UNSIGNED NOT NULL,
  `Title` VARCHAR(20) NOT NULL,
  `LetterMsg` VARCHAR(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.HSSTONETABLE
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`HSSTONETABLE` (
  `MasterName` CHAR(21) NOT NULL,
  `CreateDate` DATETIME NULL,
  `SaveDate` DATETIME NULL,
  `Slot0` TEXT(256) NULL,
  `Slot1` TEXT(256) NULL,
  `Slot2` TEXT(256) NULL,
  `Slot3` TEXT(256) NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.HSTABLE
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`HSTABLE` (
  `HSID` INT NOT NULL,
  `HSName` CHAR(21) NOT NULL,
  `MasterName` CHAR(21) NOT NULL,
  `Type` TINYINT UNSIGNED NOT NULL,
  `HSLevel` SMALLINT NOT NULL,
  `HSExp` INT NOT NULL,
  `Ability` TEXT(256) NOT NULL,
  `CreateDate` DATETIME NOT NULL,
  `SaveDate` DATETIME NOT NULL,
  `HSState` TINYINT UNSIGNED NULL,
  `DelDate` DATETIME NULL,
  `HSBody` TEXT(1024) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table HSDB.dtproperties
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `HSDB`.`dtproperties` (
  `id` INT NOT NULL,
  `objectid` INT NULL,
  `property` VARCHAR(64) NOT NULL,
  `value` VARCHAR(255) NULL,
  `uvalue` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `lvalue` LONGBLOB NULL,
  `version` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`, `property`));
SET FOREIGN_KEY_CHECKS = 1;
