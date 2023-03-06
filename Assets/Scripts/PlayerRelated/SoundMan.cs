using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{

    public AudioClip playerDeadSound, playerFireSound, playerHitSound, playerDashCharged, playerFireBC, playerBCharge, playerTexting, playerPushingWall, playerHurt, playerFall, playerFireLowSound;
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
            case "pDead":
                audioSrc.PlayOneShot(playerDeadSound);
            break;
            case "pFire":
                audioSrc.PlayOneShot(playerFireSound);
            break;
            case "pHit":
                audioSrc.PlayOneShot(playerHitSound);
            break;
            case "DashCharged":
                audioSrc.PlayOneShot(playerDashCharged);
            break;
            case "pFireBC":
                audioSrc.PlayOneShot(playerFireBC);
            break;
            case "pBCharge":
                audioSrc.PlayOneShot(playerBCharge);
            break;
            case "Text":
                audioSrc.PlayOneShot(playerTexting);
            break;
            case "PPushWall":
                audioSrc.PlayOneShot(playerPushingWall);
            break;
            case "pHurt":
                audioSrc.PlayOneShot(playerHurt);
            break;
            case "pFall":
                audioSrc.PlayOneShot(playerFall);
            break;
            case "pFireLow":
                audioSrc.PlayOneShot(playerFireLowSound);
            break;
        }
    }
}
