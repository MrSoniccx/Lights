using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeFinder : MonoBehaviour
{
    public GameObject braile;
    public GameObject queHago;
    [SerializeField] string[] code;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        string[] Bbraile = braile.GetComponent<Braile>().codigos;
        if (Bbraile[0]==code[0] && Bbraile[1]==code[1] && Bbraile[2]==code[2] && Bbraile[3]==code[3])
        {

            Destroy(gameObject,0.0f);
            //queHago.doMyThing();
        }
    }
}