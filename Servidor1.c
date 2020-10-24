#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <stdio.h>

int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
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
	serv_adr.sin_port = htons(9000);
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
			// Ahora recibimos la peticion
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que anadirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			printf ("Peticion: %s\n",peticion);
			
			// vamos a ver que quieren
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el codigo de la peticion
			char nombre[20];
			
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			conn = mysql_init(NULL);
			char consulta [80];
			char consulta2[400];
			
			
			if (codigo ==0) //peticion de desconexion
			{
				terminar=1;
			}
			
			else if (codigo ==100)
			{
				//ID/Contraseña
				char nombre_usuario[40];
				char contrasena [40];
				
				p = strtok( NULL, "/");
				strcpy (nombre_usuario, p);
				printf(nombre_usuario);
				
				strcpy (consulta,"SELECT JUGADORES.Contrasena FROM JUGADORES WHERE JUGADORES.Usuario = '"); 
				strcat (consulta, nombre_usuario);
				strcpy (consulta,"';");
				
				err=mysql_query (conn, consulta); 
				if (err!=0) 
				{
					printf ("Error al consultar usuario de la base %u %s\n",
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
					p = strtok( NULL, "/");
					strcpy (contrasena, p);
					
					
					if (contrasena == row[0])
					{
						sprintf (respuesta,"100/Correct");
					}
					
					else 
					{
						sprintf (respuesta,"100/Incorrect");
					}	
				}	
			}
			
			else if (codigo ==1) //Numero de partidas que ha ganado un jugador
			{	
				strcpy (consulta,"SELECT COUNT(PARTIDAS.ID) FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE PARTIDAS.ganador= '"); 
				strcat (consulta, nombre);
				strcat (consulta,"'&& JUGADORES.ID=PARTICIPACION.ID_J && PARTIDAS.ID=PARTICIPACION.ID_P");
				err=mysql_query (conn, consulta);
				 
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//Recogemos el resultado de la consulta
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				int num_partidas;
				
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
				}
				
				else 
				{
					num_partidas= row[0];
					printf ("El jugador %s ha ganado %d s\n", nombre, num_partidas);
				}

				sprintf (respuesta,"%d",num_partidas);
			}
								
			else if (codigo ==0)
			{
					
				printf ("Respuesta: %s\n", respuesta);
					
			}
				
		// Enviamos respuesta
		write (sock_conn,respuesta, strlen(respuesta));
				
		}

		// Se acabo el servicio para este cliente
		
		close(sock_conn); 

	}
	mysql_close (conn);
}
