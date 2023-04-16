using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public string target = "MasterVol";
    private SoundMan soundMan;
    public List<GameObject> objs = new List<GameObject>();
    public List<Sprite> sprites = new List<Sprite>();
    private float lights, valueIg;
    private float valuePercen = 100f/6f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        soundMan=GameObject.FindGameObjectWithTag("Player").gameObject.transform.Find("SoundManager").GetComponent<SoundMan>();
        audioMixer.SetFloat(target, (valuePercen*4 + ((valuePercen/4)*3))-80f);
    }

    // Update is called once per frame
    void Update()
    {
        audioMixer.GetFloat(target, out valueIg);
        lights = (valueIg+80f)/valuePercen;
        
        for(int i=0;i<=6;i++){
            float x = lights-i;

            if(x>=1f){objs[i].GetComponent<Image>().sprite = sprites[4];}
            else if(x>=0.74f){objs[i].GetComponent<Image>().sprite = sprites[3];}
            else if(x>=0.49f){objs[i].GetComponent<Image>().sprite = sprites[2];}
            else if(x>=0.24f){objs[i].GetComponent<Image>().sprite = sprites[1];}
            else if(x>=-0.001f){objs[i].GetComponent<Image>().sprite = sprites[0];}
            
        }
        

    }

    public void Add(){
        soundMan.PlaySound("UIaccept");
        audioMixer.GetFloat(target, out valueIg);
        if((valueIg+(valuePercen/4f)) <= 20f)
        {audioMixer.SetFloat(target, valueIg+(valuePercen/4f));}
        else{audioMixer.SetFloat(target, 20f);}
    }

    public void Subtrac(){
        soundMan.PlaySound("UIcancel");
        audioMixer.GetFloat(target, out valueIg);
        if((valueIg-(valuePercen/4f)) >= -80f)
        {audioMixer.SetFloat(target, valueIg-(valuePercen/4f));}
        else{audioMixer.SetFloat(target, -80f);}
    }
}
