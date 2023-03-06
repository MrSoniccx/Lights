using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointy : MonoBehaviour
{

    private PopText popping;
    // Start is called before the first frame update
    void Start()
    {
        popping = this.GetComponent<PopText>();
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            popping.active=true;
        }
        
    }
}
