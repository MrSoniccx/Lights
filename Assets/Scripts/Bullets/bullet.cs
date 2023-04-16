using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject trailPrefab;
    public float trailSpeed;
    public GameObject player;
    public GameObject lightPrefab;
    public SoundMan soundMan;
    [SerializeField] private AudioSource audioSource;
    private AudioMixer audioMixer;
    private AudioMixerGroup[] audioMixGroup;
    private int times=0;
    private bool onItTrail = false;

    [SerializeField] private float DamagePerBullet = 20f;



    Animator animator;
    private string currentState;

    //Animation states
    const string idle = "PistolBulletIdle";
    const string start = "PistolBulletAnimation";

    const string NOTHING = "NOTHING";
    const string SPIKE = "SPIKE";
    const string CRUSH = "CRUSH";

    private float timer_salida;

    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeAnimationState(start);
        //Load AudioMixer
        audioMixer = Resources.Load<AudioMixer>("AudioMix");
        audioMixGroup = audioMixer.FindMatchingGroups("Master"); 
        

    }
    

    void Update() {

        timer_salida+=1f*Time.deltaTime;
        if(timer_salida>=4.9f)
            {
                
            GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit("NOTHING");
            Destroy(this.gameObject, 0f);}

            if(onItTrail==false)
           {
             StartCoroutine(SpawnTrail());
           }



        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;
        Debug.DrawLine(currentPosition, newPosition, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);
      
                //Assign the AudioMixerGroup to AudioSource (Use first index)
                audioSource.outputAudioMixerGroup = audioMixGroup[1];

        foreach (RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;

            

            if( other != player){
            if(other.tag !="Triggers" && other.tag!="Void" && other.tag!="Stop" && other.tag !="StopDash"){
                
                if(times>=4){
                /*GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
                Destroy();*/
                } else{

                    
                    GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
                    

                    switch (other.tag)
                    {
                        case "Spike":
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(SPIKE);
                        Destroy(this.gameObject, 0f);
                        break;

                        case "Crush":
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(CRUSH);
                        break;

                        default:
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(NOTHING);
                        break;

                    }

                
                    velocity = Vector2.Reflect(velocity, hit.normal)/2f;
                    newPosition = currentPosition + velocity * Time.deltaTime;
                    Vector2 lookDir = currentPosition - newPosition;
                    float angle=Mathf.Atan2(lookDir.x, lookDir.y)*Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0,angle);
                    times++;
                    ChangeAnimationState(start);
                    
                    
                }
            
            
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

            if(other.tag == "Void"){
                audioSource.outputAudioMixerGroup = audioMixGroup[2];
            }


        }


        transform.position = new Vector3(newPosition.x,newPosition.y,1);
    }



     void ChangeAnimationState(string newState){
        animator.Play(newState);
        currentState = newState;
    }


    void OnDestroy(){
//        GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
        
    }

     IEnumerator SpawnTrail(){
        
        if(onItTrail == false){
        onItTrail=true;
        yield return new WaitForSeconds(trailSpeed);
        
        GameObject x = Instantiate(trailPrefab, transform.position, Quaternion.identity);
        x.GetComponent<Rigidbody2D>().AddForce(new Vector3(velocity.x*10, velocity.y*10, 0));
        Destroy(x,2f);
        onItTrail=false;
        
        }
            
            
        }
    }

