using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidito : MonoBehaviour
{
    public AudioClip  Nothing;
    public AudioClip  Spike;
    public AudioClip  Crush;
    static AudioSource audioSrc;

    const string NOTHING = "NOTHING";
    const string SPIKE = "SPIKE";
    const string CRUSH = "CRUSH";

    private string hitted = "";
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        if (hitted==NOTHING){
            audioSrc.PlayOneShot(Nothing);
        }
        if (hitted==SPIKE){
            audioSrc.volume= (audioSrc.volume/2);
            audioSrc.PlayOneShot(Spike);
        }
        if (hitted==CRUSH){
            audioSrc.PlayOneShot(Crush);
        }
        
    }

    

    public void WhatDoIHit(string hit){

        hitted = hit;
        
    }
}
