using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpsideCanvas : MonoBehaviour
{
    private Text Text;
    private Text SubText;

    // Start is called before the first frame update
    void Start()
    {
        Text = this.transform.Find("Text").gameObject.GetComponent<Text>();
        SubText = this.transform.Find("SubText").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Declarar(string text, string subText){
        this.transform.Find("Text").gameObject.GetComponent<Text>().text = text;
        this.transform.Find("SubText").gameObject.GetComponent<Text>().text = subText;
    }

    public void Destruir(){
        Destroy(gameObject);
    }
}
