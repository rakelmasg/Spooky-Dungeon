using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public int nSalas = 5;  //Número de salas que va a tener el nivel
    private int salaActual = 0; //Número de la sala actual
    private GameObject[] salas;
    public Transform esc_start; //Escenario preconstruido donde empiezas
    public GameObject esc_Base; //Escenario base, el puente.
    public GameObject esc_boss; //La del boss

    public GameObject[] salida_izq;

    public GameObject[] piezasPuente;
    public GameObject[] tablones;

    public Transform baulEnemigos;
    public GameObject[] enemigos;
    public EnemyController[] eControllers;
    public Transform[] boss;
    private EnemyController enemigoActual;

    public GameObject poti;
    //Variables auxiliares
    int r;
    GameObject temp;
    Transform punto;

    // Use this for initialization
    void Start()
    {
        salas = new GameObject[nSalas];
        /*for(int i=0; i < enemigos.Length; i++)
        {
            eControllers[i] = enemigos[i].GetComponent<EnemyController>();
        }*/
        //genScene();
    }


    public int genScene()
    {
        if (salaActual != nSalas-1)
        {
             genSala();
             genSalida();
             genPuente();
            genEnemigo();
            salaActual++;
        }
        else
        {
            //Crear sala del boss
            salas[salaActual] = Instantiate(esc_boss, new Vector3(esc_start.position.x, esc_start.position.y, esc_start.position.z + 30.3F * (salaActual + 1)), esc_start.rotation);
            genEnemigo();
            salaActual++;
        }
        return salaActual;
        
    }

    void genSala()
    {
        salas[salaActual] = Instantiate(esc_Base, new Vector3(esc_start.position.x, esc_start.position.y, esc_start.position.z + 30.3F * (salaActual + 1)), esc_start.rotation);
        Debug.Log("Sala " + salaActual + " creada");
    }

    void genSalida()    //Genera el trozo de escenario de la salida
    {
        //Instantiate(salida_izq[0], pSalida_izq);
    }

    void genPuente()
    {
        punto = salas[salaActual].transform.Find("Punto_side_dcha");
        //Genera los bordes
        for (int i = 0; i<6; i++)
        {
            temp = Instantiate(piezasPuente[0], punto);
            temp.transform.Translate(0,0, 4.804F * i);
        }
        punto = salas[salaActual].transform.Find("Punto_side_Izq");
        for (int i = 0; i < 6; i++)
        {
            temp = Instantiate(piezasPuente[0], punto);
            temp.transform.Translate(0, 0, 4.804F * i);
        }

        punto = salas[salaActual].transform.Find("Punto_tablones");
        for (int i = 0; i < 8; i++)
         {
             r = Random.Range(0, tablones.Length);
             temp = Instantiate(tablones[r],punto);
             temp.transform.Translate(0, 0, -2.433F * i);
         }
    }

    void genEnemigo()
    {
        if (salaActual != nSalas-1)
        {
            r = Random.Range(0, enemigos.Length-1);
        }
        else
        {
            r = enemigos.Length - 1;
        }
        punto = salas[salaActual].transform.Find("PosEnemigo");
        enemigos[r].transform.SetParent(punto,false);
        Debug.Log("intento generar un enemigo");
        eControllers[r].spawn();
        enemigoActual = eControllers[r];
    }

    public void LimpiarBasura()
    {
        
        if (salaActual == 1)
        {
            Destroy(esc_start.Find("Pared").gameObject);
        }
        else
            Destroy(salas[salaActual - 2]);
    }
    public EnemyController getEnemigo()
    {
        return enemigoActual;
    }
    public void abrirPuertas()
    {
        if (salaActual == 1)
        {
            Transform temp = esc_start.transform.Find("Puerta_out");
            Animator aniTemp = temp.Find("Puerta_dcha").GetComponent<Animator>();
            aniTemp.SetTrigger("abrir");
            aniTemp = temp.Find("Puerta_izq").GetComponent<Animator>();
            aniTemp.SetTrigger("abrir");
        }
        else
        {
            Transform temp = salas[salaActual-2].transform.Find("Puerta_out");
            Animator aniTemp = temp.Find("Puerta_dcha").GetComponent<Animator>();
            aniTemp.SetTrigger("abrir");
            aniTemp = temp.Find("Puerta_izq").GetComponent<Animator>();
            aniTemp.SetTrigger("abrir");
        }
    }

    public bool dropObject()
    {
		if (Random.Range (1, 3) == 1) {
			punto = salas[salaActual-1].transform.Find("Poti");
			temp = Instantiate(poti, punto);
			return true;
		}
		return false;
    }
}

