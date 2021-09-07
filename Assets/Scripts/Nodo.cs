using System;
namespace AssemblyCSharp
{
	public class Nodo
	{
		private bool c, mark;
		private int degree;
		private Vertice key;	//clave del nodo, que es un vertice del grafo
		private Nodo parent, child, left, right;


		public bool C
		{
			get
			{
				return c;
			}

			set
			{
				c = value;
			}
		}

		public bool Mark
		{
			get
			{
				return mark;
			}

			set
			{
				mark = value;
			}
		}

		//grado del nodo
		public int Degree
		{
			get
			{
				return degree;
			}

			set
			{
				degree = value;
			}
		}

		public Vertice Key
		{
			get
			{
				return key;
			}

			set
			{
				key = value;
			}
		}

		public Nodo Parent
		{
			get
			{
				return parent;
			}

			set
			{
				parent = value;
			}
		}

		public Nodo Left
		{
			get
			{
				return left;
			}

			set
			{
				left = value;
			}
		}

		public Nodo Right
		{
			get
			{
				return right;
			}

			set
			{
				right = value;
			}
		}

		public Nodo Child
		{
			get
			{
				return child;
			}

			set
			{
				child = value;
			}
		}

		public Nodo() 
		{
			
		}

		public Nodo(Vertice key)
		{
			this.key = key;
		}

		public Nodo(int degree, Vertice key)
		{
			this.degree = degree;
			this.key = key;
		}

		public Nodo(Nodo copy)
		{
			this.key = copy.key;
			this.child = copy.child;
			this.left = copy.left;
			this.right = copy.right;
			this.parent = copy.parent;
		}
	 }
}
