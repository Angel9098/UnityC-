using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	class FibonacciHeap
	{
		//ATRIBUTOS
		private int n;
		private Nodo min;

		//CONSRUCTOR
		public FibonacciHeap()
		{

		}

		//  GETTERS
		public int N
		{
			get
			{
				return n;
			}
		}

		public Nodo Min
		{
			get
			{
				return min;
			}
		}

		//10 METODOS DEL HEAP


            //insertar
		public void Insert(ref FibonacciHeap H, Nodo x)
		{
			x.Degree = 0;
			x.Parent = null;
			x.Child = null;
			x.Left = x;
			x.Right = x;
			x.Mark = false;
			x.C = false;

			if (H.min != null)
			{
				H.min.Left.Right = x;
				x.Right = H.min;
				x.Left = H.min.Left;
				H.min.Left = x;

				if (x.Key.distancia < H.min.Key.distancia)
					H.min = x;
			}

			else
				H.min = x;

			H.n++;
		}

        //union
		public FibonacciHeap Union(FibonacciHeap H1, FibonacciHeap H2)
		{

			Nodo np;
			FibonacciHeap H = new FibonacciHeap();

			H = H1;
			H.min.Left.Right = H2.min;
			H2.min.Left.Right = H.min;
			np = H.min.Left;
			H.min.Left = H2.min.Left;
			H2.min.Left = np;
			return H;
		}

        //enlace
		public void Link(ref FibonacciHeap H, Nodo y, Nodo z)
		{
			y.Left.Right = y.Right;
			y.Right.Left = y.Left;

			if (z.Right == z)
				H.min = z;

			y.Left = y;
			y.Right = y;
			y.Parent = z;

			if (z.Child == null)
				z.Child = y;

			y.Right = z.Child;
			y.Left = z.Child.Left;
			z.Child.Left.Right = y;
			z.Child.Left = y;

			if (y.Key.distancia < z.Child.Key.distancia)
				z.Child = y;

			z.Degree++;
		}

		public void Consolidate(ref FibonacciHeap H)
		{
			int d, D = 1 + (int)Math.Ceiling((Math.Log(H.n) / Math.Log(2.0)));
			Nodo[] A = new Nodo[D + 1];
			Nodo x = H.min, y, np, pt = x;

			do
			{
				pt = pt.Right;
				d = x.Degree;

				while (A[D] != null)
				{
					y = A[D];

					if (x.Key.distancia > y.Key.distancia)
					{
						np = x;
						x = y;
						y = np;
					}

					if (y == H.min)
						H.min = x;

					Link(ref H, y, x);

					if (x.Right == x)
						H.min = x;

					A[D] = null;
					d++;
				}

				A[D] = x;
				x = x.Right;
			} while (x != H.min);

			H = new FibonacciHeap();

			for (int j = 0; j <= D; j++)
			{
				if (A[j] != null)
				{
					A[j].Left = A[j];
					A[j].Right = A[j];

					if (H.min != null)
					{
						H.min.Left.Right = A[j];
						A[j].Right = H.min;
						A[j].Left = H.min.Left;
						H.min.Left = A[j];
						H.n++;

						if (A[j].Key.distancia < H.min.Key.distancia)
							H.min = A[j];
					}

					else
					{
						H.min = A[j];
						H.n = 1;
					}
				}
			}
		}

        //Extraer minimo
		public Nodo ExtractMin(ref FibonacciHeap H)
		{
			Nodo ptr = H.min, z = H.min, x, np;

			if (z == null)
				return z;

			x = null;

			if (z.Child != null)
				x = z.Child;

			if (x != null)
			{
				ptr = x;

				do
				{
					np = x.Right;
					H.min.Left.Right = x;
					x.Right = H.min;
					x.Left = H.min.Left;
					H.min.Left = x;

					if (x.Key.distancia < H.min.Key.distancia)
						H.min = x;

					x.Parent = null;
					x = np;
				} while (np != ptr);
			}

			z.Left.Right = z.Right;
			z.Right.Left = z.Left;
			H.min = z.Right;

			if (z == z.Right && z.Child == null)
				H.min = null;

			else
			{
				H.min = z.Right;

				if (H.n > 0)
					Consolidate(ref H);
			}

			return z;
		}


		private void Cut(ref FibonacciHeap H, Nodo x, Nodo y)
		{
			if (x == x.Right)
				y.Child = null;

			x.Left.Right = x.Right;
			x.Right.Left = x.Left;

			if (x == y.Child)
				y.Child = x.Right;

			y.Degree--;
			x.Right = x;
			x.Left = x;
			H.min.Left.Right = x;
			x.Right = H.min;
			x.Left = H.min.Left;
			H.min.Left = x;
			x.Parent = null;
			x.Mark = false;
		}

		private void CascadingCut(ref FibonacciHeap H, Nodo y)
		{
			Nodo z = y.Parent;

			if (z != null)
			{
				if (y.Mark == false)
					y.Mark = true;

				else
				{
					Cut(ref H, y, z);
					CascadingCut(ref H, z);
				}
			}
		}

		public Nodo Find(Nodo min, Vertice k)
		{
			Nodo x = min, p = null;

			x.C = true;

			if (x.Key.distancia == k.distancia)
			{
				p = x;
				x.C = false;
				return p;

			}

			if (p == null)
			{
				if (x.Child != null)
					p = Find(x.Child, k);

				if (x.Right.C != true)
					p = Find(x.Right, k);

			}

			x.C = false;
			return p;
		}

		public bool DecreaseKey(ref FibonacciHeap H, Nodo x, Vertice k)
		{
			if (k.distancia > x.Key.distancia)
				return false;

			x.Key = k;

			Nodo y = x.Parent;

			if (y != null && x.Key.distancia < y.Key.distancia)
			{
				Cut(ref H, x, y);
				CascadingCut(ref H, y);
			}

			if (x.Key.distancia < H.min.Key.distancia)
				H.min = x;

			return true;
		}

        //borrar
		public void Delete(ref FibonacciHeap H, Nodo x)
		{
			Vertice k = new Vertice("0");
			k.distancia = int.MinValue;
			if (DecreaseKey(ref H, x, k))
				ExtractMin(ref H);
		}

		public bool Empty()
		{
			return min == null;
		}	
	}
}
