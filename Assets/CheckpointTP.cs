using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTP : MonoBehaviour
{
    public GameObject check;
    // Start is called before the first frame update
    void Start()
    {
        check.transform.position = this.transform.position;
        Destroy(this.gameObject,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        
    }
}
