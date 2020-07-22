using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarPuntuaciones : MonoBehaviour {
	public Text primeroPuntos;
	public Text primeroNombre;
	public Text segundoPuntos;
	public Text segundoNombre;
	public Text terceroPuntos;
	public Text terceroNombre;
	public Text cuartoPuntos;
	public Text cuartoNombre;
	public Text quintoPuntos;
	public Text quintoNombre;

	void Start(){
		primeroPuntos.text = PlayerPrefs.GetInt ("primeroPuntos")+"";
		primeroNombre.text = PlayerPrefs.GetString ("primeroNombre");
		segundoPuntos.text = PlayerPrefs.GetInt ("segundoPuntos")+"";
		segundoNombre.text = PlayerPrefs.GetString ("segundoNombre");
		terceroPuntos.text = PlayerPrefs.GetInt ("terceroPuntos")+"";
		terceroNombre.text = PlayerPrefs.GetString ("terceroNombre");
		cuartoPuntos.text = PlayerPrefs.GetInt ("cuartoPuntos")+"";
		cuartoNombre.text = PlayerPrefs.GetString ("cuartoNombre");
		quintoPuntos.text = PlayerPrefs.GetInt ("quintoPuntos")+"";
		quintoNombre.text = PlayerPrefs.GetString ("quintoNombre");
	}
}
