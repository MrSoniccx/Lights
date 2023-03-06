using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaAmount : MonoBehaviour
{
    private GameObject player;
    private int mana_Units;
    private Image imagen;
    public Sprite manaZero, manaOne, manaTwo, manaThree;
    public GameObject light1, light2, light3;
    private int manaUnits;
    // Start is called before the first frame update
    void Start()
    {
        imagen = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        manaUnits = player.GetComponent<Shooting>().mana_Units;
        switch(manaUnits){
            case 0:
            imagen.sprite = manaZero;
            light1.SetActive(false);
            break;
            case 1:
            imagen.sprite = manaOne;
            light1.SetActive(true);
            light2.SetActive(false);
            break;
            case 2:
            imagen.sprite = manaTwo;
            light2.SetActive(true);
            light3.SetActive(false);
            break;
            case 3:
            imagen.sprite = manaThree;
            light3.SetActive(true);
            break;

        }
    }
}

