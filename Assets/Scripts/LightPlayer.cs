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
    [SerializeField] [Range(0f, 10f)]float lerpTime=1f;
    private int colorIndex=0;
    private int len;
    private float t =0f;


    public float luzMax;
    public float luzMin;
    public float Timer;
    public float i=0;
    
    public bool damaged=false;
    // Start is called before the first frame update
    void Start()
    {
        len = colors.Length;
    }

    // Update is called once per frame
    void Update()
    {
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



        ColorChanger(damaged);



    }

    public void ColorChanger(bool damaged){
        if(damaged==false){

                    GetComponent<Light2D>().color = Color.Lerp(GetComponent<Light2D>().color, colors[colorIndex], lerpTime*Time.deltaTime);

                    t = Mathf.Lerp (t, 1f, lerpTime*Time.deltaTime);
                    if(t>.9f){
                        t=0f;
                        colorIndex++;
                        colorIndex = (colorIndex>=colors.Length) ? 0 : colorIndex;
                    }
                    
        }else{
            GetComponent<Light2D>().color = Color.Lerp(GetComponent<Light2D>().color, colors[colorIndex], lerpTime*Time.deltaTime);
        }

    }
}
