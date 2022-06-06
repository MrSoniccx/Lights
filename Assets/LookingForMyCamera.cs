using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForMyCamera : MonoBehaviour
{
    enum RenderModeStates { camera, overlay, world };
    RenderModeStates m_RenderModeStates;

    Canvas m_Canvas;

    // Use this for initialization
    void Start()
    {
        m_Canvas = GetComponent<Canvas>();
        m_Canvas.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Press the space key to switch between render mode states

    }

    void ChangeState()
    {
        switch (m_RenderModeStates)
        {
            case RenderModeStates.camera:
                m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
                m_RenderModeStates = RenderModeStates.overlay;
                break;

            case RenderModeStates.overlay:
                m_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                m_RenderModeStates = RenderModeStates.world;
                break;
            case RenderModeStates.world:
                m_Canvas.renderMode = RenderMode.WorldSpace;
                m_RenderModeStates = RenderModeStates.camera;

                break;
        }
    }
}
