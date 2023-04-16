using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SnakeCinematic : MonoBehaviour
{
    public PlayState PAUSE;
    public bool viewed=false;
    public PlayableDirector secondCinematic;
    public GameObject TextoCanvas;
    public string text;
    public string subtext;
    public string textEs;
    public string subtextEs;
    private GameObject player;
    public GameObject ActivateAfterDeath;
    // Start is called before the first frame update
    void Start()
    {
        PAUSE = this.GetComponent<PlayableDirector>().state;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(viewed==true){
            if(secondCinematic.state==PAUSE){
                GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
                if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
                {
                    Canvas.GetComponent<TextUpsideCanvas>().Declarar(textEs, subtextEs);
                }else if(PlayerPrefs.GetString("language") == "english"){
                    Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
                }
                player.GetComponent<PlayerMovement>().CinematicLogic();
                ActivateAfterDeath.SetActive(true);
                Destroy(gameObject);
                Destroy(this.gameObject);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().CinematicLogic();
            this.GetComponent<PlayableDirector>().Play();
            
        }
    }
}
