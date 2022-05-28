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
    private GameObject lights;
    private int codigo;

    // Start is called before the first frame update
    void Start()
    {
        Braile = GameObject.FindGameObjectWithTag("Braile");
    }


    public void doMyThing()
    {
        if (Activated == false)
        {
            lights = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            lights.GetComponent<PillarLight>().Caller = gameObject;
            Activated=true;
            codigo = Braile.GetComponent<Braile>().Insertar(whatAmI);
        }
        else {
            if(Braile.GetComponent<Braile>()!=null){
            lights.GetComponent<PillarLight>().countDown = 0f;
            lights.GetComponent<Light2D>().intensity = 1f;
            }
        }
    }

    public void SeAcabo(){
        Activated = false;
        if(Braile.GetComponent<Braile>()!=null)
        {Braile.GetComponent<Braile>().Eliminar(codigo);}

    }



}
