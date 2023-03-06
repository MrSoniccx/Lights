using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class LightLerp : MonoBehaviour
{

    [SerializeField] Color[] colors;
    private float t =0f;
    [SerializeField] [Range(0f, 30f)]float lerp=1f;
    private int colorIndex=0;
    private int len;

    // Start is called before the first frame update
    void Start()
    {
        len = colors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Light2D>().color = Color.Lerp(GetComponent<Light2D>().color, colors[colorIndex], lerp*Time.deltaTime);

        t = Mathf.Lerp (t, 1f, lerp*Time.deltaTime);
        if(t>.9f){
            t=0f;
            colorIndex++;
            colorIndex = (colorIndex>=colors.Length) ? 0 : colorIndex;
        }
    }
}
