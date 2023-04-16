using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BigDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textComponent;
    private Image moodImage;
    public GameObject moodGO;
    public GameObject padre;
    private SoundMan soundMan;
    public GameObject player;
    public List<string> lines = new List<string>();
    public List<string> moods = new List<string>();
    public List<Sprite> moodsResources = new List<Sprite>();
    public float textSpeed;
    public int index = 0;
    public bool wantMeToBlock;
    private PlayerMovement pMovBlock;
    public Q_button qbutton;

    const string HAPPYTALK = "Happy";
    const string SERIOUSTALK = "Stalk";
    const string SERIOUS = "Serious";
    const string NICE = "Nice";
    const string HMMNICE = "Hnice";
    const string NICENT = "Nicent";
    const string NERVIOUS = "Nerv";
    const string NERVIOUSPLUS = "Nerv+";
    const string INTENSE = "Intense";
    

    void Start()
    {
        moodImage = moodGO.GetComponent<Image>();
        textComponent.text = string.Empty;
        index = 0;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        if(moods[0] != ""){
            cambiarMood(0);
        }
        pMovBlock=player.GetComponent<PlayerMovement>();
        soundMan=GameObject.FindGameObjectWithTag("Player").gameObject.transform.Find("SoundManager").GetComponent<SoundMan>();
        soundMan.PlaySound("Dialog");
        if(wantMeToBlock==true){
        pMovBlock.blocked=true;
        }
        StartCoroutine(TypeLine());
        
    }

    void Update(){
        if (Input.GetButtonDown("Interact")){
            
            if(textComponent.text == lines[index]){

                if(lines.Count-1 == index){
                    if(wantMeToBlock==true){
                        pMovBlock.blocked=false;
                    }
                Destroy(padre);
                
                    Destroy(this.gameObject);
            }else{
                soundMan.PlaySound("UIaccept");
                textComponent.text = string.Empty;
                index++;
                if(moods[index-1] != moods[index] && moods[index] != ""){
                    cambiarMood(index);
                    //soundMan.PlaySound("ChangeMood");
                    MoodAnima();
                }
                StartCoroutine(TypeLine());
            }
            }else{
                soundMan.PlaySound("UIaccept");
                StopAllCoroutines();
                textComponent.text = lines[index];
                
            }
            
            qbutton.Pressed(true);
            StartCoroutine(Qbut());
        }
    }


    IEnumerator TypeLine(){
        bool sonidosisonidono = false;
        int sonidoCount = 2;
        foreach(char c in lines[index].ToCharArray()){
            
            if (sonidosisonidono == false && sonidoCount == 2){
                sonidoCount = 0;
                sonidosisonidono=true;
                soundMan.PlaySound("Text");
            }else { sonidosisonidono=false; sonidoCount++;}
            
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
            
        }
    }

    IEnumerator Qbut(){
        yield return new WaitForSeconds(textSpeed);
            qbutton.Pressed(false);
    }

    private void cambiarMood(int x){
        switch(moods[x]){
            case SERIOUSTALK:
                moodImage.sprite = moodsResources[0];
            break;
            case SERIOUS:
                moodImage.sprite = moodsResources[1];
            break;
            case HAPPYTALK:
                moodImage.sprite = moodsResources[2];
            break;
            case NICE:
                moodImage.sprite = moodsResources[3];
            break;
            case HMMNICE:
                moodImage.sprite = moodsResources[4];
            break;
            case NICENT:
                moodImage.sprite = moodsResources[5];
            break;
            case NERVIOUS:
                moodImage.sprite = moodsResources[6];
            break;
            case NERVIOUSPLUS:
                moodImage.sprite = moodsResources[7];
            break;
            case INTENSE:
                moodImage.sprite = moodsResources[8];
            break;
            default:
                moodImage.sprite = moodsResources[0];
            break;
            
        }
    }

    private void MoodAnima(){
        moodGO.GetComponent<Animator>().Play("MoodLowing");
    }
}
