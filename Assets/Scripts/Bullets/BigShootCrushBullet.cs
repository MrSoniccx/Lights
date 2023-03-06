using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BigShootCrushBullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject player;
    public GameObject lightPrefab;
    public SoundMan soundMan;
    public GameObject particles;
    private ParticleSystem pSystem;

    [SerializeField] private AudioSource audioSource;
    private AudioMixer audioMixer;
    private AudioMixerGroup[] audioMixGroup;

    private bool hitted=false;

    [SerializeField] private float DamagePerBullet = 60f;




    Animator animator;
    private string currentState;

    //Animation states
    const string done = "BigChargeDisappear";
    const string start = "BigChargeGo";

    const string NOTHING = "NOTHING";
    const string SPIKE = "SPIKE";
    const string CRUSH = "CRUSH";
    const string VOID = "VOID";

    private float timer_salida;

    void Start()
    {
        pSystem = particles.GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        ChangeAnimationState(start);
        audioMixer = Resources.Load<AudioMixer>("AudioMix");
        audioMixGroup = audioMixer.FindMatchingGroups("Master"); 
    }
    

    void Update() {

        if(hitted==false){
        timer_salida+=1f*Time.deltaTime;
        if(timer_salida>=4.9f){
            GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit("NOTHING");
            Destroy(this.gameObject, 0f);}



        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;
        Debug.DrawLine(currentPosition, newPosition, Color.red);
        
        audioSource.outputAudioMixerGroup = audioMixGroup[1];

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;

            if( other != player){
            if(other.tag !="Triggers" && other.tag!="Void" && other.tag!="Stop"){
                
                if(other.tag =="BulletHell"){return;}

                
                    GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);


                    switch (other.tag)
                    {
                        case "Spike":
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(SPIKE);
                        pSystem.Stop();
                        break;

                        case "Crush":
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(CRUSH);
                        pSystem.Stop();
                        other.GetComponent<GettingCrush>().GetCrushed();
                        //GetCrushed
                        break;

                        default:
                        light.transform.Find("Sonidoss").GetComponent<Sonidito>().WhatDoIHit(NOTHING);
                        break;

                    }
                    Destroy(this.gameObject, 0.6f);
                    hitted=true;
                    ChangeAnimationState(done);
                
                
            
            
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
    }



     void ChangeAnimationState(string newState){
        animator.Play(newState);
        currentState = newState;
    }


    void OnDestroy(){
//        GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
        
    }
}
