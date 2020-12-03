#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>

//Declaracion de estructuras
typedef struct{
	char nombre[20];
	int socket;
}Conectado;

typedef struct {
	Conectado conectados[100];
	int num;
}ListaConectados;

typedef struct{
	Conectado jugador;
	char estado[20];
}TJugador;

typedef struct{
	TJugador jugadores[4];
	int num;
	char estado[20];
}TPartida;

typedef struct{
	TPartida partidas[100];
	int num;
}TPartidas;

//Declaracion de variables globales
int contador_servicios;
int i;
int sockets[100];
ListaConectados miLista2;
TPartidas listaPartidas;

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//Funciones
int Pon (ListaConectados *lista, char nombre[20], int socket){
	//Anade conectados a la lista
	if(lista ->num ==100)
		return -1;
	else{
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num ++;
		printf ("Conectado: %s\n", lista->conectados[lista->num -1].nombre);
		return 3;
	}
}
int DameSocket (ListaConectados *lista, char nombre[20]){
	//Devuelve el socket del nombre introducido que esta en la lista
	int i=0;
	int encontrado=0;
	while ((i<lista->num)&&!encontrado){
		if (strcmp(lista->conectados[i].nombre,nombre)==0)
			encontrado =1;
		else
			i=i+1;
	}
	if (encontrado)
		return lista->conectados[i].socket;
	else
		return -1;
}
int DamePosicion (ListaConectados *lista, char nombre[20]){
	//Devuelve la posicion del nombre introducido que esta en la lista
	int i=0;
	int encontrado=0;
	while ((i<lista->num)&&!encontrado){
		if (strcmp(lista->conectados[i].nombre,nombre)==0)
			encontrado =1;
		else
			i=i+1;
	}
	if (encontrado)
		return i;
	else
		return -1;
}

int Elimina (ListaConectados *lista, char nombre[20]){
	//Elimina en nombre de la lista de conectados
	int pos = DamePosicion (lista, nombre);
	if (pos==-1)
		return -1;
	else {
		int i;
		for (i=pos; i<lista->num-1; i++)
		{
			lista-> conectados [i] = lista->conectados[i+1];
			//strcpy(lista->conectados[i].nombre = lista->conectados[i+1].nombre, nombre);
			//lista->conectados[i].socket =lista->conectados[i+1].socket;
		}
		lista ->num--;
		return 0;
	}
}

void DameConectados (ListaConectados *lista, char conectados[300]){
	//Pone en un vector los nombres de conectados separados por una bcoma
	//Primero pone el n�mero de conectados. Ej: 3,Maria,Juan,Pedro
	sprintf (conectados, "%d", lista->num);
	printf ("%d",lista->num);
	int i;
	for (i=0; i<lista->num; i++)
		sprintf (conectados, "%s,%s", conectados, lista->conectados[i].nombre);
}

void DameTodosSockets (ListaConectados *lista, char conectados[300], char sockets[300]){
	//Pone en un vector los sockets de conectados separados por una coma. Ej: 2,3,4
	int i;
	int o =0;
	char socket[10];
	char nombre[20];
	char *p = strtok (conectados, ",");
	int n = atoi (p);
	p = strtok (NULL, ",");
	strcpy (nombre, p);
	
	for (;;)
		if (strcmp (lista->conectados[i].nombre, nombre)==0){
			sprintf(socket, "%d", lista->conectados[i].socket);
			strcat (sockets, socket); 
			if (o< n-1){
				strcat (sockets, ",");
				p = strtok (NULL, ",");
				strcpy (nombre, p);
			}
			o++;
	}
}


int CrearSala (TPartidas *listaPartidas, ListaConectados *lista, char username[20]){
	if (listaPartidas->num<100)
	{
		strcpy(listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].jugador.nombre,username);
		listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].jugador.socket = DameSocket(lista,username);
		strcpy(listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].estado,"Aceptado");
		listaPartidas->partidas[listaPartidas->num].num = listaPartidas->partidas[listaPartidas->num].num +1;
		strcpy(listaPartidas->partidas[listaPartidas->num].estado,"Pendiente");
		
		listaPartidas->num = listaPartidas->num +1;
		return listaPartidas->num-1;
	}
	else{
		int s;
		for (s=0; s<100;s++){
			if (strcmp(listaPartidas->partidas[s].estado,"Cancelada")==0){
				strcpy(listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].jugador.nombre,username);
				listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].jugador.socket = DameSocket(lista,username);
				strcpy(listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].estado,"Aceptado");
				listaPartidas->partidas[listaPartidas->num].num = listaPartidas->partidas[listaPartidas->num].num +1;
				strcpy(listaPartidas->partidas[listaPartidas->num].estado,"Pendiente");
				
				return s;
			}
				
				
		}
		return -1;
	}
}
int AnadirInvitado (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala){
	int s_invitado = DameSocket(lista,username);
	if (listaPartidas->num<numSala)
		return -1;
	else{
		if(listaPartidas->partidas[numSala].num<4){
			if (strcmp(listaPartidas->partidas[numSala].estado,"Pendiente")==0){
				strcpy(listaPartidas->partidas[numSala].jugadores[listaPartidas->partidas[numSala].num].jugador.nombre,username);
				listaPartidas->partidas[numSala].jugadores[listaPartidas->partidas[numSala].num].jugador.socket = s_invitado;
				strcpy(listaPartidas->partidas[numSala].jugadores[listaPartidas->partidas[numSala].num].estado,"Invitado");
				listaPartidas->partidas[numSala].num = listaPartidas->partidas[numSala].num + 1;
				return 0;
			}
			else
				return -2;
		}
			
		else
			return -3;
	}
	
	
	
	
	
}
int Aceptar (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala){
	if (strcmp(listaPartidas->partidas[numSala].estado,"Pendiente")==0)
	{
		int il = 0;
		while (il<listaPartidas->partidas[numSala].num)
		{
			if (strcmp(listaPartidas->partidas[numSala].jugadores[il].jugador.nombre,username)==0)
			{
				strcpy(listaPartidas->partidas[numSala].jugadores[il].estado,"Aceptado");
				return 0;
			}
			il++;
		}
		return -1;
	}
	else
		return -1;
}
int Rechazar (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala){
	if (strcmp(listaPartidas->partidas[numSala].estado,"Pendiente")==0)
	{
		int il = 0;
		int encontrado = 0;
		while (il<listaPartidas->partidas[numSala].num)
		{
			if (encontrado==0)
			{
				if (strcmp(listaPartidas->partidas[numSala].jugadores[il].jugador.nombre,username)==0)
				{
					encontrado==1;
				}
			}
			else
				listaPartidas->partidas[numSala].jugadores[il-1] = listaPartidas->partidas[numSala].jugadores[il];
			il++;
		}
		listaPartidas->partidas[numSala].num--;
		return 0;
	}
	else
		return -1;
}

int EliminarPartida (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala){
	int j;
	strcpy(listaPartidas->partidas[numSala].estado,"Cancelada");
	for (j=0;j<listaPartidas->partidas[numSala].num;j++)
	{
		char resp[200];
		sprintf(resp,"25/%d",numSala);
		write(DameSocket(&miLista2,listaPartidas->partidas[numSala].jugadores[j].jugador.nombre), resp, strlen(resp));
	}
	return 0;
}

int EliminarJugador (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala){
	int il = 0;
	int encontrado = 0;
	while (il<listaPartidas->partidas[numSala].num)
	{
		if (encontrado==0)
		{
			if (strcmp(listaPartidas->partidas[numSala].jugadores[il].jugador.nombre,username)==0)
			{
				encontrado==1;
				if (il==0){
					EliminarPartida(listaPartidas,lista,username,numSala);
				}
			}
		}
		if (encontrado == 1)
			listaPartidas->partidas[numSala].jugadores[il-1] = listaPartidas->partidas[numSala].jugadores[il];
		il++;
	}
	listaPartidas->partidas[numSala].num--;
	return 0;
	
}


void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s = (int * ) socket;
	sock_conn = *s;
	int ret;
	MYSQL *conn;
	int err;
	
	//Establecer conexion con la base de datos
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//Inicializa la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "JUEGO",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	char peticion[512];
	char respuesta[512];
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
		
		//Atendemos la peticion
		char *t = strtok (peticion, "/");
		int codigo =  atoi (t);
		printf ("Codigo: %d \n", codigo);
		char nombre[20];
		
		if (codigo ==0) //peticion de desconexion
		{
			t = strtok( NULL, "/");
			char nombre_usuario[40];
			
			if (t!= NULL)
			{
				strcpy (nombre_usuario, t);
				pthread_mutex_lock(&mutex); //No interrumpir
				int eliminar = Elimina (&miLista2, nombre_usuario);
				pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
				if (eliminar==0)
					printf("Usuario eliminado de la lista de conectados. \n");
				else
					printf("Error al eliminar el usuario de la lista de conectados. \n");
			}
			terminar=1;
		}
		
		else if (codigo ==100) //Iniciar sesion
		{
			//ID/Contrase�a
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
					pthread_mutex_lock(&mutex); //No interrumpir
					int poner = Pon (&miLista2, nombre_usuario, sock_conn);
					pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
					if (poner == 3)
					{
						printf("Usuario anadido a la lista de conectados. \n");
						
					}
					else
						printf("Error al introducir al usuario a la lista de conectados. \n");
				}
				
				else 
				{
					sprintf (respuesta,"100/Incorrect");
				}	
			}	
			printf ("%s",respuesta);
		}
		
		else if (codigo ==101) //Registrarse
		{
			//ID/Contrase�a
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
				
				strcpy (consulta,"SELECT COUNT(ID) FROM JUGADORES;"); 
	
				err=mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al consultar datos2 de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				int max_ID;
				char ID[80];
				
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);

				max_ID = atoi(row[0]) + 1;

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
		
		else if (codigo ==1) //Consultar el numero de partidas que ha ganado un jugador
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
			
			sprintf (respuesta,"1/%s",row[0]);
		}
		
		else if (codigo == 2)//Consultar jugadores que han ganado una partida de mas de 10 minutos
		{
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];

			strcpy (consulta,"SELECT DISTINCT PARTIDAS.Ganador FROM (PARTIDAS, JUGADORES, PARTICIPACION)  WHERE  PARTIDAS.Duracion > 10   AND    JUGADORES.ID = PARTICIPACION.ID_J   AND    PARTIDAS.ID = PARTICIPACION.ID_P;"); 

			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
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
					row = mysql_fetch_row (resultado);
				}
				sprintf (respuesta,"2/%s", vector_nombres);
			}
			
		}
		
		else if (codigo == 3) //Consultar fecha y horade una patida
		{
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			char IDpartida [10];
			
			t = strtok( NULL, "/");
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

			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}

			else 
			{
				if (row[0] == 0)
					strcpy(respuesta,"3/NoExist");
				else
					sprintf (respuesta,"3/%s", row[0]);
			}

		}
		
		
		
		else if (codigo == 4)
		{
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			char dia [20];
			char usuario [20];
			
			t = strtok( NULL, "-");
			strcpy (usuario, t);
			
			t = strtok( NULL, "-");
			strcpy (dia, t);
			strcpy (consulta,"SELECT COUNT(PARTICIPACION.ID_J) FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE  SUBSTRING(PARTIDAS.Fecha_Hora, 1, 10)  = '");
			strcat (consulta,dia);
			strcat (consulta,"' AND PARTICIPACION.ID_J = (SELECT ID FROM JUGADORES WHERE Usuario = '");
			strcat (consulta,usuario);
			strcat (consulta, "')	AND    JUGADORES.ID = PARTICIPACION.ID_J	AND    PARTIDAS.ID = PARTICIPACION.ID_P;");

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
				char resp[20];
				sprintf(resp,"4/%d",atoi(row[0]));
				strcpy(respuesta, resp);
			}	
			
		}
		
		

		else if (codigo == 20) //Crear sala
		{
			char username[20];
			t = strtok( NULL, "/");
			strcpy (username, t);
			pthread_mutex_lock(&mutex);
			int err = CrearSala(&listaPartidas,&miLista2,username);
			pthread_mutex_unlock(&mutex);
			if (err != -1)
				sprintf(respuesta,"20/correct,%d",err);
			else
				sprintf(respuesta,"20/incorrect");
			
		}
		else if (codigo ==21) //Invitar
		{
			int numSala;
			char nombre_invitado[20];
			char nombre_host[20];
			
			t = strtok (NULL, "/");
			numSala =  atoi (t);
			
			t = strtok(NULL,"/");
			strcpy(nombre_host,t);
			
			t = strtok(NULL,"/");
			strcpy(nombre_invitado,t);
			pthread_mutex_lock(&mutex);
			int er = AnadirInvitado(&listaPartidas,&miLista2,nombre_invitado,numSala);
			pthread_mutex_unlock(&mutex);
			if (er==0)
			{
				char resp[300];
				sprintf(resp,"21/correct,%s",nombre_invitado);
				
				char nombres[100];
				strcpy(nombres, listaPartidas.partidas[numSala].jugadores[0].jugador.nombre);
				int l;
				for (l=1;l<listaPartidas.partidas[numSala].num;l++)
				{
					
					sprintf(nombres,"%s,%s",nombres,listaPartidas.partidas[numSala].jugadores[l].jugador.nombre);
					
				}
				
				int j;
				for (j=0;j<listaPartidas.partidas[numSala].num;j++)
				{
					if (strcmp(listaPartidas.partidas[numSala].jugadores[j].estado,"Aceptado")==0)
						write(DameSocket(&miLista2,listaPartidas.partidas[numSala].jugadores[j].jugador.nombre), resp, strlen(resp));
				}
				
				char invitacion [310];
				sprintf(invitacion, "22/%d,%s,%s",numSala,nombre_host,nombres);
				write(DameSocket(&miLista2,nombre_invitado), invitacion, strlen(invitacion));
			}
				
			
			else if (er==-2) //la partida no existe
			{
				sprintf(respuesta,"21/noexist,%s",nombre_invitado);
			}
			else if (er==-1)
			{
				sprintf(respuesta,"21/llena,%s",nombre_invitado);
			}
			
			
		}
		else if (codigo == 22) //responder invitacion
		{
			int nsala;
			t=strtok(NULL,"/");
			nsala = atoi(t);
			
			char nom[20];
			t=strtok(NULL,"/");
			strcpy(nom,t);
			
			t=strtok(NULL,"/");
			
			if (strcmp(t,"accept")==0)
			{
				pthread_mutex_lock(&mutex);
				int er = Aceptar(&listaPartidas,&miLista2,nom,nsala);
				pthread_mutex_unlock(&mutex);
				if (er == 0)
				{
					
					char noti[200];
					
					
					sprintf(noti, "23/%s,accept",nom);
					
					int j;
					for (j=0; j<listaPartidas.partidas[nsala].num; j++)
						if (listaPartidas.partidas[nsala].jugadores[j].jugador.socket != sock_conn)
							write (listaPartidas.partidas[nsala].jugadores[j].jugador.socket, noti, strlen(noti));
					
				}
					
			}
			if (strcmp(t,"reject")==0)
			{
				pthread_mutex_lock(&mutex);
				int er = Rechazar(&listaPartidas,&miLista2,nom,nsala);
				pthread_mutex_unlock(&mutex);
				
				char noti[200];
				
				sprintf(noti, "23/%s,reject",nom);
				
				int j;
				for (j=0; j<listaPartidas.partidas[nsala].num; j++)
					if (listaPartidas.partidas[nsala].jugadores[j].jugador.socket != sock_conn)
						write (listaPartidas.partidas[nsala].jugadores[j].jugador.socket, noti, strlen(noti));
				
				
			}
		}
		else if (codigo==24)
		{
			int nusala;
			t = strtok(NULL,"/");
			nusala = atoi(t);
			
			char nm[20];
			t=strtok(NULL,"/");
			strcpy(nm,t);
			
			pthread_mutex_lock(&mutex);
			EliminarJugador(&listaPartidas,&miLista2,nm,nusala);
			pthread_mutex_unlock(&mutex);
			
			char resp[50];
			sprintf(resp,"24/%s",nm);
			int k;
			for (k=0;k<listaPartidas.partidas[nusala].num;k++)
			{
				write(listaPartidas.partidas[nusala].jugadores[k].jugador.socket, resp,strlen(resp));
			}
			
		}
		if ((codigo != 0)&&(codigo != 21)&&(codigo != 22)&&(codigo != 24))
		{
			// Enviamos respuesta
			printf("\nrespuesta: %s\n",respuesta);
			
			write (sock_conn, respuesta, strlen(respuesta));
		}
		
		if ((codigo ==1)||(codigo ==2)||(codigo ==3)||(codigo ==4))
		{
			pthread_mutex_lock(&mutex); //No interrumpir
			contador_servicios = contador_servicios +1;
			pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
			//notificar
			char notificacion[20];
			sprintf (notificacion,"6/%d",contador_servicios);
			int j;
			for (j=0; j<i; j++)
				write (sockets[j], notificacion, strlen(notificacion));
			
		}
		if (codigo ==100 || codigo==0)
		{
			char misConectados [300];
			char notificacion [310];
			DameConectados (&miLista2, misConectados);
			
			sprintf(notificacion, "7/%s",misConectados);
			int j;
			for (j=0; j<i; j++)
				
				write (sockets[j], notificacion, strlen(notificacion));
		}
	}
	//Finalizamos el servicio al cliente
	close(sock_conn);
	mysql_close (conn);
}	

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;

	// Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// Establecemos el puerto 
	serv_adr.sin_port = htons(9235);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	pthread_t thread[100];
	i=0;
	
	//Inicializacion de variables globales
	contador_servicios = 0;
	miLista2.num = 0;
	
	// Bucle infinito
	for (;;){
		printf ("Escuchando...\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion.\n");
		//sock_conn es el socket que usaremos para este cliente
		sockets[i] = sock_conn;
		pthread_create (&thread[i], NULL, AtenderCliente, &sockets[i]);
		i++;
	}
}
