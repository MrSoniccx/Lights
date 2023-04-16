using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;
using UnityEngine.UI;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    SpriteRenderer playerSR;
    Animaciones anima;
    private Shooting shootlogic;
    private AbleTo ableTo;
    private Vector2 moveDirection;
    private Vector2 mousePos;
    private Vector2 angle;
    private bool stunned=false;
    private bool knockbackAvaible=true;
    private float countDown=0f;
    private GameObject dashSave;
    private Q_button buttonQ;
    private Text gunNameText;
    private float bigCharging=0f;
    private float bigChargeCD=0f;
    private float holdDetect=0.6f;
    private bool bigCharge=false;
    private GameObject bigChargeObject;
    private bool bcSound=false;
    private Vector2 moveDirection2;
    
    private float wallingSound = 0.2f;
    private float wallingSoundCurrent = 0f;


    private int gunCurrent = 0;
     
     
     
    private float dashCurrentSaveTimer;
    private bool dashing;
    private int dashesUnlocked=1;
    
     
     

     //Variables publicas
     public bool accesability = false;
     [SerializeField] private float velocityForce = 8f;
     [SerializeField] private float knockback = 2f;
     [SerializeField] private float dashDistance = 2f;
     [SerializeField] private float dashVelo = 2f;
     [SerializeField] private float dashSaveTimer = 2f;
     [SerializeField] private float bigMaxCharging=2f;
     public GameObject focus;
     public GameObject crosshair;
     public GameObject dashPrefab;
     public LightPlayer lighta;
     public Light2D lightaA;
     public SoundMan soundMan;
     public CinemachineTargetGroup CMtarget;
     public float countDownTimer;
     public List<string> guns = new List<string>();
     public  bool blocked=false;
     public GameObject chargedBulletPrefab;

     private Pause pausa;
     public bool charged=true;


    // Start is called before the first frame update
    void Start()
    {
        guns.Add("pistol");
        guns.Add("overCharge");
        dashCurrentSaveTimer = dashSaveTimer;
        playerRb = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        ableTo = GetComponent<AbleTo>();
        pausa = GetComponent<Pause>();
        anima = GetComponent<Animaciones>();
        shootlogic = GetComponent<Shooting>();
        lighta = this.transform.Find("Point Light 2D").GetComponent<LightPlayer>();
        lightaA = this.transform.Find("Point Light 2D").GetComponent<Light2D>();
        buttonQ = GameObject.FindGameObjectWithTag("UI").transform.Find("Q button").GetComponent<Q_button>();
        gunNameText = GameObject.FindGameObjectWithTag("UI").transform.Find("GunName").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {

       PauseLogic();

        
        if(stunned==false && blocked==false){
        ProcessInputs();
        }else {countDown+= 1*Time.deltaTime; buttonQ.Pressed(true);}
        if(countDown >= countDownTimer){
            stunned=false;
            countDown=0;
        }


        if(bigChargeCD<=3f){bigChargeCD+=1*Time.deltaTime;}

        DashingLogic();
        if(GetComponent<Health>().invul >= GetComponent<Health>().invulTime){knockbackAvaible=true;}
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 zeroZero = new Vector2(0f,0f);

        if(playerRb.velocity==zeroZero && (moveDirection.x != zeroZero.x || moveDirection.y != zeroZero.y) && ableTo.move == true && blocked==false){
            wallingSoundCurrent+= 1*Time.deltaTime;
            if(wallingSoundCurrent>=wallingSound){
            soundMan.PlaySound("PPushWall");
            wallingSoundCurrent=0;
            }
        }

        crosshair.GetComponent<CrosshairFollow>().accesability = accesability;

    }

    void FixedUpdate()
    {
        if(stunned==false && blocked==false && ableTo.move==true){
        Move();
        }else if(stunned==true){playerRb.velocity=((-angle)*knockback);}
        if(blocked==true){Stay();}
        Aim();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;


        if (Input.GetButton("Fire1") && ableTo.shoot==true){
            if(bigCharge==false){
            Shoot();       
            }
        }

         if (Input.GetButton("Jump") && ableTo.dash==true){   
            Dash();  
        }

        if (Input.GetButton("ChangeGun") && ableTo.changeGun==true){
            if(bigCharge==true){
            anima.chargeState+=1;
            shootlogic.Refill();
            //anima.BigCharge("Consume");
            }else{
            
            ExchangeWeapon();  }
            buttonQ.Pressed(true);

        }else if (Input.GetButton("ChangeGun") && ableTo.changeGun==true){buttonQ.Pressed(false);}

        if (Input.GetButton("Fire2") && bigChargeCD >= 3f && ableTo.bigCharge==true){
            BigCharge();
        }else if(bigCharge==true && !Input.GetButton("Fire2")){
            BigChargeRealize();
        }
        else{bigCharging=0f;}

         
        
    }

    void Move()
    {
        if(bigCharge==false){
        playerRb.velocity = new Vector2(moveDirection.x * velocityForce, moveDirection.y * velocityForce);
        anima.Moving(playerRb.velocity);
        }else{
            //Aqui un if para las mejoras
            Stay();
        }

    }

    void Stay()
    {
        playerRb.velocity = new Vector2(0f, 0f);
        anima.Moving(playerRb.velocity);
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
    }


    void Aim() {
        if(accesability == false){
        float xNew = crosshair.transform.position.x-transform.position.x;
        float yNew = crosshair.transform.position.y-transform.position.y;

        float angle = Mathf.Atan2(yNew, xNew) * Mathf.Rad2Deg;

        anima.Angle(angle);
        }else{
            float moveX = Input.GetAxisRaw("HorizontalAim");
            float moveY = Input.GetAxisRaw("VerticalAim");
            Vector2 moveDirection2check = new Vector2(moveX, moveY).normalized; 
            if(moveDirection2check!=new Vector2(0f,0f)){
            moveDirection2 = new Vector2(moveX, moveY).normalized;    
            }
            float xNew = crosshair.transform.position.x-transform.position.x;
            float yNew = crosshair.transform.position.y-transform.position.y;
            float angle = Mathf.Atan2(yNew, xNew) * Mathf.Rad2Deg;
            anima.Angle(angle);

        }
        shootlogic.crosshairPos = crosshair.transform.localPosition;
        /*if (moveDirection != Vector2.zero){
        
        }*/
    }

    void Shoot() {
        shootlogic.Shoot();
    }

    public void ItookDamage(Vector2 pos){
        if(knockbackAvaible == true){
        stunned=true;
        countDown=0;
        angle = pos;
        knockbackAvaible = false;
        }
    }


    public void Dash(){
        
        //transform.position = Vector2.MoveTowards(transform.position, mousePos, dashDistance);
        if(dashCurrentSaveTimer>=dashSaveTimer){dashCurrentSaveTimer-=dashCurrentSaveTimer;}
        else{this.GetComponent<Health>().Dashed(); dashCurrentSaveTimer-=dashCurrentSaveTimer;}
        if(dashing==false && this.GetComponent<Health>().healthAmount>=0){
        dashing = true;
        blocked=true;
        anima.DashInBoolExchange("true");
        anima.DashOutBoolExchange("true");
        
        soundMan.PlaySound("pFire");
        GameObject bullet = Instantiate(dashPrefab, transform.position, Quaternion.identity);
        crosshair.GetComponent<CrosshairFollow>().PutDashObject(bullet);
        bullet.GetComponent<Dash>().velocity = dashVelo;
        bullet.GetComponent<Dash>().player = gameObject;
        bullet.GetComponent<Dash>().soundMan = soundMan;
        bullet.GetComponent<Dash>().crosshair = crosshair;
        bullet.GetComponent<Dash>().counter = dashDistance;
        dashSave = bullet;
        CMtarget.RemoveMember(this.gameObject.transform);
        CMtarget.AddMember(bullet.transform, 2f,1f);
        
    }
    }

    public void ComeBackDash(){
        buttonQ.Pressed(false);
        anima.DashInBoolExchange("false");
        anima.DashOutBoolExchange("true");
        dashing = false;
        CMtarget.RemoveMember(dashSave.transform);
        CMtarget.AddMember(this.transform, 2f,1f);
        charged = false;
        blocked=false;
        this.GetComponent<Health>().invul = this.GetComponent<Health>().invulTime;
    }

    public void DashingLogic(){
        if(dashing==true){this.GetComponent<Health>().invul = 0;}
        if(charged==false && dashCurrentSaveTimer >= dashSaveTimer){soundMan.PlaySound("DashCharged");}
        if(dashCurrentSaveTimer >= dashSaveTimer){charged=true;}
        if(dashCurrentSaveTimer <= ((float)dashesUnlocked*dashSaveTimer)){dashCurrentSaveTimer+=1f*Time.deltaTime;}
    }

    public float DashMax(){
        return dashSaveTimer;
    }
    public float DashAmountToDisplay(){
        return dashCurrentSaveTimer;
    }


    public void CinematicLogic(){
        if(blocked==true){
            
            CMtarget.RemoveMember(focus.transform);          
            CMtarget.AddMember(this.gameObject.transform, 2f,2f);
            if(accesability == true){CMtarget.AddMember(crosshair.gameObject.transform, 1f, 1f);}
            buttonQ.Pressed(false);  
            blocked=false;
            }
        else{
            CMtarget.AddMember(focus.transform, 3f,2f);
            CMtarget.RemoveMember(this.gameObject.transform);          
            if(accesability == true){CMtarget.RemoveMember(crosshair.gameObject.transform);}
            blocked=true;
            }
    }

    public void Repos(){
        focus.transform.position = this.transform.position;
    }

    public void ExchangeWeapon(){
        
        gunCurrent++;
        if(gunCurrent==guns.Count){gunCurrent=0;}
        shootlogic.ChangeEquipment(guns[gunCurrent]);

        switch(guns[gunCurrent]){
            case "pistol":
                gunNameText.text = "Pium Blaster";
                break;
            case "overCharge":
                gunNameText.text = "Akimbo-Ratatas";
                break;
        }

    }

    private void BigCharge(){
        bcSound=false;
        bigCharging+=1f*Time.deltaTime;
        if(bigCharging>=holdDetect){
            lighta.charging=true;
            anima.charge=true;
            bigCharge=true;
            if(anima.BigChargeDone()){
                anima.chargeState +=1;
                soundMan.PlaySound("pBCharge");
                bigChargeObject = Instantiate(chargedBulletPrefab, new Vector3(transform.position.x,transform.position.y-1.5f,transform.position.z), Quaternion.identity);
            }


        }
    }

    private void BigChargeRealize(){
        if(bcSound == false){soundMan.PlaySound("pFireBC"); bcSound=true;}
        anima.chargeState = 2;
        if(anima.BigChargeDone()){
                
                anima.chargeState +=1;
                anima.BigChargeStartEnded("false");
            }
    }
    
    public void BigChargeEnded(){
        bcSound=false;
        bigChargeCD=0f;
        bigCharging=0f;
        bigCharge = false;
        anima.charge = false;
        anima.chargeState=0;
        lighta.charging=false;
        Destroy(bigChargeObject);
        shootlogic.BulletShooted(chargedBulletPrefab,0f);
    }

    public void PauseLogic(){
        if (Input.GetKeyDown(KeyCode.Escape) && blocked==false)
        {
                pausa.SetPause(true);
        }else if(Input.GetKeyDown(KeyCode.Escape) && blocked==true && pausa.pausa==true){
            pausa.SetPause(false);
        }

    }
   
}
