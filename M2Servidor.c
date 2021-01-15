////////////////////////////////////////////////////////////////////////////////
//////////////////////S E R V I D O R   D E L   J U E G O.//////////////////////
///////////////////////CATALINA MOSTEIRO Y GERARD NADAL.////////////////////////
////////////////////////////////////////////////////////////////////////////////

/////////////////////////////L I B R E R I A S./////////////////////////////////
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
////////////////////////////////////////////////////////////////////////////////

////////////////////////////E S T R U C T U R A S.//////////////////////////////
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
	TPartida partidas[10];
	int num;
}TPartidas;
////////////////////////////////////////////////////////////////////////////////

/////////////////////V A R I A B L E S   G L O B A L E S.///////////////////////
int contador_servicios;
int i;
int sockets[100];
ListaConectados miLista2;
TPartidas listaPartidas;

//M�todo para cambiar las variables globales.
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////F U N C I O N E S.//////////////////////////////
int Pon (ListaConectados *lista, char nombre[20], int socket)
{
	//Anade un usuario a una lista.
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

int DameSocket (ListaConectados *lista, char nombre[20])
{
	//Devuelve el socket del nombre introducido si este esta en la lista.
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

int DamePosicion (ListaConectados *lista, char nombre[20])
{
	//Devuelve la posicion del nombre introducido si esta en la lista.
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

int Elimina (ListaConectados *lista, char nombre[20])
{
	//Elimina un usuario de una lista.
	int pos = DamePosicion (lista, nombre);
	if (pos==-1)
		return -1;
	else 
	{
		int i;
		for (i=pos; i<lista->num-1; i++)
		{
			lista-> conectados [i] = lista->conectados[i+1];
		}
		lista ->num--;
		return 0;
	}
}

void DameConectados (ListaConectados *lista, char conectados[300])
{
	//Pone en un vector los nombres de los usuarios de la lista entre comas.
	//Primero pone el numero de conectados. Ej: 3,Maria,Juan,Pedro
	sprintf (conectados, "%d", lista->num);
	printf ("%d",lista->num);
	int i;
	for (i=0; i<lista->num; i++)
		sprintf (conectados, "%s,%s", conectados, lista->conectados[i].nombre);
}

void DameTodosSockets(ListaConectados *lista,char conectados[300],char sockets[300])
{
	//Pone en un vector los sockets de conectados separados por comas. Ej: 2,3,4
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
int CrearSala (TPartidas *listaPartidas, ListaConectados *lista, char username[20])
{
	//Anade una partida a la lista de partidas y anade al jugador que la ha creado a esta
	if (listaPartidas->num<10)
	{
		strcpy(listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].jugador.nombre,username);
		listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].jugador.socket = DameSocket(lista,username);
		strcpy(listaPartidas->partidas[listaPartidas->num].jugadores[listaPartidas->partidas[listaPartidas->num].num].estado,"Aceptado");
		listaPartidas->partidas[listaPartidas->num].num = listaPartidas->partidas[listaPartidas->num].num +1;
		strcpy(listaPartidas->partidas[listaPartidas->num].estado,"Pendiente");
		
		listaPartidas->num = listaPartidas->num +1;
		return listaPartidas->num-1;
	}
	else
		return -1;
}

int AnadirInvitado (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala)
{
	//Anade un jugador a la partida pero como a invitado, falta la respuesta de este
	int s_invitado = DameSocket(lista,username);
	if (listaPartidas->num<numSala)
		return -1;
	else
	{
		if(listaPartidas->partidas[numSala].num<4)
		{
			if (strcmp(listaPartidas->partidas[numSala].estado,"Pendiente")==0)
			{
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

int Aceptar (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala)
{
	//El jugador invitado ha aceptado la partida, por lo que se cambia su estado a aceptado
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

int Rechazar (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala)
{
	//El usuario invitado ha rechazado la partida, por lo que se borra de la lista
	if (strcmp(listaPartidas->partidas[numSala].estado,"Pendiente")==0)
	{
		int il = 0;
		int encontrado = 0;
		while (il<listaPartidas->partidas[numSala].num)
		{
			if (encontrado==0)
			{
				if (strcmp(listaPartidas->partidas[numSala].jugadores[il].jugador.nombre,username)==0)
					encontrado==1;
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

int EliminarJugador (TPartidas *listaPartidas, ListaConectados *lista, char username[20], int numSala)
{
	//Elimina un jugador de la partida
	int il = 0;
	int encontrado = 0;
	while (il<listaPartidas->partidas[numSala].num)
	{
		if (encontrado==0)
		{
			if (strcmp(listaPartidas->partidas[numSala].jugadores[il].jugador.nombre,username)==0)
				encontrado==1;
		}
		else
			listaPartidas->partidas[numSala].jugadores[il-1] = listaPartidas->partidas[numSala].jugadores[il];
		il++;
	}
	listaPartidas->partidas[numSala].num--;
	return 0;
}
////////////////////////////////////////////////////////////////////////////////

///////////////////////A T E N D E R   C L I E N T E. //////////////////////////
void *AtenderCliente (void *socket)
{
	//Definir el socket.
	int sock_conn;
	int *s;
	s = (int * ) socket;
	sock_conn = *s;
	
	//Variables para la conexion con la base de datos.
	int ret;
	MYSQL *conn;
	int err;
	
	//Establecer conexion con la base de datos.
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//Inicializar la conexion con la base de datos.
	conn = mysql_real_connect(conn,"shiva2.upc.es","root","mysql","M2JUEGO",0,NULL,0);
	if (conn == NULL) {
		printf ("Error al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//Variables para las peticiones y para definir el bucle.
	char peticion[512];
	char respuesta[512];
	int terminar =0;
	
	//Bucle para atender las peticiones realizadas por el cliente.
	while (terminar == 0)
	{
		//Variables.
		char respuesta[512];
		char nombre[20];
		
		//Metodos para las consultas a la base de datos.
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		
		//Se recibe la peticion.
		ret = read(sock_conn,peticion, sizeof(peticion));
		peticion[ret]='\0';//Eliminar lo escrito despues en el buffer.
		printf ("Peticion recibida, se solicita: %s\n",peticion);
		
		//Es extraido el codigo del mensaje de peticion.
		char *t = strtok (peticion, "/");
		int codigo =  atoi (t);
		
		///////////////////////P E T I C I O N E S./////////////////////////////
		//////////////////////D E S C O N E X I O N.////////////////////////////
		if (codigo == 0) 
		{
			//Extraer el nombre del mensaje de peticion.
			t = strtok( NULL, "/");
			char nombre_usuario[40];
			
			//Eliminar el usuario de la lista.
			if (t!= NULL)
			{
				strcpy (nombre_usuario, t);
				pthread_mutex_lock(&mutex); //Bloqueo del mutex.
				int eliminar = Elimina (&miLista2, nombre_usuario); 
				pthread_mutex_unlock(&mutex); //Desbloqueo del mutex.
				if (eliminar == 0)
					printf("Desconexion y usuario eliminado de la lista. \n");
				else
					printf("Error al eliminar el usuario de la lista. \n");
			}
			terminar = 1; //Necesario para salir del bucle.
		}
		////////////////////////////////////////////////////////////////////////
		
		//////////////////I N I C I O   D E   S E S I O N.//////////////////////
		else if (codigo == 100) 
		{
			//Se extrae el nombre de usuario del mensaje de peticion.
			char nombre_usuario[40];
			t = strtok( NULL, "/");
			strcpy (nombre_usuario, t);
			
			//Variable para consultar la base de datos.
			char consulta [800];
			
			//Consultar la contrasena del nombre de usuario recibido.
			strcpy (consulta, "SELECT JUGADORES.Contrasena FROM JUGADORES WHERE JUGADORES.Usuario = '");
			strcat (consulta, nombre_usuario);
			strcat (consulta,"'");
			err = mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar la base de datos. %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)//El nombre de usuario no esta en la base de datos.
			{
				printf ("El usuario no existe en la base de datos.\n");
				sprintf (respuesta,"100/NoUser");
			}
			
			else//El nombre de usuario si esta en la base de datos.
			{
				//Se extrae la contrasena del mensaje de peticion.
				char contrasena [40];
				t = strtok( NULL, "/");
				strcpy (contrasena, t);
				 
				//Comparacion entre la contrasena de la base con la introducida.
				if (strcmp(contrasena,row[0]) == 0)//Son iguales, contrasena correcta.
				{
					sprintf (respuesta,"100/Correct");
					pthread_mutex_lock(&mutex);//Bloqueo del mutex.
					int poner = Pon (&miLista2, nombre_usuario, sock_conn);
					pthread_mutex_unlock(&mutex);//Desbloqueo del mutex.
					if (poner == 3)
					{
						printf("Nombre y contrasena correctos. Usuario anadido a la lista de conectados. \n");
					}
					else
						printf("Error al introducir al usuario a la lista de conectados. \n");
				}
				
				else //Son diferentes, contrasena incorrecta.
				{
					printf("La contrasena introducida es incorrecta. \n");
					sprintf (respuesta,"100/Incorrect");
				}	
			}	
		}
		////////////////////////////////////////////////////////////////////////
		
		///////////////////////////R E G I S T R O./////////////////////////////
		else if (codigo == 101)
		{
			//Se extrae el nombre de usuario del mensaje de peticion.
			char nombre_usuario[40];
			t = strtok( NULL, "/");
			strcpy (nombre_usuario, t);
			
			//Se extrae la contrasena del mensaje de peticion.
			char contrasena [40];
			t = strtok( NULL, "/");
			strcpy (contrasena, t);
			
			//Comprobacion de si hay un ID ocupado con el nombre introducido.
			char consulta [800];
			strcpy (consulta,"SELECT JUGADORES.ID FROM JUGADORES WHERE JUGADORES.Usuario ='"); 
			strcat (consulta,nombre_usuario);
			strcat (consulta,"';");
			
			err = mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar la base de datos. %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}

			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)//No hay un ID con ese nombre de usuario.
			{
				//Se selecciona los IDs de los jugadores.
				strcpy (consulta,"SELECT COUNT(ID) FROM JUGADORES;"); 
				err = mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al consultar la base de datos. %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				int max_ID;
				char ID[80];
				
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				
				max_ID = atoi(row[0]) + 1;//Se asigna un ID mas.
				
				//Consulta para insertar el nuevo usuario en la base de datos.
				strcpy (consulta, "INSERT INTO JUGADORES VALUES (");
				sprintf(ID, "%d", max_ID);//Convertimos el ID en un string.
				printf("ID: %s\n",ID);
				strcat (consulta, ID); //Concatenamos el ID.
				strcat (consulta, ",'");
				strcat (consulta, nombre_usuario);//Concatenamos el nombre.
				strcat (consulta, "','");
				strcat (consulta, contrasena);//Concatenamos la contrasena.
				strcat (consulta, "');");
				
				err = mysql_query(conn, consulta);
				if (err!=0) {
					printf ("Error al introducir el usuario en la base de datos. %u %s\n", 
							mysql_errno(conn), mysql_error(conn));
					sprintf (respuesta, "101/Incorrect2");
				}
				
				else
				{
					sprintf (respuesta,"101/Correct");
					printf ("Usuario anadido correctamente a la base de datos.\n");
				}	
			}
			else//Si hay un ID con ese nombre de usuario.
			{
				sprintf (respuesta, "101/Incorrect");
				printf ("Nombre de usuario ya existente en la base de datos.\n");
			}
		}
		////////////////////////////////////////////////////////////////////////
		
		////C O N S U L T A  1: numero de partidas que ha ganado un jugador.////
		else if (codigo == 1)
		{	
			//Variables para realizar la consulta a la base de datos.
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			
			//Se extrae el nombre de la peticion recibida.
			char nombre[40];
			t = strtok( NULL, "/");
			strcpy (nombre, t);

			//Elaboracion de la consulta.
			strcpy (consulta,"SELECT COUNT(PARTIDAS.ID) FROM (PARTIDAS) WHERE PARTIDAS.ganador= '"); 
			strcat (consulta, nombre);
			strcat (consulta, "';");

			//Consulta en la base de datos.
			err = mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//Se recoge el resultado de la consulta.
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
	
			//El resultado de la consulta es nulo.
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta.\n");
			}
			
			//El resultado de la consulta no es nulo.
			else 
			{
				printf ("El jugador %s ha ganado %s partidas.\n", nombre, row[0]);
			}
			
			//Se escribe la respuesta para el enviar al cliente.
			sprintf (respuesta,"1/%s",row[0]);
		}
		////////////////////////////////////////////////////////////////////////
	
		//C O N S U L T A  2: jugadores que han ganado una partida de mas de 10 minutos.//
		else if (codigo == 2)
		{
			//Variables para realizar la consulta a la base de datos.
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];

			//Elaboracion de la consulta.
			strcpy (consulta,"SELECT DISTINCT PARTIDAS.Ganador FROM (PARTIDAS, JUGADORES, PARTICIPACION)  WHERE  PARTIDAS.Duracion > 10   AND    JUGADORES.ID = PARTICIPACION.ID_J   AND    PARTIDAS.ID = PARTICIPACION.ID_P;"); 

			//Consulta en la base de datos.
			err = mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//Se recoge el resultado de la consulta.
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			//Variable necesaria para la respuesta al cliente.
			char vector_nombres[500];
			
			//El resultado de la consulta es nulo.
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta.\n");
			}

			//El resultado de la consulta no es nulo.
			else 
			{
				//Elaboraci�n de la respuesta para el cliente.
				strcpy(vector_nombres,row[0]);
				row = mysql_fetch_row (resultado);
				while (row !=NULL) 
				{
					strcat(vector_nombres," ");
					strcat(vector_nombres,row[0]);
					row = mysql_fetch_row (resultado);
				}
				//Se escribe en la variable de la respuesta que se envia al cliente.
				sprintf (respuesta,"2/%s", vector_nombres);
			}
		}
		////////////////////////////////////////////////////////////////////////
		
		/////////////C O N S U L T A  3: fecha y horade una patida./////////////
		else if (codigo == 3)
		{
			//Variables para realizar la consulta a la base de datos.
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			
			//Se extrae el ID de la partida de la peticion recibida.
			char IDpartida [10];
			t = strtok( NULL, "/");
			strcpy (IDpartida, t);
			
			//Elaboracion de la consulta.
			strcpy (consulta,"SELECT PARTIDAS.Fecha_Hora FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE  PARTIDAS.ID = ");
			strcat (consulta,IDpartida);
			strcat (consulta," AND    JUGADORES.ID = PARTICIPACION.ID_J		AND    PARTIDAS.ID = PARTICIPACION.ID_P;"); 

			//Consulta en la base de datos.
			err = mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//Se recoge el resultado de la consulta.
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			//El resultado de la consulta es nulo.
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta.\n");
			}
			
			//El resultado de la consulta no es nulo.
			else 
			{
				//Se escribe en la variable de la respuesta para el cliente.
				if (row[0] == 0)
					strcpy(respuesta,"3/NoExist");
				else
					sprintf (respuesta,"3/%s", row[0]);
			}
		}
		////////////////////////////////////////////////////////////////////////
		
		//////////C O N S U L T A  4: partidas ganadas un dia concreto./////////
		else if (codigo == 4)
		{
			//Variables para realizar la consulta a la base de datos.
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			
			//Se extrae el usuario de la peticion recibida.
			char usuario [20];
			t = strtok( NULL, "-");
			strcpy (usuario, t);
			
			//Se extrae el dia de la peticion recibida.
			char dia [20];
			t = strtok( NULL, "-");
			strcpy (dia, t);
			
			//Elaboracion de la consulta.
			strcpy (consulta,"SELECT COUNT(PARTIDAS.ID) FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE  SUBSTRING(PARTIDAS.Fecha_Hora, 1, 10)  = '");
			strcat (consulta,dia);
			strcat (consulta,"' AND PARTIDAS.Ganador ='");
			strcat (consulta,usuario);
			strcat (consulta, "';");

			//Consulta en la base de datos.
			err = mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//Se recoge el resultado de la consulta.
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);

			//El resultado de la consulta es nulo.
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta.\n");
			}
			
			//El resultado de la consulta no es nulo.
			else 
			{
				//Se escribe la respuesta que se enviara al cliente.
				char resp[20];
				sprintf(resp,"4/%d",atoi(row[0]));
				strcpy(respuesta, resp);
			}	
		}
		
		////////////////////////////////////////////////////////////////////////
		
		/////////////////////C R E A R   S A L A.///////////////////////////////
		else if (codigo == 20) 
		{
			//Extraeccion del nombre de usuario de la peticion recibida.
			char username[20];
			t = strtok( NULL, "/");
			strcpy (username, t);
			
			//Se crea la sala.
			int err = CrearSala(&listaPartidas,&miLista2,username);
			
			//La sala se ha creado bien.
			if (err != -1)
				sprintf(respuesta,"20/correct,%d",err);
			
			//La sala no se ha creado bien.
			else
				sprintf(respuesta,"20/incorrect");
			
		}
		////////////////////////////////////////////////////////////////////////
		
		//////////////////I N V I T A R   A   L A   S A L A.////////////////////
		else if (codigo == 21)
		{
			//Se extrae el numero de sala de la peticion recibida.
			int numSala;
			t = strtok (NULL, "/");
			numSala =  atoi (t);
			
			//Se extrae el nombre de host de la peticion recibida.
			char nombre_host[20];
			t = strtok(NULL,"/");
			strcpy(nombre_host,t);
			
			//Se extrae el nombre de invitado de la peticion recibida.
			char nombre_invitado[20];
			t = strtok(NULL,"/");
			strcpy(nombre_invitado,t);
			
			//Anade invitado a una partida, por lo que si se ha anadido correctamente envia un mensaje al invitado y a los actuales miembros de la sala
			int er = AnadirInvitado(&listaPartidas,&miLista2,nombre_invitado,numSala);
			if (er == 0)
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
			
			else if (er == -2)//La partida no existe.
			{
				sprintf(respuesta,"21/noexist,%s",nombre_invitado);
			}
			else if (er == -1)
			{
				sprintf(respuesta,"21/llena,%s",nombre_invitado);
			}
		}
		////////////////////////////////////////////////////////////////////////
		
		//////R E S P O N D E R   L A   I N V I T A C I O N   D E  S A L A./////
		else if (codigo == 22) 
		{
			//Se extrae el numero de sala de la peticion recibida.
			int nsala;
			t = strtok(NULL,"/");
			nsala = atoi(t);
			
			//Se extrae el nombrede la peticion recibida.
			char nom[20];
			t = strtok(NULL,"/");
			strcpy(nom,t);
			
			//Se extrae el resto de la peticion recibida.
			t = strtok(NULL,"/");
			
			//Si el jugador acepta se le cambia el estado a aceptado y se notifica a los miembros de la sala
			if (strcmp(t,"accept")==0)
			{
				int er = Aceptar(&listaPartidas, &miLista2, nom, nsala);
				if (er == 0)
				{
					char noti[200];
					sprintf(noti, "23/%s,accept", nom);
					
					int j;
					for (j=0; j<listaPartidas.partidas[nsala].num; j++)
						if (listaPartidas.partidas[nsala].jugadores[j].jugador.socket != sock_conn)
							write (listaPartidas.partidas[nsala].jugadores[j].jugador.socket, noti, strlen(noti));
					
				}
			}
			//Si rechaza se elimina al jugador de la sala y se notifica a los miembros de la sala
			if (strcmp(t,"reject")== 0)
			{
				int er = Rechazar(&listaPartidas, &miLista2, nom, nsala);
				char noti[200];
				sprintf(noti, "23/%s,reject", nom);
				
				int j;
				for (j=0; j<listaPartidas.partidas[nsala].num; j++)
					if (listaPartidas.partidas[nsala].jugadores[j].jugador.socket != sock_conn)
						write (listaPartidas.partidas[nsala].jugadores[j].jugador.socket, noti, strlen(noti));
			}
		}
		////////////////////////////////////////////////////////////////////////
		
		//////////////////S A L I R   D E   L A   S A L A.//////////////////////
		else if (codigo == 24)
		{
			//Se extrae el numero de sala de la peticion recibida.
			int nusala;
			t = strtok(NULL,"/");
			nusala = atoi(t);
			
			//Se extrae el nombre del jugador de la peticion recibida.
			char nm[20];
			t=strtok(NULL,"/");
			strcpy(nm, t);
			
			//Se elimina el jugador de la lista de partidas.
			EliminarJugador(&listaPartidas, &miLista2, nm, nusala);
			
			//Creacion de la respuesta que se enviara los jugadores de la sala.
			char resp[50];
			sprintf(resp,"24/%s", nm);
			
			//Notificacon de la salida de la sala a los jugadores de la misma.
			int k;
			for (k=0;k<listaPartidas.partidas[nusala].num;k++)
			{
				write(listaPartidas.partidas[nusala].jugadores[k].jugador.socket, resp, strlen(resp));
			}
			
		}
		////////////////////////////////////////////////////////////////////////
		
		
		////////////////////E N V I A R  M E N S A J E//////////////////////////
		else if (codigo==25)//Enviar mensaje
		{
			char frase[100];
			int sala;
			char notificacion [310];
			char nombre_usuario[40];
			t = strtok( NULL, "/");
			sala = atoi(t);
			t = strtok( NULL, "/");
			strcpy (nombre_usuario, t);
			t = strtok (NULL, "/");
			strcpy (frase, t);
			sprintf(notificacion, "253/%s/%s", nombre_usuario ,frase);
			int j;
			for (j=0; j<listaPartidas.partidas[sala].num; j++)
				write (listaPartidas.partidas[sala].jugadores[j].jugador.socket, notificacion, strlen(notificacion));
		}
		////////////////////////////////////////////////////////////////////////
		
		///////////////////I N I C I A R  P A R T I D A/////////////////////////
		else if (codigo == 500)
		{
			int numerosala;
			t = strtok(NULL,"/");
			numerosala = atoi(t);
			
			strcpy(listaPartidas.partidas[numerosala].estado,"Iniciada");
			
			//Eliminamos de la sala a los jugadores que no han aceptado
			int i = 0;
			while (i<listaPartidas.partidas[numerosala].num)
			{
				if (listaPartidas.partidas[numerosala].jugadores[i].estado=="Invitado")
				{
					EliminarJugador(&listaPartidas,&miLista2,listaPartidas.partidas[numerosala].jugadores[i].jugador.nombre,numerosala);
				}
				else
					i++;
			}
				
			//Enviamos notificacion de inicio de partida a los jugadores de la sala
			char resp[50];
			sprintf(resp,"500/%d", listaPartidas.partidas[numerosala].num);
			int k;
			char resp2[50];
			for (k=0;k<listaPartidas.partidas[numerosala].num;k++)
			{
				sprintf(resp2,"%s/%d",resp,k+1);
				write(listaPartidas.partidas[numerosala].jugadores[k].jugador.socket, resp2, strlen(resp2));
			}
				
				
		}
		////////////////////////////////////////////////////////////////////////
		
		//////////////////////M U E R E  J U G A D O R//////////////////////////
		else if (codigo==501)
		{
			int numerosala;
			t = strtok(NULL,"/");
			numerosala = atoi(t);
			
			int jug;
			t = strtok(NULL,"/");
			jug = atoi(t);
			
			//Cambiamos el estado a muerto
			listaPartidas.partidas[numerosala].jugadores[jug].estado=="Muerto";
			
			//notificamos a los jugadores que siguen jugando
			char resp[50];
			sprintf(resp,"501/%d", jug);
			int k;
			for (k=0;k<listaPartidas.partidas[numerosala].num;k++)
			{
				if (listaPartidas.partidas[numerosala].jugadores[k].estado!="Muerto")
					write(listaPartidas.partidas[numerosala].jugadores[k].jugador.socket, resp, strlen(resp));
			}
			
		}
		
		////////////////////////////////////////////////////////////////////////
		
		//////////////////////M U E V E  J U G A D O R//////////////////////////
		else if (codigo==502)
		{
			int numerosala;
			t = strtok(NULL,"/");
			numerosala = atoi(t);
			
			int jug;
			t = strtok(NULL,"/");
			jug = atoi(t);
			
			int dir;
			t = strtok(NULL,"/");
			dir = atoi(t);
			
			//notificamos a los jugadores que siguen jugando
			char resp[50];
			sprintf(resp,"502/%d/%d", jug, dir);
			int k;
			for (k=0;k<listaPartidas.partidas[numerosala].num;k++)
			{
				if (listaPartidas.partidas[numerosala].jugadores[k].estado!="Muerto")
					if (listaPartidas.partidas[numerosala].jugadores[k].jugador.socket != sock_conn)
						write(listaPartidas.partidas[numerosala].jugadores[k].jugador.socket, resp, strlen(resp));
			}
			
		}
		////////////////////////////////////////////////////////////////////////

		////////////////////P A R T I D A  A C A B A D A////////////////////////
		else if (codigo==503)
		{
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			int npartida;

			int numerosala;
			t = strtok(NULL,"/");
			numerosala = atoi(t);
			
			int jug;
			t = strtok(NULL,"/");
			jug = atoi(t)-1;
			
			char fecha[30];

			t = strtok(NULL,"/");
			strcpy(fecha,t);
			strcat(fecha,"/");

			t = strtok(NULL,"/");
			strcat(fecha,t);
			strcat(fecha,"/");

			t = strtok(NULL,"/");
			strcat(fecha,t);

			

			//Elaboracion de la consulta.
			strcpy (consulta,"SELECT COUNT(ID) FROM (PARTIDAS);");
			
			//Consulta en la base de datos.
			err = mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}

			//Se recoge el resultado de la consulta.
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);

			//El resultado de la consulta es nulo.
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta.\n");
			}
			
			//El resultado de la consulta no es nulo.
			else 
			{
				npartida= atoi(row[0])+1;
			}	
			char idpartidaa[4];
			char nombreejugador[20];
			strcpy(nombreejugador,listaPartidas.partidas[numerosala].jugadores[jug].jugador.nombre);

			//Consulta para insertar la partida
			strcpy (consulta, "INSERT INTO PARTIDAS VALUES (");
			sprintf(idpartidaa, "%d", npartida);//Convertimos id partida en un string.
			strcat (consulta, idpartidaa); //Concatenamos el ID.
			strcat (consulta, ",'");
			strcat (consulta, fecha);//Concatenamos la fecha.
			strcat (consulta, "', 1, '");
			strcat (consulta, nombreejugador);
			strcat (consulta, "');");


			
			
			err = mysql_query(conn, consulta);
			if (err!=0) {
				printf ("Error al introducir el usuario en la base de datos. %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
			}


			
		}
		////////////////////////////////////////////////////////////////////////
		//////////////////E N V I A R   R E S P U E S T A.//////////////////////
		if ((codigo != 0)&&(codigo != 21)&&(codigo != 22)&&(codigo != 24)&&(codigo != 25)&&(codigo != 500)&&(codigo != 501)&&(codigo != 502)&&(codigo != 503))
		{//Codigos que no generan respuesta para el cliente.
			
			//Imprimimos la respuesta para tener un seguimiento del servidor.
			printf("Respuesta: %s\n",respuesta);
			
			//Evio de la respuesta al cliente.
			write (sock_conn, respuesta, strlen(respuesta));
		}
		////////////////////////////////////////////////////////////////////////
		
		//////////////C O N T A D O R   D E   S E R V I C I O S.////////////////
		if ((codigo == 1)||(codigo == 2)||(codigo == 3)||(codigo == 4))
		{
			//Incrementamos el numero de servicios realizados al servidor.
			pthread_mutex_lock(&mutex);//Bloqueo del mutex.
			contador_servicios = contador_servicios +1; 
			pthread_mutex_unlock(&mutex);//Desbloqueo del mutex.
			
			//Notificacion del numero de servicios a los usuarios conectados.
			char notificacion[20];
			sprintf (notificacion,"6/%d",contador_servicios);
			int j;
			for (j=0; j<i; j++)
				write (sockets[j], notificacion, strlen(notificacion));
		}
		
		////////////////////////////////////////////////////////////////////////
		
		////////////////L I S T A   D E   C O N E C T A D O S.//////////////////
		if (codigo == 100 || codigo == 0)
		{
			//Variables y metodos necesarios para notificar.
			char misConectados [300];
			char notificacion [310];
			DameConectados (&miLista2, misConectados);
			
			//Notificacion de los usuarios conectados a todos los usuarios conectados.
			sprintf(notificacion, "7/%s",misConectados);
			int j;
			for (j=0; j<i; j++)
				write (sockets[j], notificacion, strlen(notificacion));
		}
	
	}
	//Se finaliza el servicio al cliente cerrando la conexion con la base de datos y con el socket.
	mysql_close (conn);
	close(sock_conn);
}	
////////////////////////////////////////////////////////////////////////////////

////////////////////E J E C U C I O N   P R I N C I P A L.//////////////////////
int main(int argc, char *argv[])
{
	//Definicion del socket.
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error en la creacion del socket.\n");
	
	//Se realiza el bind al puerto.
	memset(&serv_adr, 0, sizeof(serv_adr));//Es inicializado a zero el serv_addr.
	serv_adr.sin_family = AF_INET;
	
	//Asociacion del socket a una direccion IP de la maquina. 
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);//Cambio del numero que recibe al formato necesario.
	
	//El puerto es establecido.
	serv_adr.sin_port = htons(50005);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error en el bind.\n");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el listen.\n");

	//Creacion del vector de threads para las diversas conexiones cliente-servidor.
	pthread_t thread[100];
	
	//Inicializacion de variables globales,
	contador_servicios = 0;
	miLista2.num = 0;
	i = 0;
	
	for (;;) // Bucle infinito
	{
		printf ("Escuchando...\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("Se ha establecido la conexion cliente-servidor.\n");
		sockets[i] = sock_conn;//Socket asignado al cliente.
		pthread_create (&thread[i], NULL, AtenderCliente, &sockets[i]);
		i++;
	}
}
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
