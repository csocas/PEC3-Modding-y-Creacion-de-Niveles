using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMov : MonoBehaviour


{
    public GameObject player;
    public float offsetZ = 2f;
    public float offsetY = 2f;
    public float v;


    // Start is called before the first frame update
    IEnumerator getplayer()
    {

        yield return new WaitForSeconds(1f);

        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnEnable()
    {
        StartCoroutine(getplayer());

    }


    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z - offsetZ), Time.deltaTime * v);
        }
        
    }
}
