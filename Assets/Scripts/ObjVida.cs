using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjVida : MonoBehaviour
{
    public int Vidas;

    public Text Cpuntos;

    public GameObject SonidoDestruccion;

    public GameObject SonidoDU;

    bool y = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnParticleCollision(GameObject other)
    {
        if (y == true)
        {
            StartCoroutine(VidaMenos());
            y = false;
        }
    }

    public IEnumerator VidaMenos()
    {
        Vidas--;
        Instantiate (SonidoDU);
        transform.position =
            new Vector3(transform.position.x + 1,
                transform.position.y,
                transform.position.z + 1);
        if (Vidas == 0)
        {
            Instantiate (SonidoDestruccion);
            Destroy(transform.gameObject);
            int Cp = 0;
            int.TryParse(Cpuntos.text, out Cp);
            Cp += 100;
            Cpuntos.text = Cp.ToString();
        }
        yield return new WaitForSecondsRealtime(1f);
        y = true;
    }
}
