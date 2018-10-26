/*==============================================================*/
/* DBMS name:      ORACLE Version 11g                           */
/* Created on:     26/10/2018 02:29:35 p.m.                     */
/*==============================================================*/


alter table ARTICULO
   drop constraint FK_ARTICULO_REFERENCE_CAJA;

alter table ARTICULO
   drop constraint FK_ARTICULO_REFERENCE_TIPOARTI;

alter table CAJA
   drop constraint FK_CAJA_REFERENCE_CENTROVO;

alter table CAJA
   drop constraint FK_CAJA_REFERENCE_COMPLETA;

alter table CAJA
   drop constraint FK_CAJA_REFERENCE_ESTADOCA;

alter table CAJA
   drop constraint FK_CAJA_REFERENCE_INCONSIS;

alter table CENTROVOTACION
   drop constraint FK_CENTROVO_REFERENCE_MUNICIPI;

alter table MUNICIPIO
   drop constraint FK_MUNICIPI_REFERENCE_SEDELOGI;

alter table SEDELOGISTICA
   drop constraint FK_SEDELOGI_REFERENCE_DEPARTAM;

alter table TIPOARTICULO
   drop constraint FK_TIPOARTI_REFERENCE_RETORNAR;

drop table ARTICULO cascade constraints;

drop table CAJA cascade constraints;

drop table CENTROVOTACION cascade constraints;

drop table COMPLETAR cascade constraints;

drop table DEPARTAMENTO cascade constraints;

drop table ESTADOCAJA cascade constraints;

drop table INCONSISTENCIA cascade constraints;

drop table MUNICIPIO cascade constraints;

drop table RETORNAR cascade constraints;

drop table SEDELOGISTICA cascade constraints;

drop table TIPOARTICULO cascade constraints;

/*==============================================================*/
/* Table: TIPOARTICULO                                          */
/*==============================================================*/
create table TIPOARTICULO 
(
   ID_TIPO              INT                  not null,
   ID_RETORNO           INT,
   NOMBRE_ARTICULO      VARCHAR2(25),
   NOMENCLATURA         VARCHAR2(3),
   constraint PK_TIPOARTICULO primary key (ID_TIPO)
);

/*==============================================================*/
/* Table: RETORNAR                                              */
/*==============================================================*/
create table RETORNAR 
(
   ID_RETORNO           INT                  not null,
   NOMBRE_RETORNO       VARCHAR2(50),
   constraint PK_RETORNAR primary key (ID_RETORNO)
);


/*==============================================================*/
/* Table: ESTADOCAJA                                            */
/*==============================================================*/
create table ESTADOCAJA 
(
   ID_ESTADO            INT                  not null,
   NOMBRE_ESTADO        VARCHAR2(25),
   constraint PK_ESTADOCAJA primary key (ID_ESTADO)
);

/*==============================================================*/
/* Table: INCONSISTENCIA                                        */
/*==============================================================*/
create table INCONSISTENCIA 
(
   ID_INCON             INT                  not null,
   NOMBRE_INCON         VARCHAR2(100),
   constraint PK_INCONSISTENCIA primary key (ID_INCON)
);

/*==============================================================*/
/* Table: COMPLETAR                                             */
/*==============================================================*/
create table COMPLETAR 
(
   ID_COMPLETO          INT                  not null,
   DESCRIPCION          VARCHAR(50),
   constraint PK_COMPLETAR primary key (ID_COMPLETO)
);

/*==============================================================*/
/* Table: DEPARTAMENTO                                          */
/*==============================================================*/
create table DEPARTAMENTO 
(
   ID_DEPTO             INTEGER              not null,
   NOMBRE_MUNI          VARCHAR2(50),
   constraint PK_DEPARTAMENTO primary key (ID_DEPTO)
);

/*==============================================================*/
/* Table: SEDELOGISTICA                                         */
/*==============================================================*/
create table SEDELOGISTICA 
(
   ID_DEPTO             INTEGER              not null,
   ID_SEDE              INT                  not null,
   NOMBRE_SEDE          VARCHAR2(100),
   RUTA_SEDE            VARCHAR2(10),
   EQUIPO_TRANSMICION   INT,
   EQUIPO_CONTINGENCIA  INT,
   constraint PK_SEDELOGISTICA primary key (ID_SEDE, ID_DEPTO)
);

/*==============================================================*/
/* Table: MUNICIPIO                                             */
/*==============================================================*/
create table MUNICIPIO 
(
   ID_DEPTO             INTEGER              not null,
   ID_SEDE              INT                  not null,
   ID_MUNI              INTEGER              not null,
   NOMBRE_MUNI          VARCHAR2(255),
   constraint PK_MUNICIPIO primary key (ID_MUNI, ID_SEDE, ID_DEPTO)
);

/*==============================================================*/
/* Table: CENTROVOTACION                                        */
/*==============================================================*/
create table CENTROVOTACION 
(
   ID_DEPTO             INTEGER              not null,
   ID_SEDE              INT                  not null,
   ID_MUNI              INTEGER              not null,
   ID_CENTRO            INT                  not null,
   NOMBRE_CENTRO        VARCHAR2(255),
   NUMERO_JUNTAS        INT,
   constraint PK_CENTROVOTACION primary key (ID_CENTRO, ID_MUNI, ID_SEDE, ID_DEPTO)
);


/*==============================================================*/
/* Table: CAJA                                                  */
/*==============================================================*/
create table CAJA 
(
   ID_DEPTO             INTEGER              not null,
   ID_SEDE              INT                  not null,
   ID_MUNI              INTEGER              not null,
   ID_CENTRO            INT                  not null,
   ID_CAJA              INT                  not null,
   ID_COMPLETO          INT,
   ID_ESTADO            INT,
   ID_INCON             INT,
   CODEBAR              VARCHAR2(15),
   constraint PK_CAJA primary key (ID_DEPTO, ID_SEDE, ID_CAJA, ID_CENTRO, ID_MUNI)
);


/*==============================================================*/
/* Table: ARTICULO                                              */
/*==============================================================*/
create table ARTICULO 
(
   ID_DEPTO             INTEGER              not null,
   ID_SEDE              INT                  not null,
   ID_MUNI              INTEGER              not null,
   ID_CENTRO            INT                  not null,
   ID_ARTICULO          INT                  not null,
   ID_TIPO              INT,
   CODEBAR              VARCHAR2(20),
   RFID                 VARCHAR2(32),
   NUM_INV              VARCHAR2(50),
   constraint PK_ARTICULO primary key (ID_DEPTO, ID_SEDE, ID_MUNI, ID_CENTRO, ID_ARTICULO)
);


alter table SEDELOGISTICA
   add constraint FK_SEDELOGI_REFERENCE_DEPARTAM foreign key (ID_DEPTO)
      references DEPARTAMENTO (ID_DEPTO);

alter table TIPOARTICULO
   add constraint FK_TIPOARTI_REFERENCE_RETORNAR foreign key (ID_RETORNO)
      references RETORNAR (ID_RETORNO);

alter table CENTROVOTACION
   add constraint FK_CENTROVO_REFERENCE_MUNICIPI foreign key (ID_MUNI, ID_SEDE, ID_DEPTO)
      references MUNICIPIO (ID_MUNI, ID_SEDE, ID_DEPTO);

alter table MUNICIPIO
   add constraint FK_MUNICIPI_REFERENCE_SEDELOGI foreign key (ID_SEDE, ID_DEPTO)
      references SEDELOGISTICA (ID_SEDE, ID_DEPTO);

alter table CAJA
   add constraint FK_CAJA_REFERENCE_CENTROVO foreign key (ID_CENTRO, ID_MUNI, ID_SEDE, ID_DEPTO)
      references CENTROVOTACION (ID_CENTRO, ID_MUNI, ID_SEDE, ID_DEPTO);

alter table CAJA
   add constraint FK_CAJA_REFERENCE_COMPLETA foreign key (ID_COMPLETO)
      references COMPLETAR (ID_COMPLETO);

alter table CAJA
   add constraint FK_CAJA_REFERENCE_ESTADOCA foreign key (ID_ESTADO)
      references ESTADOCAJA (ID_ESTADO);

alter table CAJA
   add constraint FK_CAJA_REFERENCE_INCONSIS foreign key (ID_INCON)
      references INCONSISTENCIA (ID_INCON);

alter table ARTICULO
   add constraint FK_ARTICULO_REFERENCE_CAJA foreign key (ID_DEPTO, ID_SEDE, ID_ARTICULO, ID_CENTRO, ID_MUNI)
      references CAJA (ID_DEPTO, ID_SEDE, ID_CAJA, ID_CENTRO, ID_MUNI);

alter table ARTICULO
   add constraint FK_ARTICULO_REFERENCE_TIPOARTI foreign key (ID_TIPO)
      references TIPOARTICULO (ID_TIPO);



