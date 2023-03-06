using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_button : MonoBehaviour
{
    public bool pressed = false;
    Animator animator;
    private string currentState;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed==false){
            ChangeAnimationState("Q-button");
        }else{
            ChangeAnimationState("Q-button_pressed");
        }
    }
    

    public void Pressed(bool x){
        pressed = x;
    }


    void ChangeAnimationState(string newState){
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
