using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStart : MonoBehaviour
{
    private GameObject player;
    public GameObject nameGun;
    public MenuPause1 menuPause;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<Pause>().SetPause(true);
        menuPause.HideMenu();
        nameGun.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }
}
