using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


public class PrimerPuzzle : MonoBehaviour
{
    private CodeFinder codeFinder;
    public PlayerMovement stopFocus;
    public PlayState PAUSE;
    public bool viewed=false;


    // Start is called before the first frame update
    void Start()
    {
        
        PAUSE = this.GetComponent<PlayableDirector>().state;
        codeFinder = GetComponent<CodeFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(viewed==true && this.GetComponent<PlayableDirector>().state == PAUSE){
            stopFocus.Repos();
                Destroy(gameObject);
        }
        if(codeFinder.Done==true){
            Cine();
            viewed=true;
        }
        
    }

    public void Cine(){
        this.GetComponent<PlayableDirector>().Play();
        
    }
}
