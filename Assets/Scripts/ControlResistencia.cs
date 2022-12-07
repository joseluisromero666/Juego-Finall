using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlResistencia : MonoBehaviour
{
    public Text Cpuntos;

    public int Vidas;

    Animator hit;

    Animator morir;

    public GameObject sonidoDaño;

    bool b = true;

    // Start is called before the first frame update
    void Start()
    {
        morir = GetComponent<Animator>();
        hit = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnParticleCollision(GameObject other)
    {
        if (b)
        {
            StartCoroutine(VidaMenos());
            b = false;
        }
    }

    public IEnumerator VidaMenos()
    {
        Instantiate (sonidoDaño);
        Vidas--;
        if (Vidas != 0)
        {
            hit.SetLayerWeight(3, 1);
            yield return new WaitForSecondsRealtime(1f);
            hit.SetLayerWeight(3, 0);
        }

        if (Vidas == 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            morir.SetLayerWeight(2, 1);
            yield return new WaitForSecondsRealtime(2f);
            Destroy(transform.gameObject);
            int Cp = 0;
            int.TryParse(Cpuntos.text, out Cp);
            Cp += 200;
            Cpuntos.text = Cp.ToString();
            morir.SetLayerWeight(2, 0);
        }
        b = true;
    }
}
