using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimationWritting : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TextMeshProUGUI textComponent;
    public GameObject gameObjDialog;
    public GameObject player;
    public GameObject padre;
    private SoundMan soundMan;
    public bool wantMeToBlock;
    public List<string> lines = new List<string>();
    public float textSpeed;
    public int index = 0;
    private PlayerMovement pMovBlock;
    

    void Start()
    {
        if (player == null){
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        }

         //You probably want a more specific type than GameObject
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if(gameObj.name == "DialogPoP(Clone)" && gameObj != this.gameObject)
            {
                Destroy(gameObj);
            }
        }
        
        textComponent.text = string.Empty;
        index = 0;
        gameObjDialog.transform.position = new Vector3(player.transform.position.x + 3, player.transform.position.y+1,player.transform.position.z);
        pMovBlock=player.GetComponent<PlayerMovement>();
        soundMan=GameObject.FindGameObjectWithTag("Player").gameObject.transform.Find("SoundManager").GetComponent<SoundMan>();
        if(wantMeToBlock==true){
        pMovBlock.blocked=true;
        }
        StartCoroutine(TypeLine());
        
    }

    void Update(){
        if (Input.GetKeyDown("e")){
            if(textComponent.text == lines[index]){

                if(lines.Count-1 == index){
                    if(wantMeToBlock==true){
                        pMovBlock.blocked=false;
                    }
                Destroy(padre);
                Destroy(this.gameObject);
            }else{
                textComponent.text = string.Empty;
                index++;
                StartCoroutine(TypeLine());
            }
            }else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObjDialog.transform.position = new Vector3(player.transform.position.x + 3, player.transform.position.y+1,player.transform.position.z);
    }

    IEnumerator TypeLine(){
        bool sonidosisonidono = false;
        foreach(char c in lines[index].ToCharArray()){
            if (sonidosisonidono == false){
                sonidosisonidono=true;
                soundMan.PlaySound("Text");
            }else { sonidosisonidono=false;}
            
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
