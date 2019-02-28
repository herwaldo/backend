CREATE TABLE usuarios (id serial primary key, 
	identificacion integer NOT NULL UNIQUE, 
	Nombre varchar(100), 
	Apellidos varchar(100), 
	ciudad_residencia varchar(150),
	usuario varchar(50) NOT NULL,
	constrasenia varchar(30) NOT NULL);

CREATE TABLE tareas (id serial primary key, 
	fecha_creacion date,
	descripcion text,
	estado_tarea varchar(2),
	fecha_vencimiento date,
	id_usuario integer NOT NULL);

alter table tareas
   add constraint FK_tareas_id_usuario
   foreign key (id_usuario)
   references usuarios(id);

insert into usuarios(identificacion, Nombre, Apellidos, ciudad_residencia, usuario,constrasenia) values (123, 'Carlos Andres', 'Manrique soza', 'Medellin', 'masoza','123456789');
insert into usuarios(identificacion, Nombre, Apellidos, ciudad_residencia, usuario,constrasenia) values (456, 'Javier Andres', 'Perez Gonzales', 'Bogota', 'andreperez','123456789');
insert into usuarios(identificacion, Nombre, Apellidos, ciudad_residencia, usuario,constrasenia) values (789, 'Ana Maria', 'Lopez Molina', 'Cali', 'anmalo','123456789');

insert into tareas(fecha_creacion,descripcion, estado_tarea, fecha_vencimiento,id_usuario) values ('2019/01/31','Realización de la primera tarea de prueba', 'Si', '2019/2/14',1);
insert into tareas(fecha_creacion,descripcion, estado_tarea, fecha_vencimiento,id_usuario) values ('2019/01/31','Segunda tarea de prueba', 'No', '2019/2/14',1);

insert into tareas(fecha_creacion,descripcion, estado_tarea, fecha_vencimiento,id_usuario) values ('2019/01/31','Tercera tarea de prueba', 'No', '2019/2/22',2);
insert into tareas(fecha_creacion,descripcion, estado_tarea, fecha_vencimiento,id_usuario) values ('2019/02/15','Cuarta tarea de prueba', 'Si', '2019/2/27',2);
insert into tareas(fecha_creacion,descripcion, estado_tarea, fecha_vencimiento,id_usuario) values ('2019/02/16','Cuarta tarea de prueba', 'No', '2019/2/26',2);