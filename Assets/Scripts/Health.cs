using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float healthAmount = 100;
    public float regenTime = 10;
    public float regen = 10;
    public int vidas = 2;
    public SoundMan soundMan;
    public Transform spawn;
    public GameObject DeathScreen;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (regen >= regenTime)
        {
            healthAmount = 100;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else {
            regen += 1 * (Time.deltaTime);
        }
    }

    public void TakeDamage()
    {
        healthAmount -= 99;
        GetComponent<SpriteRenderer>().color = Color.red;
        regen = 0;
        if(healthAmount <=0){
            soundMan.PlaySound("pDead");
            if(vidas!=0){
            healthAmount = 100;
            GetComponent<SpriteRenderer>().color = Color.white;
            regen = 100;
            transform.position = spawn.position; 
            vidas--;
            }
            else{
            Destroy(gameObject);
            Instantiate(DeathScreen, transform.position, Quaternion.identity);}
        }

    }
}
