using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    public string selectedLanguage;
    public List<string> languages = new List<string>();
    private int index=0;
    private int aux=0;
    public bool onIt=false;
    public Button primaryButton, secundaryButton;
    // Start is called before the first frame update
    void Start()
    {
        
        languages.Add("english");
        languages.Add("spanish");

        if (PlayerPrefs.GetString("language") == ""){
            PlayerPrefs.SetString("language", "spanish");
            selectedLanguage = PlayerPrefs.GetString("language");
            index = languages.Count-1;
        }else{
            for(index=0; index!=languages.Count; index++){
                if(PlayerPrefs.GetString("language") == languages[index]){selectedLanguage = languages[index]; aux = index;}
            }
        }
        index=aux;
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Advance(){
        index++;
        if(index>=languages.Count){index=0;}
        PlayerPrefs.SetString("language", languages[index]);
        selectedLanguage = PlayerPrefs.GetString("language");
         
}

    public void Back(){
        index--;
        if(index<0){index=languages.Count-1;}
        PlayerPrefs.SetString("language", languages[index]);
        selectedLanguage = PlayerPrefs.GetString("language");
         
}

    public void LanguagePress(){
        if(onIt==false){onIt=true;
        secundaryButton.Select();
        }else{onIt=false;
        primaryButton.Select();
        }
    }

    public void AdvanceManual(InputAction.CallbackContext context){
        if (onIt==true && context.performed){Advance();}
    }
    public void BackManual(InputAction.CallbackContext context){
        if (onIt==true && context.performed){Back();}
    }
}