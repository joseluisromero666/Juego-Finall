using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TnTController : MonoBehaviour
{
    // Start is called before the first frame update
    public float movSpeed;

    public float exposionSpeed;

    public float radius;

    public float power;

    public int Vidas;

    public Text Cpuntos;

    public GameObject SonidoDestruccion;

    public GameObject SonidoDU;

    public ParticleSystem explosion;

    bool y = true;

    void Start()
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
        if (Vidas == 0)
        {
            Instantiate (SonidoDestruccion);
            explosion.Play();
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
            }
            yield return new WaitForSecondsRealtime(1f);
            explosion.Stop();
            Destroy(transform.gameObject);
            int Cp = 0;
            int.TryParse(Cpuntos.text, out Cp);
            Cp += 100;
            Cpuntos.text = Cp.ToString();
        }
        yield return new WaitForSecondsRealtime(1f);
        y = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
