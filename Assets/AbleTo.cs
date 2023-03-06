using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleTo : MonoBehaviour
{
    public bool shoot=false;
    public bool move=false;
    public bool dash=false;
    public bool changeGun=false;
    public bool bigCharge=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Todo(bool x){
        shoot = x;
        move = x;
        dash = x;
        changeGun = x;
        bigCharge = x;
    }
}
