using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSnake : MonoBehaviour
{
    [SerializeField] float speed = 280f;
    Rigidbody2D SnakeRb;
    SpriteRenderer SnakeSr;
    Transform target;
    Animator animator;
    private float timerSpring=0f;
    public float timerIntercalando;
    private string animationHelper="";
    private string state="";

    private float boostTimer=0f;


    //Animaciones
    const string SNAKE_SPRING = "SnakeSpring";

    // Start is called before the first frame update
    void Start()
    {
        
        SnakeRb = GetComponent<Rigidbody2D>();
        SnakeSr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ChangeAnimationState("Safe");
    }

    void Update(){

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(timerSpring >= timerIntercalando){
            Spring();}
        else{timerSpring += 1*Time.deltaTime;
        MoveTowardsPlayer(1f);
        }
        Vector2 lookDir = target.position-transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg+90f;
        SnakeRb.rotation = angle;
        


    }
        
    void Spring(){
        if(boostTimer==0){ChangeAnimationState(SNAKE_SPRING);}
        
        if(animationHelper=="Realize"){
        MoveTowardsPlayer(4f);
        }
        if(animationHelper=="EndedSpring"){
            ChangeAnimationState("Safe");
            MoveTowardsPlayer(1.8f);
            boostTimer+=1*Time.deltaTime;
            if(boostTimer>=2.2){
                boostTimer=0;
                timerSpring=0f;
                animationHelper="";
            }
            }
        
    }

    public void State(string imIn){
        animationHelper = imIn;
    }


    void ChangeAnimationState(string newState){
        if (state == newState) return;

        animator.Play(newState);

        state = newState;
    }

    void MoveTowardsPlayer(float Multiplier){
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime*Multiplier);
    }


    
 }



