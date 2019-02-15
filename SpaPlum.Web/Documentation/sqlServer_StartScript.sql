/* 
 * SCRIPT DE RECRIACAO DE BANCO DE DADOS
 */

USE [master]
GO

alter database dbo_spaplum set single_user with rollback immediate 

/****** Object:  Database [dbo_spaplum]    Script Date: 8/19/2018 8:35:38 AM ******/
DROP DATABASE [dbo_spaplum] 
GO

/****** Object:  Database [dbo_spaplum]    Script Date: 8/19/2018 8:35:38 AM ******/
CREATE DATABASE [dbo_spaplum]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbo_spaplum', FILENAME = N'D:\Database\dbo_spaplum.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbo_spaplum_log', FILENAME = N'D:\Database\dbo_spaplum_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [dbo_spaplum] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbo_spaplum].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [dbo_spaplum] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [dbo_spaplum] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [dbo_spaplum] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [dbo_spaplum] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [dbo_spaplum] SET AUTO_SHRINK ON 
GO

ALTER DATABASE [dbo_spaplum] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [dbo_spaplum] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [dbo_spaplum] SET  MULTI_USER 
GO

ALTER DATABASE [dbo_spaplum] SET PAGE_VERIFY CHECKSUM  
GO

USE [dbo_spaplum]
GO

ALTER DATABASE [dbo_spaplum] SET  READ_WRITE 
GO
