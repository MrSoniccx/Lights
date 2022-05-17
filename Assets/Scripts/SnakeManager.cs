using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] float distanceBetween = .2f;
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> snakeBody = new List<GameObject>();

    float countUp = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        CreateBodyParts();
            

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bodyParts.Count>0)
        {
            CreateBodyParts();
        }
        SnakeMovement();
    }
    
    void SnakeMovement()
    {
        

        if(snakeBody.Count>1)
        {
            for (int i=1; i<snakeBody.Count; i++)
            {
                
                MarkManager markM = snakeBody[i-1].GetComponent<MarkManager>();
                Vector3 temp3 = new Vector3(markM.markerList[0].position.x,markM.markerList[0].position.y, (markM.markerList[0].position.z+i*2));
                snakeBody[i].transform.position = temp3;
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                snakeBody[i].GetComponent<SpriteRenderer>().color = snakeBody[0].GetComponent<SpriteRenderer>().color;
                markM.markerList.RemoveAt(0);
            }
        }
    }

    void CreateBodyParts(){

        if(snakeBody.Count == 0)
        {
            
        GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            if (!temp1.GetComponent<MarkManager>())
                {temp1.AddComponent<MarkManager>();}
            if (!temp1.GetComponent<Rigidbody2D>()){
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            snakeBody.Add(temp1);
            bodyParts.RemoveAt(0);
        }


        MarkManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkManager>();
        if(countUp == 0)
        {
            markM.ClearMarkerList();
        }
        countUp += Time.deltaTime;
        if(countUp>=distanceBetween)
        {
            Vector3 temp2 = new Vector3(markM.markerList[0].position.x, markM.markerList[0].position.y, markM.markerList[0].position.z+1*countUp);
            GameObject temp = Instantiate(bodyParts[0], temp2, markM.markerList[0].rotation, transform);
        if (!temp.GetComponent<MarkManager>())
            {temp.AddComponent<MarkManager>();}
        if (!temp.GetComponent<Rigidbody2D>()){
            temp.AddComponent<Rigidbody2D>();
            temp.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        snakeBody.Add(temp);
        bodyParts.RemoveAt(0);
        temp.GetComponent<MarkManager>().ClearMarkerList();
        countUp=0;
        }
        
        
    }
}