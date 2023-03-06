using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause1 : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit(){
        player.GetComponent<Pause>().SetPause(false);

    }

    public void OpcionesSonido(){
        this.transform.Find("MenuStart").gameObject.SetActive(false);
        this.transform.Find("OpcionesSound").gameObject.SetActive(true);

    }

    public void goMain(){
        this.transform.Find("MenuStart").gameObject.SetActive(true);
        this.transform.Find("OpcionesSound").gameObject.SetActive(false);
    }
    
}
