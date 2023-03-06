using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParticlesPlayer : MonoBehaviour
{

    Animaciones father;
    PlayerMovement father2;
    // Start is called before the first frame update
    void Start()
    {
        father = this.transform.parent.gameObject.GetComponent<Animaciones>();
        father2 = this.transform.parent.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DashInBoolExchange(string x){
        bool y;
        if(x=="true"){y = true;}else{y = false;}
        father.dashIn=y;
    }

    public void DashOutBoolExchange(string x){
        bool y;
        if(x=="true"){y = true;}else{y = false;}
        father.dashOut=y;
    }


    public void AnimationBlocked(string block){
        if(block == "false"){
            father.blocked = false;
            
        }else{
            father.blocked = true;
            
        }
    }

    public void BigChargeStartEnded(string x){
        if(x=="true"){
            father.bCSEnded=true;
        }else{father.bCSEnded=false;}
    }

    public void BigChargeEnded(string x){
            father2.BigChargeEnded();
    }

}
