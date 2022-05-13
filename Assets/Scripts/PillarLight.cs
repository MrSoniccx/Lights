using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class PillarLight : MonoBehaviour
{
    public float countDown=0f;
    public float Timer;
    public GameObject Caller;


    [SerializeField] Color[] colors;
    [SerializeField] [Range(0f, 10f)]float lerpTime=1f;
    private int colorIndex=0;
    private int len;
    private float t =0f;


    void Start()
    {
        len = colors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        

        GetComponent<Light2D>().color = Color.Lerp(GetComponent<Light2D>().color, colors[colorIndex], lerpTime*Time.deltaTime);
        t = Mathf.Lerp (t, 1f, lerpTime*Time.deltaTime);
        if(t>.9f){
            t=0f;
            colorIndex++;
            colorIndex = (colorIndex>=colors.Length) ? 0 : colorIndex;
            }

        GetComponent<Light2D>().intensity -= 0.2f*Time.deltaTime;
    
        if(countDown>=Timer){
            Destroy(gameObject,0.0f);
            Caller.GetComponent<Pillar>().SeAcabo();
            }
        countDown+=0.1f*Time.deltaTime;
    }
}
