using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstShoot : MonoBehaviour
{
    public GameObject TextoCanvas;
    private AbleTo player;
    public string text;
    public string subtext;

    public string textEs;
    public string subtextEs;
    private PopText popping;

    // Start is called before the first frame update
    void Start()
    {
            popping = this.GetComponent<PopText>();
            GameObject Canvas = Instantiate(TextoCanvas, transform.position, Quaternion.identity);
            if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
        {
            Canvas.GetComponent<TextUpsideCanvas>().Declarar(textEs, subtextEs);
        }else if(PlayerPrefs.GetString("language") == "english"){
            Canvas.GetComponent<TextUpsideCanvas>().Declarar(text, subtext);
        }
                
                player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<AbleTo>();
                player.move = false;
                player.shoot = true;

    }

    void FixedUpdate()
    {
         if (Input.GetButton("Fire1")){
                StartCoroutine(Le_go());
            }
        
        }

    
    IEnumerator Le_go(){
            yield return new WaitForSeconds(1f);
            popping.active=true;
            player.move = true;
        }

}
