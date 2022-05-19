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
    public List<GameObject> pointsTP = new List<GameObject>();
    int children;


    /*---------------------------
    Variables para Condiciones
    ---------------------------*/
    
    private string animationHelper="Safe";
    private string state="";
    private float healthAmount;
    private float healthAmountMax;
    private float healthPercent=100;

    
    public string attack="MovingFoward";


    /*---------------------------
    Variables para ataques especiales
    ---------------------------*/
    private int randomNumber=0;
    private float timer=0f;
    private float boostTimerHelp=0f;
    public float timerBetween=2f;
    public float boostTimer=2.2f;
    public float tpRespawn=2f;
    public float transparent=2f;
    private float transparentHelp=1f;





    //Animaciones
    const string SNAKE_SPRING = "SnakeSpring";
    const string MOVING_FOWARD = "MovingFoward";
    

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
        children = GameObject.FindGameObjectWithTag("TPBoss").transform.childCount;
        for(int i=0; i<children;i++){
        pointsTP[i] = GameObject.FindGameObjectWithTag("TPBoss").transform.GetChild(i).gameObject;
        Debug.Log(pointsTP[i]);
        }
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
        if(healthPercent>0 && healthPercent<=100){

            /*-----------------------------
            Primera fase: Lista de ataques
            +Spring (Movimiento de resorte = Boost)
            +MoveFoward
            +Camuflaje


            -----------------------------*/

            
            if(attack=="Special"){ 

                if(randomNumber == 0)
                {randomNumber= Random.Range(1,3);}

                switch (randomNumber){
                    case 1:
                        Spring();
                        LookPlayer(90f);
                        break;
                    case 2:
                        Camuflaje();
                        LookPlayer(90f);
                        break;
                }
                
                
                
            }
            
            if(attack==MOVING_FOWARD){
                MoveTowardsPlayer(1f);
                LookPlayer(90f);
                timer+=1*Time.deltaTime;
                if(timer >= timerBetween){timer=0;attack="Special";}


                
            }

           

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



        
        
        


    }
        
    void Spring(){
        if(boostTimerHelp==0){ChangeAnimationState(SNAKE_SPRING);}
        
        if(animationHelper=="Realize"){
        MoveTowardsPlayer(4f);
        }
        if(animationHelper=="EndedSpring"){
            ChangeAnimationState("Safe");
            MoveTowardsPlayer(1.8f);
            boostTimerHelp+=1*Time.deltaTime;
            if(boostTimerHelp>=boostTimer){
                boostTimerHelp=0;
                animationHelper="Safe";
                randomNumber=0;
                attack=MOVING_FOWARD;
            }
            }
    }


    void Camuflaje(){
        if(animationHelper=="Safe"){
            MoveTowardsPlayer(0.75f);
        SnakeSr.color = new Color(1, 1, 1, transparentHelp);
        transparentHelp-=transparent*Time.deltaTime;
        if(transparentHelp<=0){transform.position = pointsTP[0].transform.position; animationHelper="Fuga";}
        }
        if(animationHelper=="Fuga")
        {
            transparentHelp += transparent*Time.deltaTime;
            if(transparentHelp >= tpRespawn)
            {
                int i=Random.Range(1,children);
                transform.position = pointsTP[i].transform.position;
                animationHelper="Respawn";
                transparentHelp=0f;
            }
        }
        if(animationHelper=="Respawn"){
            transparentHelp+=1*Time.deltaTime;
            SnakeSr.color = new Color(1, 1, 1, transparentHelp);
            if(transparentHelp>=1){
                transparentHelp=1f;
                animationHelper="Safe";
                SnakeSr.color = new Color(1, 1, 1, transparentHelp);
                randomNumber=0;
                attack=MOVING_FOWARD;
            }
            MoveTowardsPlayer(0.75f);
        }

        
        LookPlayer(90f);
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

    void LookPlayer(float deg){

        Vector2 lookDir = target.position-transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg+deg;
        SnakeRb.rotation = angle;
    }


    
 }




