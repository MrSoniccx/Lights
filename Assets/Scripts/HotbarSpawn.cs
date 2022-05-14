using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSpawn : MonoBehaviour
{
    [SerializeField] private float easyIn;
    [SerializeField] private float easyOut;
    [SerializeField] private float bounce;
    [SerializeField] private float multiplicador=1.7f;
    Rigidbody2D hotbarRb;
    public Text text;
    private int stage=0;
    private Vector3 destino;



    // Start is called before the first frame update
    void Start()
    {    
        hotbarRb = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(stage==0)
        {
            if(hotbarRb.velocity == new Vector2(0f,0f)){hotbarRb.velocity = new Vector2(easyIn*2,0f);}
            
            hotbarRb.AddForce(new Vector2(easyIn*Time.deltaTime,0f));
            if(transform.position.x >= destino.x){stage++;}
        }
        if(stage==1)
        {
            hotbarRb.AddForce(new Vector2(-bounce*multiplicador,0f));
            stage++;
        }
        if(stage==2)
        {
            hotbarRb.AddForce(new Vector2(-easyIn*Time.deltaTime,0f));
            if(transform.position.x <= destino.x){
                stage++;}
        }
        if(stage==3){
            if(hotbarRb.velocity.x >= -80f && hotbarRb.velocity.x <= 80f)
            {hotbarRb.velocity=new Vector2(0f,0f);}
            else{
            hotbarRb.AddForce(new Vector2(easyIn*2*Time.deltaTime,0f));
            
            }
            }
        if(stage==4)
        {
        hotbarRb.AddForce(new Vector2(easyOut*Time.deltaTime*2,0f));
        if(transform.position.x <= -42f){
            Destroy(transform.parent.gameObject);
        
            }
        }
        
    }

    public void Exit()
    {
        stage=4;
        hotbarRb.AddForce(new Vector2(bounce*Time.deltaTime,0f));
    }

    public void WhatAmI(string code){
        text.text = code;
    }

    public void Posicion(float offset){
        destino = new Vector3(transform.position.x,transform.position.y-offset,0);
        Debug.Log(destino);
        hotbarRb = GetComponent<Rigidbody2D>();
        text = this.transform.Find("Text").GetComponent<Text>();
        transform.position = new Vector3(-42f,transform.position.y-offset,0);  
    }
}
