using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    private Vector2 moveDirection;
    private float moveSpeed;
    public GameObject lightPrefab;


    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

          
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition * moveSpeed * Time.deltaTime;

        Debug.DrawLine(currentPosition, newPosition, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;
            if( other != this.gameObject){
            //Debug.Log(hit.collider.gameObject);
            GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);

            //Destroy(gameObject,0.0f);
            }

            if( other.tag == "Player"){
                Vector2 temp = (transform.position-other.transform.position).normalized;

                other.GetComponent<Health>().TakeDamage(temp*0.5f);
            }


        transform.position = newPosition;
     }
    }

    public void SetMoveDirections(Vector2 dir)
    {
        moveDirection = dir;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
