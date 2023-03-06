using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingCrush : MonoBehaviour
{
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    public Sprite block;
    public AudioSource soundMan1;
    public AudioSource soundMan2;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
    {
        bodyParts.Add(child.gameObject);
     }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCrushed(){
        StartCoroutine(waiter());
    }

    IEnumerator waiter(){
        soundMan1.Play(0);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i<bodyParts.Count; i++){
            bodyParts[i].GetComponent<SpriteRenderer>().sprite = block;
        }
        soundMan2.Play(0);
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);

    }
}
