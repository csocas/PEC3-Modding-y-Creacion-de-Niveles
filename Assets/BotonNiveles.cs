using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BotonNiveles : MonoBehaviour

{
    int nivel;
    public Text texto;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.parent.GetChildCount(); i++)
        {
            if (transform.parent.GetChild(i) == transform)
            {
                nivel = i;
                texto.text = "Nivel " + i;
                break;
            }
        } 
    }

    // Update is called once per frame
    public void Boton()
    {
        GameManager.singleton.Cargar(nivel);

    }
}
