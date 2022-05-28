using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float velocity = 1f;
    public GameObject player;
    public SoundMan soundMan;
    public float counter = 1f;
    Vector2 currentPosition;

    void FixedUpdate() {
        LookPlayer(0f);
        counter -= 1*Time.deltaTime;
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), (velocity)*Time.deltaTime);
        
        Vector2 newPositionLow = new Vector2(newPosition.x,newPosition.y-0.5f);

        Debug.DrawLine(currentPosition, newPositionLow, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);
        RaycastHit2D[] hits2 = Physics2D.LinecastAll(currentPosition, newPositionLow);

        foreach (RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;
            if(other.tag=="Wall"){
                counter = 0f;
            }
        }

        foreach (RaycastHit2D hit in hits2) {
            GameObject other = hit.collider.gameObject;
            if(other.tag=="Wall"){
                counter = 0f;
            }
        }

        
        if(counter<=0f){ OnDestroy1();}
        transform.position = newPosition;
        }

        public void OnDestroy1()
        {
            
            player.transform.position = currentPosition;
            player.GetComponent<PlayerMovement>().ComeBackDash();
            Destroy(gameObject);
        }

        void LookPlayer(float deg){

        Vector2 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg+deg;
        GetComponent<Rigidbody2D>().rotation = angle;
    }


        
    
}
