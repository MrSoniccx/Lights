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


    /*---------------------------
    Variables para Condiciones
    ---------------------------*/
    
    private string animationHelper="";
    private string state="";
    private float healthAmount;
    private float healthAmountMax;
    private float healthPercent=100;

    
    public string attack="MovingFoward";


    /*---------------------------
    Variables para ataques especiales
    ---------------------------*/

    private float timerSpring=0f;
    public float timerIntercalando;
    private float boostTimer=0f;




    //Animaciones
    const string SNAKE_SPRING = "SnakeSpring";

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = this.GetComponent<HealthEnemy>().GetHealth();
        healthAmountMax = this.GetComponent<HealthEnemy>().GetHealthMax();
        SnakeRb = GetComponent<Rigidbody2D>();
        SnakeSr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ChangeAnimationState("Safe");
    }

    void Update(){
        healthAmount = this.GetComponent<HealthEnemy>().GetHealth();
        healthPercent = healthAmount/healthAmountMax;
        healthPercent = healthPercent*100;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //-----First Phase-----//
        if(healthPercent>66 && healthPercent<=100){

            /*-----------------------------
            Primera fase: Lista de ataques
            +Spring (Movimiento de resorte = Boost)
            +MoveFoward
            +Camuflaje


            -----------------------------*/

            // if (Input.GetKey("space")){SnakeSr.color = new Color(1, 1, 1, 0.5f);}

        }


         //-----Second Phase-----//
        if(healthPercent>33 && healthPercent<=66){

            /*-----------------------------
            Segunda fase: Lista de ataques
            +Spring natural = MoveFoward natural
            +Camuflaje
            +Growl-TP
            +Supress
            -----------------------------*/

            

        }

            //-----Second Phase-----//
         if(healthPercent>=0 && healthPercent<=33){

            /*-----------------------------
            Segunda fase: Lista de ataques
            +Spring natural = MoveFoward natural
            +Camuflaje
            +Growl-TP
            +Supress
            +Inrage (Invulnerability)
            -Maybe spawn obstacles
            -----------------------------*/

            

        }



        
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



