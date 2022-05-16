using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

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




     //Variables publicas
     [SerializeField] private float velocityForce = 8f;
     [SerializeField] private float bulletSpeed = 0f;
     [SerializeField] private float bulletCD = 2f;
     [SerializeField] private float knockback = 2f;
     public GameObject crosshair;
     public GameObject bulletPrefab;
     public LightPlayer lighta;
     public Light2D lightaA;
     public SoundMan soundMan;
     public float countDownTimer;
     


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animaciones>();
        lighta = this.transform.Find("Point Light 2D").GetComponent<LightPlayer>();
        lightaA = this.transform.Find("Point Light 2D").GetComponent<Light2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stunned==false){
        ProcessInputs();
        }else {countDown+= 1*Time.deltaTime;}
        if(countDown >= countDownTimer){
            stunned=false;
            countDown=0;
        }


        if(GetComponent<Health>().invul >= GetComponent<Health>().invulTime){knockbackAvaible=true;}
        bulletCurrentCD += 1f * Time.deltaTime;
        Aim();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if(stunned==false){
        Move();
        }else{playerRb.velocity=((-angle)*knockback);Debug.Log(playerRb.velocity);}
        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;


        if (Input.GetKey("space") && bulletCurrentCD >= bulletCD){
            Shoot();
            bulletCurrentCD = 0;
            
            lighta.i=lighta.Timer;

            if(lightaA.intensity < lighta.luzMax)
                {lightaA.intensity += 0.4f;}
            
        }
    }

    void Move()
    {
        playerRb.velocity = new Vector2(moveDirection.x * velocityForce, moveDirection.y * velocityForce);
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

   
}
