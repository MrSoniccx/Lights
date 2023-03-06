using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public bool pausa = false;
    public GameObject menu;
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
            menu.GetComponent<MenuPause1>().goMain();
            this.GetComponent<PlayerMovement>().blocked = false;
            menu.SetActive(false);
        }else{
            this.GetComponent<PlayerMovement>().blocked = true;
            menu.SetActive(true);
        }
        pausa=x;
    }
}
