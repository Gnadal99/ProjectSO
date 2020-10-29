#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>

int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9005);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	//Establecer conexion con la base de datos
	MYSQL *conn;
	int err;
	
	
	
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion

	conn = mysql_real_connect (conn, "localhost","root", "mysql", "JUEGO",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		int terminar =0;
		// Entramos en un bucle para atender todas las peticiones de este cliente
		//hasta que se desconecte
		while (terminar ==0)
		{
			char respuesta[512];
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			// Ahora recibimos la peticion
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que anadirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			printf ("P: %s\n",peticion);
			
			// vamos a ver que quieren
			char *t = strtok (peticion, "/");
			
			int codigo =  atoi (t);
			
			printf ("Codigo: %d \n", codigo);
			
			// Ya tenemos el codigo de la peticion
			char nombre[20];
			
			
			if (codigo ==0) //peticion de desconexion
			{
				terminar=1;
			}
			
			else if (codigo ==100)
			{
				//ID/Contraseña
				char nombre_usuario[40];
				char contrasena [40];
				char consulta [800];
				
				t = strtok( NULL, "/");
				strcpy (nombre_usuario, t);
				printf("Nombre usuario %s\n", nombre_usuario);
				
				strcpy (consulta,"SELECT JUGADORES.Contrasena FROM JUGADORES WHERE JUGADORES.Usuario = '"); 
				strcat (consulta, nombre_usuario);
				strcat (consulta,"'");
				
				err=mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al consultar datos2 de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
				{
					printf ("No se han obtenido usuario en la consulta\n");
					sprintf (respuesta,"100/NoUser");
				}
				
				else
				{
					t = strtok( NULL, "/");
					strcpy (contrasena, t);
					printf("Con recib %s\n", contrasena);
					printf("con db %s\n",row[0]);
					
					if (strcmp(contrasena,row[0]) == 0)
					{
						sprintf (respuesta,"100/Correct");
					}
					
					else 
					{
						sprintf (respuesta,"100/Incorrect");
					}	
				}	
				printf ("%s",respuesta);
			}
			
			else if (codigo ==101)
			{
				//ID/Contraseña
				char nombre_usuario[40];
				char contrasena [40];
				char consulta [800];
				
				t = strtok( NULL, "/");
				strcpy (nombre_usuario, t);
				printf("Nombre usuario %s\n", nombre_usuario);
				
				t = strtok( NULL, "/");
				strcpy (contrasena, t);
				printf("Contrasena %s\n", contrasena);
				
				strcpy (consulta,"SELECT JUGADORES.ID FROM JUGADORES WHERE JUGADORES.Usuario ='"); 
				strcat (consulta,nombre_usuario);
				strcat (consulta,"';");
				
				err=mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al consultar datos2 de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
				{
					
					strcpy (consulta,"SELECT ID FROM JUGADORES;"); 
					
					
					err=mysql_query (conn, consulta);
					if (err!=0) {
						printf ("Error al consultar datos2 de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						exit (1);
					}
					
					
					resultado = mysql_store_result (conn); 
					row = mysql_fetch_row (resultado);
					
					int k = 0;
					int max_ID = 0;
					char ID[50];
					
					while ( k < sizeof(row))
					{
						printf("pr: %s\n",row[1]);
						
/*						if (max_ID < atoi (row[k]))*/
/*							max_ID = atoi (row[k]);*/
						k = k + 1;
					
					}
					
					max_ID = max_ID + 1;
					
					
					strcpy (consulta, "INSERT INTO JUGADORES VALUES (");
					//convertimos el ID en un string y lo concatenamos 
					
					sprintf(ID, "%d", max_ID);
					printf("ID: %s\n",ID);
					strcat (consulta, ID); 
					strcat (consulta, ",'");
					//concatenamos el nombre
					strcat (consulta, nombre_usuario); 
					strcat (consulta, "','");
					//concatenamos la contrasena
					strcat (consulta, contrasena); 
					strcat (consulta, "');");
					
					err = mysql_query(conn, consulta);
					if (err!=0) {
						printf ("Error al introducir datos la base %u %s\n", 
								mysql_errno(conn), mysql_error(conn));
						sprintf (respuesta, "101/Incorrect2");
					}
					
					else
					{
						sprintf (respuesta,"101/Correct");
					}	
					printf ("%s",respuesta);
					
				}
				else
					sprintf (respuesta, "101/Incorrect");
				
				
			}
			
			else if (codigo ==1) //Numero de partidas que ha ganado un jugador
			{	
				MYSQL_RES *resultado;
				MYSQL_ROW row;
				char consulta [800];
				
				//nombre
				char nombre[40];
				
				
				t = strtok( NULL, "/");
				strcpy (nombre, t);
				printf("Nombre usuario %s\n", nombre);
				
				
				strcpy (consulta,"SELECT COUNT(PARTIDAS.ID) FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE PARTIDAS.ganador= '"); 
				strcat (consulta, nombre);
				strcat (consulta,"' AND JUGADORES.ID=PARTICIPACION.ID_J AND PARTIDAS.ID=PARTICIPACION.ID_P     AND    PARTICIPACION.ID_J = (SELECT JUGADORES.ID FROM JUGADORES WHERE JUGADORES.Usuario = '");
				strcat (consulta, nombre);
				strcat (consulta, "');");
				
				
				err=mysql_query (conn, consulta);
				 
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//Recogemos el resultado de la consulta
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				
				
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
				}
				
				else 
				{
					printf ("El jugador %s ha ganado %s s\n", nombre, row[0]);
				}

				sprintf (respuesta,row[0]);
			}
			
			else if (codigo == 2)
			{
				MYSQL_RES *resultado;
				MYSQL_ROW row;
				char consulta [800];
				
			
				strcpy (consulta,"SELECT DISTINCT PARTIDAS.Ganador FROM (PARTIDAS, JUGADORES, PARTICIPACION)  WHERE  PARTIDAS.Duracion <= 10   AND    JUGADORES.ID = PARTICIPACION.ID_J   AND    PARTIDAS.ID = PARTICIPACION.ID_P;"); 
				
				
				
				err=mysql_query (conn, consulta);
				
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//Recogemos el resultado de la consulta
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				printf("row0 %s\n",row[0]);
				char vector_nombres[500];
				
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
				}
				
				
				else 
				{
					strcpy(vector_nombres,row[0]);
					row = mysql_fetch_row (resultado);
					while (row !=NULL) 
					{
						strcat(vector_nombres," ");
						strcat(vector_nombres,row[0]);
						printf ("Los ganadores de las partidas de mas de 10 minutos son: %s\n", row[0] );
						row = mysql_fetch_row (resultado);
					}
					strcpy (respuesta, vector_nombres);
					printf("resp: %s\n",respuesta);
				}
				
				
				
			}
			
			else if (codigo == 3)
			{
				MYSQL_RES *resultado;
				MYSQL_ROW row;
				char consulta [800];
				char IDpartida [10];
				
				t = strtok( NULL, "/");
				printf("t: %s\n",t);
				strcpy (IDpartida, t);
				
				strcpy (consulta,"SELECT PARTIDAS.Fecha_Hora FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE  PARTIDAS.ID = ");
				strcat (consulta,IDpartida);
				strcat (consulta," AND    JUGADORES.ID = PARTICIPACION.ID_J		AND    PARTIDAS.ID = PARTICIPACION.ID_P;"); 
				
				
				
				err=mysql_query (conn, consulta);
				
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//Recogemos el resultado de la consulta
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				printf("row0 %s\n",row[0]);
				
				
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
				}
				
				
				else 
				{
					if (row[0] == 0)
						strcpy(respuesta,"3/NoExist");
					strcpy (respuesta, row[0]);
					printf("resp: %s\n",respuesta);
				}
				
				
				
			}
			
			
			if (codigo != 0)
			{
				// Enviamos respuesta
				write (sock_conn, respuesta, strlen(respuesta));
			}
		}

		// Se acabo el servicio para este cliente
		
		close(sock_conn); 

	}
	mysql_close (conn);
}
