using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFirstEncounter : MonoBehaviour
{
    [SerializeField] float speed = 280f;
    Rigidbody2D SnakeRb;
    SpriteRenderer SnakeSr;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        SnakeRb = GetComponent<Rigidbody2D>();
        SnakeSr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target.GetComponent<Pause>().pausa==false){
        LookPlayer(90f);
        MoveTowardsPlayer(1f);
        }
    }

    void LookPlayer(float deg){

        Vector2 lookDir = target.GetComponent<Transform>().position-transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg+deg;
        SnakeRb.rotation = angle;
    }

    void MoveTowardsPlayer(float Multiplier){
        
        transform.position = Vector2.MoveTowards(transform.position, target.GetComponent<Transform>().position, speed*Time.deltaTime*Multiplier);
    }

    void VisualFeedBack(){

    }

}
