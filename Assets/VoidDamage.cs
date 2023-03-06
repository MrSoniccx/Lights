using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector2 temp = (transform.position-collision.gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Health>().FallFromVoid();
        }
    }
}
