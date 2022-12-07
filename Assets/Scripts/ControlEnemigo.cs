using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlEnemigo : MonoBehaviour
{
    public ProgressBar Pb;

    Animator caminar;

    Animator atacar;

    Transform positionJugador;

    public GameObject position1;

    public GameObject position2;

    public GameObject sonidoDaño;

    public GameObject sonidoMorir;

    NavMeshAgent agente;

    bool morir = true;

    bool b = true;

    // Start is called before the first frame update
    void Start()
    {
        positionJugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        agente = GetComponent<NavMeshAgent>();
        caminar = GetComponent<Animator>();
        atacar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            StartCoroutine(Patrullar());
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

    public IEnumerator Patrullar()
    {
        if (
            transform.position.x == position2.transform.position.x ||
            transform.position.z == position2.transform.position.z
        )
        {
            caminar.SetBool("Caminar", false);
            yield return new WaitForSecondsRealtime(3.0f);
            caminar.SetBool("Caminar", true);
            agente.SetDestination(position1.transform.position);
        }
        else if (
            transform.position.x == position1.transform.position.x ||
            transform.position.z == position1.transform.position.z
        )
        {
            caminar.SetBool("Caminar", false);
            yield return new WaitForSecondsRealtime(3.0f);
            caminar.SetBool("Caminar", true);
            agente.SetDestination(position2.transform.position);
        }
        else
        {
            agente.SetDestination(position2.transform.position);
            yield return new WaitForSecondsRealtime(8.0f);
        }

        yield return new WaitForSecondsRealtime(0.0f);
    }
}
