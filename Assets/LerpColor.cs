using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{

    [SerializeField] Color[] colors;
    [SerializeField] [Range(0f, 10f)]float lerpTime=1f;
    private int colorIndex=0;
    private int len;
    private float t =0f;

    // Start is called before the first frame update
    void Start()
    {
        len = colors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Graphic>().color = Color.Lerp(GetComponent<Graphic>().color, colors[colorIndex], lerpTime*Time.deltaTime);
        t = Mathf.Lerp (t, 1f, lerpTime*Time.deltaTime);
        if(t>.9f){
            t=0f;
            colorIndex++;
            colorIndex = (colorIndex>=colors.Length) ? 0 : colorIndex;
            }
    }
}
