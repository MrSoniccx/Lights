using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Enemy;
    Image healthBar;
    float maxHealth;
    float health;

    void Start(){
        healthBar = GetComponent<Image>();
        maxHealth = Enemy.GetComponent<HealthEnemy>().healthMax;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Enemy.GetComponent<HealthEnemy>().healthAmount;
        healthBar.fillAmount = health / maxHealth;
    }
}
