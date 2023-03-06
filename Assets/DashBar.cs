using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    private GameObject player;
    Image dashBar;
    public int dashNumber=0;
    float maxBar;
    float bar;
    float inicio=0f;

    void Start(){
        dashBar = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        maxBar = player.GetComponent<PlayerMovement>().DashMax();
        bar = player.GetComponent<PlayerMovement>().DashAmountToDisplay();
        if(dashNumber!=0){            
                inicio = maxBar*dashNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {

        bar = player.GetComponent<PlayerMovement>().DashAmountToDisplay();
        bar = bar-inicio;
        float total = bar/maxBar;
        
        if(total <=0.05f){total=0f;
        }else if(total <=0.1f){total=0.05f;
        }else if(total <=0.15f){total=0.1f;
        }else if(total <=0.2f){total=0.15f;
        }else if(total <=0.25f){total=0.2f;
        }else if(total <=0.3f){total=0.25f;
        }else if(total <=0.35f){total=0.3f;
        }else if(total <=0.4f){total=0.35f;
        }else if(total <=0.45f){total=0.4f;
        }else if(total <=0.5f){total=0.45f;
        }else if(total <=0.55f){total=0.5f;
        }else if(total <=0.6f){total=0.55f;
        }else if(total <=0.65f){total=0.6f;
        }else if(total <=0.7f){total=0.65f;
        }else if(total <=0.75f){total=0.7f;
        }else if(total <=0.8f){total=0.75f;
        }else if(total <=0.85f){total=0.8f;
        }else if(total <=0.9f){total=0.85f;
        }else if(total <=0.95f){total=0.9f;
        }else if(total <=1f){total=0.95f;
        }else if(total >=1f){total=1f;}
        
        dashBar.fillAmount = total; 
    }
}

