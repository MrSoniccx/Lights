using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class ColorChanger : MonoBehaviour
{
    public float luzMax;
    public float luzMin;
    bool a=false;
    private int i;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        switch(i)
        {
            case 0:
        GetComponent<Light2D>().color = new Color(1, 5, 5, 1);
        i=(i+1);
        break;
            case 4:
        GetComponent<Light2D>().color = new Color(5, 1, 5, 1);
        i=(i+1);
        break;
            case 8:
        GetComponent<Light2D>().color = new Color(5, 5, 1, 1);
        i=(i+1);
        break;
        default:
        i=(i+1);
        break;
        }

        if(i >= 12){
            i=1;
        }

        if(a==true)
        {GetComponent<Light2D>().intensity -= 0.01f*Time.deltaTime;}
        else
        {GetComponent<Light2D>().intensity += 0.01f*Time.deltaTime;}

        if(GetComponent<Light2D>().intensity >= luzMax){a=true;}
        else if(GetComponent<Light2D>().intensity <= luzMin) {a=false;}

    }
}
