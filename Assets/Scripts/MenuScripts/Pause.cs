using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public bool pausa = false;
    public GameObject menu;
    public GameObject blackfadeout;
    public Color black;
    public Color semiBlack;
    public float masterTime;
    // Start is called before the first frame update
    void Start()
    {
        masterTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(pausa==false){masterTime = Time.deltaTime;}
        
    }

    public void SetPause(bool x){
        if(x==false){
            menu.GetComponent<MenuPause1>().CloseAll();
            StartCoroutine(LeavingPause());
        }else{
            menu.SetActive(true);
            menu.GetComponent<MenuPause1>().goMain();
            this.GetComponent<PlayerMovement>().blocked = true;
            blackfadeout.GetComponent<Image>().color = semiBlack;
            pausa=x;
        }
        
    }

    IEnumerator LeavingPause(){
        yield return new WaitForSeconds(0.15f);
        pausa=false;
        this.GetComponent<PlayerMovement>().blocked = false;
        blackfadeout.GetComponent<Image>().color = black;
        menu.SetActive(false);
        
    }
}
