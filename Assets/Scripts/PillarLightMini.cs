using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class PillarLightMini : MonoBehaviour
{

    public GameObject intensity;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        GetComponent<Light2D>().intensity = intensity.GetComponent<Light2D>().intensity;
    }
}
