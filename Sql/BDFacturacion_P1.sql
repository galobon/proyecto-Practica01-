--Creacion BD
create table Formas_pago(
    id_forma_pago int identity(1,1),
    nombre varchar(25),
	activo bit not null default 1
    constraint pk_formas_pago primary key(id_forma_pago)
);

create table Facturas(
    nro_factura int identity(1,1),
    fecha date,
    id_forma_pago int,
    cliente varchar(50),
	activo bit not null default 1
    constraint pk_facturas primary key(nro_factura),
    constraint fk_fp_facturas foreign key(id_forma_pago)
        references Formas_pago(id_forma_pago)
);

create table Articulos(
    id_articulo int identity(1,1),
    nombre varchar(100),
    precio_u int,
	activo bit not null default 1
    constraint pk_articulos primary key(id_articulo)
);

create table Detalles_factura(
    id_det_factura int identity(1,1),
    nro_factura int,
    id_articulo int,
    cantidad int,
	activo bit not null default 1
    constraint pk_det_facturas primary key(id_det_factura),
    constraint fk_facturas_df foreign key(nro_factura)
        references Facturas(nro_factura),
    constraint fk_art_df foreign key(id_articulo)
        references Articulos(id_articulo)
);



--Inserts
--Formas de pago
insert into Formas_pago (nombre) values
('Efectivo'),
('Tarjeta de Crédito'),
('Tarjeta de Débito'),
('Transferencia Bancaria'),
('Cheque');

--Articulos
insert into Articulos (nombre, precio_u) values
('Tornillo 3mm', 15),
('Tuerca 5mm', 10),
('Arandela metálica', 5),
('Martillo', 2500),
('Taladro eléctrico', 18000),
('Llave inglesa', 3500),
('Soldadora MIG', 120000);

--Facturas
insert into Facturas (fecha, id_forma_pago, cliente) values
('2025-09-01', 1, 'Juan Pérez'),
('2025-09-02', 2, 'Carla López'),
('2025-09-03', 1, 'Metalúrgica El Fuerte S.A.'),
('2025-09-04', 3, 'Diego Fernández');

--Detalles de Facturas
insert into Detalles_factura (nro_factura, id_articulo, cantidad) values
(1, 1, 50),
(1, 2, 30),
(1, 3, 20);

insert into Detalles_factura (nro_factura, id_articulo, cantidad) values
(2, 4, 1),
(2, 6, 2);

insert into Detalles_factura (nro_factura, id_articulo, cantidad) values
(3, 5, 1),    
(3, 7, 1);   

insert into Detalles_factura (nro_factura, id_articulo, cantidad) values
(4, 2, 100),
(4, 1, 100);



--Borrar registros y reiniciar Identity
--DBCC CHECKIDENT ('Articulos', RESEED, 0);
--DELETE FROM Articulos

--DBCC CHECKIDENT ('Facturas', RESEED, 0);
--Delete FROM Facturas

--DBCC CHECKIDENT('Detalles_factura', RESEED, 0)
--DELETE FROM Detalles_factura

--DBCC CHECKIDENT ('Formas_pago', RESEED, 0);
--DELETE FROM Formas_pago