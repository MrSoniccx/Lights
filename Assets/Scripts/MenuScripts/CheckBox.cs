using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject check1;
    public bool voice = false;
    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt("Voices") == 0 || PlayerPrefs.GetInt("Voices") == null){
            PlayerPrefs.SetInt("Voices", 0);
            voice = false;
            if(check1.activeSelf == true){check1.SetActive(false);}
        }else{
            PlayerPrefs.SetInt("Voices", 1);
            voice = true;
            if(check1.activeSelf == false){check1.SetActive(true);}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(){
        if (PlayerPrefs.GetInt("Voices") == 0){
            PlayerPrefs.SetInt("Voices", 1);
            voice = true;
            if(check1.activeSelf == false){check1.SetActive(true);}
            
        }else{
            PlayerPrefs.SetInt("Voices", 0);
            voice = false;
            if(check1.activeSelf == true){check1.SetActive(false);}
        }

    }
}
