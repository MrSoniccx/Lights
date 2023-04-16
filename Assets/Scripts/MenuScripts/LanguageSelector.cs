using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    public string selectedLanguage;
    public List<string> languages = new List<string>();
    private int index=0;
    // Start is called before the first frame update
    void Start()
    {
        
        languages.Add("english");
        languages.Add("spanish");

        if (PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null){
            PlayerPrefs.SetString("language", "spanish");
            selectedLanguage = PlayerPrefs.GetString("language");
        }else{
            for(index=0; index!=languages.Count-1; index++){
                if(PlayerPrefs.GetString("language") == languages[index]){selectedLanguage = languages[index];}
            }
        }
        
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
}