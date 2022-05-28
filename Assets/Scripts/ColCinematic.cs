using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


public class ColCinematic : MonoBehaviour
{
    public GameObject TextoCanvas;
    public string text;
    public string subtext;
    public PlayState PAUSE;
    public bool viewed=false;
    // Start is called before the first frame update
    void Start()
    {
        PAUSE = this.GetComponent<PlayableDirector>().state;
    }

    // Update is called once per frame
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.GetComponent<PlayableDirector>().Play();
            viewed = true;
        }
    }
    
    void Update(){
        if(viewed==true && this.GetComponent<PlayableDirector>().state == PAUSE){
            GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
                Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
                Destroy(gameObject);
        }
    }
}
