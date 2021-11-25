CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `clientes` (
        `idCLIENTE` int NOT NULL,
        `NOME` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
        `CPF` double NOT NULL,
        `NASCIMENTO` datetime NOT NULL,
        `PROFISSAO` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
        `MARGEM` double NOT NULL,
        `MARGEM_CARTAO` double NOT NULL,
        `SALARIO` double NOT NULL,
        `STATUS` int NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`idCLIENTE`)
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `IdentityUser<string>` (
        `Id` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `NormalizedUserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `TipoAcesso` varchar(256) CHARACTER SET utf8mb4 NULL,
        `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `NormalizedEmail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `EmailConfirmed` tinyint(1) NOT NULL,
        `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `PhoneNumberConfirmed` tinyint(1) NOT NULL,
        `TwoFactorEnabled` tinyint(1) NOT NULL,
        `LockoutEnd` datetime(6) NULL,
        `LockoutEnabled` tinyint(1) NOT NULL,
        `AccessFailedCount` int NOT NULL
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `IdentityUserClaim<string>` (
        `Id` int NOT NULL,
        `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `IdentityUserLogin<string>` (
        `LoginProvider` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `ProviderKey` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `IdentityUserRole<string>` (
        `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `RoleId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `IdentityUserToken<string>` (
        `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `LoginProvider` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `emprestimos` (
        `idEMPRESTIMO` int NOT NULL,
        `idCLIENTE` int NOT NULL,
        `VALOR_LIBERADO` double(10,2) NOT NULL,
        `VALOR_EMPRESTIMO` double(10,2) NOT NULL,
        `DATA_VENC` datetime NOT NULL,
        `VALOR_PARCELA` double(10,2) NOT NULL,
        `QTD_PARCELA` int NOT NULL,
        `TAXA_JUROS` double NOT NULL,
        `QTD_PARCELA_REST` int NOT NULL,
        `STATUS_EMPREST` int NOT NULL,
        `TIPO_EMPREST` int NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`idEMPRESTIMO`),
        CONSTRAINT `fk_EMPRESTIMO_CLIENTE` FOREIGN KEY (`idCLIENTE`) REFERENCES `clientes` (`idCLIENTE`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE TABLE `usuarios` (
        `idUSUARIOS` int NOT NULL,
        `idCLIENTE` int NOT NULL,
        `USER` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `SENHA` varbinary(6) NULL,
        `EMAIL` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        `CELULAR` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`idUSUARIOS`),
        CONSTRAINT `fk_USUARIOS_CLIENTES1` FOREIGN KEY (`idCLIENTE`) REFERENCES `clientes` (`idCLIENTE`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE INDEX `fk_EMPRESTIMO_CLIENTE_idx` ON `emprestimos` (`idCLIENTE`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE INDEX `fk_USUARIOS_CLIENTES1_idx` ON `usuarios` (`idCLIENTE`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    CREATE UNIQUE INDEX `USER_UNIQUE` ON `usuarios` (`USER`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20211123203910_CreateIdentity') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20211123203910_CreateIdentity', '5.0.10');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

