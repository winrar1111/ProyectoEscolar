create database PRUEBA
go
use PRUEBA
go


CREATE TABLE TBANIOS_CURSAR(
	ID_CURSO int identity(1,1) PRIMARY KEY,
	STR_CURSO NVARCHAR(200)
);

CREATE TABLE TBESTUDIANTE (
	id_estudiante int identity(1,1) primary key,
	STR_NOMBRE NVARCHAR(200),
	STR_APELLIDO NVARCHAR(200),
	STR_DIRECCION NVARCHAR(200),
	STR_CORREO NVARCHAR(100),
	DT_FECHA_NAC DATE,
	STR_DOMICILIO NVARCHAR(200), 
	STR_TELEFONO NVARCHAR(20),
	STR_GENERO NVARCHAR(200)	,
	CodESTUDIANTE Nvarchar(200) unique ,
	COD_MINED_ESTUDIANTE NVARCHAR(20),	
	STR_NOMBRE_PADRE NVARCHAR(200) NULL,
	STR_NOMBRE_MADRE NVARCHAR (200) NULL,
	numAÑO_EN_CURSO int REFERENCES TBANIOS_CURSAR(ID_CURSO)
	)

CREATE TABLE CAJERO
(
 idCajero int identity(1,1) primary key
,numCajero int
,nombre nvarchar(200)
,apellido nvarchar(200)
)

CREATE TABLE CAJA
(
 idCaja int identity(1,1) primary key
,numCaja nvarchar(100)
)

CREATE TABLE TIPOCONCEPTO
(
 idConcepto int identity(1,1) primary key
,nombreConcepto nvarchar(500)
)

CREATE TABLE MES
(
 idMes int identity(1,1) primary key
,nombreMes nvarchar(300)
)

CREATE TABLE CONCEPTOSPAGO
(
 idConcepto int identity(1,1) primary key
,nombreConcepto int
,monto decimal(19,4)
,mes int
,ano nvarchar(200)
)

CREATE TABLE TIPODEPAGO
(
 idTipo int identity(1,1) primary key
,nombre nvarchar(200)
)

CREATE TABLE MONEDA
(
 idMoneda int identity(1,1) primary key
,nombre nvarchar(200)
)

CREATE TABLE EQUIVALENCIA
(
 idEquivalencia int identity(1,1) primary key
,monto decimal(19,4)
,fecha date
)

CREATE TABLE ESTADO
(
 idEstado int identity(1,1) primary key
,nombre nvarchar(200)
)

CREATE TABLE PAGO (
 idPago int identity(1,1) primary key
,fechaTransaccion date
,idCajero int
,idCaja int
,idEstudiante int
,nombreEstudiante nvarchar(200)
,apellidoEstudiante nvarchar(200)
,conceptoPago int
,tipoPago int
,moneda int
,pagoAbonado decimal(19,4)
,equivalencia decimal(19,4)
,balance decimal(19,4)
,estado int
,mora decimal(19,4)
,CONSTRAINT FK_PAGO_Cajero FOREIGN KEY (idCajero) REFERENCES Cajero (idCajero)
,CONSTRAINT FK_PAGO_Caja FOREIGN KEY (idCaja) REFERENCES Caja (idCaja)
,CONSTRAINT FK_PAGO_Estudiante FOREIGN KEY (idEstudiante) REFERENCES TBESTUDIANTE (id_Estudiante)
,CONSTRAINT FK_PAGO_ConceptoPago FOREIGN KEY (conceptoPago) REFERENCES ConceptosPago (idConcepto)
,CONSTRAINT FK_PAGO_tipoPago FOREIGN KEY (tipoPago) REFERENCES tipodepago (idTipo)
,CONSTRAINT FK_PAGO_moneda FOREIGN KEY (moneda) REFERENCES Moneda (idMoneda)
,CONSTRAINT FK_PAGO_estado FOREIGN KEY (estado) REFERENCES estado (idEstado)
)