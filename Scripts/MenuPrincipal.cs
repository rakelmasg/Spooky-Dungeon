using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour {

	public GameObject camara;
	public GameObject menuObj;
	public GameObject puntosObj;
	public GameObject creditosObj;
	public GameObject jugarObj;
	public GameObject comoJugarObj;
	public Button sonido;
	public Button facil;
	public Button medio;
	public Button dificil;
	private int dificultad = 0;
	private int maxMove = 250;
	private float movimiento = 0.01f;
	private Vector3 rot = Vector3.down;

	void Start(){
		//PlayerPrefs.DeleteAll ();
	}


	void Update(){
		maxMove = maxMove - 1;
		if (maxMove == 0) {
			maxMove = 500;
			movimiento = movimiento*(-1);
			if (rot == Vector3.down) {
				rot = Vector3.up;
			} else {
				rot = Vector3.down;
			}
		}
		camara.transform.position = new Vector3 (camara.transform.position.x+movimiento, camara.transform.position.y, camara.transform.position.z);
		camara.transform.Rotate(rot * Time.deltaTime);
	}
	public void jugar(){
		menuObj.SetActive (false);
		jugarObj.SetActive (true);
	}

	public void dificultadBotones(int d){
		facil.interactable = true;
		medio.interactable = true;
		dificil.interactable = true;
		if (d == 0) {
			facil.interactable = false;
		} else if (d == 1) {
			medio.interactable = false;
		} else {
			dificil.interactable = false;
		}
		dificultad = d;
	}

	public void batalla(){
		string nombre = GameObject.Find ("HeroName").GetComponent<Text> ().text.ToString();
		PlayerPrefs.SetString ("heroe", nombre);
		PlayerPrefs.SetInt ("puntuacion", 0);

		if (dificultad == 0) {
			SceneManager.LoadScene ("Nivel1");
		} else if (dificultad == 1) {
			SceneManager.LoadScene ("Nivel2");
		} else {
			SceneManager.LoadScene ("Nivel3");
		}

	}

	public void como(){
		menuObj.SetActive (false);
		comoJugarObj.SetActive (true);
	}
		

	public void puntuacion(){
		menuObj.SetActive (false);
		puntosObj.SetActive (true);
	}

	public void creditos(){
		menuObj.SetActive (false);
		creditosObj.SetActive (true);
	}

	public void volver(int from){
		if (from == 1) {
			puntosObj.SetActive (false);
		} else if (from == 2) {
			comoJugarObj.SetActive (false);
		} else if (from == 3) {
			creditosObj.SetActive (false);
		} else {
			jugarObj.SetActive (false);
		}
		menuObj.SetActive (true);
	}

	public void alternar(){
		if(AudioListener.volume > 0f){
			sonido.GetComponent<Image> ().sprite = Resources.Load<Sprite>("Images/sonido_desactivado");
			AudioListener.volume = 0f;
		}else{
			sonido.GetComponent<Image> ().sprite = Resources.Load<Sprite>("Images/sonido_activo");
			AudioListener.volume = 1f;

		}

	}

	public void click(){
		GetComponent<AudioSource> ().Play ();
	}

}
