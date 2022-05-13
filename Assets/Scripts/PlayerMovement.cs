using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    SpriteRenderer playerSR;
    Animator animator;
    private string currentState;
     private float bulletCurrentCD = 0f;
     private Vector2 moveDirection;
     private Vector2 mousePos;



     //Variables publicas
     [SerializeField] private float velocityForce = 8f;
     [SerializeField] private float bulletSpeed = 0f;
     [SerializeField] private float bulletCD = 2f;
     public GameObject crosshair;
     public GameObject bulletPrefab;
     public LightPlayer lighta;
     public Light2D lightaA;
     public SoundMan soundMan;

    //Animation states
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_MOVDOWN = "MovDown";
    const string PLAYER_MOVUP = "MovUp";
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        lighta = this.transform.Find("Point Light 2D").GetComponent<LightPlayer>();
        lightaA = this.transform.Find("Point Light 2D").GetComponent<Light2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        bulletCurrentCD += 1f * Time.deltaTime;
        Aim();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Move();
        
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

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
    }


    void Aim() {

        crosshair.transform.localPosition = mousePos-(new Vector2(1f,1f));

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

    void ChangeAnimationState(string newState){
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
