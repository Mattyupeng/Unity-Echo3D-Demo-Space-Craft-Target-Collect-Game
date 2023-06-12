using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol : MonoBehaviour
{
    public Transform player; // The transform of the object to follow
    public Vector3 offset; // The camera's position relative to the target
    public float smoothTime = 0.3f; // The time it takes for the camera to smoothly catch up to the target's position

    private Vector3 velocity = Vector3.zero; // The current velocity of the camera

    // The amount of shake to apply
    public float shakeMagnitude = 0.5f;

    // The duration of the shake effect
    public float shakeDuration = 0.5f;

    // The original position of the camera
    private Vector3 originalPosition;

    //shake flag
    private bool shake = false;

    public GameObject shakeTextObject;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();

        // Set the text property of the Win Text and Lose Text UI to an empty string, making the 'You Win' and 'You Lose' messages blank

        shakeTextObject.SetActive(false);

    }

    // Function to shake the camera
    public void Shake()
    {
        if(shake == true)
        {
            originalPosition = transform.localPosition;
            StartCoroutine(ShakeCoroutine());
        }
    }

     void OnTriggerEnter(Collider other)
    {
        Debug.Log("shake");

        if (other.gameObject.CompareTag("BP")) 
        {
			// Add 100 to the score variable 'count'
			shake = true;

            // Activate'winText'
            shakeTextObject.SetActive(true);

            Shake();
        }
    }
    // Coroutine to apply the shake effect
    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    void LateUpdate()
    {
        // Calculate the new position for the camera
        Vector3 targetPosition = player.position + offset;

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
