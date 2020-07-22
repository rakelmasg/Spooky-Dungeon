using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour {

	private int vida = 100;
	private int daño = 20;
	private float stamina = 130f;
	private int experiencia = 0;
	private int ataquePos = -1; //0=der 1=izq 2=arriba 3=bajo
	private int defendiendoPos = -1; //0=der 1=izq 2=arriba 3=bajo
	private bool defendiendo = false;
	private bool couldown = false;
	private bool muerto = false;

	private Vector3 input01;
	private AudioSource source;
	private Animator anim;
	private Animator animPiernas;

	public Slider vidaSlider;
	public Slider staminaSlider;
	public Text vidaTxt;
	public Text dañoTxt;
    public Text lowStamina;
	public Text experienciaTxt;
	public GameObject piernas;
	public SingleJoystick singleJoystick;
	public AudioClip impacto, bloqueo, hacha, grito, beber;
	public EnemyController enemy;

	//Valores de la cola LIFO
	private int a;
	private int b;


    // Use this for initialization
    void Start()
    {
		vidaSlider.maxValue = vida;
		vidaSlider.value = vida;
		staminaSlider.maxValue = stamina;
		staminaSlider.value = stamina;
		vidaTxt.text = vida + "/" + vida;

		a = 0;
        b = 0;
        anim = GetComponent<Animator>();
     
        if (singleJoystick == null)
        {
            Debug.LogError("A single joystick is not attached.");
        }
        if (anim == null)
        {
            Debug.LogError("There is no animator component");
        }
        if (piernas == null)
        {
            Debug.LogError("NO SIENTO LAS PIERNAS");
        }
        animPiernas = piernas.GetComponent<Animator>();
        if (animPiernas == null)
        {
            Debug.LogError("There is no animator component in piernas");
        }
        source = GetComponent<AudioSource> ();
    }

    void FixedUpdate()
    {
		if(!muerto){
		reaccionar ();
		stamina = stamina + 0.3f;
		if (stamina > staminaSlider.maxValue)
			stamina = staminaSlider.maxValue;
		staminaSlider.value = stamina;
		if (stamina < 40) {
			lowStamina.text = "Fatigado";
		} else {
			lowStamina.text = "";
		}


		// get input from both joysticks
        input01 = singleJoystick.GetInputDirection();


        // if there is no input on joystick 01
        if (input01 == Vector3.zero)
        {
            //animator.SetBool("isRunning", false);
            mapearAtaque(0);
        }

        // if there is only input from joystick 01
        if (input01 != Vector3.zero)
        {
            //Move player the same distance in each direction. Player must move in a circular motion.
            if (input01.x > 0.85)
                mapearAtaque(1);
            else if (input01.x < -0.85)
                mapearAtaque(2);
            else if (input01.y > 0.85)
                mapearAtaque(3);
            else if (input01.y < -0.85)
                mapearAtaque(4);
	         // Make rotation object(The child object that contains animation) rotate to direction we are moving in.
            /*
            if (animator != null)
            {
                animator.SetBool("isRunning", true);
            }
            */
        }
		}
    }

    private void mapearAtaque(int inp)
    {
        if (b != inp)
        {
			ataquePos = -1;
			a = b;
            b = inp;
            switch (a)
            {
                case 0:
                    if (b == 1)
                    {
                        anim.SetInteger("guardia", 0);
						if(defendiendo){
							defendiendoPos = 0;
						}
                    } 
                    else if (b == 2)
                    {
                        anim.SetInteger("guardia", 1);
                        if (defendiendo)
                        {
                            defendiendoPos = 1;
                        }
                    }
                        
                    else if (b == 3)
                    {
                        anim.SetInteger("guardia", 4);
                        if (defendiendo)
                        {
                            defendiendoPos = 2;
                        }
                    }
						
                    else if (b == 4)
                    {
                        anim.SetInteger("guardia", 5);
                        if (defendiendo)
                        {
                            defendiendoPos = 3;
                        }
                    }
                        
                    break;
                case 1:
                    if (b == 2)
                    {
						if ((stamina >= 40) && (!defendiendo) && (!couldown)) {
							anim.SetTrigger ("rightoleft");
							animPiernas.SetTrigger ("attack");
							ataquePos = 0;
							actualizarStamina (40);
							Debug.Log ("Corte hacia la izquierda");
							source.PlayOneShot (hacha);
							couldown = true;
							StartCoroutine ("desactivarCouldown");
						}else if(defendiendo){
							defendiendoPos = 1;
						}
                        anim.SetInteger("guardia", 1);
                    }
                    else if (b == 0)
                    {
                        anim.SetInteger("guardia", 3);
                        Debug.Log("Volver a guardia");
                    }
                    else if (b == 3)
                    {
						if(defendiendo){
							defendiendoPos = 2;
						}
                        anim.SetInteger("guardia", 4);
                        Debug.Log("Cambiar a guardia alta");
                    }
                    else if (b == 4)
                    {
						if(defendiendo){
							defendiendoPos = 3;
						}
                        anim.SetInteger("guardia", 5);
                        Debug.Log("Cambiar a guardia baja");
                    }

                    break;
                case 2:
                    if (b == 1)
                    {
						if ((stamina >= 40) && (!defendiendo) && (!couldown)) {
							anim.SetTrigger ("leftoright");
							animPiernas.SetTrigger ("attack");
							ataquePos = 1;
							actualizarStamina (40);
							Debug.Log ("Corte hacia la derecha");
							source.PlayOneShot (hacha);
							couldown = true;
							StartCoroutine ("desactivarCouldown");
						}else if(defendiendo){
							defendiendoPos = 0;
						}	
                        anim.SetInteger("guardia", 0);

                    }
                    else if (b == 0)
                    {
                        anim.SetInteger("guardia", 3);
                        Debug.Log("Volver a guardia");
                    }
                    else if (b == 3)
                    {
						if(defendiendo){
							defendiendoPos = 2;
						}
                        anim.SetInteger("guardia", 4);
                        Debug.Log("Cambiar a guardia alta");
                    }
                    else if (b == 4)
                    {
						if(defendiendo){
							defendiendoPos = 3;
						}
						anim.SetInteger("guardia", 5);
                        Debug.Log("Cambiar a guardia baja");
                    }
                    break;
				case 3:
					if (b == 4) {
						if ((stamina >= 40) && (!defendiendo) && (!couldown)) {
							anim.SetTrigger ("uptodown");
							animPiernas.SetTrigger ("attack");
							actualizarStamina (40);
							ataquePos = 2;
							Debug.Log ("Corte hacia abajo");
							source.PlayOneShot (hacha);
							couldown = true;
							StartCoroutine ("desactivarCouldown");
						}else if(defendiendo){
							defendiendoPos = 3;
						}
                        anim.SetInteger("guardia", 5);
                    } 
					else if (b == 0)
					{
					anim.SetInteger("guardia", 3);
					Debug.Log("Volver a guardia");
					}
                    else if (b == 1)
                    {
						if(defendiendo){
							defendiendoPos = 0;
						}
                        anim.SetInteger("guardia", 0);
                        Debug.Log("Cambiar a guardia derecha");
                    }
                    else if (b == 2)
                    {
                        anim.SetInteger("guardia", 1);
                        Debug.Log("Cambiar a guardia izquierda");
						if(defendiendo){
							defendiendoPos = 1;
						}
                    }
                    break;
				case 4:
					if (b == 3) {
						if ((stamina >= 40 )&&(!defendiendo) && (!couldown)) {
							anim.SetTrigger ("downtoup");
							animPiernas.SetTrigger ("attack");
							ataquePos = 3;
							actualizarStamina (40);
							Debug.Log ("Corte hacia arriba");
							source.PlayOneShot (hacha);
							couldown = true;
							StartCoroutine ("desactivarCouldown");
						}else if(defendiendo){
							defendiendoPos = 2;
						}
                        anim.SetInteger("guardia", 4);
                    }   
					else if (b == 0)
					{
					anim.SetInteger("guardia", 3);
					Debug.Log("Volver a guardia");
					}
                    else if (b == 1)
                    {
                        anim.SetInteger("guardia", 0);
                        Debug.Log("Cambiar a guardia derecha");
						if(defendiendo){
							defendiendoPos = 0;
						}
                    }
                    else if (b == 2)
                    {
                        anim.SetInteger("guardia", 1);
                        Debug.Log("Cambiar a guardia izquierda");
						if(defendiendo){
							defendiendoPos = 1;
						}
                    }
                    break;     
            }
        }
    }

	private void herir(int daño){
		source.clip = impacto;
		source.Play();
		if (vida > daño) {
			vida = vida - daño;
		} else {
			vida = 0;
			muerto = true;
			source.PlayOneShot (grito);
		}
		vidaSlider.value = vida;
		dañoTxt.text = daño+"";
		StartCoroutine ("quitarTexto");
		vidaTxt.text = vida + "/" + vidaSlider.maxValue;
	}

	private void bloqueadoTxt(){
		dañoTxt.text = "Bloqueado";
		StartCoroutine ("quitarTexto");
	}

	private void reaccionar(){
		int daño;
		if(enemy.atacando()!=-1){ //0=der 1=izq 2=arriba 3=bajo
			if (enemy.atacando() == 0) {
				if(defendiendo && defendiendoPos==0){
					source.PlayOneShot (bloqueo);
					anim.SetTrigger("react");
					bloqueadoTxt ();
				}else{
					Debug.Log (defendiendoPos);
					daño = enemy.getDaño ();
					herir (daño);
				}
			} else if (enemy.atacando() == 1) {
				if(defendiendo && defendiendoPos==1){
					source.PlayOneShot (bloqueo);
					anim.SetTrigger("react");
					bloqueadoTxt ();
				}else{
					daño = enemy.getDaño ();
					herir (daño);
				}
			} else if (enemy.atacando() == 2) {
				if(defendiendo && defendiendoPos==2){
					source.PlayOneShot (bloqueo);
					anim.SetTrigger("react");
					bloqueadoTxt ();
				}else{
					daño = enemy.getDaño ();
					herir (daño);
				}
			} else {
				if(defendiendo && defendiendoPos==3){
					source.PlayOneShot (bloqueo);
					anim.SetTrigger("react");
					bloqueadoTxt ();
				}else{
					daño = enemy.getDaño ();
					herir (daño);
				}
			}
		}
		enemy.reaccionadoJugador();
	}

	public int atacando(){
		return ataquePos; //0=der 1=izq 2=arriba 3=bajo
	}

	public void reaccionadoEnemigo(){
		ataquePos = -1;
	}
    
    public void setDefendiendo(bool valor)
    {
		defendiendo = valor;
		anim.SetBool("defendiendo",valor);
	}

	public int getDaño(){
		return Random.Range (daño-5,daño);
	}
		
	private void actualizarStamina(int st){
		stamina = stamina - st;
		staminaSlider.value = stamina;
	}
    public void actualizarEnemigo(EnemyController e)
    {
        enemy = e;
    }

	public void tomarPocion(){
		vida = vida + 40;
		if (vida > 100) {
			vida = 100;
		}
		source.PlayOneShot (beber);
		vidaSlider.value = vida;
		vidaTxt.text = vida + "/" + vidaSlider.maxValue;
		dañoTxt.color = Color.green;
		dañoTxt.text = "+"+40;
		StartCoroutine ("quitarTexto");
	}

	public void darExperiencia(int exp){
		experiencia = experiencia + exp;
		experienciaTxt.text = "Exp: " + experiencia;
	}

	public int getExperiencia(){
		return experiencia;
	}

	public bool isMuerto(){
		return muerto;
	}

	IEnumerator desactivarCouldown(){
		yield return new WaitForSeconds (0.5f);
		couldown = false;
	}

	IEnumerator quitarTexto(){
		yield return new WaitForSeconds (0.4f);
		dañoTxt.text = "";
		dañoTxt.color = Color.red;
	}
}
