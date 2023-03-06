using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDash : MonoBehaviour
{
    public GameObject TextoCanvas;
    private AbleTo player;
    public string text;
    public string subtext;
    private PopText popping;
    public SpriteRenderer thisIsGonnaExplode;
    public Sprite convertInto;

    // Start is called before the first frame update
    void Start()
    {
            popping = this.GetComponent<PopText>();
            GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
                Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
                player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<AbleTo>();

                thisIsGonnaExplode =  GameObject.FindGameObjectWithTag("Crush").transform.Find("CrushDash").gameObject.transform.Find("Square0").gameObject.GetComponent<SpriteRenderer>();
                player.move = true;
                player.shoot= true;
                player.bigCharge = true;
                player.dash = true;

    }

    void Update()
    {
         if (thisIsGonnaExplode.sprite == convertInto){
            StartCoroutine(Le_go());
         }
        
        }

    
    IEnumerator Le_go(){
            yield return new WaitForSeconds(1f);
            popping.active=true;
        }
}
