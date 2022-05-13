using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrolling : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1f * Time.deltaTime, -0.5f * Time.deltaTime);

        if(transform.position.y <= -46.57 || transform.position.x <= -26.49)
        {
            transform.position = new Vector3(274.567f, 151.232f);
            //-129.25  -62.15
        }
    }
}
