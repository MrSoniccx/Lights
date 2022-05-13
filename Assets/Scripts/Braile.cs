using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braile : MonoBehaviour
{
    public string[] codigos;
    private int current=0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        /* Debug
            int i=0;
            while(codigos[i] != "")
            {
            Debug.Log(codigos[i]);
            i++;
            }

        */
        
    }

    public void Insertar(string codigo){
        codigos[current] = codigo;
        current++;
    }

    public void Eliminar(){
        int i=0;
        while(i!=4){
            codigos[i] = "";
            i++;
        }
        current--;
    }
}
