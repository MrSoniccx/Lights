using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopText : MonoBehaviour
{
    public GameObject DialogPrefab;
    public bool bigDialog=false;
    public bool active=false;
    public bool blockPlayer=false;
    private bool readingIg=false;
    public bool onTrigger=false;
    public List<string> linesEn = new List<string>();
    public List<string> linesEs = new List<string>();
    public List<string> moods = new List<string>();
    public GameObject afterDestroyed;
    public GameObject afterDestroyedActive;
    public GameObject attachToWhat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active==true&&readingIg==false &&onTrigger==false){
            readingIg=true;
            GameObject Dialog = Instantiate(DialogPrefab, transform.position, Quaternion.identity);
            if(bigDialog == false){

                if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
                {
                    Dialog.GetComponent<TextAnimationWritting>().lines = linesEs;
                }else if(PlayerPrefs.GetString("language") == "english"){
                    Dialog.GetComponent<TextAnimationWritting>().lines = linesEn;
                }
                Dialog.GetComponent<TextAnimationWritting>().wantMeToBlock = blockPlayer;
                Dialog.GetComponent<TextAnimationWritting>().padre = this.gameObject;
                if(attachToWhat != null){
                    Dialog.GetComponent<TextAnimationWritting>().player = attachToWhat;
                }
        }else{
            if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
                {
                    Dialog.GetComponent<BigDialog>().lines = linesEs;
                }else if(PlayerPrefs.GetString("language") == "english"){
                    Dialog.GetComponent<BigDialog>().lines = linesEn;
                }
                Dialog.GetComponent<BigDialog>().wantMeToBlock = blockPlayer;
                Dialog.GetComponent<BigDialog>().padre = this.gameObject;
                Dialog.GetComponent<BigDialog>().moods = moods;
        }
        
        }
    }

    void OnDestroy(){
        if (afterDestroyed!=null){
            GameObject pepe = Instantiate(afterDestroyed, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
        }
        if(afterDestroyedActive!=null){
            afterDestroyedActive.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(onTrigger==true){
        if(collision.gameObject.tag == "Player")
        {
            readingIg=true;
            GameObject Dialog = Instantiate(DialogPrefab, transform.position, Quaternion.identity);

             if(bigDialog == false){

                if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
                {
                    Dialog.GetComponent<TextAnimationWritting>().lines = linesEs;
                }else if(PlayerPrefs.GetString("language") == "english"){
                    Dialog.GetComponent<TextAnimationWritting>().lines = linesEn;
                }
                Dialog.GetComponent<TextAnimationWritting>().wantMeToBlock = blockPlayer;
                Dialog.GetComponent<TextAnimationWritting>().padre = this.gameObject;
                if(attachToWhat != null){
                    Dialog.GetComponent<TextAnimationWritting>().player = attachToWhat;
                }
        }else{
            if(PlayerPrefs.GetString("language") == "spanish" || PlayerPrefs.GetString("language") == "" || PlayerPrefs.GetString("language") == null)
                {
                    Dialog.GetComponent<BigDialog>().lines = linesEs;
                }else if(PlayerPrefs.GetString("language") == "english"){
                    Dialog.GetComponent<BigDialog>().lines = linesEn;
                }
                Dialog.GetComponent<BigDialog>().wantMeToBlock = blockPlayer;
                Dialog.GetComponent<BigDialog>().padre = this.gameObject;
                Dialog.GetComponent<BigDialog>().moods = moods;
        }
        }
        }
    }
}
