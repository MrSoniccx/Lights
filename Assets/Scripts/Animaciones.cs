using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour
{
    Animator animator;
    private string currentState;
    float angle;
    bool running=false;
    public bool shooting=false;
    string lookDir;
    bool blocked = false;
    //float timer = 1f;




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


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WhereImLookingAt();
        ChoosingAnimation();
    }
    
    void FixedUpdate()
    {
       /* if(timer<=0.3f){
            Debug.Log(timer);
            timer+=1f*Time.deltaTime;
            if(timer>=0.3){
                shooting=false;
            }else{shooting=true;}
        }*/
        
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
        //Idles
        if(lookDir==PLAYER_IDLEUP && running==false && shooting==false){
            ChangeAnimationState(PLAYER_IDLEUP);}

        else if(lookDir==PLAYER_IDLEDOWN && running==false && shooting==false){
            ChangeAnimationState(PLAYER_IDLEDOWN);}
            
        else if(lookDir==PLAYER_IDLELEFT && running==false && shooting==false){
            ChangeAnimationState(PLAYER_IDLELEFT);}

        else if(lookDir==PLAYER_IDLERIGHT && running==false && shooting==false){
            ChangeAnimationState(PLAYER_IDLERIGHT);}


        //Idles Shotting
        if(lookDir==PLAYER_IDLEUP && running==false && shooting==true){
            ChangeAnimationState(PLAYER_IDLESHOOTUP);}
        else if(lookDir==PLAYER_IDLEDOWN && running==false && shooting==true){
            ChangeAnimationState(PLAYER_IDLESHOOTDOWN);}
            
        else if(lookDir==PLAYER_IDLELEFT && running==false && shooting==true){
            ChangeAnimationState(PLAYER_IDLESHOOTLEFT);}

        else if(lookDir==PLAYER_IDLERIGHT && running==false && shooting==true){
            ChangeAnimationState(PLAYER_IDLESHOOTRIGHT);}

        //Running
        if(lookDir==PLAYER_IDLEUP && running==true && shooting==false){
            ChangeAnimationState(PLAYER_MOVUP);}

        else if(lookDir==PLAYER_IDLEDOWN && running==true && shooting==false){
            ChangeAnimationState(PLAYER_MOVDOWN);}
            
        else if(lookDir==PLAYER_IDLELEFT && running==true && shooting==false){
            ChangeAnimationState(PLAYER_MOVLEFT);}

        else if(lookDir==PLAYER_IDLERIGHT && running==true && shooting==false){
            ChangeAnimationState(PLAYER_MOVRIGHT);}

        //Running Shotting
        if(lookDir==PLAYER_IDLEUP && running==true && shooting==true){
            ChangeAnimationState(PLAYER_MOVSHOOTUP);}

        else if(lookDir==PLAYER_IDLEDOWN && running==true && shooting==true){
            ChangeAnimationState(PLAYER_MOVSHOOTDOWN);}
            
        else if(lookDir==PLAYER_IDLELEFT && running==true && shooting==true){
            ChangeAnimationState(PLAYER_MOVSHOOTLEFT);}

        else if(lookDir==PLAYER_IDLERIGHT && running==true && shooting==true){
            ChangeAnimationState(PLAYER_MOVSHOOTRIGHT);}
    }


     void ChangeAnimationState(string newState){
        if (currentState == newState || blocked==true) return;

        animator.Play(newState);

        currentState = newState;
    }


    

}
