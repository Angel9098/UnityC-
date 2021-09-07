using UnityEngine;

public class MouseClick : MonoBehaviour {

	/*
	 * Se manda la información al algoritmo de Dijstra cuando se hace click en el nodo
	 * Al primer click se establece el nodo origen
	 * Al segundo el nodo destino
	 * Además se colorea de color rojo el nodo seleccionado
	*/
	void OnMouseDown()
	{
		if (gameObject.name.Length == 7)
		{
			CaminoMinimo.Click(gameObject.name[gameObject.name.Length - 1].ToString());
		}
		else 
		{
			CaminoMinimo.Click(gameObject.name[gameObject.name.Length - 2].ToString()
			                   +gameObject.name[gameObject.name.Length - 1]);
		}
		gameObject.GetComponent<SpriteRenderer>().color = Color.red;
	}


}
