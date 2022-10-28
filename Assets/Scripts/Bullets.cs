using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    private Vector2 moveDirection;
    private float moveSpeed;
    public GameObject lightPrefab;


    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        
     
    }
    

    public void SetMoveDirections(Vector2 dir)
    {
        moveDirection = dir;
    }


    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
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
