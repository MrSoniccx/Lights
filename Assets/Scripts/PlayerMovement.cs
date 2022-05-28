using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    SpriteRenderer playerSR;
    Animaciones anima;
    
    
     private float bulletCurrentCD = 0f;
     private Vector2 moveDirection;
     private Vector2 mousePos;
     private Vector2 angle;
     private bool stunned=false;
     private bool knockbackAvaible=true;
     private float countDown=0f;
     private GameObject dashSave;
     public  bool blocked=false;
     
     private float dashCurrentSaveTimer;
     private bool dashing;
     public bool charged=true;
     
     





     //Variables publicas
     [SerializeField] private float velocityForce = 8f;
     [SerializeField] private float bulletSpeed = 0f;
     [SerializeField] private float bulletCD = 2f;
     [SerializeField] private float knockback = 2f;
     [SerializeField] private float dashDistance = 2f;
     [SerializeField] private float dashVelo = 2f;
     [SerializeField] private float dashSaveTimer = 2f;
     public GameObject crosshair;
     public GameObject bulletPrefab;
     public GameObject dashPrefab;
     public LightPlayer lighta;
     public Light2D lightaA;
     public SoundMan soundMan;
     public CinemachineTargetGroup CMtarget;
     public float countDownTimer;
     


    // Start is called before the first frame update
    void Start()
    {
        dashCurrentSaveTimer = dashSaveTimer;
        playerRb = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animaciones>();
        lighta = this.transform.Find("Point Light 2D").GetComponent<LightPlayer>();
        lightaA = this.transform.Find("Point Light 2D").GetComponent<Light2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(stunned==false && blocked==false){
        ProcessInputs();
        }else {countDown+= 1*Time.deltaTime;}
        if(countDown >= countDownTimer){
            stunned=false;
            countDown=0;
        }


        if(bulletCurrentCD <= bulletCD){bulletCurrentCD += 1f * Time.deltaTime;}
        
        DashingLogic();
        

        if(GetComponent<Health>().invul >= GetComponent<Health>().invulTime){knockbackAvaible=true;}
        
        
        Aim();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if(stunned==false && blocked==false){
        Move();
        }else if(stunned==true){playerRb.velocity=((-angle)*knockback);}
        if(blocked==true){Stay();}
        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;


        if (Input.GetMouseButton(0) && bulletCurrentCD >= bulletCD){
            Shoot();
            bulletCurrentCD = 0;
            
            lighta.i=lighta.Timer;

            if(lightaA.intensity < lighta.luzMax)
                {lightaA.intensity += 0.4f;}
            
        }

         if (Input.GetKeyDown("space")){   
            Dash();  
        }
        
    }

    void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x * velocityForce, moveDirection.y * velocityForce);
        anima.Moving(playerRb.velocity);
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

        crosshair.transform.localPosition = mousePos;
        float xNew = crosshair.transform.position.x-transform.position.x;
        float yNew = crosshair.transform.position.y-transform.position.y;

        float angle = Mathf.Atan2(yNew, xNew) * Mathf.Rad2Deg;

        anima.Angle(angle);
        /*if (moveDirection != Vector2.zero){
        crosshair.transform.localPosition = moveDirection*1.5f;
        }*/
    }

    void Shoot() {
        soundMan.PlaySound("pFire");
        Vector2 shootDirection = mousePos;
        Vector2 lookDir = shootDirection - playerRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg-90f;
        lookDir.Normalize();
        shootDirection.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<bullet>().velocity = lookDir * bulletSpeed;
        bullet.GetComponent<bullet>().player = gameObject;
        bullet.GetComponent<bullet>().soundMan = soundMan;
        bullet.transform.Rotate(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);
        Destroy(bullet, 3.0f);


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
        if(dashCurrentSaveTimer>=dashSaveTimer){dashCurrentSaveTimer=0f;}
        else{this.GetComponent<Health>().Dashed(); dashCurrentSaveTimer=0f;}
        if(dashing==false && this.GetComponent<Health>().healthAmount>=0){
        dashing = true;
        blocked=true;
        
        soundMan.PlaySound("pFire");
        GameObject bullet = Instantiate(dashPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Dash>().velocity = dashVelo;
        bullet.GetComponent<Dash>().player = gameObject;
        bullet.GetComponent<Dash>().soundMan = soundMan;
        bullet.GetComponent<Dash>().counter = dashDistance;
        dashSave = bullet;
        CMtarget.RemoveMember(this.gameObject.transform);
        CMtarget.AddMember(bullet.transform, 2f,1f);
        
    }
    }

    public void ComeBackDash(){
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
        if(dashCurrentSaveTimer <= dashSaveTimer){dashCurrentSaveTimer+=1f*Time.deltaTime;}
    }


    public void CinematicLogic(){
        if(blocked==true){
             CMtarget.RemoveMember(this.transform.Find("Focus").gameObject.transform);
            blocked=false;
            }
        else{
            CMtarget.AddMember(this.transform.Find("Focus").gameObject.transform, 3f,2f);
            blocked=true;
            }
    }
    
   
}
