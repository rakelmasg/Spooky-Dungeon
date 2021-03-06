using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public int maxVida = 100;
	public int daño = 15;
	public int probabilidadBloqueo = 5; 
	public int experiencia = 50;
	private int vida;
	private int cont = 160;
	private int accion = 0;
	private int ataque = -1;
	private int combo = 4;
	private bool muerto = true;
	private bool reaccionando = false;

	private AudioSource source;
	private Animator anim;
	private CanvasGroup vidaSliderGroup;
	//LevelManager para los eventos
	private LevelManagerController lvm;

    public Transform baul;
	public Slider vidaSlider;
    public Text vidaTxt;
	public Text dañoTxt;
	public AudioClip bloqueo, impacto, espada;
	public PlayerInputController player;

   
	void Start()
	{
        vidaSliderGroup = vidaSlider.GetComponent<CanvasGroup>();
		source = GetComponent<AudioSource> ();
		anim = GetComponent<Animator>();
        lvm = GameObject.Find("LevelManager").GetComponent<LevelManagerController>();
    }

	void FixedUpdate()
	{
		if (player.isMuerto ()) {
			accion = 0;
			realizaAccion ();
		}else if (!muerto) {
			cont--;
			reaccionar ();
			if (cont == 0) {
				cont = Random.Range(120,171);
				cambiarAccion();
			}
		}
	}

	public void startCombat(){
		vidaSlider.maxValue = maxVida;
		vida = maxVida;
		vidaSlider.value = vida;
		vidaTxt.text = maxVida + "/" + maxVida;
		muerto = false;
		vidaSliderGroup.alpha = 1F;
		accion = 0;
		realizaAccion ();
	}


	private void cambiarAccion(){
		accion = Random.Range (1, 5); //[1 y 4]
		realizaAccion ();
		if (Random.Range (1, 11) <= combo) {
			cont = 92;
			combo = 0;
		}
		if ((combo==0) && (Random.Range (1, 4) == 1)) {
			combo = 4;
		}
	}

	private void realizaAccion()
	{
		int daño;
		ataque = -1;
		switch (accion) {
		case 0: //idle
			Debug.Log ("Idle");
			anim.SetTrigger ("startCombat");
			break;
		case 1: //ataque izquierda
			StartCoroutine("retrasoAtaque",0);
			source.clip = espada;
			Debug.Log ("Ataque izquierda");
			anim.SetTrigger ("atIzq");
			break;
		case 2: //ataque derecha
			StartCoroutine("retrasoAtaque",1);
			source.clip = espada;
			Debug.Log ("Ataque derecha");
			anim.SetTrigger ("atDcha");
			break;
		case 3: //ataque arriba
			StartCoroutine("retrasoAtaque",2);
			source.clip = espada;
			Debug.Log ("Ataque arriba");
			anim.SetTrigger ("atArriba");
			break;
		case 4: //ataque abajo
			StartCoroutine("retrasoAtaque",3);
			source.clip = espada;
			Debug.Log ("Ataque abajo");
			anim.SetTrigger ("atBajo");
			break;
		case 5: //bloqueo izquierda
			source.clip = bloqueo;
			Debug.Log ("Bloqueo izquierda");
			anim.SetTrigger ("blVertical");
			break;
		case 6: //bloqueo derecha
			source.clip = bloqueo;
			anim.SetTrigger ("blVertical");
			Debug.Log ("Bloqueo derecha");
			break;
		case 7: //bloqueo arriba
			source.clip = bloqueo;
			anim.SetTrigger ("blHorizontal");
			Debug.Log ("Bloqueo arriba");
			break;
		case 8: //bloqueo abajo
			source.clip = bloqueo;
			Debug.Log ("Bloqueo abajo");
			anim.SetTrigger ("blHorizontal");
			break;
		case 9: //impacto arriba
			source.clip = impacto;
			daño = player.getDaño()+5;
			if (!herir (daño)) {
				Debug.Log ("Impacto arriba");
				anim.SetInteger ("chosen", 2);
				anim.SetTrigger ("damage");
			} else {
                    morirse();
			}
			break;
		case 10: //impacto abajo
			source.clip = impacto;
			daño = player.getDaño()+5;
			if(!herir (daño)){
				Debug.Log ("Impacto abajo");
				anim.SetInteger ("chosen", 0);
				anim.SetTrigger ("damage");
			}else {
                    morirse();
			}
			break;
		case 11: //impacto derecha
			source.clip = impacto;
			daño = player.getDaño();
			if(!herir (daño)){
				Debug.Log ("Impacto derecha");
				anim.SetInteger ("chosen", 1);
				anim.SetTrigger ("damage");
			}else {
                    morirse();
            }
			break;
		case 12: //impacto izquierda
			source.clip = impacto;
			daño = player.getDaño();
			if(!herir (daño)){
				Debug.Log ("Impacto izquierda");
				anim.SetInteger ("chosen", 2);
				anim.SetTrigger ("damage");
			}else {
                    morirse();
             }
			break;
		}
		source.Play();
	}

	private bool herir(int daño){
		vida = vida - daño;
		vidaSlider.value = vida;
		dañoTxt.text = daño+"";
		vidaTxt.text = vida + "/" + maxVida;
		StartCoroutine ("quitarTexto");
		if (vida <= 0) {
			muerto = true;
			return true;
		} else {
			return false;
		}
	}
		
	public int getDaño(){
		return Random.Range (daño-5,daño);
	}

	private void bloqueadoTxt(){
		dañoTxt.text = "Bloqueado";
		StartCoroutine ("quitarTexto");
	}

	public void reaccionar(){
		
		if (player.atacando () != -1) {  //0=der 1=izq 2=arriba 3=bajo 
			if (Random.Range (1, 4) < 3) { //probabilidad reaccionar dejando de atacar por el daño o bloqueando
				cont = Random.Range (75, 111);
				reaccionando = true;
				int probabilidad = Random.Range (1, probabilidadBloqueo); // probabilidad de bloquear
				if (player.atacando () == 0) {
					if (probabilidad <= 2) {
						accion = 5;
						bloqueadoTxt ();
					} else {
						accion = 12;
					}
				} else if (player.atacando () == 1) {
					if (probabilidad <= 2) {
						accion = 6;
						bloqueadoTxt ();
					} else {
						accion = 11;
					}
				} else if (player.atacando () == 2) {
					if (probabilidad <= 3) {
						accion = 7;		
						bloqueadoTxt ();
					} else {
						accion = 9;
					}
				} else {
					if (probabilidad <= 3) {
						accion = 8;
						bloqueadoTxt ();
					} else {
						accion = 10;
					}
				}
				realizaAccion ();
				StartCoroutine ("retrasoBloqueo");
			} else { //probabilidad de seguir atacando aunque se dañe
				int daño = player.getDaño ();
				if (player.atacando () > 1) {
					daño = daño + 5;
				}
				if (herir (daño)) {
					morirse ();
				}
				source.PlayOneShot (impacto);
			}
			player.reaccionadoEnemigo ();
		}	
	}

	public void reaccionadoJugador(){
		ataque = -1;
	}


	public int atacando(){
		return ataque;
	}

    public void morirse()
    {
        Debug.Log("Muerte");
        anim.SetTrigger("death");
        muerto = true;
        vidaSliderGroup.alpha = 0;
        StartCoroutine("desaparecer");
		player.darExperiencia (experiencia);
    }

    public void spawn()
    {
        anim.SetTrigger("spawn");
        Debug.Log("Enemigo spawneado con éxito");
    }


    IEnumerator desaparecer()
    {
        yield return new WaitForSeconds(3.0f);
		Debug.Log("Enemigo desaparece");
        transform.SetParent(baul, false);
    	lvm.EnemigoMuerto();
    }

	IEnumerator quitarTexto(){
		yield return new WaitForSeconds (0.5f);
		dañoTxt.text = "";
	}

	IEnumerator retrasoBloqueo(){
		yield return new WaitForSeconds (1f);
		reaccionando = false;
	}

	IEnumerator retrasoAtaque(int at){
		yield return new WaitForSeconds (0.6f);
		if (!reaccionando) {
			ataque = at;
		}
	}

    

}
