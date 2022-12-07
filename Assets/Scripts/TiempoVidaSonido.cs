using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoVidaSonido : MonoBehaviour
{
    public float tiempoVidaSonido;

    void Start()
    {
        Destroy (gameObject, tiempoVidaSonido);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
