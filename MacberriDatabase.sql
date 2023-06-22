
-- Verificando si la base de datos ya existe
use master
go

if db_id('macberriproject') is not null
begin
	-- cambiando a usuario conectado único y finalizando transacciones
	alter database [macberriproject] set single_user with rollback immediate;
	drop database [macberriproject];
end
go


create database [macberriproject];
go

use [macberriproject];
go

create table [rol](
	[id_rol] int identity(1,1) primary key,
	[name] varchar(20) not null,
	[create_at] datetime default getdate()
)

create table [user](
	[id_user] uniqueidentifier default newid() primary key,
	[names] nvarchar(80) not null,
	[lastnames] nvarchar(80) not null,
	[email] nvarchar(80) not null,
	[password] nvarchar(20) not null,
	[create_at] datetime default getdate(),
	[id_rol] int not null,
	foreign key (id_rol) references [rol](id_rol)
)
go

create table [product](
	[id_product] uniqueidentifier default newid() primary key,
	[name] nvarchar(100) not null,
	[description] nvarchar(400) not null,
	[price] decimal(8,2) not null,
	[url_image] nvarchar(250) not null
)
go

create table [service](
	[id_service] uniqueidentifier default newid() primary key,
	[name] nvarchar(100) not null,
	[description] nvarchar(400) not null,
	[url_image] nvarchar(250) not null,
)
go

create table [user_services](
	[id_user_services] uniqueidentifier default newid() primary key,
	[id_service] uniqueidentifier not null,
	[id_user] uniqueidentifier not null,

	foreign key (id_service) references [service](id_service),
	foreign key (id_user) references [user](id_user)
)
go

create table [reserve](
	[id_reserve] uniqueidentifier default newid() primary key,
	[id_user] uniqueidentifier not null,
	[id_service] uniqueidentifier not null,
	[requested_at] datetime default getdate(),
	[limit_date] datetime not null,

	foreign key (id_user) references [user](id_user),
	foreign key (id_service) references [service](id_service)
)
go

create table [sale](
	[id_sale] uniqueidentifier default newid() primary key,
	[id_user] uniqueidentifier not null,
	[total] decimal(8,2),
	[sale_date] datetime default getdate(),

	foreign key (id_user) references [user](id_user)
)
go

create table [detail](
	[id_detail] uniqueidentifier default newid() primary key,
	[id_product] uniqueidentifier not null,
	[id_sale] uniqueidentifier not null,
	[unit_price] decimal(8,2) not null,
	[total_price] decimal(8,2) not null,
	[quantity] int not null,

	foreign key (id_product) references [product](id_product),
	foreign key (id_sale) references [sale](id_sale)
)
go

create table [shopcart](
	[id_shopcart] uniqueidentifier default newid() primary key,
	[id_user] uniqueidentifier not null,
	[id_product] uniqueidentifier not null,
	[quantity] int not null,

	foreign key (id_user) references [user](id_user),
	foreign key (id_product) references [product](id_product)
)
go

-- Insertando datos de roles
insert into [rol] ([name]) values ('ADMIN')
go

insert into [rol] ([name]) values ('STANDARD')
go

-- Insertando datos de usuarios (Por cuestiones de tiempo no se encriptará la contraseña)
insert into [user] ([names], [lastnames], [email], [password], [id_rol])
values (
	'Admin',
	'Admin',
	'admin@macberri.com',
	'admin123',
	1
	)
go

insert into [user] ([names], [lastnames], [email], [password], [id_rol])
values (
	'Julio Hernesto',
	'Maldonado Cáceres',
	'julito_h@gmail.com',
	'juliohernesto',
	2
	)
go

insert into [user] ([names], [lastnames], [email], [password], [id_rol])
values (
	'Carlos Miguel',
	'Hernandez Fernandez',
	'carlos_h@gmail.com',
	'carlosmiguel',
	2
	)
go


-- Insertando datos de productos

insert into [product] ([name], [description], [price],[url_image]) 
values (
	'Smartphone Galaxy S21', 
	'Teléfono inteligente de última generación con pantalla AMOLED, cámara de alta resolución y capacidad de almacenamiento ampliable.',
	999.99,
	'https://pe.celulares.com/fotos/samsung-galaxy-s21-5g-89038-g-alt.jpg'
	)
go

insert into [product] ([name], [description], [price],[url_image]) 
values (
	'Laptop Inspiron 15', 
	'Computadora portátil con procesador Intel Core, pantalla de 15.6 pulgadas y memoria RAM de 8 GB.',
	799.99,
	'https://www.notebookcheck.org/typo3temp/_processed_/3/c/csm_case11_af5c4e2ddb.jpg'
	)
go

insert into [product] ([name], [description], [price],[url_image]) 
values (
	'Zapatillas deportivas Air Max', 
	'Calzado deportivo con tecnología de amortiguación Air Max, ideal para correr y hacer ejercicio.',
	129.99,
	'https://oechsle.vteximg.com.br/arquivos/ids/10507311-1500-1500/imageUrl_2.jpg?v=637958061463730000'
	)
go

insert into [product] ([name], [description], [price],[url_image]) 
values (
	'Televisor Smart LED 4K', 
	'Televisor inteligente con resolución 4K, pantalla LED y acceso a aplicaciones de streaming.',
	699.99,
	'https://home.ripley.com.pe/Attachment/WOP_5/2018295640229/2018295640229_2.jpg'
	)
go

insert into [product] ([name], [description], [price],[url_image]) 
values (
	'Cámara DSLR EOS 80D', 
	'Cámara réflex digital con sensor de alta resolución, grabación de video Full HD y múltiples modos de disparo.',
	1199.99,
	'https://promart.vteximg.com.br/arquivos/ids/3013127-1000-1000/image-99053d3f634048a9875ca8183a52931a.jpg?v=637729356952770000'
	)
go


-- Insertando datos de servicios
insert into [service] ([name], [description], [url_image])
values(
	'Renta de auto',
	'Reserva un automóvil para tu próximo viaje y disfruta de la libertad de moverte a tu propio ritmo',
	'https://autoland.com.pe/wp-content/uploads/2022/04/MgZxPlus_Interna_miniatura.png'
	)
go

insert into [service] ([name], [description], [url_image])
values(
	'Reserva de cancha deportiva',
	'Programa partidos o prácticas reservando canchas deportivas para fútbol, tenis, baloncesto y más.',
	'https://upload.wikimedia.org/wikipedia/commons/thumb/2/22/Cancha_sintetica.jpg/1200px-Cancha_sintetica.jpg'
	)
go

insert into [service] ([name], [description], [url_image])
values(
	'Alquiler de equipo audiovisual',
	'Reserva equipos audiovisuales como proyectores, sistemas de sonido y pantallas para tus eventos o presentaciones.',
	'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRwyHgAphYxmZLMK__fuN_fnoZxZjOFLvBh_cq02k64rySfn82n6gq8mZ_n8sHPpcwT1Zc&usqp=CAU'
	)
go

insert into [service] ([name], [description], [url_image])
values(
	'Tour guiado en bicicleta',
	'Explora la ciudad en bicicleta con un tour guiado que te llevará a través de los principales puntos de interés.',
	'https://cdn.getyourguide.com/img/tour/5cc190c9dc213.jpeg/146.jpg'
	)
go

insert into [service] ([name], [description], [url_image])
values(
	'Renta de yate',
	'Disfruta de un día de navegación reservando un yate y explorando las aguas en compañía de tus seres queridos.',
	'https://guiarutasmayas.com/wp-content/uploads/PHOTO-2019-09-17-09-37-24.jpg'
	)
go