using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boost : MonoBehaviour
{
    private Rigidbody rb;
    
    public float speed = 0f; // The constant speed of the spacecraft, adjustable
    public float boostSpeed = 2f; // The speed increase when the "boost" button is pressed, adjustable
    public float boostDuration = 1f; // The duration of the boost effect, adjustable
    
    private float currentSpeed; // The current speed of the spacecraft
    private float boostStartTime; // The time when the "boost" button was pressed
    private bool isBoosting = false; // Flag indicating whether the boost effect is active

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("The Rigidbody component is missing!");
        }
        currentSpeed = speed;
    }

    public void BoostStart()
    {
        isBoosting = true;
        currentSpeed += boostSpeed;
        boostStartTime = Time.time;
    }

    private void BoostEnd()
    {
        isBoosting = false;
        currentSpeed = speed;
    }

    void Update()
    {
        if (isBoosting)
        {
            float timeSinceboost = Time.time - boostStartTime;
            if (timeSinceboost < boostDuration)
            {
                // Apply acceleration during the boost duration
                rb.velocity = transform.forward * Mathf.Lerp(speed, currentSpeed, timeSinceboost / boostDuration);
            }
            else
            {
                // Apply deceleration after the thrust duration
                BoostEnd();
            }
        }
        else
        {
            // Apply the constant speed
            rb.velocity = transform.forward * speed;
        }
    }
}
