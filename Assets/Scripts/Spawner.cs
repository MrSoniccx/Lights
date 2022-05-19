using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(Boss, transform.parent.gameObject.transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }
}
