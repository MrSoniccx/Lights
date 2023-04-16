using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float healthAmount = 100;
    public float regenTime = 10;
    public float regen = 10;
    public int vidas = 2;    public SoundMan soundMan;
    public Transform spawn;
    public GameObject DeathScreen;
    public GameObject healthDisplay;
    public bool death = false;

    public float invul = 0;
    public float invulTime = 20;

    private bool fullHealth = false;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (regen >= regenTime && this.GetComponent<PlayerMovement>().charged==true)
        {
            healthAmount = 100;
            AnimaHealth(2);
        }
        else {
            regen += 1 * (Time.deltaTime);
        }
        if(regen>=regenTime){regen=regenTime;}

        if (invul <= invulTime){invul += 1 * (Time.deltaTime);}

    }

    public void TakeDamage(Vector2 angle)
    {
        GetComponent<PlayerMovement>().ItookDamage(angle);
        if(invul >= invulTime)
        {
        healthAmount -= 99;
        soundMan.PlaySound("pHurt");
        GetComponent<SpriteRenderer>().color = Color.red;
        regen = 0;
        AnimaHealth(1);
        if(healthAmount <=0){
            AnimaHealth(0);
            StartCoroutine(Death());
        }
        invul=0;
        }

    }

    public void FallFromVoid(){
        
        soundMan.PlaySound("pFall");
        StartCoroutine(Death());


    }

    public void Dashed()
    {
        
        if(healthAmount >=0){
            healthAmount -= 99;
            AnimaHealth(5);
            regen = 0;
        }

        }

    

    public void Push(){
         


    }

    public void AnimaHealth(int state){
        
        switch(state){
            case 2:
                if(fullHealth==false){
                    fullHealth=true;
                    healthDisplay.GetComponent<Animator>().Play("RECOVER");
                }
                break;
            case 1:
                fullHealth=false;
                healthDisplay.GetComponent<Animator>().Play("DAMAGED");
            break;
            case 0:
                fullHealth=false;
                healthDisplay.GetComponent<Animator>().Play("DEATH");
            break;
            case 5:
                fullHealth=false;
                healthDisplay.GetComponent<Animator>().Play("DAMAGED");
            break;
        }
    }

    IEnumerator Death(){
        death = true;
        AnimaHealth(0);
        GetComponent<PlayerMovement>().blocked=true;
        soundMan.PlaySound("pDead");
        yield return new WaitForSeconds(1f);
        death = false;
        healthAmount = 100;
        AnimaHealth(0);
        regen = 100;
        transform.position = spawn.position; 
        GetComponent<PlayerMovement>().blocked=false;
    }
}
