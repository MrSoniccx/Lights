using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSounds : MonoBehaviour
{
    public AudioClip growl;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clip)
    {
        switch(clip){
            case "growl":
                audioSrc.PlayOneShot(growl);
            break;
            
        }
    }
}
