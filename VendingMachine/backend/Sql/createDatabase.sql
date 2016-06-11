USE [master]
GO

/****** Object:  Database [vm]    Script Date: 23.02.2015 23:41:58 ******/
CREATE DATABASE [vm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'vm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\vm.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'vm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\vm_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [vm] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [vm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [vm] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [vm] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [vm] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [vm] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [vm] SET ARITHABORT OFF 
GO

ALTER DATABASE [vm] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [vm] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [vm] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [vm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [vm] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [vm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [vm] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [vm] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [vm] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [vm] SET  DISABLE_BROKER 
GO

ALTER DATABASE [vm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [vm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [vm] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [vm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [vm] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [vm] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [vm] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [vm] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [vm] SET  MULTI_USER 
GO

ALTER DATABASE [vm] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [vm] SET DB_CHAINING OFF 
GO

ALTER DATABASE [vm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [vm] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [vm] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [vm] SET  READ_WRITE 
GO


