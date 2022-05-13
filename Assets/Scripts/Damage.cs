using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Health health;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            health.TakeDamage();
        }
    }
}
