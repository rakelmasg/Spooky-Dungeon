using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour {

	void Start () {
		int puntos = PlayerPrefs.GetInt ("puntuacion");
		string nombre = PlayerPrefs.GetString ("heroe");
		GameObject.Find ("Puntuacion").GetComponent<Text> ().text = puntos+"";
		GetComponent<AñadirPuntuacion> ().Añadir(nombre, puntos);
	}
	
	public void volver(){
		SceneManager.LoadScene ("MenuPrincipal");
	}
}
