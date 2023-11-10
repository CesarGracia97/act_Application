/*Scaffold-DbContext "server=192.168.21.193; port=3306; database=act_desarrollo; uid=cgarcia; password=cgarcia;" MySql.EntityFrameworkCore -OutputDir "Models/BD/" -Force*/

CREATE DATABASE act_desarrollo;

#Tabla de usuario
CREATE TABLE `act_desarrollo`.`act_User` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Cedula` VARCHAR(10) NOT NULL,
  `Correo` VARCHAR(45) NOT NULL,
  `NombreYApellido` VARCHAR(45) NOT NULL,
  `Celular` VARCHAR(45) NOT NULL,
  `Contrasena` VARCHAR(75) NOT NULL,
  `TipoUser` VARCHAR(45) NOT NULL,
  `IdSocio` INT(11) NOT NULL,
  `FotoPerfil` LONGBLOB NULL,
  `Estado` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),
  UNIQUE INDEX `Cedula_UNIQUE` (`Cedula` ASC))
COMMENT = 'Tabla de Usuarios';

#Inyeccion de Datos
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('1', '0803262252', 'ces.ze.97@gmail.com', 'CESAR GRACIA GALLON', '0990344916', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Administrador', '0', NULL, 'ACTIVO');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('2', '0914256029', 'hhmirandac@gmail.com', 'HECTOR MIRANDA', '0996188315', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Socio', '0', NULL, 'ACTIVO');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('3', '1723553507', 'bmejia@xtrim.com.ec', 'LUCIA BLANCA MEDINA MEJIA', '0998400582', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Socio', '0', NULL, 'ACTIVO');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('4', '1710333046', 'smunoz@tvcable.com.ec', 'SANDRA MUÑOZ', '0995092596', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Socio', '0', NULL, 'ACTIVO');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('5', '1711608156', 'vpinto@tvcable.com.ec', 'VERONICA PINTO', '0992598575', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Socio', '0', NULL, 'ACTIVO');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('6', '0801355017', 'gortiz@tvcable.com.ec', 'GRACIELA ORTIZ', '0995009860', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Socio', '0', NULL, 'ACTIVO');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('7', '1714562327', 'asalgado@tvcable.com.ec', 'ANDRES SALGADO', '0998673542', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Socio', '0', NULL, 'ACTIVO');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_User` (`Id`, `Cedula`, `Correo`, `NombreYApellido`, `Celular`, `Contrasena`, `TipoUser`, `IdSocio`,`FotoPerfil`, `Estado`) VALUES ('8', '1002256533', 'dbuitron@tvcable.com.ec', 'DIEGO BUITRON', '0994161746', 'c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646', 'Referido', '7', NULL, 'ACTIVO');

#Relacion
USE act_desarrollo; ALTER TABLE act_User ADD CONSTRAINT FK_IdSocio FOREIGN KEY (IdSocio) REFERENCES act_User(Id);

#Tabla de Roles
CREATE TABLE `act_desarrollo`.`act_Rol` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `NombreRol` VARCHAR(45) NOT NULL,
  `DescripcionRol` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),
  UNIQUE INDEX `NombreRol_UNIQUE` (`NombreRol` ASC))
COMMENT = 'Tabla de Roles';

#Tabla Relacion Roles Usuario
CREATE TABLE `act_desarrollo`.`act_RolUser` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdUser` int(11) NOT NULL,
  `IdRol` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE INDEX `IdUser_UNIQUE` (`IdUser` ASC))
COMMENT='Tabla Relacion Rol Usuario';

#Inyeccion de datos
#Datos Rol
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_Rol` (`Id`, `NombreRol`, `DescripcionRol`) VALUES ('1', 'Administrador', 'Rol con acceso total al sistema');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_Rol` (`Id`, `NombreRol`, `DescripcionRol`) VALUES ('2', 'Socio', 'Rol con permisos limitados');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_Rol` (`Id`, `NombreRol`, `DescripcionRol`) VALUES ('3', 'Referido', 'Rol con permisos limitados como Referido');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_Rol` (`Id`, `NombreRol`, `DescripcionRol`) VALUES ('4', 'En Espera', 'Usuario Nuevo sin roles');

#Datos Roles Usuario
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('1', '1', '1');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('2', '2', '1');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('3', '3', '2');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('4', '4', '2');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('5', '5', '2');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('6', '6', '2');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('7', '7', '2');
USE act_desarrollo; INSERT INTO `act_desarrollo`.`act_RolUser` (`Id`, `IdUser`, `IdRol`) VALUES ('8', '8', '3');

#Relaciones
USE act_desarrollo;  ALTER TABLE act_RolUser ADD CONSTRAINT fk_RolUser_User FOREIGN KEY (IdUser) REFERENCES act_User(Id);
USE act_desarrollo;  ALTER TABLE act_RolUser ADD CONSTRAINT fk_Socio_User FOREIGN KEY (IdRol) REFERENCES act_Rol(Id);

#TRIGGER Rol Automatico
USE act_desarrollo;
DELIMITER $$
CREATE TRIGGER tr_NuevoUsuarioRol
AFTER INSERT ON act_User
FOR EACH ROW
BEGIN
    INSERT INTO act_RolUser (IdUser, IdRol)
    VALUES (NEW.Id, 4);
END;
$$
DELIMITER ;

#Tabla de Notificaciones
CREATE TABLE `act_desarrollo`.`act_Notificaciones` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `IdUser` INT NOT NULL,
  `IdActividad` VARCHAR(45) NOT NULL,
  `FechaGeneracion` DATETIME NOT NULL,
  `Razon` VARCHAR(200) NOT NULL,
  `Descripcion` VARCHAR(5000) NOT NULL,
  `Destino` VARCHAR(45) NOT NULL,
  `Visto` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),
  UNIQUE INDEX `IdActividad_UNIQUE` (`IdActividad` ASC))
  COMMENT = 'Tabla de Notificaciones';

#Realcion Notificaciones Usuario (Peticion de Adminision)
USE act_desarrollo; ALTER TABLE act_Notificaciones ADD CONSTRAINT fk_Notificaciones_NewUser FOREIGN KEY (IdActividad) REFERENCES act_User(Cedula);
USE act_desarrollo; ALTER TABLE act_Notificaciones ADD CONSTRAINT fk_Notificaciones_User FOREIGN KEY (IdUser) REFERENCES act_User(Id);

#Tabla de Aportaciones
CREATE TABLE `act_desarrollo`.`act_Aportaciones` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `IdApor` VARCHAR(45) NULL,
  `IdUser` INT(11) NOT NULL,
  `FechaAportacion` DATE NOT NULL,
  `Cuadrante` VARCHAR(45) NOT NULL,
  `Valor` DECIMAL(10,2) NOT NULL,
  `NBancoOrigen` VARCHAR(45) NOT NULL,
  `CBancoOrigen` VARCHAR(45) NOT NULL,
  `NBancoDestino` VARCHAR(45) NOT NULL,
  `CBancoDestino` VARCHAR(45) NOT NULL,
  `CapturaPantalla` LONGBLOB NULL,
  `Aprobacion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),
  UNIQUE INDEX `IdApor_UNIQUE` (`IdApor` ASC))
COMMENT = 'Tabla de Aportaciones';

#TRIGGER IdApor Automatico
USE act_desarrollo;
DELIMITER //

CREATE TRIGGER GeneradorIdApor
BEFORE INSERT ON act_Aportaciones
FOR EACH ROW
BEGIN
    DECLARE last_id INT;

    -- Buscar el último Id de la tabla
    SELECT Id INTO last_id
    FROM act_Aportaciones
    ORDER BY Id DESC
    LIMIT 1;

    -- Verificar si existe un registro anterior
    IF last_id IS NULL THEN
        -- No hay registros anteriores, asignar APOR-1
        SET NEW.IdApor = 'APOR-1';
    ELSE
        -- Hay registros anteriores, calcular nuevo valor para IdApor
        SET NEW.IdApor = CONCAT('APOR-', last_id + 1);
    END IF;
END;

//

DELIMITER ;

USE act_desarrollo; ALTER TABLE act_Aportaciones ADD CONSTRAINT fk_Aportaciones_User FOREIGN KEY (IdUser) REFERENCES act_User(Id);
USE act_desarrollo; ALTER TABLE act_Aportaciones ADD CONSTRAINT fk_Aportaciones_Notificaciones FOREIGN KEY (IdApor) REFERENCES act_Notificaciones(IdActividad);
