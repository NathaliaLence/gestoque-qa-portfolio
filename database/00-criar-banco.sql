-- ============================================================
-- Gestoque · cria (ou recria do zero) o banco local
-- Uso: sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\00-criar-banco.sql
-- ============================================================
USE master;
GO
IF DB_ID('Gestoque') IS NOT NULL
BEGIN
    ALTER DATABASE [Gestoque] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [Gestoque];
END
GO
CREATE DATABASE [Gestoque];
GO
