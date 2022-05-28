using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject player;
    public GameObject lightPrefab;
    public SoundMan soundMan;
    private bool once=false;
    [SerializeField] private float DamagePerBullet = 20f;

    void Update() {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        Debug.DrawLine(currentPosition, newPosition, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;
            if( other != player && once==false){
            //Debug.Log(hit.collider.gameObject);
            if(other.tag !="Triggers"){
            GameObject light = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            soundMan.PlaySound("pHit");
            once=true;
            Destroy(gameObject,0.0f);
            }
            }

            if( other.tag == "Enemy"){
                other.GetComponent<HealthEnemy>().TakeDamage(DamagePerBullet);
            }

            if( other.tag == "Pillar"){
                other.GetComponent<Pillar>().doMyThing();
            }
        }


        transform.position = newPosition;
    }
}
