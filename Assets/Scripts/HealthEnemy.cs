using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float healthMax = 100f;
    public float healthAmount = 100f;
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    
    void Start()
    {
        healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBar.transform.Find("HealthBackground").gameObject.transform.Find("HealthBar").gameObject.GetComponent<HealthBar>().Enemy = gameObject;
    }


    public void TakeDamage(float Damage)
    {
        healthAmount -= Damage;
        if(healthAmount <=0){
            Destroy(gameObject,0.5f);
            Destroy(healthBar.gameObject,0.5f);
        }

    }

    public float GetHealth(){
        return healthAmount;
    }

    public float GetHealthMax(){
        return healthMax;
    }
}
