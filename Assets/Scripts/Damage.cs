using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector2 temp = (transform.position-collision.gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Health>().TakeDamage(temp);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector2 temp = (transform.position-collision.gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Health>().TakeDamage(temp);
        }
    }
}
