create database Instituciones
go

use Instituciones
go

create table School
(
SchoolId int primary key identity,
SchoolName varchar(50) not null,
Descripcion varchar(1000) null,
Direccion varchar(50) null,
Phone varchar(50) null,
PostCode varchar(50) null,
PostAdress varchar(50) null,
Estado varchar(50) check (Estado = 'Activo' or Estado = 'Inactivo')
)


create table Class
(
ClassId int primary key identity, 
SchoolId int foreign key references School(SchoolId),
ClassName varchar(50) not null,
Descripcion varchar(1000) null,
Estado varchar(50) check (Estado = 'Activo' or Estado = 'Inactivo')
)

create procedure consultarSchoolFiltro
@SchoolId int
as 
	begin 
		select * from School where SchoolId = @SchoolId
	end

	exec consultarSchoolFiltro 1

create procedure consultarSchool
as 
	begin 
		select * from School where School.Estado = 'Activo'
	end

	exec consultarSchool

create procedure ingresarSchool
@SchoolName varchar(50),
@Descripcion varchar(1000),
@Direccion varchar(50),
@Phone varchar(50),
@PostCode varchar(50),
@PostAdress varchar(50),
@Estado varchar(50)

as 
	begin 
		insert into School values (@SchoolName,@Descripcion,@Direccion,@Phone,@PostCode,@PostAdress,@Estado)
	end

	exec ingresarSchool 'Hispanoamericana','no se', 'Puntarenas','8293 9239','9011','0011', 'Activo'

create procedure borrarSchool
@SchoolId int 
as 

begin 

update School set Estado = 'Inactivo' where SchoolId = @SchoolId

end

exec borrarSchool 1

create procedure modificarSchool
@SchoolId int,
@SchoolName varchar(50),
@Descripcion varchar(1000),
@Direccion varchar(50),
@Phone varchar(50),
@PostCode varchar(50),
@PostAdress varchar(50),
@Estado varchar(50)
as 
	begin 

	update School set SchoolName = @SchoolName,Descripcion = @Descripcion,Direccion = @Direccion,Phone = @Phone,PostCode = @PostCode, PostAdress = @PostAdress, Estado = @Estado where SchoolId = @SchoolId
	
	end

exec modificarSchool 2,'Universidad','si se', 'Heredia','1122 3344','8011','0024', 'Activo'


create procedure consultarClassFiltro
@ClassId int
as 
	begin 
		select * from Class where ClassId = @ClassId
	end

	exec consultarClassFiltro 1

create procedure consultarClass
as 
	begin 
		select * from Class where Class.Estado = 'Activo'

	end

exec consultarClass

create procedure ingresarClass
@SchoolId int,
@ClassName varchar(50),
@Descripcion varchar(1000),
@Estado varchar(50)

as 
	begin 
		insert into Class values (@SchoolId,@ClassName,@Descripcion,@Estado)
	end

  exec ingresarClass 1,'Hispanoamericana','no se', 'Activo'

create procedure borrarClass
@ClassId int 
as 

begin 

update Class set Estado = 'Inactivo' where ClassId = @ClassId 

end

exec borrarClass 1

create procedure modificarClass
@ClassId int, 
@SchoolId int,
@ClassName varchar(50),
@Descripcion varchar(1000),
@Estado varchar(50)
as 
	begin 

	update Class set SchoolId = @SchoolId,ClassName = @ClassName,  Descripcion = @Descripcion, Estado = @Estado where ClassId = @ClassId
	
	end

	exec modificarClass 1,2,'Universidad','si se', 'Activo'
