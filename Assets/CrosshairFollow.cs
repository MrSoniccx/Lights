using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    public bool accesability=false;
    private Vector2 moveDirection2;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(accesability == false){
        this.transform.localPosition = mousePos;
        }else{
            float moveX = Input.GetAxisRaw("HorizontalAim");
            float moveY = Input.GetAxisRaw("VerticalAim");
            Vector2 moveDirection2check = new Vector2(moveX, moveY).normalized; 
            if(moveDirection2check!=new Vector2(0f,0f)){
            moveDirection2 = new Vector2(moveX, moveY).normalized;    
            }
            this.transform.localPosition = (new Vector2(this.transform.localPosition.x,this.transform.localPosition.y)) + (moveDirection2*5f);
            

        }
    }
}
