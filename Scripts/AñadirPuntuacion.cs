using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AñadirPuntuacion : MonoBehaviour {

	public void Añadir(string nombre, int puntos){
		if (PlayerPrefs.GetInt ("primeroPuntos")<puntos) {
			PlayerPrefs.SetInt ("quintoPuntos", PlayerPrefs.GetInt("cuartoPuntos"));
			PlayerPrefs.SetString ("quintoNombre", PlayerPrefs.GetString("cuartoNombre"));
			PlayerPrefs.SetInt ("cuartoPuntos", PlayerPrefs.GetInt("terceroPuntos"));
			PlayerPrefs.SetString ("cuartoNombre",  PlayerPrefs.GetString("terceroNombre"));
			PlayerPrefs.SetInt ("terceroPuntos", PlayerPrefs.GetInt("segundoPuntos"));
			PlayerPrefs.SetString ("terceroNombre",  PlayerPrefs.GetString("segundoNombre"));
			PlayerPrefs.SetInt ("segundoPuntos", PlayerPrefs.GetInt("primeroPuntos"));
			PlayerPrefs.SetString ("segundoNombre",  PlayerPrefs.GetString("primeroNombre"));
			PlayerPrefs.SetInt ("primeroPuntos", puntos);
			PlayerPrefs.SetString ("primeroNombre", nombre);

		}else if(PlayerPrefs.GetInt ("segundoPuntos")<puntos){
			PlayerPrefs.SetInt ("quintoPuntos", PlayerPrefs.GetInt("cuartoPuntos"));
			PlayerPrefs.SetString ("quintoNombre", PlayerPrefs.GetString("cuartoNombre"));
			PlayerPrefs.SetInt ("cuartoPuntos", PlayerPrefs.GetInt("terceroPuntos"));
			PlayerPrefs.SetString ("cuartoNombre",  PlayerPrefs.GetString("terceroNombre"));
			PlayerPrefs.SetInt ("terceroPuntos", PlayerPrefs.GetInt("segundoPuntos"));
			PlayerPrefs.SetString ("terceroNombre",  PlayerPrefs.GetString("segundoNombre"));
			PlayerPrefs.SetInt ("segundoPuntos", puntos);
			PlayerPrefs.SetString ("segundoNombre", nombre);

		}else if(PlayerPrefs.GetInt ("terceroPuntos")<puntos){
			PlayerPrefs.SetInt ("quintoPuntos", PlayerPrefs.GetInt("cuartoPuntos"));
			PlayerPrefs.SetString ("quintoNombre", PlayerPrefs.GetString("cuartoNombre"));
			PlayerPrefs.SetInt ("cuartoPuntos", PlayerPrefs.GetInt("terceroPuntos"));
			PlayerPrefs.SetString ("cuartoNombre",  PlayerPrefs.GetString("terceroNombre"));
			PlayerPrefs.SetInt ("terceroPuntos", puntos);
			PlayerPrefs.SetString ("terceroNombre", nombre);

		}else if(PlayerPrefs.GetInt ("cuartoPuntos")<puntos){
			PlayerPrefs.SetInt ("quintoPuntos", PlayerPrefs.GetInt("cuartoPuntos"));
			PlayerPrefs.SetString ("quintoNombre", PlayerPrefs.GetString("cuartoNombre"));
			PlayerPrefs.SetInt ("cuartoPuntos", puntos);
			PlayerPrefs.SetString ("cuartoNombre", nombre);

		}else if(PlayerPrefs.GetInt ("quintoPuntos")<puntos){
			PlayerPrefs.SetInt ("quintoPuntos", puntos);
			PlayerPrefs.SetString ("quintoNombre", nombre);
		}
	}
}
