using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextitoSpawn : MonoBehaviour
{

    public GameObject TextoCanvas;
    public string text;
    public string subtext;
    public bool onTrigger=false;

    public string textEs;
    public string subtextEs;

    // Start is called before the first frame update
    void Start()
    {
        if(onTrigger==false){
            GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
                if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
                {
                    Canvas.GetComponent<TextUpsideCanvas>().Declarar(textEs, subtextEs);
                }else if(PlayerPrefs.GetString("language") == "english"){
                    Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
                }
                Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(onTrigger==true){
        if(collision.gameObject.tag == "Player")
        {
            GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
                if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
            {
                Canvas.GetComponent<TextUpsideCanvas>().Declarar(textEs, subtextEs);
            }else if(PlayerPrefs.GetString("language") == "english"){
                Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
            }
                Destroy(gameObject);
        }
        }
    }
}
