using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesibiltyTurnOnOff : MonoBehaviour
{
    public GameObject check1;
    public bool accesability = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        if (PlayerPrefs.GetInt("accesability") == 0 || PlayerPrefs.GetInt("accesability") == null){
            PlayerPrefs.SetInt("accesability", 0);
            accesability = false;
            if(check1.activeSelf == true){check1.SetActive(false);}
        }else{
            PlayerPrefs.SetInt("accesability", 1);
            accesability = true;
            if(check1.activeSelf == false){check1.SetActive(true);}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(){
        if (PlayerPrefs.GetInt("accesability") == 0){
            PlayerPrefs.SetInt("accesability", 1);
            accesability = true;
            if(check1.activeSelf == false){check1.SetActive(true);}
            
        }else{
            PlayerPrefs.SetInt("accesability", 0);
            accesability = false;
            if(check1.activeSelf == true){check1.SetActive(false);}
        }

        player.GetComponent<PlayerMovement>().accesability = accesability;
    }

}
