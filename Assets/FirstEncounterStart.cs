using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEncounterStart : MonoBehaviour
{
    public GameObject SnakeSpawner, Respawn, Dialog;
    private GameObject player, snake;
    private bool deathAnima = false;
    public bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        Respawn.transform.position = new Vector2(this.transform.position.x-15f,this.transform.position.y);
        player = GameObject.FindGameObjectWithTag("Player");
        Dialog.SetActive(true);
        snake = Instantiate(SnakeSpawner, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Dialog.GetComponent<SecondCinematic>().connected == true){
            Destroy (snake, 0f);
            Destroy (this, 0f);
        }
        else if(player.GetComponent<Health>().death==true && deathAnima==false){
            deathAnima=true;
        }
        else if(player.GetComponent<Health>().death==false && deathAnima==true){
            deathAnima=false;
            Destroy (snake, 0f);
            snake = Instantiate(SnakeSpawner, transform.position, Quaternion.identity);
        }

        
    }
}
