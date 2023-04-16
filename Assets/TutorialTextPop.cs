using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextPop : MonoBehaviour
{
    private GameObject player;
    public GameObject stato;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<Pause>().SetPause(true);
        stato.SetActive(true);
        Destroy(this.gameObject,1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
