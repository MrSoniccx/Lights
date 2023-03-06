using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OC_Bullet : MonoBehaviour
{
     public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject player;
    public GameObject lightPrefab;
    public SoundMan soundMan;

    [SerializeField] private float DamagePerBullet = 10f;




    Animator animator;
    private string currentState;

    //Animation states
    const string idle = "OC_Bullet";
    const string start = "OC_BulletSpawn";

    const string NOTHING = "NOTHING";
    const string SPIKE = "SPIKE";
    const string CRUSH = "CRUSH";

    private float timer_salida;

    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeAnimationState(start);
    }
    

    void Update() {

        timer_salida+=1f*Time.deltaTime;
        if(timer_salida>=4.9f){
            GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0f);}



        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;
        Debug.DrawLine(currentPosition, newPosition, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;

            if( other != player){
            if(other.tag !="Triggers"){
                
                if(other.tag =="BulletHell"){return;}

                
                    GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);

                    switch (other.tag)
                    {
                        case "Spike":
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(SPIKE);
                        
                        break;

                        case "Crush":
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(CRUSH);
                        break;

                        default:
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(NOTHING);
                        break;

                    }
                    Destroy(this.gameObject, 0f);
            
            }
            }

            
            if( other.tag == "Enemy"){
                other.GetComponent<HealthEnemy>().TakeDamage(DamagePerBullet);
                Destroy(this.gameObject, 0f);
            }

            if( other.tag == "Pillar"){
                other.GetComponent<Pillar>().doMyThing();
                Destroy(this.gameObject, 0f);
            }
        }


        transform.position = new Vector3(newPosition.x,newPosition.y,1);
    }



     void ChangeAnimationState(string newState){
        animator.Play(newState);
        currentState = newState;
    }
}
