using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backcamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Button switchButton;

    void Start()
    {
        // Add a listener to the switch button
        switchButton.onClick.AddListener(SwitchCameras);
    }

    public void SwitchCameras()
    {
        
        // Enable and disable cameras
        if (mainCamera.enabled)
        {
            mainCamera.enabled = false;
            secondaryCamera.enabled = true;
        }
        else if (secondaryCamera.enabled)
        {
            secondaryCamera.enabled = false;
            mainCamera.enabled = true;
        }
    }
}
