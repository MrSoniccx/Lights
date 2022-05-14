using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class Pillar : MonoBehaviour
{

    [SerializeField] string whatAmI = "";
    public GameObject Braile;
    public GameObject lightPrefab;
    public bool Activated = false;
    private GameObject light;
    private int codigo;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void doMyThing()
    {
        if (Activated == false)
        {
            light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            light.GetComponent<PillarLight>().Caller = gameObject;
            Activated=true;
            codigo = Braile.GetComponent<Braile>().Insertar(whatAmI);
        }
        else {
            light.GetComponent<PillarLight>().countDown = 0f;
            light.GetComponent<Light2D>().intensity = 1f;
        }   
    }

    public void SeAcabo(){
        Activated = false;
        Braile.GetComponent<Braile>().Eliminar(codigo);

    }



}
