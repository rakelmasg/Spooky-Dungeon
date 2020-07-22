using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerController : MonoBehaviour {
    private LevelBuilder builder;
    private int estado = 0;
    private bool ejecutando = false;
    private EnemyController eActual;
    public Transform player;
    public PlayerInputController pCon;
	private int salaActual = 0;

    private void Start()
    {
        builder = GetComponent<LevelBuilder>();
    }
    private void FixedUpdate()
    {
			if (estado == 0) {
				if (!ejecutando) {
					Debug.Log ("Fase 0");
					ejecutando = true;
					salaActual = builder.genScene();
					eActual = builder.getEnemigo ();
					pCon.actualizarEnemigo (eActual);
					StartCoroutine ("abrirPuerta");
				}
				player.transform.Translate (Vector3.up * 3 * Time.deltaTime);
            
		} else if(estado == 1){
			if (pCon.isMuerto ()) {
				estado = 2;
				StartCoroutine ("gameover");
			} 
		}

    }
   
    public void EnemigoMuerto()
    {
		if (salaActual == builder.nSalas) {
			StartCoroutine ("gameover");
		} else {
		} 
		estado = 0;
		ejecutando = false;
		if (builder.dropObject ()) {
			StartCoroutine ("tomarPoti");
		}
    }

	IEnumerator gameover(){
		yield return new WaitForSeconds (3f);
		PlayerPrefs.SetInt ("puntuacion", pCon.getExperiencia ());
		SceneManager.LoadScene ("GameOver");
	}

    IEnumerator abrirPuerta()
    {
        yield return new WaitForSeconds(5.0f);
        //AbrirPuerta
        builder.abrirPuertas();
        Debug.Log("AbriendoPuerta");
        StartCoroutine("empezarCombate");
    }
    IEnumerator empezarCombate()
    {
        yield return new WaitForSeconds(5.0f);
        //EmpezarCombate
        Debug.Log("Empezar Combate");
        eActual.startCombat();
        estado = 1;
        ejecutando = false;
        builder.LimpiarBasura();
    }
 	
	IEnumerator tomarPoti()
	{
		yield return new WaitForSeconds(4.2f);
		pCon.tomarPocion ();
	}
		
}
