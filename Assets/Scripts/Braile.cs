using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braile : MonoBehaviour
{
    public string[] codigos;
    public GameObject[] hotbars;
    public GameObject hotbarPrefab;
    public float spaceBetween;
    private int current=0;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    public int Insertar(string codigo){
        codigos[current] = codigo;
        hotbars[current] = Instantiate(hotbarPrefab, transform.position, Quaternion.identity);

        switch(current)
        {
            case 0:
            hotbars[current].transform.Find("Hotbar").GetComponent<HotbarSpawn>().Posicion(0f);
            break;
            case 1:
            hotbars[current].transform.Find("Hotbar").GetComponent<HotbarSpawn>().Posicion(spaceBetween);
            break;
            case 2:
            hotbars[current].transform.Find("Hotbar").GetComponent<HotbarSpawn>().Posicion(spaceBetween*2);
            break;
            case 3:
            hotbars[current].transform.Find("Hotbar").GetComponent<HotbarSpawn>().Posicion(spaceBetween*3);
            break;
            case 4:
            hotbars[current].transform.Find("Hotbar").GetComponent<HotbarSpawn>().Posicion(spaceBetween*4);
            break;
        }

        hotbars[current].transform.Find("Hotbar").GetComponent<HotbarSpawn>().WhatAmI(codigo);
        int i=current;
        Check();
        return i;
        

    }

    public void Eliminar(int index){
        codigos[index] = "";
        hotbars[index].transform.Find("Hotbar").GetComponent<HotbarSpawn>().Exit();
        hotbars[index] = null;
        Check();

    }

    void Check(){
        current = 0;

            while(codigos[current]!="")
            {current++;}
    }

}
