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

    // Start is called before the first frame update
    void Start()
    {
        if(onTrigger==false){
            GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
                Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
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
                Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
                Destroy(gameObject);
        }
        }
    }
}
