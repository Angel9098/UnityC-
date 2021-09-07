using System;
namespace AssemblyCSharp
{
	public class Vertice
	{
		private String nombre;
		private int numVertice;
		//distancia del vertice al nodo S
		public int distancia;

		public Vertice(String nombre)
		{
			this.nombre = nombre;
			numVertice = -1;
		}

		public int NumVertice
		{
			get
			{
				return numVertice;
			}
		}

		public String NomVertice() //devuelve el identificador del vertice
		{
			return nombre;
		}

		public bool Equals(Vertice n) //true si dos vertices son iguales
		{
			return nombre.Equals(n.NomVertice());
		}

		public void AsignarNumeroVertice(int n)
		{
			numVertice = n;
		}


	}
}
