using System;
namespace AssemblyCSharp
{
	class Dijkstra
	{
		private int[,] peso;
		private bool[] visto;
		private int[] padre;
		private int[] distancia;
		private GrafMatPeso grafo;
		private int source;
		FibonacciHeap fibHeap = new FibonacciHeap();

		/*
		 * Constructor de la clase Dijstra
		 * En donde se establece a cero la distancia hasta el orgigen
		 * y a "Infnito" la distancia hacia los demás nodos
		*/
		public Dijkstra(GrafMatPeso grafo, int origen)
		{
			padre = new int[grafo.matAd.GetLength(0)];
			distancia = new int[grafo.matAd.GetLength(0)];
			visto = new bool[grafo.matAd.GetLength(0)];

			for (int u = 0; u < grafo.matAd.GetLength(0); u++)
			{
				distancia[u] = Int32.MaxValue - Int16.MaxValue;
				visto[u] = false;
				padre[u] = -1;
			}
			distancia[origen] = 0;
			peso = grafo.matPeso;
			this.grafo = grafo;
			source = origen;
		}

		/*
		 * Con este método se calcula el camino más corto
		 * y la información queda guardada en los vectores
		 * "padre" y "distancia"
 		*/
		public void CalcularCaminoMinimo()
		{
			Vertice s = grafo.verts[source], u;
			s.distancia = distancia[source];
			Nodo nodo = new Nodo();
			nodo.Key = s;

			fibHeap.Insert(ref fibHeap, nodo);
			while (!fibHeap.Empty())
			{
				nodo = fibHeap.ExtractMin(ref fibHeap);
				u = nodo.Key;
				visto[u.NumVertice] = true;
				foreach (Vertice v in grafo.verts)
				{
					if (!visto[v.NumVertice] && distancia[v.NumVertice] > distancia[u.NumVertice] + peso[u.NumVertice, v.NumVertice])
					{
						distancia[v.NumVertice] = distancia[u.NumVertice] + peso[u.NumVertice, v.NumVertice];
						v.distancia = distancia[v.NumVertice];
						nodo = new Nodo();
						nodo.Key = v;
						padre[v.NumVertice] = u.NumVertice;
						fibHeap.Insert(ref fibHeap, nodo);
					}
				}
			}
		}

		/*
		 * Se devuelve el camino más corto en formato de cadena de texto
		*/
		public String RecuperarCamino(int v)
		{
			int anterior = padre[v];
			if (v != source)
			{
                return RecuperarCamino(anterior) + ("-" + grafo.verts[v].NomVertice());
			}
			else
				return(grafo.verts[source].NomVertice());
		}
	 }
}
