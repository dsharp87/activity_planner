-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema activity_planner
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema activity_planner
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `activity_planner` DEFAULT CHARACTER SET utf8 ;
USE `activity_planner` ;

-- -----------------------------------------------------
-- Table `activity_planner`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `activity_planner`.`Users` (
  `UserID` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(255) NULL,
  `LastName` VARCHAR(255) NULL,
  `Email` VARCHAR(255) NULL,
  `Password` VARCHAR(255) NULL,
  `CreatedAt` DATETIME NULL DEFAULT now(),
  `UpdatedAt` DATETIME NULL DEFAULT now() on update now(),
  PRIMARY KEY (`UserID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `activity_planner`.`Activities`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `activity_planner`.`Activities` (
  `ActivityID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(255) NULL,
  `StartTime` DATETIME NULL,
  `Duration` INT NULL,
  `Description` VARCHAR(255) NULL,
  `CreatorID` INT NOT NULL,
  `CreatedAt` DATETIME NULL DEFAULT now(),
  `UpdatedAt` DATETIME NULL DEFAULT now() on update now(),
  PRIMARY KEY (`ActivityID`),
  INDEX `fk_Activities_Users_idx` (`CreatorID` ASC),
  CONSTRAINT `fk_Activities_Users`
    FOREIGN KEY (`CreatorID`)
    REFERENCES `activity_planner`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `activity_planner`.`UserActivities`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `activity_planner`.`UserActivities` (
  `UserActivityID` INT NOT NULL AUTO_INCREMENT,
  `ActivityID` INT NOT NULL,
  `UserID` INT NOT NULL,
  `CreatedAt` DATETIME NULL DEFAULT now(),
  `UpdatedAt` DATETIME NULL DEFAULT now() on update now(),
  PRIMARY KEY (`UserActivityID`, `ActivityID`, `UserID`),
  INDEX `fk_Activities_has_Users_Users1_idx` (`UserID` ASC),
  INDEX `fk_Activities_has_Users_Activities1_idx` (`ActivityID` ASC),
  CONSTRAINT `fk_Activities_has_Users_Activities1`
    FOREIGN KEY (`ActivityID`)
    REFERENCES `activity_planner`.`Activities` (`ActivityID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Activities_has_Users_Users1`
    FOREIGN KEY (`UserID`)
    REFERENCES `activity_planner`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `activity_planner`.`Reviews`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `activity_planner`.`Reviews` (
  `ReviewID` INT NOT NULL AUTO_INCREMENT,
  `Title` VARCHAR(255) NULL,
  `Description` VARCHAR(255) NULL,
  `Rating` INT NULL,
  `CreatedAt` DATETIME NULL DEFAULT now(),
  `UpdatedAt` DATETIME NULL DEFAULT now() on update now(),
  `ActivityID` INT NOT NULL,
  `ReviewerID` INT NOT NULL,
  PRIMARY KEY (`ReviewID`),
  INDEX `fk_Reviews_Activities1_idx` (`ActivityID` ASC),
  INDEX `fk_Reviews_Users1_idx` (`ReviewerID` ASC),
  CONSTRAINT `fk_Reviews_Activities1`
    FOREIGN KEY (`ActivityID`)
    REFERENCES `activity_planner`.`Activities` (`ActivityID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reviews_Users1`
    FOREIGN KEY (`ReviewerID`)
    REFERENCES `activity_planner`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
