using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class DestroyLight : MonoBehaviour
{
    private float u=0;
    private int i=0;
    public float Timer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        switch(i)
        {
            case 7:
        GetComponent<Light2D>().color = new Color(1, 5, 5, 1);
        i= (i+1);
        break;
            case 14:
        GetComponent<Light2D>().color = new Color(5, 1, 5, 1);
        i= (i+1);
        break;
            case 21:
        GetComponent<Light2D>().color = new Color(5, 5, 1, 1);
        i= (i+1);
        break;
        default:
        i= (i+1);
        break;
        }

        if(i >= 30){
            i=1;
        }

        GetComponent<Light2D>().intensity -= 0.1f*Time.deltaTime;

        if(u>=Timer){Destroy(gameObject,0.0f);}
        u+=0.1f*Time.deltaTime;
    }
}
