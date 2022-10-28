using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    Animaciones anima;
    
    
    private Vector2 mousePos;
    
    private LightPlayer lighta;
    private Light2D lightaA;
    private SoundMan soundMan;
    private PlayerMovement pMovement;

    [SerializeField] private string Equiped="pistol";   
    [SerializeField] private float CD=0f;


    [Header("Pistol")]

    [SerializeField] private GameObject pistol_Bullets_UI;
    [SerializeField] GameObject pistol_Bullets_Prefab;
    [SerializeField] private float CD_pistol=0.3f;
    [SerializeField] private float pistol_Bullets_Speed=20f;
    [SerializeField] private float pistol_Bullets_Max = 3f;
    [SerializeField] private int pistol_Bullets_units = 0;
    [SerializeField] private float pistol_Bullets_charge = 0f;
    [SerializeField] private float pistol_Bullets_Recharge_Ratio = 0.5f;
    [SerializeField] private List<GameObject> pistol_UI_GameObjects;


    [Header("Over Charge")]

    [SerializeField] private GameObject OC_Bullets_UI;
    [SerializeField] GameObject OC_Bullets_Prefab;
    [SerializeField] private float CD_OC=0.01f;
    [SerializeField] private float OC_Bullets_Speed=21f;
    [SerializeField] private float OC_Bullets_Max = 10f;
    [SerializeField] private float OC_Bullets_Charges = 10f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(Equiped=="pistol"){
            for(int i = 0; i<pistol_Bullets_Max; i++){
                UIpistol(true);
                pistol_Bullets_units++;
            }
        }
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch(Equiped){
            case "pistol":

            if(pistol_Bullets_charge<=1 && pistol_Bullets_units<pistol_Bullets_Max)
                {
                pistol_Bullets_charge += pistol_Bullets_Recharge_Ratio * Time.deltaTime;
                if(pistol_Bullets_charge>=1){
                    pistol_Bullets_charge=0f;
                    pistol_Bullets_units+=1;
                    UIpistol(true);
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
            if(pistol_Bullets_units>=1)
            {
                
                pistol_Bullets_units-=1;
                UIpistol(false);
                lighta.i=lighta.Timer;
                if(lightaA.intensity < lighta.luzMax)
                    {lightaA.intensity += 0.4f;}


                BulletShooted(pistol_Bullets_Prefab, pistol_Bullets_Speed);

        
                CD=CD_pistol;
            }else{//soundMan.PlaySound("Cant");
            }
            break;
            case "overCharge":
            BulletShooted(OC_Bullets_Prefab, OC_Bullets_Speed);
            lighta.i=lighta.Timer;
            if(lightaA.intensity < lighta.luzMax)
                {lightaA.intensity += 0.4f;}
            CD=CD_OC;

            break;
        }
        }

    }

    public void ChangeEquipment(string Gun){
        Equiped = Gun;
    }

    public void UIpistol(bool AddOrRemove){
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

    public void BulletShooted(GameObject prefab,float vel){

                anima.shooting = true;
        
                soundMan.PlaySound("pFire");
                Vector2 shootDirection = mousePos;
                Vector2 lookDir = shootDirection - GetComponent<Rigidbody2D>().position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg-90f;
                lookDir.Normalize();
                shootDirection.Normalize();

                if(prefab==OC_Bullets_Prefab){
                    float desPosicion = Random.Range(-0.25f, 0.25f);
                    GameObject bullet = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y+desPosicion,4), Quaternion.identity);
                    float desVelocidad = Random.Range(0.8f,1.2f);
                    bullet.GetComponent<OC_Bullet>().velocity = (lookDir * vel)*desVelocidad;
                    bullet.GetComponent<OC_Bullet>().player = gameObject;
                    bullet.GetComponent<OC_Bullet>().soundMan = soundMan;
                    bullet.transform.Rotate(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);
                }
                if(prefab==pistol_Bullets_Prefab){
                    GameObject bullet = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y,4), Quaternion.identity);
                    bullet.GetComponent<bullet>().velocity = lookDir * vel;
                    bullet.GetComponent<bullet>().player = gameObject;
                    bullet.GetComponent<bullet>().soundMan = soundMan;
                    bullet.transform.Rotate(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);
                }
                
    }
}
