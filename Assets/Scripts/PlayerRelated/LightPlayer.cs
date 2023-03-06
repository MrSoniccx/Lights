using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class LightPlayer : MonoBehaviour
{
    public Rigidbody2D playerRb;
    [SerializeField] Color[] colors;
    [SerializeField] Color[] colorsDamaged;
    [SerializeField] [Range(0f, 30f)]float lerpTime=1f;
    private float lerpTimeHalf;
    private int colorIndex=0;
    private int len;
    private float t =0f;


    public float luzMax;
    public float luzMin;
    public float Timer;
    public float i=0;
    
    public bool damaged=false;
    public bool charging=false;
    // Start is called before the first frame update
    void Start()
    {
        len = colors.Length;
        lerpTimeHalf=lerpTime/2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!charging){
        if(playerRb.velocity != new Vector2(0f,0f))
        {
            if(i<Timer)
                {i=Timer;}

            if(GetComponent<Light2D>().intensity < luzMax)
                {GetComponent<Light2D>().intensity += 0.01f;}
        }
        else if(playerRb.velocity==new Vector2(0f,0f) && i > 0f)
        {i-=1f*Time.deltaTime;}
        else if(playerRb.velocity==new Vector2(0f,0f) && i <= 0f)
        {
            if(GetComponent<Light2D>().intensity > luzMin)
                {GetComponent<Light2D>().intensity -= 0.01f;}
            }

                ColorChanger(damaged, lerpTime);
        }else{
            if(GetComponent<Light2D>().intensity < (luzMax/2f))
                {GetComponent<Light2D>().intensity += 0.01f;}
                ColorChanger(damaged, lerpTimeHalf);
        }   
        



    }

    public void ColorChanger(bool damaged, float lerp){
        if(damaged==false){

                    GetComponent<Light2D>().color = Color.Lerp(GetComponent<Light2D>().color, colors[colorIndex], lerp*Time.deltaTime);

                    t = Mathf.Lerp (t, 1f, lerp*Time.deltaTime);
                    if(t>.9f){
                        t=0f;
                        colorIndex++;
                        colorIndex = (colorIndex>=colors.Length) ? 0 : colorIndex;
                    }
                    
        }else{
            GetComponent<Light2D>().color = Color.Lerp(GetComponent<Light2D>().color, colors[colorIndex], lerp*Time.deltaTime);
        }

    }
}
