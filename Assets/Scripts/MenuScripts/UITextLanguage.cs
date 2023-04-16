using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITextLanguage : MonoBehaviour
{
    public string english, spanish;
    private string textSelected;
    private TMP_Text tmpvalider = null; 
    private Text textvalider = null; 

    // Start is called before the first frame update
    void Start()
    {
        if (this.GetComponent<TMP_Text>() != null){
            tmpvalider = this.GetComponent<TMP_Text>();
        }

        if (this.GetComponent<Text>() != null){
            textvalider = this.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
            switch(PlayerPrefs.GetString("language")){
                case "english":
                    textSelected = english;
                    break;
                case "spanish":
                    textSelected = spanish;
                    break;
                default:
                    textSelected = spanish;
                    break;
            }
        

        if(textvalider){
            textvalider.text = textSelected;
        }
        if(tmpvalider){
            tmpvalider.text = textSelected;
        }
    }
}
