# Tecnologías Backend
* Asp.Net Core 2.2
* EntityFrameworkCore
* Logging

# Tecnologías Front End
* Angular 5
* Bootstrap
* Jquery
* Javascript

# Base de Datos
* Sql Server Express 2017

# Configuración y ejecución
La aplicación fué desarrollada en Visual Studio 2017 y contiene 2 Proyectos:
1. 	EdwinTest.Services:
	Éste proyecto contiene lo que corresponde al backend, tiene las controladoras Web Api necesarias para poder presentar la prueba.
	En éste proyecto se empleó CodeFirst para la creación de la Base de Dato a partir de las clases a usar en la migración.
	Para ejecutar ésta app se debe realizar lo siguiente:
	
	- Restaurar el backup que se encuentra en la carpeta Backup en el proyecto, allí se encuentra el archivo .bak
	- Configurar la cadena de conexión a la base de datos en el archivo "appsettings.json".
	- Ejecutar.
	
2.  EdwinTest:
	Éste proyecto contiene lo que corresponde al frontend, tiene implementado angular 5 y se hace uso de webpack para la carga de todas las librerías necesarias.
	Para ejecutar ésta app se debe realizar lo siguiente:
	
	- Configurar la "url" donde quedan expuestas las Apis que se encuentran en el proyecto EdwinTest.Services, en el archivo "ClientApp/app/app.module.browser.ts", 
	en caso de que cambien la configruación actual.
	- Todos los componentes de angular y vistas usados están en la ruta "ClientApp/app/" donde cada vista tiene su propio componente typescript.
	- Ejecutar.
