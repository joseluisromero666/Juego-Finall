using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlSubirRampa : MonoBehaviour
{
    // Start is called before the first frame update
    bool ok = true;

    void Start()
    {
        StartCoroutine("Elevador");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Elevador()
    {
        int contador = 0;
        int contadorB = 0;

        for (; ; )
        {
            yield return new WaitForSecondsRealtime(0.5f);

            if (ok)
            {
                transform.position =
                    new Vector3(transform.position.x,
                        transform.position.y,
                        transform.position.z - 1);
                contador++;
                if (contador == 8)
                {
                    ok = false;
                    yield return new WaitForSecondsRealtime(2f);
                }
            }
            else
            {
                transform.position =
                    new Vector3(transform.position.x,
                        transform.position.y,
                        transform.position.z + 1);
                contadorB++;
                if (contadorB == 8)
                {
                    ok = true;
                    contadorB = 0;
                    contador = 0;
                    yield return new WaitForSecondsRealtime(2f);
                }
            }
        }
    }
}
