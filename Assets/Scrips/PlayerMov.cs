using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Move(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Move(Vector3.forward*-1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            Move(Vector3.right*-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            Move(Vector3.right);
        }

        
    }

    void Move(Vector3 direccion)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))

        {
            if (hit.collider.gameObject.tag == "Caja")
            {
                hit.collider.GetComponent<cubo>().Move(direccion, transform.forward);

            }
        }
        else
        {
            transform.Translate(direccion, Space.World); 
        }
        
    }



}



