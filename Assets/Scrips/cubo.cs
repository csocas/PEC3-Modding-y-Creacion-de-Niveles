using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubo : MonoBehaviour
{

    public void Move(Vector3 direccion, Vector3 rotacion)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, rotacion, out hit, 1f))

        {

        }
        else
        {
            transform.Translate(direccion, Space.World);
        }

    }



}
