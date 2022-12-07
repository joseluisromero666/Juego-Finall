using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // private Rigidbody rb;
    //  public float speed;
    public ParticleSystem poder1;

    Animator poderOne;

    public ParticleSystem poder2;

    Animator poderTwo;

    Animator morir;

    Animator defender;

    public ProgressBar Pb;

    private Vector3 posicion;

    private Rigidbody rb;

    public GameObject SonidoPoder1;

    public GameObject SonidoPoder2;

    public GameObject sonidoMorir;

    public GameObject posInicial;

    public Text GameOver;

    public GameObject SonidoGanador;

    Animator revivir;

    bool BMorir = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Pb.BarValue = 100;
        morir = GetComponent<Animator>();
        defender = GetComponent<Animator>();
        poderOne = GetComponent<Animator>();
        poderTwo = GetComponent<Animator>();
        revivir = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate (SonidoPoder1);

            StartCoroutine(Poder1());
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate (SonidoPoder2);
            StartCoroutine(Poder2());
        }
        if (Pb.BarValue == 0)
        {
            if (BMorir == true)
            {
                StartCoroutine(Reiniciar());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("water"))
        {
            if (BMorir == true)
            {
                StartCoroutine(Reiniciar());
            }
        }
        if (other.gameObject.CompareTag("portal"))
        {
            Instantiate (SonidoGanador);
            GameOver.text = "!GANASTE¡";
            StartCoroutine(Ganador());
        }
        if (other.gameObject.CompareTag("portal2"))
        {
            Instantiate (SonidoGanador);
            GameOver.text = "!GANASTE¡";
            StartCoroutine(SalirAplicacion());
        }
    }

    public IEnumerator SalirAplicacion()
    {
        yield return new WaitForSecondsRealtime(4.0f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
        #endif
    }

    public IEnumerator Ganador()
    {
        yield return new WaitForSecondsRealtime(4.0f);
        SceneManager.LoadScene(1);
    }

    public IEnumerator Reiniciar()
    {
        Instantiate (sonidoMorir);
        BMorir = false;
        Pb.BarValue = 0;
        morir.SetBool("Die", true);
        GameOver.text = "Game Over";
        yield return new WaitForSecondsRealtime(4.0f);
        transform.position = posInicial.transform.position;
        GameOver.text = "";
        revivir.SetLayerWeight(1, 1);
        morir.SetBool("Die", false);
        yield return new WaitForSecondsRealtime(1.0f);
        revivir.SetLayerWeight(1, 0);
        Pb.BarValue = 100;
        BMorir = true;
    }

    public IEnumerator Poder1()
    {
        poderOne.SetBool("atacar1", true);
        yield return new WaitForSecondsRealtime(2.0f);
        poder1.Play();
        yield return new WaitForSecondsRealtime(1.0f);
        poder1.Stop();
        poderOne.SetBool("atacar1", false);
    }

    public IEnumerator Poder2()
    {
        poderTwo.SetBool("atacar2", true);
        yield return new WaitForSecondsRealtime(2.0f);
        poder2.Play();
        yield return new WaitForSecondsRealtime(1.0f);
        poder2.Stop();
        poderTwo.SetBool("atacar2", false);
    }

    public IEnumerator Defend()
    {
        defender.SetBool("Defend", true);
        yield return new WaitForSecondsRealtime(2.0f);
        defender.SetBool("Defend", false);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
    }
}
