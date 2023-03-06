using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrolling : MonoBehaviour
{

    private Vector3 startPos;

    [SerializeField] private float xVelocity=-1f;
    [SerializeField] private float yVelocity=-0.5f;

    private float howMuchMoveX=-160.6f;
    private float howMuchMoveY=-98.15f;
    [SerializeField] private int screenNumber=1;

    [SerializeField] private float multiplier=1;
    
    [SerializeField] private Vector3 repeatPos;
    [SerializeField] private Vector3 limite;

    void Start(){
        startPos=transform.position;
        repeatPos = new Vector3(startPos.x-(howMuchMoveX), startPos.y-(howMuchMoveY));
        if(screenNumber==3){
            
            transform.position = repeatPos;}
        if(screenNumber==2){ transform.position = new Vector3(startPos.x+(howMuchMoveX),startPos.y+(howMuchMoveY));}
        limite = new Vector3(startPos.x+(howMuchMoveX*2),startPos.y+(howMuchMoveY*2));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(((xVelocity*multiplier)* Time.deltaTime), ((yVelocity*multiplier) * Time.deltaTime));
        //-46.57 -26.49

        

        if(transform.position.x <= limite.x || transform.position.y <= limite.y)
        {
            
            transform.position = repeatPos;
            //-129.25  -62.15
        }
    }
}
