using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTrashCollector : MonoBehaviour
{
    public int scorePerTrash = 10;
    public int scorePerWrong = -5;
    public float beamSpeed = 10f;
    public Color successColor = Color.green;
    public Color failureColor = Color.red;
    public AudioClip successSound;
    public AudioClip failureSound;
    public Camera mainCamera;

    private int score = 0;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Cast a ray from the camera to the touch position
            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object is space trash
                if (hit.collider.CompareTag("PT"))
                {
                    CollectSpaceTrash(hit.collider.gameObject);
                }
                else
                {
                    // Touching an object that decreases the score
                    DecrementScore();
                }
            }
        }
    }

    void CollectSpaceTrash(GameObject trash)
    {
        // Change the color of the trash to indicate success
        trash.GetComponent<MeshRenderer>().material.color = successColor;

        // Play the success sound
        audioSource.PlayOneShot(successSound);

        // Move the trash towards the spacecraft
        Vector3 direction = transform.position - trash.transform.position;
        trash.GetComponent<Rigidbody>().velocity = direction.normalized * beamSpeed;

        // Increment the score
        score += scorePerTrash;
    }

    void DecrementScore()
    {
        // Play the failure sound
        audioSource.PlayOneShot(failureSound);

        // Decrement the score
        score += scorePerWrong;
    }

    void OnDestroy()
    {
        // Save the score to a persistent data store
        PlayerPrefs.SetInt("Score", score);
    }
}
