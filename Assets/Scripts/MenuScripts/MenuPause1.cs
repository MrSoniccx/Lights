using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause1 : MonoBehaviour
{
    private GameObject player;
    private SoundMan soundMan;
    public GameObject xSonidos;
    public GameObject xBigCharge;
    public GameObject xDash;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        soundMan = player.transform.Find("SoundManager").gameObject.GetComponent<SoundMan>();


        if (PlayerPrefs.GetInt("accesability") == 0 || PlayerPrefs.GetInt("accesability") == null){
            PlayerPrefs.SetInt("accesability", 0);
            player.GetComponent<PlayerMovement>().accesability = false;
        }else{
            PlayerPrefs.SetInt("accesability", 1);
            player.GetComponent<PlayerMovement>().accesability = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit(){
        player.GetComponent<Pause>().SetPause(false);

    }

    public void OpcionesSonido(){
        soundMan.PlaySound("UIaccept");
        StartCoroutine(LeavingOut(this.transform.Find("Opciones").gameObject));
        StartCoroutine(GoingIn(this.transform.Find("OpcionesSound").gameObject));

    }

    public void Opciones(){
        soundMan.PlaySound("UIaccept");
        StartCoroutine(LeavingOut(this.transform.Find("MenuStart").gameObject));
        StartCoroutine(GoingIn(this.transform.Find("Opciones").gameObject));

    }

    public void goMain(){
        soundMan.PlaySound("UIaccept");
        StartCoroutine(LeavingOut(this.transform.Find("OpcionesSound").gameObject));
        StartCoroutine(LeavingOut(this.transform.Find("Opciones").gameObject));
        StartCoroutine(GoingIn(this.transform.Find("MenuStart").gameObject));
    }

    public void CloseAll(){
        soundMan.PlaySound("UIcancel");
        StartCoroutine(LeavingOut(this.transform.Find("MenuStart").gameObject));
        StartCoroutine(LeavingOut(this.transform.Find("OpcionesSound").gameObject));
        StartCoroutine(LeavingOut(this.transform.Find("Opciones").gameObject));
        if(this.transform.Find("Tutorials").transform.Find("Sonidos").gameObject.activeSelf){
            GameObject pepe = Instantiate(xSonidos, transform.position, Quaternion.identity);
            StartCoroutine(LeavingOut(this.transform.Find("Tutorials").transform.Find("Sonidos").gameObject));
        }
        if(this.transform.Find("Tutorials").transform.Find("BigCharge").gameObject.activeSelf){
            GameObject pepe = Instantiate(xBigCharge, transform.position, Quaternion.identity);
            StartCoroutine(LeavingOut(this.transform.Find("Tutorials").transform.Find("BigCharge").gameObject));
        }
        if(this.transform.Find("Tutorials").transform.Find("Dash").gameObject.activeSelf){
            GameObject pepe = Instantiate(xDash, transform.position, Quaternion.identity);
            StartCoroutine(LeavingOut(this.transform.Find("Tutorials").transform.Find("Dash").gameObject));
        }
        
        
    }

    public void HideMenu(){
        soundMan.PlaySound("UIpop");
        this.transform.Find("MenuStart").gameObject.SetActive(false);
    }




    IEnumerator LeavingOut(GameObject x){
        if(x.activeSelf){
        x.GetComponent<Animator>().Play("UiLeavingOut");
        if (x == this.transform.Find("OpcionesSound").gameObject){
            x.transform.Find("MainVol").gameObject.GetComponent<VolumeControl>().onIt = false;
            x.transform.Find("SFXVol").gameObject.GetComponent<VolumeControl>().onIt = false;
            x.transform.Find("MusicVol").gameObject.GetComponent<VolumeControl>().onIt = false;
            x.transform.Find("VoicesVol").gameObject.GetComponent<VolumeControl>().onIt = false;
        }else if(x == this.transform.Find("Opciones").gameObject){
            x.transform.Find("Language").gameObject.GetComponent<LanguageSelector>().onIt = false;
        }
        yield return new WaitForSeconds(0.1f);
        x.SetActive(false);
        }
    }

    IEnumerator GoingIn(GameObject x){
        x.SetActive(true);
        x.GetComponent<Animator>().Play("UiComingOut");
        
        yield return new WaitForSeconds(0.1f);
        GetSelectedButtonStart(x);
        
    }

    public void GetSelectedButtonStart(GameObject x){
        x.GetComponent<UISelectbutton>().SelectMain();
    }
    
}
