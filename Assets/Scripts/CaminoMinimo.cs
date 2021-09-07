using UnityEngine.UI;
using UnityEngine;
using AssemblyCSharp;
using System;

public class CaminoMinimo : MonoBehaviour {
	private static int NumClicks = 0;
	private static GrafMatPeso grafo;
	GameObject[] nodos;
	private static Dijkstra caminoMasCorto;
	private static String camino;
	private static Color colorInicial;
	private const int NUMERO_DE_CIRCULOS = 14;
	// Método usado para inicializar los datos
	void Awake()
	{
        //Se crea el vector que contiene los datos de los departamentos
        //Que son circulos en el mapa
        nodos = new GameObject[15];

		//Llenamos el vector
		for (int i = 1; i < nodos.Length; i++)
		{
            nodos[i] = GameObject.Find("Circle" + i.ToString());
		}


		// grafo con 14 vértices
		grafo = new GrafMatPeso(14);

		for (int i = 1; i <= 14; i++)
		{
			grafo.NuevoVertice(i.ToString());
		}

		try
		{
			//Conectando los nodos en el grafo, es un grafo no dirigido por lo tanto se crea
			//también una coneccion en sentido contrario

			grafo.NuevoArco("1", "2", Distancia(nodos[1], nodos[2]));
			grafo.NuevoArco("1", "3", Distancia(nodos[1], nodos[3]));
            grafo.NuevoArco("1", "8", Distancia(nodos[1], nodos[8]));

            grafo.NuevoArco("2", "1", Distancia(nodos[2], nodos[1]));
			grafo.NuevoArco("2", "3", Distancia(nodos[2], nodos[3]));
			grafo.NuevoArco("2", "5", Distancia(nodos[2], nodos[5]));

			grafo.NuevoArco("3", "1", Distancia(nodos[3], nodos[1]));
			grafo.NuevoArco("3", "8", Distancia(nodos[3], nodos[8]));
			grafo.NuevoArco("3", "4", Distancia(nodos[3], nodos[4]));
			grafo.NuevoArco("3", "6", Distancia(nodos[3], nodos[6]));
            grafo.NuevoArco("3", "2", Distancia(nodos[3], nodos[2]));

           
            grafo.NuevoArco("4", "3", Distancia(nodos[4], nodos[3]));
			grafo.NuevoArco("4", "5", Distancia(nodos[4], nodos[5]));
			grafo.NuevoArco("4", "6", Distancia(nodos[4], nodos[6]));
			

			grafo.NuevoArco("5", "2", Distancia(nodos[5], nodos[2]));
			grafo.NuevoArco("5", "4", Distancia(nodos[5], nodos[4]));
			grafo.NuevoArco("5", "6", Distancia(nodos[5], nodos[6]));
			grafo.NuevoArco("5", "14", Distancia(nodos[5], nodos[14]));
			grafo.NuevoArco("5", "11", Distancia(nodos[5], nodos[11]));

			grafo.NuevoArco("6", "4", Distancia(nodos[6], nodos[4]));
			grafo.NuevoArco("6", "5", Distancia(nodos[6], nodos[5]));
			grafo.NuevoArco("6", "7", Distancia(nodos[6], nodos[7]));
			grafo.NuevoArco("6", "3", Distancia(nodos[6], nodos[3]));
            grafo.NuevoArco("6", "11", Distancia(nodos[6], nodos[11]));

            grafo.NuevoArco("7", "6", Distancia(nodos[7], nodos[6]));
			grafo.NuevoArco("7", "8", Distancia(nodos[7], nodos[8]));
			grafo.NuevoArco("7", "10", Distancia(nodos[7], nodos[10]));
			grafo.NuevoArco("7", "9", Distancia(nodos[7], nodos[9]));

			grafo.NuevoArco("8", "3", Distancia(nodos[8], nodos[3]));
			grafo.NuevoArco("8", "1", Distancia(nodos[8], nodos[1]));
			grafo.NuevoArco("8", "7", Distancia(nodos[8], nodos[7]));
	
			grafo.NuevoArco("9", "7", Distancia(nodos[9], nodos[7]));
			grafo.NuevoArco("9", "10", Distancia(nodos[9], nodos[10]));
			grafo.NuevoArco("9", "12", Distancia(nodos[9], nodos[12]));

			grafo.NuevoArco("10", "7", Distancia(nodos[10], nodos[7]));
			grafo.NuevoArco("10", "11", Distancia(nodos[10], nodos[11]));
			grafo.NuevoArco("10", "9", Distancia(nodos[10], nodos[9]));
			grafo.NuevoArco("10", "12", Distancia(nodos[10], nodos[12]));

			grafo.NuevoArco("11", "10", Distancia(nodos[11], nodos[10]));
			grafo.NuevoArco("11", "12", Distancia(nodos[11], nodos[12]));
            grafo.NuevoArco("11", "6", Distancia(nodos[11], nodos[6]));
            grafo.NuevoArco("11", "13", Distancia(nodos[11], nodos[13]));
            grafo.NuevoArco("11", "5", Distancia(nodos[11], nodos[5]));


            grafo.NuevoArco("12", "11", Distancia(nodos[12], nodos[11]));
			grafo.NuevoArco("12", "9", Distancia(nodos[12], nodos[9]));
			grafo.NuevoArco("12", "10", Distancia(nodos[12], nodos[10]));
			grafo.NuevoArco("12", "14", Distancia(nodos[12], nodos[14]));

			grafo.NuevoArco("13", "11", Distancia(nodos[13], nodos[11]));
			grafo.NuevoArco("13", "14", Distancia(nodos[13], nodos[14]));

			grafo.NuevoArco("14", "12", Distancia(nodos[14], nodos[12]));
			grafo.NuevoArco("14", "13", Distancia(nodos[14], nodos[13]));
            grafo.NuevoArco("14", "5", Distancia(nodos[14], nodos[5]));
        }
		catch (Exception ex)
		{
			Debug.Log("Fallo al agregar el nodo: " + ex.Message);
		}

		//inicialmente el color de los arcos y nodos es negro
		colorInicial = Color.black;

		//metodo condiciones iniciales
		CondicionesIniciales();
	}

	
	void Update () {
		
	}

    /*
	 * Método: Distancia, Calcula la distancia entre el objeto a y b dentro del mapa
	 * Aegumentos: GameObject a, GameObject b
	 * 
	*/
    int Distancia(GameObject a, GameObject b)
	{
		int dist = Mathf.RoundToInt(
			Mathf.Sqrt(Mathf.Pow(b.transform.position.x - a.transform.position.x,2)
			          +Mathf.Pow(b.transform.position.y - a.transform.position.y,2))
		);
		return dist;
	}

	/*
	 * Método: GetNumeroDeClicks
	 * Devuelve el número de clicks que se han hecho sobre los nodos
	*/
	public static int GetNumeroDeClicks()
	{
		return NumClicks;
	}
	
	public static void Click(String s)
	{
		if (NumClicks <= 2)
		{
			NumClicks++;
			if (NumClicks == 1)
			{
				caminoMasCorto = new Dijkstra(grafo, grafo.NumVertice(s));
				caminoMasCorto.CalcularCaminoMinimo();
				GameObject.Find("Text").GetComponent<Text>().text = "Nodo uno seleccionado";
				CondicionesIniciales();
			}
			if (NumClicks == 2)
			{
				
				camino = caminoMasCorto.RecuperarCamino(grafo.NumVertice(s));
				ColorearCamino(camino);
				GameObject.Find("Text").GetComponent<Text>().text = "Camino más corto calculado";
				NumClicks = 0;
			}
		}
	}

    /*
	 * Método: ColorearCamino, recibe el camino más corto y lo
	 * colorea de rojo
	 * Argumentos: String camino
	 * 
	*/
    private static void ColorearCamino(String camino)
	{
		char delimiter = '-';
		String[] vectNodos = camino.Split(delimiter);
		String nombreArista="";
		int numNodo1, numNodo2, aux;
		for (int i = 0; i < vectNodos.Length - 1; i++)
		{
			numNodo1 = Int32.Parse(vectNodos[i]);
			numNodo2 = Int32.Parse(vectNodos[i + 1]);
			
			if (numNodo1 > numNodo2)
			{
				aux = numNodo1;
				numNodo1 = numNodo2;
				numNodo2 = aux;
			}

			nombreArista = "Arista" + numNodo1.ToString() + numNodo2.ToString();
			//cambiando el color a rojo
			GameObject.Find(nombreArista).GetComponent<SpriteRenderer>().color = Color.red;
			GameObject.Find("Circle" + numNodo1).GetComponent<SpriteRenderer>().color = Color.red;
			GameObject.Find("Circle" + numNodo2).GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	/*
	 * Método: Condiciones Iniciales,
	 * Establece las condiciones iniciales del mapa
	 * con los nodos y aristas de color negro
	*/
	public static void CondicionesIniciales()
	{
		for (int i = 1; i <= NUMERO_DE_CIRCULOS; i++)
		{
			GameObject.Find("Circle" + i).GetComponent<SpriteRenderer>().color = colorInicial;
		}

		for (int i = 0; i < grafo.matAd.GetLength(0); i++)
		{
			String nomArista = "";
			for (int j = 0; j < grafo.matAd.GetLength(1); j++)
			{
				if (grafo.matAd[i, j] != 0 && i < j)
				{
					nomArista = "Arista";
					nomArista += grafo.verts[i].NomVertice();
					nomArista += grafo.verts[j].NomVertice();
					GameObject.Find(nomArista).GetComponent<SpriteRenderer>().color = colorInicial;
				}
			}
		}
		GameObject.Find("Text").GetComponent<Text>().text = "Seleccione un nodo";
	}

	
	public static void SetNumeroDeClicks(int numClick)
	{
		NumClicks = 0;
	}
}
