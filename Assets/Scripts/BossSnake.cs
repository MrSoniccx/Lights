using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BossSnake : MonoBehaviour
{


    #region Variables
    [SerializeField] float speed = 280f;
    Rigidbody2D SnakeRb;
    SpriteRenderer SnakeSr;
    GameObject target;
    Animator animator;
    public List<GameObject> pointsTP = new List<GameObject>();
    int children;
    public UnityEvent shock;
    private string[] braile;
    public SnakeSounds snakeSound;


    /*---------------------------
    Variables para Condiciones
    ---------------------------*/
    
    private string animationHelper="Safe";
    private string springHelper="Safe";
    private string state="";
    private float healthAmount;
    private float healthAmountMax;
    private float healthPercent=100;
    private bool phaseChange=false;
    private bool codeUnlocked=false;

    
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
    public float transparent=3f;
    private float transparentHelp=1f;
    public int phase=2;
    private float freeUse=0;
    private int indexHelp=0;
    public List<Vector3> pointsCircle = new List<Vector3>();
    public float radius = 8;
    

    public float phaseAnimation=0f;





    //Animaciones
    const string SNAKE_SPRING = "SnakeSpring";
    const string MOVING_FOWARD = "MovingFoward";
    const string BLOCKED = "Blocked";
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = this.GetComponent<HealthEnemy>().GetHealth();
        healthAmountMax = this.GetComponent<HealthEnemy>().GetHealthMax();
        SnakeRb = GetComponent<Rigidbody2D>();
        SnakeSr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        ChangeAnimationState("Safe");
        children = GameObject.FindGameObjectWithTag("TPBoss").transform.childCount;
        for(int i=0; i<children;i++){
        pointsTP[i] = GameObject.FindGameObjectWithTag("TPBoss").transform.GetChild(i).gameObject;
        }
    }

    void Update(){
        healthAmount = this.GetComponent<HealthEnemy>().GetHealth();
        healthPercent = healthAmount/healthAmountMax;
        healthPercent = healthPercent*100;
        braile = GameObject.FindGameObjectWithTag("Braile").GetComponent<Braile>().codigos;
        

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //-----First Phase-----//
        if(healthPercent<=67 && phase==1 && phaseChange==false){
                this.GetComponent<HealthEnemy>().blocked = true;

                if(attack==MOVING_FOWARD){
                    ScreamPhase(1);
                }else{FirstPhase();}
        }
        else if(phase==1){
            /*-----------------------------
            Primera fase: Lista de ataques
            +Spring (Movimiento de resorte = Boost)
            +MoveFoward
            +Camuflaje
            -----------------------------*/
            if(healthPercent<=67 && phase==1 && phaseChange==true){
                if(braile[0] == "#" && braile[1] == "1" && braile[2] == "" && braile[3] == "" && braile[4] == ""){Cling(); //clingk
                }
            }
            
            if(codeUnlocked==true && attack==MOVING_FOWARD){
                ScreamPhase(10);
                
                
            }else{FirstPhase();}

        }
        


         //-----Second Phase-----//
         if(healthPercent<=34 && phase==2 && phaseChange==false){
                this.GetComponent<HealthEnemy>().blocked = true;

                if(attack==MOVING_FOWARD){
                    ScreamPhase(2);
                }else{SecondPhase();}
        }else if(phase==2 || phase==3){

            /*-----------------------------
            Segunda fase: Lista de ataques
            +Spring natural = MoveFoward natural
            +Camuflaje
            +Growl-TP
            +Supress
            -----------------------------*/
            if(healthPercent<=34 && phase==2 && phaseChange==true){
                if(braile[0] == "#" && braile[1] == "2" && braile[2] == "" && braile[3] == "" && braile[4] == ""){Cling(); //clingk
                }
            }

            if(codeUnlocked==true && attack==MOVING_FOWARD){
                ScreamPhase(20);
                
            }else{
                SecondPhase();}

        }

            //-----Third Phase-----//
         if(phase==3){

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


    #region SpecialAttacksPhase1    

    void FirstPhase(){
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

    #endregion

     void SecondPhase(){
        if(attack=="Special"){ 
            if(randomNumber == 0)
                {randomNumber= Random.Range(1,3);}

            switch (randomNumber){
                case 1:
                    Supress();
                    
                break;
                case 2:
                    Camuflaje();
                    LookPlayer(90f);
                break;
                     }
                }
            
                if(attack==MOVING_FOWARD){
                    SpringWalk(0.8f);
                    timer+=1*Time.deltaTime;
                    if(timer >= timerBetween){timer=0;attack="Special";boostTimerHelp=0;}  
                        }

             }


    void SpringWalk(float i){
        if(springHelper=="Safe"){
            MoveTowardsPlayer(i*-0.6f);
            boostTimerHelp+=1*Time.deltaTime;
            if(boostTimerHelp>=0.2f){
                springHelper="Realize";
                boostTimerHelp=0;
            }
        }
        if(springHelper=="Realize"){
            MoveTowardsPlayer(i*3.1f);
            boostTimerHelp+=1*Time.deltaTime;
            if(boostTimerHelp>=0.42f){
                springHelper="Hold";
                boostTimerHelp=0;
            }
        }
        if(springHelper=="Hold"){
            boostTimerHelp+=1*Time.deltaTime;
            if(boostTimerHelp>=0.2){
                springHelper="Safe";
                boostTimerHelp=0;
            }
        }
        
        LookPlayer(90f);
    }

    void Supress(){
        int Size = 30;
        Vector3 startCorner = new Vector3(radius, 0, 0) + new Vector3(target.GetComponent<Transform>().position.x, target.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z);

        // The "previous" corner point, initialised to the starting corner.
             Vector3 previousCorner = startCorner;

            for(int i=0; i < Size; i++){

                // Calculate the angle of the corner in radians.
                float cornerAngle = 2f * Mathf.PI / (float)Size * i;
                // Get the X and Y coordinates of the corner point.
                Vector3 currentCorner = new Vector3(Mathf.Cos(cornerAngle) * radius, Mathf.Sin(cornerAngle) * radius, 0f) + new Vector3(target.GetComponent<Transform>().position.x, target.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z);
                pointsCircle[i] = currentCorner;
                // Draw a side of the polygon by connecting the current corner to the previous one.
                Debug.DrawLine(currentCorner, previousCorner);
                // Having used the current corner, it now becomes the previous corner.
                previousCorner = currentCorner;
            }



        if(animationHelper=="Safe"){
            freeUse+=1*Time.deltaTime;
            
            if(freeUse>=0.1){
                MoveTowardsPlayer(-1);
                LookPlayer(-90f);
                SnakeSr.color = new Color(1, 1, 1, transparentHelp);
                transparentHelp-=transparent*Time.deltaTime;
                if(transparentHelp<=0){
                transform.position = pointsTP[0].transform.position;
                animationHelper="IwasHiding";
                freeUse=0;
                }

            }else{LookPlayer(90f);}
            
        }
        if(animationHelper=="IwasHiding"){
            indexHelp = Random.Range(1,Size);
                transform.position = pointsCircle[indexHelp];
                animationHelper="Panic";
        }
        if(animationHelper=="Panic"){ 
            boostTimerHelp+=1*Time.deltaTime;
            
            if(transparentHelp<=1f){transparentHelp+=transparent*Time.deltaTime; SnakeSr.color = new Color(1, 1, 1, transparentHelp);}
            if(transparentHelp>=1f){transparentHelp=1; SnakeSr.color = new Color(1, 1, 1, transparentHelp);}

            freeUse+=1f*Time.deltaTime;
            if(freeUse>= 0.08){
                freeUse=0;
                indexHelp+=1;
                if(indexHelp>=Size){
                    indexHelp=0;
                }
                }
            transform.position = Vector2.MoveTowards(transform.position, pointsCircle[indexHelp], speed*Time.deltaTime*3.1f);
            Vector2 lookDir;
            if(indexHelp+1 < Size){
            lookDir =  pointsCircle[indexHelp+1]-transform.position;
            }else{
            lookDir =  pointsCircle[0]-transform.position;
            }
            float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg+90;
            SnakeRb.rotation = angle;

            if(boostTimerHelp>=4){
                animationHelper="Close";
                boostTimerHelp=0;
            }
        }
        if(animationHelper=="Close"){
            radius-=4*Time.deltaTime;
            freeUse+=1f*Time.deltaTime;
            if(freeUse>= 0.08){
                freeUse=0;
                indexHelp+=1;
                if(indexHelp>=Size){
                    indexHelp=0;
                }
                }
            transform.position = Vector2.MoveTowards(transform.position, pointsCircle[indexHelp], speed*Time.deltaTime*3.1f);
            LookPlayer(90f);
            if(radius<=0){
                animationHelper="End";
            }
        }
        if(animationHelper=="End"){
            radius=8;
            freeUse=0;
            indexHelp=0;
            animationHelper="Safe";
            attack=MOVING_FOWARD;
            randomNumber=0;
            boostTimerHelp=0;
        }
        
    }

    void ChangeAnimationState(string newState){
        if (state == newState) return;

        animator.Play(newState);

        state = newState;
    }


    void MoveTowardsPlayer(float Multiplier){
        
        transform.position = Vector2.MoveTowards(transform.position, target.GetComponent<Transform>().position, speed*Time.deltaTime*Multiplier);
    }

    void LookPlayer(float deg){

        Vector2 lookDir = target.GetComponent<Transform>().position-transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg+deg;
        SnakeRb.rotation = angle;
    }


    void SpringMoveFoward(float Multiplier){
        if(animationHelper=="Safe"){
            
        transform.position = Vector2.MoveTowards(transform.position, target.GetComponent<Transform>().position, (speed*Time.deltaTime*Multiplier)*-1f);
        boostTimerHelp+=1*Time.deltaTime;
            if(boostTimerHelp>=1f){
                boostTimerHelp=0;
                animationHelper="SpringRealize";
            }
        }
        if(animationHelper=="SpringRealize"){
            transform.position = Vector2.MoveTowards(transform.position, target.GetComponent<Transform>().position, (speed*Time.deltaTime*Multiplier)*2f);
            boostTimerHelp+=1*Time.deltaTime;
            if(boostTimerHelp>=0.7f){
                boostTimerHelp=0;
                animationHelper="Safe";
            }
        }
    }


    void ScreamPhase(int i){
        target.GetComponent<PlayerMovement>().blocked = true;
        
        if(i==1){
            GrowlPlusShake();
            if(phaseAnimation>=2.5f){
                    animationHelper="Safe";
                    target.transform.Find("Focus").gameObject.transform.position = target.GetComponent<Transform>().position;
                    target.GetComponent<PlayerMovement>().CinematicLogic();
                    
                    phaseAnimation=0f;
                    phaseChange=true;
                    target.GetComponent<PlayerMovement>().blocked = false;
                    
                    
                    
                }
            
        }
        if(i==10){
            GrowlPlusShake();
            if(phaseAnimation>=2.5f){
                    animationHelper="Safe";
                    target.transform.Find("Focus").gameObject.transform.position = target.GetComponent<Transform>().position;
                    target.GetComponent<PlayerMovement>().CinematicLogic();
                    phaseAnimation=0f;
                    phase=2;
                    phaseChange=false;
                    this.GetComponent<HealthEnemy>().blocked = false;
                    target.GetComponent<PlayerMovement>().blocked = false;
                    codeUnlocked=false;
                }
        }

        if(i==2){
            GrowlPlusShake();
            if(phaseAnimation>=2.5f){
                    animationHelper="Safe";
                    target.transform.Find("Focus").gameObject.transform.position = target.GetComponent<Transform>().position;
                    target.GetComponent<PlayerMovement>().CinematicLogic();
                    
                    phaseAnimation=0f;
                    phaseChange=true;
                    target.GetComponent<PlayerMovement>().blocked = false;
                    
                }
            
        }
        if(i==20){
            GrowlPlusShake();
            if(phaseAnimation>=2.5f){
                    animationHelper="Safe";
                    target.transform.Find("Focus").gameObject.transform.position = target.GetComponent<Transform>().position;
                    target.GetComponent<PlayerMovement>().CinematicLogic();
                    phaseAnimation=0f;
                    phase=3;
                    phaseChange=false;
                    this.GetComponent<HealthEnemy>().blocked = false;
                    target.GetComponent<PlayerMovement>().blocked = false;
                    codeUnlocked=false;
                    
                }
        }
    }

    void GrowlPlusShake(){
            if(animationHelper=="Safe"){
                //Growl
                shock.Invoke();
                target.GetComponent<PlayerMovement>().CinematicLogic();
                target.transform.Find("Focus").gameObject.transform.position = this.transform.position;
                animationHelper="Holding";
                snakeSound.PlaySound("growl");
            }if(animationHelper=="Holding"){
                shock.Invoke();
                phaseAnimation +=1f*Time.deltaTime;
                LookPlayer(90f);
                
            }
            
    }


    void Cling(){
        codeUnlocked=true;
    }


    void OnDestroy()
    {
        //Cambio de escena!
        Destroy(transform.parent.gameObject, 0f);
        SceneManager.LoadScene("scene2");
    }


    
 }




