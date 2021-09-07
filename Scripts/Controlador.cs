using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour {

	/*
	 * Método: CambiarEscena
	 * Carga una escena, cuyo nombre se pasa como argumento
	 * Las escenas disponibles son la pantalla de inicio
	 * y el mapa de la aplicación
	*/
	public void CambiarEscena(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);
	}

	/*
	 * Método: Limpiar
	 * Método que establece las condiciones iniciales del mapa
	*/
	public void Limpiar()
	{
		CaminoMinimo.CondicionesIniciales();
	}
}
