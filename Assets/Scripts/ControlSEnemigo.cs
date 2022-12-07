using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlSEnemigo : MonoBehaviour
{
    public ProgressBar Pb;

    Animator caminar;

    Animator atacar;

    //Animator anim2;
    Transform positionJugador;

    public GameObject position2;

    public GameObject sonidoDaño;

    public GameObject sonidoMorir;

    NavMeshAgent agente;

    bool morir = true;

    bool b = true;

    public int zona = 0;

    public GameObject[] respawns;

    // Start is called before the first frame update
    void Start()
    {
        if (zona == 1)
        {
            respawns = GameObject.FindGameObjectsWithTag("zona1");
            int random = Random.Range(0, respawns.Length);
            transform.position = respawns[random].transform.position;
            position2 = respawns[random];
        }
        if (zona == 2)
        {
            respawns = GameObject.FindGameObjectsWithTag("zona2");
            int random = Random.Range(0, respawns.Length);
            transform.position = respawns[random].transform.position;
            position2 = respawns[random];
        }
        if (zona == 3)
        {
            respawns = GameObject.FindGameObjectsWithTag("zona3");
            int random = Random.Range(0, respawns.Length);
            transform.position = respawns[random].transform.position;
            position2 = respawns[random];
        }

        positionJugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        agente = GetComponent<NavMeshAgent>();
        caminar = GetComponent<Animator>();
        atacar = GetComponent<Animator>();
    }

    int a = 0;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == position2.transform.position.x)
        {
            caminar.SetBool("Caminar", false);
        }
        if (
            Mathf.Abs(transform.position.x - positionJugador.position.x) < 4 &&
            Mathf.Abs(transform.position.y - positionJugador.position.y) < 4 &&
            Mathf.Abs(transform.position.z - positionJugador.position.z) < 4
        )
        {
            caminar.SetBool("Caminar", true);
            agente.SetDestination(positionJugador.position);
            if (
                Mathf.Abs(transform.position.x - positionJugador.position.x) <
                3 &&
                Mathf.Abs(transform.position.y - positionJugador.position.y) <
                3 &&
                Mathf.Abs(transform.position.z - positionJugador.position.z) < 3
            )
            {
                atacar.SetLayerWeight(1, 1);
            }
            if (
                Mathf.Abs(transform.position.x - positionJugador.position.x) <
                2 &&
                Mathf.Abs(transform.position.y - positionJugador.position.y) <
                2 &&
                Mathf.Abs(transform.position.z - positionJugador.position.z) < 2
            )
            {
                if (b)
                {
                    StartCoroutine(Atacar());
                    b = false;
                }
            }
        }
        else
        {
            atacar.SetLayerWeight(1, 0);
            agente.SetDestination(position2.transform.position);
        }
    }

    public IEnumerator Atacar()
    {
        Pb.BarValue -= 20;
        if (Pb.BarValue > 0)
        {
            Instantiate (sonidoDaño);
        }
        if (Pb.BarValue == 0)
        {
            if (morir == true)
            {
                Instantiate (sonidoMorir);
                morir = false;
            }
        }
        yield return new WaitForSecondsRealtime(2.0f);

        b = true;
    }
}
