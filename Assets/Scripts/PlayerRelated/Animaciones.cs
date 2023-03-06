using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour
{

    private string currentState;
    private string currentStateLegs;
    float angle;
    bool running=false;
    public bool shooting=false;
    string lookDir;
    public bool blocked = false;
    public bool dashIn = false;
    public bool dashOut = false;
    public bool charge = false;
    public int chargeState=0;
    public bool bCSEnded=false;





    //Animation states
    const string PLAYER_IDLEDOWN = "IdleDown";
    const string PLAYER_IDLEUP = "IdleUp";
    const string PLAYER_IDLERIGHT = "IdleRight";
    const string PLAYER_IDLELEFT = "IdleLeft";
    const string PLAYER_MOVDOWN = "MovDown";
    const string PLAYER_MOVUP = "MovUp";
    const string PLAYER_MOVRIGHT = "MovRight";
    const string PLAYER_MOVLEFT = "MovLeft";
    const string PLAYER_IDLESHOOTDOWN = "IdleShootingDown";
    const string PLAYER_IDLESHOOTUP = "IdleShootingUp";
    const string PLAYER_IDLESHOOTRIGHT = "IdleShootingRight";
    const string PLAYER_IDLESHOOTLEFT = "IdleShootingLeft";
    const string PLAYER_MOVSHOOTDOWN = "RunningShootingDown";
    const string PLAYER_MOVSHOOTUP = "RunningShootingUp";
    const string PLAYER_MOVSHOOTRIGHT = "RunningShootingRight";
    const string PLAYER_MOVSHOOTLEFT = "RunningShootingLeft";

    const string PLAYER_DASH_IN = "DashTransformIn";
    const string PLAYER_DASH_OUT = "DashTransformOut";
    
    const string PLAYER_CHARGE_START = "ChargeStart";
    const string PLAYER_CHARGE_REPEAT = "ChargeRepeat";
    const string PLAYER_CHARGE_DONE = "ChargeDone";

    Animator legs;
    Animator body;
    Animator hair;
    Animator particles;


    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        legs = this.transform.Find("Legs").gameObject.GetComponent<Animator>();
        body = this.transform.Find("UpperBody").gameObject.GetComponent<Animator>();
        hair = this.transform.Find("Hair").gameObject.GetComponent<Animator>();
        particles = this.transform.Find("Particles").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WhereImLookingAt();
        ChoosingAnimation();
    }
    

    public void AnimationBlocked(string block){
        if(block == "false"){
            blocked = false;
            shooting=false;
        }else{
            blocked = true;
            shooting=true;
        }
    }

    public void ShootingOrNot(string block){
        if(block== "false"){
            shooting=false;
        }else{
            shooting=true;
        }
    }

    public void Angle(float angl){
        angle = angl;
    }


    void WhereImLookingAt(){
        if(angle>=45f && angle<=135f){
            lookDir=PLAYER_IDLEUP;

        }else if((angle>=135f && angle<=180) || (angle<=-135)){
            lookDir=PLAYER_IDLELEFT;

        }else if(angle>=-135f && angle<=-45){
            lookDir=PLAYER_IDLEDOWN;

        }else if(angle>=-45f && angle<=45f){
            lookDir=PLAYER_IDLERIGHT;
        }
    }


    public void Moving(Vector2 velocity){
        if(velocity == new Vector2(0f,0f)){running=false;}
        else{running=true;}

    }


    void ChoosingAnimation(){


        if(dashIn==true || dashOut==true){

            if(dashIn==true){
            ChangeAnimationState(PLAYER_DASH_IN);
            LegsAnimationState(PLAYER_DASH_IN);
            }else{
            ChangeAnimationState(PLAYER_DASH_OUT);
            LegsAnimationState(PLAYER_DASH_OUT);
            }

        }else if(charge==true){
            
            
            if(chargeState==0){
                ChangeAnimationState(PLAYER_CHARGE_START);
                LegsAnimationState(PLAYER_CHARGE_START);
            }
            else if(chargeState==1){
                bCSEnded=false;
                ChangeAnimationState(PLAYER_CHARGE_REPEAT);
                LegsAnimationState(PLAYER_CHARGE_REPEAT);
            }
            else if(chargeState==2){
                ChangeAnimationState(PLAYER_CHARGE_DONE);
                LegsAnimationState(PLAYER_CHARGE_DONE);
            }
        }
        else{

        //Idles
            if(lookDir==PLAYER_IDLEUP && running==false && shooting==false){
                ChangeAnimationState(PLAYER_IDLEUP);
                LegsAnimationState(PLAYER_IDLEUP);}

            else if(lookDir==PLAYER_IDLEDOWN && running==false && shooting==false){
                ChangeAnimationState(PLAYER_IDLEDOWN);
                LegsAnimationState(PLAYER_IDLEDOWN);}
                
            else if(lookDir==PLAYER_IDLELEFT && running==false && shooting==false){
                ChangeAnimationState(PLAYER_IDLELEFT);
                LegsAnimationState(PLAYER_IDLELEFT);}

            else if(lookDir==PLAYER_IDLERIGHT && running==false && shooting==false){
                ChangeAnimationState(PLAYER_IDLERIGHT);
                LegsAnimationState(PLAYER_IDLERIGHT);}


        //Idles Shotting
            if(lookDir==PLAYER_IDLEUP && running==false && shooting==true){
                ChangeAnimationState(PLAYER_IDLESHOOTUP);
                LegsAnimationState(PLAYER_IDLEUP);}
            else if(lookDir==PLAYER_IDLEDOWN && running==false && shooting==true){
               ChangeAnimationState(PLAYER_IDLESHOOTDOWN);
               LegsAnimationState(PLAYER_IDLEDOWN);}
            
            else if(lookDir==PLAYER_IDLELEFT && running==false && shooting==true){
                ChangeAnimationState(PLAYER_IDLESHOOTLEFT);
                LegsAnimationState(PLAYER_IDLELEFT);}

            else if(lookDir==PLAYER_IDLERIGHT && running==false && shooting==true){
                ChangeAnimationState(PLAYER_IDLESHOOTRIGHT);
                LegsAnimationState(PLAYER_IDLERIGHT);}

        //Running
            if(lookDir==PLAYER_IDLEUP && running==true && shooting==false){
                ChangeAnimationState(PLAYER_MOVUP);
                LegsAnimationState(PLAYER_MOVUP);}

            else if(lookDir==PLAYER_IDLEDOWN && running==true && shooting==false){
                ChangeAnimationState(PLAYER_MOVDOWN);
                LegsAnimationState(PLAYER_MOVDOWN);}
            
            else if(lookDir==PLAYER_IDLELEFT && running==true && shooting==false){
                ChangeAnimationState(PLAYER_MOVLEFT);
                LegsAnimationState(PLAYER_MOVLEFT);}

            else if(lookDir==PLAYER_IDLERIGHT && running==true && shooting==false){
                ChangeAnimationState(PLAYER_MOVRIGHT);
                LegsAnimationState(PLAYER_MOVRIGHT);}

        //Running Shotting
            if(lookDir==PLAYER_IDLEUP && running==true && shooting==true){
                ChangeAnimationState(PLAYER_MOVSHOOTUP);
                LegsAnimationState(PLAYER_MOVUP);}

            else if(lookDir==PLAYER_IDLEDOWN && running==true && shooting==true){
                ChangeAnimationState(PLAYER_MOVSHOOTDOWN);
                LegsAnimationState(PLAYER_MOVDOWN);}
            
            else if(lookDir==PLAYER_IDLELEFT && running==true && shooting==true){
              ChangeAnimationState(PLAYER_MOVSHOOTLEFT);
              LegsAnimationState(PLAYER_MOVLEFT);}

            else if(lookDir==PLAYER_IDLERIGHT && running==true && shooting==true){
              ChangeAnimationState(PLAYER_MOVSHOOTRIGHT);
              LegsAnimationState(PLAYER_MOVRIGHT);}
        }
    }


     void ChangeAnimationState(string newState){
        if (currentState == newState || blocked==true) return;

        //animator.Play(newState);
        hair.Play(newState);
        body.Play(newState);
        particles.Play(newState);
//
        currentState = newState;
    }

    void LegsAnimationState(string newState){
        
        if (currentStateLegs == newState || blocked==true) return;

        legs.Play(newState);
        

        currentStateLegs = newState;
    
    }


    public void DashInBoolExchange(string x){
        bool y;
        if(x=="true"){y = true;}else{y = false;}
        dashIn=y;
    }

    public void DashOutBoolExchange(string x){
        bool y;
        if(x=="true"){y = true;}else{y = false;}
        dashOut=y;
    }

    public bool BigChargeDone(){
        return bCSEnded;
    }

    public void BigChargeStartEnded(string x){
        if(x=="true"){
            bCSEnded=true;
        }else{bCSEnded=false;}
    }
    

}
