using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextitoSpawn : MonoBehaviour
{

    public GameObject TextoCanvas;
    public string text;
    public string subtext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
                Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
                Destroy(gameObject);
        }
    }
}
