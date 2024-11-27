create schema PokeDexDb;

USE PokeDexDb;

CREATE TABLE rol(
	Id int PRIMARY KEY AUTO_INCREMENT,
    Descripcion VARCHAR(50)
);

INSERT INTO Rol (Descripcion) VALUES ('Admin');
INSERT INTO Rol (Descripcion) VALUES ('User');
INSERT INTO Rol (Descripcion) VALUES ('Guest');

create table Usuario_Rol(
	Id int PRIMARY KEY  AUTO_INCREMENT,
    IdRol int,
    IdUsuario int
);

create table Usuario(
	IdUsuario int primary key,
    Nombre varchar(100),
    UserName varchar(100),
    Pass varchar(1024)
);

create table usuario_pocket(
idPkt int primary key auto_increment,
idUsuario int,
1_pkm_id int,
2_pkm_id int,
3_pkm_id int,
4_pkm_id int,
5_pkm_id int,
6_pkm_id int
);


