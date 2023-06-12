using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yawpitch : MonoBehaviour
{
    public Slider yawSlider;
    public Slider pitchSlider;

    private bool isDraggingYaw = false;
    private bool isDraggingPitch = false;

    void Start()
    {
        yawSlider = GameObject.Find("Yaw").GetComponent<Slider>();
        pitchSlider = GameObject.Find("Pitch").GetComponent<Slider>();
    }

    public void OnDragYaw()
    {
        isDraggingYaw = true;
    }

    public void OnDragPitch()
    {
        isDraggingPitch = true;
    }

    public void OnEndDrag()
    {
        isDraggingYaw = false;
        isDraggingPitch = false;
    }

    void Update()
    {
        if (isDraggingYaw)
        {
            float yaw = yawSlider.value;
            transform.rotation = Quaternion.Euler(0, yaw, 0);
        }

        if (isDraggingPitch)
        {
            float pitch = pitchSlider.value;
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}

