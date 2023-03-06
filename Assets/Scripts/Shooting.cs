using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    Animaciones anima;
    
    public Vector2 crosshairPos;
    private Vector2 mousePos;
    
    private LightPlayer lighta;
    private Light2D lightaA;
    private SoundMan soundMan;
    private PlayerMovement pMovement;
    private float remanentShoot=0f;

    [SerializeField] private string Equiped="pistol";   
    [SerializeField] private float CD=0f;


    [Header("Mana")]

    [SerializeField] private float mana_Max = 3f;
    public int mana_Units = 0;
    [SerializeField] private float mana_Charge = 0f;


    [Header("Pistol")]

    [SerializeField] private GameObject pistol_Bullets_UI;
    [SerializeField] GameObject pistol_Bullets_Prefab;
    [SerializeField] private float CD_pistol=0.3f;
    [SerializeField] private float pistol_Bullets_Speed=20f;
    [SerializeField] private float pistol_Bullets_Recharge_Ratio = 0.5f;
    [SerializeField] private List<GameObject> pistol_UI_GameObjects;


    [Header("Over Charge")]

    [SerializeField] private GameObject OC_Bullets_UI;
    [SerializeField] GameObject OC_Bullets_Prefab;
    [SerializeField] private float CD_OC=0.01f;
    [SerializeField] private float OC_Bullets_Speed=21f;
    [SerializeField] private float OC_Bullets_Max = 10f;
    [SerializeField] private float OC_Bullets_Charges = 10f;
    [SerializeField] private float randomes = 0.2f; 
    [SerializeField] private List<GameObject> OC_UI_GameObjects;

    [Header("Big Charge")]
    [SerializeField] GameObject BigCharge_Prefab;
    [SerializeField] GameObject BigChargeShoot_Prefab;
    [SerializeField] private float BigCharge_Speed=21f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        int i=mana_Units; 
        mana_Units = (int)mana_Max;

        for(; i<mana_Max; i++){
                
                //UImana(true);
            }

        mana_Charge = 0f;

        anima = GetComponent<Animaciones>();
        pMovement = GetComponent<PlayerMovement>();
        soundMan = pMovement.soundMan;
        lighta = this.transform.Find("Point Light 2D").GetComponent<LightPlayer>();
        lightaA = this.transform.Find("Point Light 2D").GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CD>=0f){
            CD -= 1f * Time.deltaTime;
        }
        if(remanentShoot>=0f){
            remanentShoot -= 1f * Time.deltaTime;
            anima.shooting=true;
        }else{anima.shooting=false;}
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch(Equiped){
            case "pistol":

            if(mana_Charge<=1 && mana_Units<mana_Max)
                {
                mana_Charge += pistol_Bullets_Recharge_Ratio * Time.deltaTime;
                if(mana_Charge>=1){
                    mana_Charge=0f;
                    mana_Units+=1;
                    
                    //UImana(true);
                    }
                }
            break;

            case "overcharged": 

                
            break;

        

        }
        
    }

    public void Shoot()
    {
        if(CD<=0){
        switch(Equiped){
            case "pistol":
            if(mana_Units>=1)
            {
                
                mana_Units-=1;
                //UImana(false);
                lighta.i=lighta.Timer;
                if(lightaA.intensity < lighta.luzMax)
                    {lightaA.intensity += 0.6f;}


                BulletShooted(pistol_Bullets_Prefab, pistol_Bullets_Speed);

        
                CD=CD_pistol;
                remanentShoot = 0.2f;
            }else{//soundMan.PlaySound("pFireLow");
            }
            break;
            case "overCharge":
            //calenta++
                if(OC_Bullets_Charges>0){
                    UI_OC(false);
                OC_Bullets_Charges--;
                BulletShooted(OC_Bullets_Prefab, OC_Bullets_Speed);
                lighta.i=lighta.Timer;
                if(lightaA.intensity < lighta.luzMax)
                    {lightaA.intensity += 0.3f;}
                CD=CD_OC;
                remanentShoot = 0.2f;
            }else{//soundMan.PlaySound("Cant");
            }

            break;
        }
        }

    }

    public void ChangeEquipment(string Gun){
        Equiped = Gun;
    }

    public void UImana(bool AddOrRemove){
        int lenght = pistol_UI_GameObjects.Count;


        if(AddOrRemove==true){
            
            pistol_UI_GameObjects.Add(Instantiate(pistol_Bullets_UI, transform.position, Quaternion.identity));

            GameObject temp = pistol_UI_GameObjects[lenght].gameObject.transform.Find("BPistol_Display").gameObject;
            float porcentualPantalla = ((5*temp.transform.position.x)/10)/2;

            Vector3 pos = new Vector3((temp.transform.position.x+(lenght*porcentualPantalla)), temp.transform.position.y, temp.transform.position.z);

            temp.transform.position = pos;

        }else{
            
            GameObject temp = pistol_UI_GameObjects[lenght-1];
            pistol_UI_GameObjects.Remove(pistol_UI_GameObjects[lenght-1]);
            Destroy(temp.gameObject,0f);

        }


    }


    public void UI_OC(bool AddOrRemove){
        int lenght = OC_UI_GameObjects.Count;


        if(AddOrRemove==true){
            
            OC_UI_GameObjects.Add(Instantiate(OC_Bullets_UI, transform.position, Quaternion.identity));

            GameObject temp = OC_UI_GameObjects[lenght].gameObject.transform.Find("BPistol_Display").gameObject;
            float porcentualPantalla = ((3*temp.transform.position.x)/10)/2.5f;

            Vector3 pos = new Vector3((temp.transform.position.x+(lenght*porcentualPantalla)), temp.transform.position.y, temp.transform.position.z);

            temp.transform.position = pos;
            float lenghtTemp = (float)lenght;
            if(lenght>5){
                float x = Mathf.Floor(lenghtTemp/5f);
                
                if(x%2==0){
                    lenghtTemp= lenghtTemp-(5f*x);
                }else{
                    lenghtTemp= lenghtTemp+(5f*x);
                }
                
            }
            
            temp.GetComponent<Animator>().Play("BOC_Display1", 0, (lenghtTemp/100f*3f));

        }else{
            
            GameObject temp = OC_UI_GameObjects[lenght-1];
            OC_UI_GameObjects.Remove(OC_UI_GameObjects[lenght-1]);
            Destroy(temp.gameObject,0f);

        }


    }

    public void BulletShooted(GameObject prefab,float vel){

                anima.shooting = true;
        
                
                Vector2 shootDirection = crosshairPos;
                Vector2 lookDir = shootDirection - GetComponent<Rigidbody2D>().position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg-90f;
                lookDir.Normalize();
                shootDirection.Normalize();

                if(prefab==OC_Bullets_Prefab){
                    soundMan.PlaySound("pFire");
                    
                    float desPosicion = Random.Range(-randomes, randomes);
                    GameObject bullet = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y+desPosicion,4), Quaternion.identity);
                    float spray = Random.Range(1f-randomes,1f+randomes);
                    Vector2 newlookDir = new Vector2(lookDir.x*spray, lookDir.y*spray);
                    bullet.GetComponent<OC_Bullet>().velocity = (newlookDir * vel)*spray;
                    bullet.GetComponent<OC_Bullet>().player = gameObject;
                    bullet.GetComponent<OC_Bullet>().soundMan = soundMan;
                    bullet.transform.Rotate(0, 0, (Mathf.Atan2(newlookDir.y, newlookDir.x) * Mathf.Rad2Deg));
                }
                if(prefab==pistol_Bullets_Prefab){
                    soundMan.PlaySound("pFire");
                    if(mana_Units<=0){
                        soundMan.PlaySound("pFireLow");
                    }
                    GameObject bullet = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y,4), Quaternion.identity);
                    bullet.GetComponent<bullet>().velocity = lookDir * vel;
                    bullet.GetComponent<bullet>().player = gameObject;
                    bullet.GetComponent<bullet>().soundMan = soundMan;
                    bullet.transform.Rotate(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);
                }
                if(prefab==BigCharge_Prefab){
                    
                    GameObject bullet = Instantiate(BigChargeShoot_Prefab, new Vector3(transform.position.x,transform.position.y,4), Quaternion.identity);
                    bullet.GetComponent<BigShootCrushBullet>().velocity = lookDir * BigCharge_Speed;
                    bullet.GetComponent<BigShootCrushBullet>().player = gameObject;
                    bullet.GetComponent<BigShootCrushBullet>().soundMan = soundMan;
                    bullet.transform.Rotate(0, 0, (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg)+90f);
                }
                
    }

    public void Refill(){
        int i=mana_Units; 
        mana_Units = (int)mana_Max;

        for(; i<mana_Max; i++){
                UImana(true);
            }

        mana_Charge = 0f;
        for(float o = OC_Bullets_Charges; o<OC_Bullets_Max; o++){
            OC_Bullets_Charges++;
            UI_OC(true);

        }
    }
}
