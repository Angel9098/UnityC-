using UnityEngine;
using System;

namespace AssemblyCSharp
{
	public class GrafMatPeso
	{
		//"INFINITO", valor lo suficientemente grande para considerarse como infinito
		public const int INFINITO = Int32.MaxValue - Int16.MaxValue;
		private int numVerts;
		public Vertice[] verts;
		//Matriz de adyacencia
		public int[,] matAd;
		//Matriz de pesos
		public int[,] matPeso;

		/*
		 * Constructor del grafo
		 * se establece el numero máximo de elementos
		 * Se llena de 0's la matriz de adyacencia
		 * Y de "Infinitos" la matriz de pesos del grafo
		*/
		public GrafMatPeso(int max)
		{
			matAd = new int[max, max];
			matPeso = new int[max, max];
			verts = new Vertice[max];
			for (int i = 0; i < max; i++)
			{
				for (int j = 0; j < max; j++)
				{
					matAd[i, j] = 0;
					matPeso[i, j] = INFINITO;
				}
			}
			numVerts = 0;
		}

		//Se crea un nuevo vertice y se agrega al vector de vertices "verts"
		public void NuevoVertice(String nombre)
		{
			bool esta = NumVertice(nombre) >= 0;
			if (!esta)
			{
				Vertice v = new Vertice(nombre);
				v.AsignarNumeroVertice(numVerts);
				verts[numVerts++] = v;
			}
		}
		//Regresa el numero del vertice dado su nombre
		public int NumVertice(String vs)
		{
			Vertice v = new Vertice(vs);
			bool encontrado = false;
			int i = 0;
			for (; (i < numVerts) && !encontrado;)
			{
				encontrado = verts[i].Equals(v);
				if (!encontrado) i++;
			}
			return (i < numVerts) ? i : -1;
		}
		//Une dos grafos, la información se agrega a la matriz de adyacencia
		//y el peso a la matriz de pesos
		public void NuevoArco(String a, String b, int peso) //throws Exception
		{
			int va, vb;
			va = NumVertice(a);
			vb = NumVertice(b);
			if (va < 0 || vb < 0) throw new Exception("Vértice no existe");
			matAd[va, vb] = 1;
			matPeso[va, vb] = peso;
		}
		//Une dos grafos, la información se agrega a la matriz de adyacencia
		//y el peso a la matriz de pesos
		public void NuevoArco(int va, int vb, int peso)//throws Exception
		{
			if (va < 0 || vb < 0) throw new Exception("Vértice no existe");
			matAd[va, vb] = 1;
			matPeso[va, vb] = peso;
		}
		//Devuelve true si dos nodos son adyacentes
		public bool Adyacente(String a, String b) //throws Exception
		{
			int va, vb;
			va = NumVertice(a);
			vb = NumVertice(a);
			if (va < 0 || vb < 0) throw new Exception("Vértice no existe");
			return matAd[va, vb] == 1;
		}

		/*
		 * Devuelve true si dos nodos son adyacentes
		*/
		public bool Adyacente(int va, int vb) //throws Exception
		{
			if (va < 0 || vb < 0) throw new Exception("Vértica no existe");
			return matAd[va, vb] == 1;
		}

		public int NumeroDeVertices()
		{
			return numVerts;
		}

	}
}