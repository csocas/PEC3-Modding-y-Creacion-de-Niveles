using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salida : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Caja")
        {
            GameManager.singleton.CompletarSalida();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Caja")
        {
            GameManager.singleton.salidasCompletadas--;

        }
    }

}
